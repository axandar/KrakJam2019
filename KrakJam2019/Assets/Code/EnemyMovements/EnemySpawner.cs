using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using UnityEngine;

namespace Code.EnemyMovements{
	public class EnemySpawner : MonoBehaviour{
		[SerializeField] private Camera gameCamera;
		[SerializeField] private GameController gameController;

		[SerializeField] private GameObject enemyBombPrefab;
		[SerializeField] private GameObject enemyLineXRightPrefab;
		[SerializeField] private GameObject enemyLineXLeftPrefab;

		[SerializeField] private Transform playerTransform;
		[SerializeField] private Transform wallLeftTransform;
		[SerializeField] private Transform wallRightTransform;

		[Header("Spawner values")] 
		[SerializeField] private float inLineXRightSpawnChance = 33.33f;
		[SerializeField] private float inLineXLeftSpawnChance = 33.33f;
		[SerializeField] private float bombSpawnChance = 33.33f;
		[SerializeField] private int minIntervalInMillis = 100;
		[SerializeField] private int maxIntervalInMillis = 1000;
		[SerializeField] private int minNumberOfSpawnedAtOnce = 1;
		[SerializeField] private int maxNumberOfSpawnedAtOnce = 10;
		[SerializeField] private int timeFlowMultiplicatorBetweenIntervals = 350;

		private readonly Queue<EnemySpawnInfo> _enemySpawnInfoQueue = new Queue<EnemySpawnInfo>();

		private EnemySpawnInfo.SpawnEnemyFunction _inLineXRight;
		private EnemySpawnInfo.SpawnEnemyFunction _inLineXLeft;
		private EnemySpawnInfo.SpawnEnemyFunction _bomb;

		private void Start(){
			_inLineXRight = SpawnEnemyGoesInLineXRight;
			_inLineXLeft = SpawnEnemyGoesInLineXLeft;
			_bomb = SpawnEnemyBomb;
		}

		private void Update(){
			if(_enemySpawnInfoQueue.Count == 0){
				GenerateSpawnInfo();
			}

			var spawnInfo = _enemySpawnInfoQueue.Peek();
			var temp = (int) (Time.deltaTime * timeFlowMultiplicatorBetweenIntervals);
			spawnInfo.IntervalInMillis -= temp;
			if(spawnInfo.IntervalInMillis <= 0){
				spawnInfo.SpawnEnemy.Invoke();
				_enemySpawnInfoQueue.Dequeue();
			}
		}

		private void GenerateSpawnInfo(){
			var quantity = Random.Range(minNumberOfSpawnedAtOnce, maxNumberOfSpawnedAtOnce);
			for(var i = 0; i < quantity; i++){
				var spawnFunction = GetSpawnEnemyFunction();
				var interval = Random.Range(minIntervalInMillis, maxIntervalInMillis);
				var spawnInfo = new EnemySpawnInfo(interval, spawnFunction);
				_enemySpawnInfoQueue.Enqueue(spawnInfo);
			}
		}

		private EnemySpawnInfo.SpawnEnemyFunction GetSpawnEnemyFunction(){
			var weightsList = new List<float>{inLineXRightSpawnChance, inLineXLeftSpawnChance, bombSpawnChance};
			var index = GetRandomWeightedIndex(weightsList);
			
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

		private static int GetRandomWeightedIndex(IReadOnlyList<float> weights){
			var weightSum = weights.Sum();
			
			var index = 0;
			var lastIndex = weights.Count - 1;
			while(index < lastIndex){
				// Do a probability check with a likelihood of weights[index] / weightSum.
				if(Random.Range(0, weightSum) < weights[index]){
					return index;
				}

				// Remove the last item from the sum of total untested weights and try again.
				weightSum -= weights[index++];
			}
			
			return index;
		}

		private void SpawnEnemyGoesInLineXRight(){
			var enemy = Instantiate(enemyLineXRightPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = wallRightTransform;
			enemyAi.addScore.AddListener(gameController.AddScore);
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		private void SpawnEnemyGoesInLineXLeft(){
			var enemy = Instantiate(enemyLineXLeftPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = wallLeftTransform;
			enemyAi.addScore.AddListener(gameController.AddScore);
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		private void SpawnEnemyBomb(){
			var enemy = Instantiate(enemyBombPrefab);
			var enemyAi = enemy.GetComponent<EnemyAI>();
			enemyAi.target = playerTransform;
			enemyAi.addScore.AddListener(gameController.AddScore);
			enemy.transform.position = GenerateVectorForSpawn(enemyAi.GetRespawnArea());
		}

		private Vector3 GenerateVectorForSpawn(RespawnArea respawnArea){
			var x = Random.Range(respawnArea.MinX, respawnArea.MaxX);
			var y = Random.Range(respawnArea.MinY, respawnArea.MaxY);
			return gameCamera.ViewportToWorldPoint(new Vector3(x, y, 1));
		}
	}
}