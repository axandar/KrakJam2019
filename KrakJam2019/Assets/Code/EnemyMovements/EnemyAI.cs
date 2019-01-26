using Code.EnemyMovements;
using UnityEngine;
using UnityEngine.Analytics;

namespace Code{
	// ReSharper disable once InconsistentNaming
	public class EnemyAI : MonoBehaviour{
		public Transform target;

		[SerializeField] EnemyMovement enemyMovement;
		[SerializeField] Vector3 nextStep;
		[SerializeField] float speed = 5;
		[SerializeField] bool isBomb;
		public float Health = 5;


		bool _isMoving = true;

		void Start(){
			enemyMovement.GenerateRoute(transform.position, target.position);
			nextStep = enemyMovement.GetNextVector();
		}

		void Update(){
			if(!isBomb){
				//przeciwnicy odwracaja sie w strone gracza
			}
			
			if(Health <= 0)
				Destroy(gameObject);
		}

		void FixedUpdate(){
			if(_isMoving){
				MoveEnemy();
			}else if(isBomb){
				Debug.Log("BOOM");
				Destroy(gameObject);
			}
		}

		void MoveEnemy(){
			if(IsInNextStep()){
				if(enemyMovement.IsNextVector()){
					nextStep = enemyMovement.GetNextVector();
				}else{
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

		public RespawnArea GetRespawnArea(){
			return enemyMovement.GetRespawnArea();
		}
		
		[ContextMenu("Kill")]
		void Kill()
		{
			Health -= 5;
		}
	}
}