using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

	void Update () {
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		Material starfieldMaterial = meshRenderer.material;
		Vector2 offset = starfieldMaterial.mainTextureOffset;
		offset.x += Time.deltaTime / 10f;
		starfieldMaterial.mainTextureOffset = offset;
	}
}
