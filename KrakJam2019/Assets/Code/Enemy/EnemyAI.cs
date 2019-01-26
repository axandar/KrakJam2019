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
		[SerializeField] private bool isBomb;
		[SerializeField] private int scoreValue;
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

		void Update(){
			if(!isBomb){
				//przeciwnicy odwracaja sie w strone gracza
			}

			if(health <= 0){
				addScore.Invoke(scoreValue);
			if(currentHealth <= 0){
				Destroy(gameObject);
			}
		}

		void MoveEnemy(){
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

		bool IsInNextStep(){
			var distance = Vector3.Distance(transform.position, nextStep);
			return distance < 0.01f;
		}

			RespawnArea GetRespawnArea(){
			return enemyMovement.GetRespawnArea();
		}


			void DamageMeBoi(int damage) {
			currentHealth -= damage;
		}
	}
	
	[Serializable]
	public class AddScoreEvent : UnityEvent<int>{}
}