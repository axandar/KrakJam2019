using Code.EnemyMovements;
using UnityEngine;

namespace Code{
	// ReSharper disable once InconsistentNaming
	public class EnemyAI : MonoBehaviour{
		public Transform target;

		[SerializeField] private EnemyMovement enemyMovement;
		[SerializeField] private Vector3 nextStep;
		[SerializeField] private float speed = 5;
		[SerializeField] private bool isBomb;

		private bool _isMoving = true;

		private void Start(){
			enemyMovement.GenerateRoute(transform.position, target.position);
			nextStep = enemyMovement.GetNextVector();
		}

		private void Update(){
			if(!isBomb){
				//przeciwnicy odwracaja sie w strone gracza
			}
		}

		private void FixedUpdate(){
			if(_isMoving){
				MoveEnemy();
			}else if(isBomb){
				Debug.Log("BOOM");
				Destroy(gameObject);
			}
		}

		private void MoveEnemy(){
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

		private bool IsInNextStep(){
			var distance = Vector3.Distance(transform.position, nextStep);
			return distance < 0.01f;
		}

		public RespawnArea GetRespawnArea(){
			return enemyMovement.GetRespawnArea();
		}
	}
}