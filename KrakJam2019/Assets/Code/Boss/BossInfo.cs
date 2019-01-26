using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enemy;
using UnityEngine;
using UnityEngine.Events;

public class BossInfo : MonoBehaviour{
   
   private float currentHealth;

   [SerializeField] private float _health;
   [SerializeField] private int _scoreValue;
   [SerializeField] private BossRespawner _bossRespawner;
   [SerializeField] private AmbientMusicManager _musicManager;
   
   
   public AddScoreEvent addScore;


   private void OnEnable(){
      CurrentHealth = _health;
      _musicManager.StopAmbientMusic();
   }

   public float CurrentHealth{
      get { return currentHealth; }
      set{
         currentHealth = value;
         if (currentHealth <= 0){
            _bossRespawner.InvokeRespawnBoss();
            addScore.Invoke(_scoreValue);
            
            gameObject.SetActive(false);
         }
      }
   }

   private void OnDisable()
   {
      _musicManager.StartAmbientMusic();
   }

   [Serializable]
   public class AddScoreEvent : UnityEvent<int> {
   }
   
  
}
