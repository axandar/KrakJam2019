using System.Collections;
using UnityEngine;

namespace Code{
    [RequireComponent(typeof(AudioSource))]
    public class ExplosionSoundManager : MonoBehaviour{
    
        private AudioSource _explosionSource;

        private void Start(){
            _explosionSource = GetComponent<AudioSource>();
            
        }

        public void PlaySound(){
            if (_explosionSource != null) {
                _explosionSource.Play();
                var explosionLength = _explosionSource.clip.length;
                StartCoroutine(WaitForEnd(explosionLength));
            }
        }

        IEnumerator WaitForEnd(float lenght){
            yield return new WaitForSeconds(lenght);
            LetItBeDestroyed();
        }
        
        private void LetItBeDestroyed(){
            Destroy(gameObject);
        }
    }
}
