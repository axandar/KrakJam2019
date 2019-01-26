using UnityEngine;

namespace Code{
	public class SpaceshipController : MonoBehaviour{
		public float speed = 5f;
		
		private void Update(){
			transform.Translate(new Vector3(
				                    Input.GetAxis("Horizontal") * speed,
				                    Input.GetAxis("Vertical") * speed,
				                    0) * Time.deltaTime);
		}
	}
}