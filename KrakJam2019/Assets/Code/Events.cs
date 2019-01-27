using System;

namespace Code{
	public static class Events{
		public static event Action<float> StartShake;

		public static void BroadcastStartShake(float shakingTime){
			StartShake?.Invoke(shakingTime);
		}
	}
}