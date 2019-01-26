using UnityEngine;
using UnityEngine.UI;  // add to the top
using System.Collections;
 
public class ScreenFlash : MonoBehaviour {
 
    public CanvasGroup playerFlash;
    public CanvasGroup enemyFlash;
    bool hit;
    bool flash;
    

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
        playerFlash.alpha = .5f;
        Invoke(nameof(StopHit), 0.05f);
    }

    void StopHit() {
        hit = false;
        playerFlash.alpha = 0;
    }

    public void DoHit() {
        flash = true;
        enemyFlash.alpha = .5f;
        Invoke(nameof(StopFlash), 0.05f);
    }

    void StopFlash() {
        flash = false;
        enemyFlash.alpha = 0;
    }
}