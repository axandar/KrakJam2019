using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code{
	public class GameController : MonoBehaviour{
		public static float PlayerDmg = 1;
		
		[SerializeField] private Text scoreText;
		[SerializeField] private Text healthText;
		[SerializeField] private Text criticalHealthText;
		[SerializeField] private GameObject gameUi;
		[SerializeField] private GameObject gameOverUi;
		[SerializeField] private GameObject criticalHealthUi;
		[SerializeField] private GameObject bonusPrefab;
		[SerializeField] private float timeToSpawn = 15;
		[Header("Bonus Values")] [SerializeField] private int healingValues = 10;
		[SerializeField] private float addSpeed = 0.1f;
		[SerializeField] private float addDmg = 1;
		[SerializeField] private int healthPoints = 100;
		[SerializeField] private Camera playerCamera;
		
		private bool _isPies;
		private int _scoreValue;
		private int _stopFirstCoroutineInduction = 1;
		private GameObject _bonus;
		private bool _dontAsk;
		private Vector2 _shipTransform;
		
		public int HealthPoints {
			get => healthPoints;
			set{
				healthPoints = value;
				Debug.Log("PlayerGetHit");
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
            
				if (_stopFirstCoroutineInduction <= 0) {
					var x = Random.Range(0f,1f);
					var y = Random.Range(0f,1f);
					
					_bonus =  Instantiate(bonusPrefab, 
						playerCamera.ViewportToWorldPoint(new Vector3(x, y, 1))
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

		private void PickUpBonus(){
			_shipTransform = gameObject.transform.GetChild(0).transform.position;
			if(!_dontAsk){
				return;
			}

			if(Vector2.Distance(_shipTransform, _bonus.transform.position) <= 1){
				PickUp();
				Destroy(_bonus);
				_dontAsk = false;
			}
		}

		private void PickUp(){
			var pickUpId = Random.Range(0,3);
			Debug.Log("pickUpId" +pickUpId);
			switch (pickUpId) {
				case 0: //Health
					healingValues += healingValues;
					Debug.LogWarning("CollectHeal");
					break;
				case 1: //Speed 0.1
					PlayerController.Acceleration += addSpeed;
					Debug.LogWarning("CollectSpeed");
					break;
                
				case 2: //DMG 0.1
					PlayerDmg += addDmg;
					Debug.LogWarning("CollectDMG");
					break;
			}
		}

		public void AddScore(int scoreToAdd){
			_scoreValue += scoreToAdd;
		}
	}
}

