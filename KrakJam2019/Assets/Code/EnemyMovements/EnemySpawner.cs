using System.Collections.Generic;
using UnityEngine;

namespace Code{
	public class EnemySpawner : MonoBehaviour{
		[SerializeField] Camera gameCamera;

		[SerializeField] GameObject enemyBombPrefab;
		[SerializeField] GameObject enemyLineXRightPrefab;
		[SerializeField] GameObject enemyLineXLeftPrefab;
		
		[SerializeField] Transform playerTransform;
		[SerializeField] Transform wallLeftTransform;
		[SerializeField] Transform wallRightTransform;

		void Start(){
			SpawnEnemyGoesInLineXRight();
			SpawnEnemyGoesInLineXLeft();
			SpawnEnemyBomb();
		}

		void SpawnEnemyGoesInLineXRight(){
			var enemy = Instantiate(enemyLineXRightPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = wallRightTransform;
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		void SpawnEnemyGoesInLineXLeft(){
			var enemy = Instantiate(enemyLineXLeftPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = wallLeftTransform;
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		void SpawnEnemyBomb(){
			var enemy = Instantiate(enemyBombPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = playerTransform;
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		Vector3 GenerateVectorForSpawn(RespawnArea respawnArea){
			var x = Random.Range(respawnArea.MinX, respawnArea.MaxX);
			var y = Random.Range(respawnArea.MinY, respawnArea.MaxY);
			return gameCamera.ViewportToWorldPoint(new Vector3(x, y, 1));
		}
	}
}
