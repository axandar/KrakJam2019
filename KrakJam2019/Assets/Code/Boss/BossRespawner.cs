using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRespawner : MonoBehaviour
{
    [SerializeField] private float _bossRespawnTimer;
    [SerializeField] private Transform _bossRespawnTransform;
    [SerializeField] private GameObject _boss;
    
    
    

    private void RespawnBoss()
    {
        _boss.transform.position = _bossRespawnTransform.position;
        _boss.SetActive(true);
    }


    public void InvokeRespawnBoss(){
        Invoke("RespawnBoss",_bossRespawnTimer);
    }
}
