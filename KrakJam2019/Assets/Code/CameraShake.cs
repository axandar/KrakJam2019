using System.Collections;
using UnityEngine;

namespace Code{
	public class CameraShake : MonoBehaviour{
		public Transform camTransform;
		
		private Coroutine _shakeCoroutine;
		private Vector3 _originalPos;
		private float _tmp;

		private void Awake(){
			if(camTransform == null){
				camTransform = GetComponent(typeof(Transform)) as Transform;
				_tmp = 0;
			}

			Events.StartShake += StartShake;
		}

		private void OnEnable(){
			_originalPos = camTransform.localPosition;
		}

		private void StartShake(float shakeDuration, float shakeMagnitude){
			if(_shakeCoroutine != null)
				StopCoroutine(_shakeCoroutine);
			_shakeCoroutine = StartCoroutine(ShakeCor(shakeDuration, shakeMagnitude));
		}

		private IEnumerator ShakeCor(float shakeDuration, float shakeMagnitude){
			while(shakeDuration > 0){
				if(_tmp > shakeDuration)
					camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeMagnitude;
				shakeDuration -= Time.deltaTime;
				yield return null;
			}

			camTransform.localPosition = _originalPos;
		}
	}
}