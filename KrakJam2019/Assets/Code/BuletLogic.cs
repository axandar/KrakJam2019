using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Enemy;
using Code.EnemyMovements;
using UnityEngine;

public class BuletLogic : MonoBehaviour
{
    private float dMG;
    private EnemyAI enemyPrefab;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float DMGRange;

    private void Update()
    {
        dMG = GameController.PlayerDMG;
        Debug.Log(dMG);
        if (LookForTarget()) {
            enemyPrefab.health -= dMG;
        }
    }
    bool LookForTarget()
    {
        var enemys = Physics.OverlapSphere(transform.position,DMGRange,layer);
        if (enemys.Length == 0)
            return false;
        enemyPrefab = enemys[0].GetComponent<EnemyAI>();
        return true;
    }
}
