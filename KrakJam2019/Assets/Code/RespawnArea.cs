using System;
using UnityEngine;

namespace Code{
	[Serializable]
	public class RespawnArea{
		[SerializeField] private float minX;
		[SerializeField] private float maxX;
		[SerializeField] private float maxY;
		[SerializeField] private float minY;

		public RespawnArea(float minX, float maxX, float maxY, float minY){
			this.minX = minX;
			this.maxX = maxX;
			this.maxY = maxY;
			this.minY = minY;
		}


		public float MinX{
			get => minX;
			set => minX = value;
		}

		public float MaxX{
			get => maxX;
			set => maxX = value;
		}

		public float MaxY{
			get => maxY;
			set => maxY = value;
		}

		public float MinY{
			get => minY;
			set => minY = value;
		}
	}
}