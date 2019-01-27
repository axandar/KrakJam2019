using System;
using Code.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Boss{
	public class BossInfo : MonoBehaviour{
		private float currentHealth;
		
		[SerializeField] private float health;
		[SerializeField] private int scoreValue;
		[SerializeField] private BossRespawner bossRespawner;
		[SerializeField] private AmbientMusicManager musicManager;
		
		public EnemyAI.AddScoreEvent addScore;


		private void OnEnable(){
			CurrentHealth = health;
			if (musicManager != null){
				musicManager.StopAmbientMusic();
			}
		}

		public float CurrentHealth{
			get => currentHealth;
			set{
				currentHealth = value;
				if(currentHealth <= 0){
					bossRespawner.InvokeRespawnBoss();
					addScore.Invoke(scoreValue);

					gameObject.SetActive(false);
				}
			}
		}

		private void OnDisable(){
			musicManager.StartAmbientMusic();
		}
	}
}