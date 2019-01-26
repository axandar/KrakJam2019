using UnityEngine;
// add to the top

namespace Code{
    public class ScreenFlash : MonoBehaviour {
 
        public CanvasGroup playerFlash;
        public CanvasGroup enemyFlash;
        private bool _hit;
        private bool _flash;
    
        public Transform camTransform;
	
        public float shakeDuration = 0f;
	
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;

        private Vector3 _originalPos;

        private void Awake() {
            if (camTransform == null)
            {
                camTransform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        private void OnEnable() {
            _originalPos = camTransform.localPosition;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.K)) {
                GetHit();
                if (_hit) {
                    playerFlash.alpha = playerFlash.alpha - Time.deltaTime;
                    if (playerFlash.alpha <= 0) {
                        playerFlash.alpha = 0;
                        _hit = false;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.L)) {
                DoHit();
                if (_flash) {
                    enemyFlash.alpha = enemyFlash.alpha - Time.deltaTime;
                    if (enemyFlash.alpha <= 0) {
                        enemyFlash.alpha = 0;
                        _flash = false;
                    }
                }

            }
        }

        private void GetHit () {
            _hit = true;
            if (shakeDuration > 0) {
                camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
			
                shakeDuration -= Time.deltaTime * decreaseFactor;
            } else {
                shakeDuration = 0f;
                camTransform.localPosition = _originalPos;
            }
            playerFlash.alpha = .5f;
            Invoke(nameof(StopHit), 0.05f);
        
        }

        private void StopHit() {
            _hit = false;
            playerFlash.alpha = 0;
        }

        private void DoHit() {
            _flash = true;
            if (shakeDuration > 0) {
                camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
			
                shakeDuration -= Time.deltaTime * decreaseFactor;
            } else {
                shakeDuration = 0f;
                camTransform.localPosition = _originalPos;
            }
            enemyFlash.alpha = .5f;
            Invoke(nameof(StopFlash), 0.05f);
        }

        private void StopFlash() {
            _flash = false;
            enemyFlash.alpha = 0;
        }
    }
}