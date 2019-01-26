using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code{
	public class GameController : MonoBehaviour{
		[SerializeField] Text scoreText;
		[SerializeField] Text healthText;
		[SerializeField] Text criticalHealthText;
		[SerializeField] GameObject gameUI;
		[SerializeField] GameObject gameOverUI;
		[SerializeField] GameObject criticalHealthUI;
		[SerializeField] GameObject cameraGO;
		public static float PlayerDMG = 1;

		[SerializeField] GameObject _player;
		[SerializeField] GameObject _bonusPrefab;
		[SerializeField] float _timeToSpawn = 15;

		int StopFirstCorutineInduction = 1;
		GameObject bonus;
		bool _dontAsk;

		[Header("Map Sizes")] [SerializeField] float _minVectorXValue;
		[SerializeField] float _maxVectorXValue;
		[SerializeField] float _minVectorYValue;
		[SerializeField] float _maxVectorYValue;

		[Header("Bonus Values")] [SerializeField]
		int _healingValues = 10;

		[SerializeField] float _addSpeed = 0.1f;
		[SerializeField] float _addDMG = 1;


		int scoreValue;
		[SerializeField] int healthValue;
		bool isPies;

		void Awake(){
			StartCoroutine(SpawnBonus());
		}

		void Start(){
			gameUI.SetActive(true);
			gameOverUI.SetActive(false);
			criticalHealthUI.SetActive(false);
		}

		void Update() {
			scoreText.text = "Score: " + scoreValue;
			healthText.text = "Health: " + healthValue;


			if (healthValue <= 0)
				GameOver();

			HealthIndicator();
        
			PickUpBonus();
		}

		void HealthIndicator(){
			if(healthValue > 50)
				healthText.color = Color.green;
			if(healthValue <= 50 && healthValue > 30)
				healthText.color = Color.yellow;
			if(healthValue <= 30){
				healthText.color = Color.red;
				criticalHealthText.color = Color.red;
				StartCoroutine(FlashCriticalHealthUI());
			}
		}

		IEnumerator FlashCriticalHealthUI(){
			while(true){
				criticalHealthUI.SetActive(true);
				criticalHealthText.text = " ";
				yield return new WaitForSeconds(.7f);
				criticalHealthText.text = "Health critical!";
				yield return new WaitForSeconds(.7f);
				criticalHealthUI.SetActive(false);
			}
		}

		void GameOver(){
			Destroy(criticalHealthUI);
			gameUI.SetActive(false);
			gameOverUI.SetActive(true);
			if(Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		IEnumerator SpawnBonus()
		{
			while (true) {
				_dontAsk = true;
				StopFirstCorutineInduction -= 1;
            
				if (StopFirstCorutineInduction <= 0) {
                
					bonus =  Instantiate(_bonusPrefab, 
						new Vector3(Random.Range(_minVectorXValue,_maxVectorXValue),
							Random.Range(_minVectorYValue,_maxVectorYValue))
						, new Quaternion(0,0,0,0));
					yield return new WaitForSeconds(_timeToSpawn);
					if (bonus != null) {
						_dontAsk = false;
						Destroy(bonus);
					}
					yield return new  WaitForSeconds(_timeToSpawn);
				}
			}
		}

		void PickUpBonus()
		{
			if(!_dontAsk)
				return;
        
			if(Vector2.Distance(_player.transform.position, bonus.transform.position) <= 1){
				PickUp();
				Destroy(bonus);
				_dontAsk = false;
			}
		}

		void PickUp()
		{
			var pickUpId = Random.Range(0,2);
			Debug.Log(pickUpId);
			switch (pickUpId) {
				case 0: //Health
					healthValue += _healingValues;
					break;
				case 1: //Speed 0.1
					PlayerController.acceleration += _addSpeed;
					break;
                
				case 2: //DMG 0.1
					PlayerDMG += _addDMG;
					break;
			}
		}

		public void AddScore(int scoreToAdd){
			scoreValue += scoreToAdd;
			Debug.Log(scoreValue);
		}
	}
}

