using Code.Enemy;
using UnityEngine;

namespace Code{
	public class PingHouseLogic : MonoBehaviour{
		
		[SerializeField] private LayerMask layerMask;
		private EnemyAI _enemyAi;
		[SerializeField] private GameObject isAlpaka;
		private SpriteRenderer renderererererererer;

		void Start()
		{
			renderererererererer = GetComponentInParent<SpriteRenderer>();
		}

		private void DestroyEnemyAround()
		{
			var colliders = Physics2D.OverlapCircleAll(transform.position, 20, layerMask);
			if(colliders != null){
				foreach(var enemy in colliders){
					enemy.GetComponent<EnemyAI>().DamageMeBoi(10);
					Debug.Log("DIEEVERyONE");
					Destroy(enemy.gameObject);
					}
			}
		}

		private void OnCollisionEnter2D(Collision2D other){
			if(other.gameObject.CompareTag("Player")) {
				renderererererererer.enabled = false;
				DestroyEnemyAround();
				Destroy(gameObject);
				Destroy(isAlpaka);

			}
		}

	}
}