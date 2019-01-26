using System.Collections;
using System.Xml;
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
		public static float PlayerDMG = 1;
		private Vector2 shipTransform;

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
		bool isPies;
		private int scoreValue;
		
		[SerializeField] private int _healthPoints = 100;
		public int HealthPoints {
			get { return _healthPoints; }
			set{
				_healthPoints = value;
				Debug.Log(_healthPoints);
			}
		}


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
			healthText.text = "Health: " + HealthPoints;
			if (Input.GetKeyDown(KeyCode.K))
				HealthPoints = HealthPoints - 10;
			Debug.Log(HealthPoints);	
			if (HealthPoints <= 0)
				GameOver();

			HealthIndicator();
        
			PickUpBonus();
		}

		void HealthIndicator(){
			if(HealthPoints > 50)
				healthText.color = Color.green;
			if(HealthPoints <= 50 && HealthPoints > 30)
				healthText.color = Color.yellow;
			if(HealthPoints <= 30){
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
                
					float x = Random.Range(0f,1f);
					float y = Random.Range(0f,1f);
					
					bonus =  Instantiate(_bonusPrefab, 
						Camera.main.ViewportToWorldPoint(new Vector3(x, y, 1))
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
			shipTransform = gameObject.transform.GetChild(0).transform.position;
			if(!_dontAsk)
				return;
			if(Vector2.Distance(shipTransform, bonus.transform.position) <= 1){
				PickUp();
				
				Destroy(bonus);
				_dontAsk = false;
			}
		}

		void PickUp()
		{
			var pickUpId = Random.Range(0,3);
			Debug.Log("pickUpId" +pickUpId);
			switch (pickUpId) {
				case 0: //Health
					_healingValues += _healingValues;
					Debug.LogWarning("CollectHeal");
					break;
				case 1: //Speed 0.1
					PlayerController.acceleration += _addSpeed;
					Debug.LogWarning("CollectSpeed");
					break;
                
				case 2: //DMG 0.1
					PlayerDMG += _addDMG;
					Debug.LogWarning("CollectDMG");
					break;
			}
		}

		public void AddScore(int scoreToAdd){
			scoreValue += scoreToAdd;
			Debug.Log(scoreValue);
		}
	}
}

