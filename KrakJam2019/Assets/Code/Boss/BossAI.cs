using System.Collections;
using System.Collections.Generic;
using Code.Boss;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private BossInfo _bossInfo;


    private void Start(){
        _bossInfo = GetComponent<BossInfo>();
    }
    
    private void DamageRecieved(int damageTaken){
        _bossInfo.CurrentHealth -= damageTaken;
    }
}
