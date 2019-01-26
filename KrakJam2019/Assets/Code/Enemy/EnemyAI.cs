using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemy {
	// ReSharper disable once InconsistentNaming
	public class EnemyAI : MonoBehaviour {
		public Transform target;

		[SerializeField] EnemyMovement enemyMovement;
		[SerializeField] Vector3 nextStep;
		[SerializeField] float speed = 5;
		[SerializeField] bool isBomb;
		[SerializeField] int scoreValue;
		public AddScoreEvent addScore;
		public float health = 5;
		float currentHealth;

		bool _isMoving = true;

		void Start() {
			currentHealth = health;
			var movementInstance = Instantiate(enemyMovement);
			enemyMovement = movementInstance;

			var temp = target.position;
			enemyMovement.GenerateRoute(transform.position,
				new Vector3(temp.x, temp.y, temp.z));
			nextStep = enemyMovement.GetNextVector();
			
		}

		void Update() {
			if (!isBomb) {
				//przeciwnicy odwracaja sie w strone gracza
			}

			if (currentHealth <= 0) {
				addScore.Invoke(scoreValue);
				if (currentHealth <= 0) {
					Destroy(gameObject);
				}
			}

			Vector3 MakeZAxisZero;
			MakeZAxisZero = transform.position;
			MakeZAxisZero.z = 0;
			transform.position = MakeZAxisZero;

		}

		void FixedUpdate(){
			if(_isMoving){
				MoveEnemy();
			} else{
				if(isBomb){
					Debug.Log("BOOM");
				}

				Destroy(gameObject);
			}
		}

		void MoveEnemy() {
				if (IsInNextStep()) {
					if (enemyMovement.IsNextVector()) {
						nextStep = enemyMovement.GetNextVector();
					}
					else {
						_isMoving = false;
					}
				}

				var step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, nextStep, step);
			}

			bool IsInNextStep() {
				var distance = Vector3.Distance(transform.position, nextStep);
				return distance < 0.01f;
			}
		
		public void DamageMeBoi(int boomBoomValue) {
			currentHealth -= boomBoomValue;
		}

		public RespawnArea GetRespawnArea() {
			return enemyMovement.GetRespawnArea();
		}
		
		[ContextMenu("Kill")]
		private void Kill(){
			health -= health;
		}
		
		[Serializable]
		public class AddScoreEvent : UnityEvent<int> {
		}


		private void OnCollisionEnter2D(Collision2D other)
		{
			var player = other.gameObject.CompareTag("Player");
			Destroy(gameObject);
		}
	}
}