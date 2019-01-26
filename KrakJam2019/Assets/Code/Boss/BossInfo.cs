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
   
   
   public AddScoreEvent addScore;


   private void OnEnable(){
      CurrentHealth = _health;
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
   
   [Serializable]
   public class AddScoreEvent : UnityEvent<int> {
   }
   
  
}
