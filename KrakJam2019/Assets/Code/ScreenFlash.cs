using UnityEngine;
using UnityEngine.UI;  // add to the top
using System.Collections;
 
public class ScreenFlash : MonoBehaviour {
 
    public CanvasGroup playerFlash;
    public CanvasGroup enemyFlash;
    bool hit;
    bool flash;
    
    public Transform camTransform;
	
    public float shakeDuration = 0f;
	
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
	
    Vector3 originalPos;

    void Awake() {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    
    	void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            GetHit();
            if (hit) {
                playerFlash.alpha = playerFlash.alpha - Time.deltaTime;
                if (playerFlash.alpha <= 0) {
                    playerFlash.alpha = 0;
                    hit = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            DoHit();
            if (flash) {
                enemyFlash.alpha = enemyFlash.alpha - Time.deltaTime;
                if (enemyFlash.alpha <= 0) {
                    enemyFlash.alpha = 0;
                    flash = false;
                }
            }

        }
    }

    public void GetHit () {
        hit = true;
        if (shakeDuration > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
        playerFlash.alpha = .5f;
        Invoke(nameof(StopHit), 0.05f);
        
    }

    void StopHit() {
        hit = false;
        playerFlash.alpha = 0;
    }

    public void DoHit() {
        flash = true;
        if (shakeDuration > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
        enemyFlash.alpha = .5f;
        Invoke(nameof(StopFlash), 0.05f);
    }

    void StopFlash() {
        flash = false;
        enemyFlash.alpha = 0;
    }
}