using UnityEngine;
// add to the top

namespace Code{
    public class ScreenFlash : MonoBehaviour {
 
        public CanvasGroup playerFlash;
        public CanvasGroup enemyFlash;
        private bool _hit;
        private bool _flash;
    



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
            playerFlash.alpha = .5f;
            Invoke(nameof(StopHit), 0.05f);
        
        }

        private void StopHit() {
            _hit = false;
            playerFlash.alpha = 0;
        }

        private void DoHit() {
            _flash = true;
            enemyFlash.alpha = .5f;
            Invoke(nameof(StopFlash), 0.05f);
        }

        private void StopFlash() {
            _flash = false;
            enemyFlash.alpha = 0;
        }
    }
}