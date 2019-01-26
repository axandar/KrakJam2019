using UnityEngine;

namespace Code{
	public class FollowUV : MonoBehaviour{
		public float parralax = 2f;

		private void Update(){
			var meshRenderer = GetComponent<MeshRenderer>();
			var starfieldMaterial = meshRenderer.material;
			var offset = starfieldMaterial.mainTextureOffset;
			
			var transform1 = transform;
			var localScale = transform1.localScale;
			var position = transform1.position;
			
			offset.x = position.x / localScale.x / parralax;
			offset.y = position.y / localScale.y / parralax;

			starfieldMaterial.mainTextureOffset = offset;
		}
	}
}