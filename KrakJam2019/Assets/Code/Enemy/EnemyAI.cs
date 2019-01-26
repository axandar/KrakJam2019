using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemy{
	// ReSharper disable once InconsistentNaming
	public class EnemyAI : MonoBehaviour{
		public Transform target;
		[SerializeField] private EnemyMovement enemyMovement;
		[SerializeField] private Vector3 nextStep;
		[SerializeField] private float speed = 5;
		[SerializeField] private int scoreValue;
		public AddScoreEvent addScore;
		public float health = 5;
		private float _currentHealth;
		private bool _isMoving = true;

		private void Start(){
			_currentHealth = health;
			var movementInstance = Instantiate(enemyMovement);
			enemyMovement = movementInstance;
			enemyMovement.transform.parent = gameObject.transform;

			var temp = target.position;
			enemyMovement.GenerateRoute(transform.position,
				new Vector3(temp.x, temp.y, temp.z));
			nextStep = enemyMovement.GetNextVector();
		}

		private void Update(){
			if(_currentHealth <= 0){
				Debug.Log("Invoked function");
				addScore.Invoke(scoreValue);
				Destroy(gameObject);
			}
		}

		private void FixedUpdate(){
			if(_isMoving){
				MoveEnemy();
			}else{
				Destroy(gameObject);
			}
		}

		private void MoveEnemy(){
			if(IsInNextStep()){
				if(enemyMovement.IsNextVector()){
					nextStep = enemyMovement.GetNextVector();
				} else{
					_isMoving = false;
				}
			}

			var step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, nextStep, step);
		}

		private bool IsInNextStep(){
			var distance = Vector3.Distance(transform.position, nextStep);
			return distance < 0.01f;
		}

		public void DamageMeBoi(int boomBoomValue){
			_currentHealth -= boomBoomValue;
		}

		public RespawnArea GetRespawnArea(){
			return enemyMovement.GetRespawnArea();
		}

		private void OnCollisionEnter2D(Collision2D other){
			if(other.gameObject.CompareTag("Player")){
				var controller = other.gameObject.GetComponentInParent<GameController>();
				controller.HealthPoints -= 10;
				Destroy(gameObject);
			}
		}

		[Serializable]
		public class AddScoreEvent : UnityEvent<int> { }
	}
}