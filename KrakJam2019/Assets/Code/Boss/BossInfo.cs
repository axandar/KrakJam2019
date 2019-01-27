using System;
using Code.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Boss{
	public class BossInfo : MonoBehaviour{
		public AmbientMusicManager musicManager;		
		public BossDeathEvent bossDeathEvent;

		[SerializeField] private float health;
		[SerializeField] private int scoreValue;
		
		public void PrepareBoss(){
			CurrentHealth = health;
			if (musicManager != null){
				musicManager.StopAmbientMusic();
			}
		}

		public float CurrentHealth{
			get => health;
			set{
				health = value;
				if(health <= 0){
					bossDeathEvent.Invoke(scoreValue);
					Destroy(gameObject);
				}
			}
		}

		private void OnDisable(){
			musicManager.StartAmbientMusic();
		}
		
		[Serializable]
		public class BossDeathEvent : UnityEvent<int> { }
	}
}