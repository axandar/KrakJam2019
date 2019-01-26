using UnityEngine;
using System.Collections;

public class FollowUV : MonoBehaviour {

	public float parralax = 2f;

	void Update () {
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		Material starfieldMaterial = meshRenderer.material;
		Vector2 offset = starfieldMaterial.mainTextureOffset;
		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.y / transform.localScale.y / parralax;

		starfieldMaterial.mainTextureOffset = offset;
	}
}
