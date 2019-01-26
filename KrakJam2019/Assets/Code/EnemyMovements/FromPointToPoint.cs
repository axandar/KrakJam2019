using System;
using System.Collections.Generic;
using Code.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.EnemyMovements{
	[Serializable]
	public class FromPointToPoint : EnemyMovement{
		[SerializeField] private int predictionStepMillis = 100;
		[SerializeField] private int predictionTimeMillis = 4000;
		[SerializeField] private List<RespawnArea> rangesToSpawn = new List<RespawnArea>();

		private readonly Queue<Vector3> _route = new Queue<Vector3>();

		public override void GenerateRoute(Vector3 start, Vector3 target){
			var distance = Vector3.Distance(start, target);

			var step = distance / (int) (predictionTimeMillis / predictionStepMillis);
			for(var i = predictionStepMillis; i <= predictionTimeMillis; i += predictionStepMillis){
				var vec3 = Vector3.MoveTowards(start, target, step);
				start = vec3;
				_route.Enqueue(vec3);
				Debug.Log(vec3);
			}
		}

		public override bool IsNextVector(){
			return _route.Count > 0;
		}

		public override Vector3 GetNextVector(){
			return _route.Dequeue();
		}

		public override RespawnArea GetRespawnArea(){
			var index = Random.Range(0, rangesToSpawn.Count);
			return rangesToSpawn[index];
		}
	}
}