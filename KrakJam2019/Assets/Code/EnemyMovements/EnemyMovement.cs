using System;
using UnityEngine;

namespace Code.EnemyMovements{
	[Serializable]
	public abstract class EnemyMovement : MonoBehaviour{
		public abstract void GenerateRoute(Vector3 start, Vector3 target);
		public abstract bool IsNextVector();
		public abstract Vector3 GetNextVector();
	}
}
