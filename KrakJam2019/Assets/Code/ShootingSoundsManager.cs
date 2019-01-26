using UnityEngine;

namespace Code{
	[RequireComponent(typeof(AudioSource))]
	public class ShootingSoundsManager : MonoBehaviour{
		private AudioSource _shootingSoundSource;

		private void Start(){
			_shootingSoundSource = GetComponent<AudioSource>();
		}

		public void PlayShootingSound(){
			if(_shootingSoundSource.isPlaying){
				return;
			}

			_shootingSoundSource.Play();
		}
	}
}