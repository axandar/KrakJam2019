using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Code;

public class CameraShake : MonoBehaviour {
	public Transform camTransform;
	
	
	private Coroutine _shakeCoroutine;
	Vector3 originalPos;
	private float tmp;

	private void Update()
	{
		camTransform = transform;
	}

	void Awake() {
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
			tmp = 0;
		}

		Events.StartShake += StartShake;
	}
	void OnEnable() {
		originalPos = camTransform.localPosition;
	}

	void StartShake(float shakeDuration)
	{
		if(_shakeCoroutine != null)
			StopCoroutine(_shakeCoroutine);
		_shakeCoroutine = StartCoroutine(Shake(shakeDuration));
	}


	IEnumerator Shake(float shakeDuration)
	{
		Vector3 orginalPos = transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < shakeDuration) {
			float x = Random.Range(transform.position.x -1, transform.position.x +1)  ;
			float y = Random.Range(transform.position.y -1 , transform.position.y + 1);
            
			transform.localPosition = new Vector3(x,y,orginalPos.z);
			elapsed += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = orginalPos;
	}
    
}