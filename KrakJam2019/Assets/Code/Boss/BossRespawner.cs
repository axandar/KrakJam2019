using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRespawner : MonoBehaviour
{
    [SerializeField] private float _bossRespawnTimer;
    [SerializeField] private Transform _bossRespawnTransform;
    [SerializeField] private GameObject _boss;
    
    public bool isAlive;
    
    private void Start(){
        StartCoroutine(RespawnBossInTime());
        
    }

    private void RespawnBoss()
    {
        _boss.transform.position = _bossRespawnTransform.position;
        _boss.SetActive(true);
    }
    

    IEnumerator RespawnBossInTime(){
        while (true){
            if (!isAlive){
                yield return new WaitForSeconds(_bossRespawnTimer);
                isAlive = true;
                RespawnBoss();
            }
        }
    }
}
