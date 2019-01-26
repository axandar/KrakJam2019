using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    public Transform camTransform;
	
	
    public float shakeAmount = 0.7f;
    private Coroutine _shakeCoroutine;
    Vector3 originalPos;
	
    void Awake() {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        Events.TakeResourcesAfterConstract += StartShake;
    }
	
    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void StartShake(float shakeDuration)
    {
        if(_shakeCoroutine != null)
             StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = StartCoroutine(ShakeCor(shakeDuration));
    }

    IEnumerator ShakeCor ( float shakeDuration)
    {
        while (shakeDuration > 0) {
            Debug.Log("ShakeScreen");
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime;
            yield return null;
        } 
         camTransform.localPosition = originalPos;
    }
}