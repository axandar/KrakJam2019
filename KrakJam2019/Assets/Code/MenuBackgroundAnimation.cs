using UnityEngine;
using System.Collections;

public class MenuBackgroundAnimation : MonoBehaviour{
	[SerializeField] float newXvalue, newYvalue;

	void Update(){
		transform.position = new Vector3(transform.position.x + newXvalue, transform.position.y + newYvalue, 0);
	}
}