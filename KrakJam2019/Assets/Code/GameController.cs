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
		
		[SerializeField] private Text scoreText;
		[SerializeField] private Text healthText;
		[SerializeField] private Text criticalHealthText;
		[SerializeField] private GameObject gameUi;
		[SerializeField] private GameObject gameOverUi;
		[SerializeField] private GameObject criticalHealthUi;
		[SerializeField] private GameObject player;
		[SerializeField] private GameObject bonusPrefab;
		[SerializeField] private float timeToSpawn = 15;
		[Header("Map Sizes")] [SerializeField] private float minVectorXValue;
		[SerializeField] private float maxVectorXValue;
		[SerializeField] private float minVectorYValue;
		[SerializeField] private float maxVectorYValue;
		[Header("Bonus Values")] [SerializeField] private int healingValues = 10;
		[SerializeField] private float addSpeed = 0.1f;
		[SerializeField] private float addDmg = 1;
		[SerializeField] private int healthPoints = 100;
		
		private bool _isPies;
		private int _scoreValue;
		private int _stopFirstCoroutineInduction = 1;
		private GameObject _bonus;
		private bool _dontAsk;
		
		public int HealthPoints {
			get { return healthPoints; }
			set{
				healthPoints = value;
			}
		}

		private void Awake(){
			StartCoroutine(SpawnBonus());
		}

		private void Start(){
			gameUi.SetActive(true);
			gameOverUi.SetActive(false);
			criticalHealthUi.SetActive(false);
		}

		private void Update() {
			scoreText.text = "Score: " + _scoreValue;
			healthText.text = "Health: " + HealthPoints;
			if(Input.GetKeyDown(KeyCode.K)){
				HealthPoints = HealthPoints - 10;
			}

			if(HealthPoints <= 0){
				GameOver();
			}
			
			HealthIndicator();
			PickUpBonus();
		}

		private void HealthIndicator(){
			if(HealthPoints > 50)
				healthText.color = Color.green;
			if(HealthPoints <= 50 && HealthPoints > 30)
				healthText.color = Color.yellow;
			if(HealthPoints <= 30){
				healthText.color = Color.red;
				criticalHealthText.color = Color.red;
				StartCoroutine(FlashCriticalHealthUi());
			}
		}

		private IEnumerator FlashCriticalHealthUi(){
			while(true){
				criticalHealthUi.SetActive(true);
				criticalHealthText.text = " ";
				yield return new WaitForSeconds(.7f);
				criticalHealthText.text = "Health critical!";
				yield return new WaitForSeconds(.7f);
				criticalHealthUi.SetActive(false);
			}
		}

		private void GameOver(){
			Destroy(criticalHealthUi);
			gameUi.SetActive(false);
			gameOverUi.SetActive(true);
			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		private IEnumerator SpawnBonus(){
			while (true) {
				
				_dontAsk = true;
				_stopFirstCoroutineInduction -= 1;
            
				if (StopFirstCorutineInduction <= 0) {
                
					float x = Random.Range(0f,1f);
					float y = Random.Range(0f,1f);
					
					bonus =  Instantiate(_bonusPrefab, 
						Camera.main.ViewportToWorldPoint(new Vector3(x, y, 1))
						, new Quaternion(0,0,0,0));
					yield return new WaitForSeconds(timeToSpawn);
					if (_bonus != null) {
						_dontAsk = false;
						Destroy(_bonus);
					}
					yield return new  WaitForSeconds(timeToSpawn);
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
			_scoreValue += scoreToAdd;
		}
	}
}

