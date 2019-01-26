using Code.Enemy;
using UnityEngine;

namespace Code{
	public class BulletLogic : MonoBehaviour{
		private float _dmg;
		private EnemyAI _enemyPrefab;
		[SerializeField] private LayerMask layer;
		[SerializeField] private float dmgRange;

		private void Update(){
			_dmg = GameController.PlayerDmg;
			if(LookForTarget()){
				_enemyPrefab.health -= _dmg;
			}
		}

		private bool LookForTarget(){
			var enemys = Physics.OverlapSphere(transform.position, dmgRange, layer);
			if(enemys.Length == 0){
				return false;
			}else{
				_enemyPrefab = enemys[0].GetComponent<EnemyAI>();
				return true;
			}
		}
	}
}


