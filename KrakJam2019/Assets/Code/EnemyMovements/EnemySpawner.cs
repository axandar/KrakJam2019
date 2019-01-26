using System.Collections.Generic;
using Code.Enemy;
using UnityEngine;

namespace Code.EnemyMovements{
	public class EnemySpawner : MonoBehaviour{
		[SerializeField] Camera gameCamera;

		[SerializeField] GameObject enemyBombPrefab;
		[SerializeField] GameObject enemyLineXRightPrefab;
		[SerializeField] GameObject enemyLineXLeftPrefab;

		[SerializeField] Transform playerTransform;
		[SerializeField] Transform wallLeftTransform;
		[SerializeField] Transform wallRightTransform;

		readonly Queue<EnemySpawnInfo> _enemySpawnInfoQueue = new Queue<EnemySpawnInfo>();

		EnemySpawnInfo.SpawnEnemyFunction _inLineXRight;
		EnemySpawnInfo.SpawnEnemyFunction _inLineXLeft;
		EnemySpawnInfo.SpawnEnemyFunction _bomb;

		void Start(){
			_inLineXRight = SpawnEnemyGoesInLineXRight;
			_inLineXLeft = SpawnEnemyGoesInLineXLeft;
			_bomb = SpawnEnemyBomb;
		}

		void Update(){
			if(_enemySpawnInfoQueue.Count == 0){
				GenerateSpawnInfo();
			}

			var spawnInfo = _enemySpawnInfoQueue.Peek();
			var temp = (int) (Time.deltaTime * 350);
			spawnInfo.IntervalInMillis -= temp;
			if(spawnInfo.IntervalInMillis <= 0){
				spawnInfo.SpawnEnemy.Invoke();
				_enemySpawnInfoQueue.Dequeue();
			}
		}

		void GenerateSpawnInfo(){
			var quantity = Random.Range(1, 10);
			for(var i = 0; i < quantity; i++){
				var spawnFunction = GetSpawnEnemyFunction();
				var interval = Random.Range(100, 1000);
				var spawnInfo = new EnemySpawnInfo(interval, spawnFunction);
				_enemySpawnInfoQueue.Enqueue(spawnInfo);
			}
		}

		EnemySpawnInfo.SpawnEnemyFunction GetSpawnEnemyFunction(){
			var index = Random.Range(0, 3);
			switch(index){
				case 0:
					return _inLineXRight;
				case 1:
					return _inLineXLeft;
				case 2:
					return _bomb;
				default:
					return _bomb;
			}
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