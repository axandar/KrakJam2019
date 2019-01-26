using System.Collections.Generic;
using UnityEngine;

namespace Code{
	public class EnemySpawner : MonoBehaviour{
		[SerializeField] private Camera gameCamera;

		[SerializeField] private GameObject enemyBombPrefab;
		[SerializeField] private GameObject enemyLineXRightPrefab;
		[SerializeField] private GameObject enemyLineXLeftPrefab;
		
		[SerializeField] private Transform playerTransform;
		[SerializeField] private Transform wallLeftTransform;
		[SerializeField] private Transform wallRightTransform;

		private void Start(){
			SpawnEnemyGoesInLineXRight();
			SpawnEnemyGoesInLineXLeft();
			SpawnEnemyBomb();
		}

		private void SpawnEnemyGoesInLineXRight(){
			var enemy = Instantiate(enemyLineXRightPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = wallRightTransform;
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		private void SpawnEnemyGoesInLineXLeft(){
			var enemy = Instantiate(enemyLineXLeftPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = wallLeftTransform;
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		private void SpawnEnemyBomb(){
			var enemy = Instantiate(enemyBombPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = playerTransform;
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		private Vector3 GenerateVectorForSpawn(RespawnArea respawnArea){
			var x = Random.Range(respawnArea.MinX, respawnArea.MaxX);
			var y = Random.Range(respawnArea.MinY, respawnArea.MaxY);
			return gameCamera.ViewportToWorldPoint(new Vector3(x, y, 1));
		}
	}
}
