using UnityEngine;

namespace Code.Enemy{
	// ReSharper disable once InconsistentNaming
	public class EnemyAI : MonoBehaviour{
		public Transform target;

		[SerializeField] private EnemyMovement enemyMovement;
		[SerializeField] private Vector3 nextStep;
		[SerializeField] private float speed = 5;
		[SerializeField] private bool isBomb;
		public float health = 5;

		private bool _isMoving = true;

		private void Start(){
			var movementInstance = Instantiate(enemyMovement);
			enemyMovement = movementInstance;
			
			var temp = target.position;
			enemyMovement.GenerateRoute(transform.position, 
				new Vector3(temp.x, temp.y, temp.z));
			nextStep = enemyMovement.GetNextVector();
		}

		private void Update(){
			if(!isBomb){
				//przeciwnicy odwracaja sie w strone gracza
			}

			if(health <= 0){
				Destroy(gameObject);
			}
		}

		private void FixedUpdate(){
			if(_isMoving){
				MoveEnemy();
			}else{
				if(isBomb){
					Debug.Log("BOOM");
				}
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
			//Debug.Log(nextStep);
			transform.position = Vector3.MoveTowards(transform.position, nextStep, step);
		}

		private bool IsInNextStep(){
			var distance = Vector3.Distance(transform.position, nextStep);
			return distance < 0.01f;
		}

		public RespawnArea GetRespawnArea(){
			return enemyMovement.GetRespawnArea();
		}
		
		[ContextMenu("Kill")]
		private void Kill(){
			health -= health;
		}
	}
}