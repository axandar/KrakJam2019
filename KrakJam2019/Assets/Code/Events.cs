using System;

namespace Code{
	public static class Events{
		public static event Action<float, float> StartShake;

		public static void BroadcastStartShake(float shakingTime, float shakeMagnitude){
			StartShake?.Invoke(shakingTime, shakeMagnitude);
		}
	}
}