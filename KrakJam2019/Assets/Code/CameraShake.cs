using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    public Transform camTransform;
	
	
    private Coroutine _shakeCoroutine;
    Vector3 originalPos;
    private float tmp;

	
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

    void StartShake(float shakeDuration, float shakeMagnitude)
    {
        if(_shakeCoroutine != null)
             StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = StartCoroutine(ShakeCor(shakeDuration,shakeMagnitude));
    }

    IEnumerator ShakeCor ( float shakeDuration, float shakeMagnitude)
    {
        while (shakeDuration > 0) {
            if (tmp > shakeDuration)
                shakeDuration = tmp;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
            yield return null;
        } 
         camTransform.localPosition = originalPos;
    }
}