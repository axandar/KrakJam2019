using UnityEngine;

namespace Code.Boss{
    public class BossRespawner : MonoBehaviour
    {
        [SerializeField] private float bossRespawnTimer;
        [SerializeField] private Transform bossRespawnTransform;
        [SerializeField] private GameObject boss;
    
    
    

        private void RespawnBoss()
        {
            boss.transform.position = bossRespawnTransform.position;
            boss.SetActive(true);
        }


        public void InvokeRespawnBoss(){
            Invoke(nameof(RespawnBoss), bossRespawnTimer);
        }
    }
}
