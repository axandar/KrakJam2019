using UnityEngine;

namespace Code{
	public class MenuBackgroundAnimation : MonoBehaviour{
		[SerializeField] private float newXvalue, newYvalue;

		private void Update(){
			var transform1 = transform;
			var position = transform1.position;
			position = new Vector3(position.x + newXvalue, position.y + newYvalue, 0);
			transform1.position = position;
		}
	}
}