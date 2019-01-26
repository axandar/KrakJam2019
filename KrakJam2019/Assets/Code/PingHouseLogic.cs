using Code.Enemy;
using UnityEngine;

namespace Code{
	public class PingHouseLogic : MonoBehaviour{
		[SerializeField] private LayerMask layerMask;
		private EnemyAI _enemyAi;

		public float speed = 25;
		private bool _slow;

		private void Update(){
			SlowMotion();
		}

		private void SlowMotion(){
			var h = Input.GetAxis("Horizontal");
			var v = Input.GetAxis("Vertical");
			
			transform.localPosition += new Vector3(h, 0, v)
			                           * Time.fixedTime
			                           * speed
			                           * 1 / Time.timeScale;
			if(Input.GetKeyDown(KeyCode.Space)){
				_slow = !_slow;
				Time.timeScale = _slow ? .1f : 1;
				speed = _slow ? speed * 1 / Time.timeScale : 25;
			}
		}

		private void DestroyEnemyAround(){
			var colliders = Physics.OverlapSphere(transform.position, 20, layerMask);
			if(colliders != null){
				foreach(var enemy in colliders){
					enemy.GetComponent<EnemyAI>().DamageMeBoi(10);
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D other){
			if(other.gameObject.CompareTag("Player")){
				var controller = other.gameObject.GetComponentInParent<GameController>();
				controller.HealthPoints -= 10;
				Destroy(gameObject);
			}
		}
	}
}