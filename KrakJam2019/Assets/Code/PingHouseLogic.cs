using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Enemy;
using UnityEngine;

public class PingHouseLogic : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private EnemyAI _enemyAi;

    public float speed = 25;
    private bool slow = false;


    private void Update()
    {
        Slowmotion();
    }


    private void Slowmotion()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        transform.localPosition += new Vector3(h,0,v)
            *Time.fixedTime
            *speed
            *1/ Time.timeScale;
        if (Input.GetKeyDown(KeyCode.Space)) {
            slow = !slow;
            Time.timeScale = slow ? .1f : 1;
            speed = slow ? speed * 1 / Time.timeScale : 25;
        }
    }
    private void DestroyEnemyAround()
    {
        var colliders = Physics.OverlapSphere(transform.position, 20, _layerMask);
        if (colliders != null) {
            foreach (var enemy in colliders) {
                enemy.GetComponent<EnemyAI>().DamageMeBoi(10);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            var controller = other.gameObject.GetComponentInParent<GameController>();
            controller.HealthPoints -= 10;
            Destroy(gameObject);
        }
    }
    
}
