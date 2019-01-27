using UnityEngine;

namespace Code.Boss{
    public class BossRespawner : MonoBehaviour
    {
        [SerializeField] private float bossRespawnTimer;
        [SerializeField] private Transform bossRespawnTransform;
        [SerializeField] private GameObject boss;
    
    
    

        private void RespawnBoss()
        {
            boss.SetActive(true);
            boss.transform.position = bossRespawnTransform.position;
        }


        public void InvokeRespawnBoss(){
            Invoke(nameof(RespawnBoss), bossRespawnTimer);
        }
    }
}
