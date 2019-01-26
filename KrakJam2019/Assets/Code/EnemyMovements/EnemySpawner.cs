using System.Collections.Generic;
using UnityEngine;

namespace Code{
	public class EnemySpawner : MonoBehaviour{
		[SerializeField] private Camera gameCamera;
		[SerializeField] private GameObject enemyPrefab;
		[SerializeField] private RespawnArea[] rangesToSpawn = {
			new RespawnArea(-0.3f, -0.1f, 0.5f, 1.5f),
			new RespawnArea(1.1f, 1.3f, 0.5f, 1.5f),
			new RespawnArea(0f, 1f, 1.1f, 1.3f) 
		};

		private void Start(){
			SpawnEnemy();
		}

		public void SpawnEnemy(){
			var enemy = Instantiate(enemyPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = transform;
			enemy.transform.position = GenerateVectorForSpawn();
		}

		private Vector3 GenerateVectorForSpawn(){
			var index = Random.Range(0, rangesToSpawn.Length);
			var respawnArea = rangesToSpawn[index];
			
			var x = Random.Range(respawnArea.MinX, respawnArea.MaxX);
			var y = Random.Range(respawnArea.MinY, respawnArea.MaxY);
			return gameCamera.ViewportToWorldPoint(new Vector3(x, y, -9));
		}
	}
}
