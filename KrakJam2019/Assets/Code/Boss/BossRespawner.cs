using UnityEngine;

namespace Code.Boss{
	public class BossRespawner : MonoBehaviour{
		[SerializeField] private float bossRespawnTimer;
		[SerializeField] private GameObject boss;
		[SerializeField] private GameController gameController;
		[SerializeField] private Camera gameCamera;
		[SerializeField] private Transform playerTransform;
		[SerializeField] private Transform strifeRightSide;
		[SerializeField] private Transform strifeLeftSide;
		[SerializeField] private AmbientMusicManager musicManager;

		private void Start(){
			InvokeRespawnBoss(0);
		}

		private void InvokeRespawnBoss(int ignore){
			Invoke(nameof(RespawnBoss), bossRespawnTimer);
		}

		private void RespawnBoss(){
			Debug.Log("Boss respawned");
			var livingBoss = Instantiate(boss);
			var bossInfo = livingBoss.GetComponent<BossInfo>();
			bossInfo.bossDeathEvent.AddListener(gameController.AddScore);
			bossInfo.bossDeathEvent.AddListener(InvokeRespawnBoss);
			bossInfo.musicManager = musicManager;
			bossInfo.PrepareBoss();

			var bossAi = livingBoss.GetComponent<BossAI>();
			bossAi.gameCamera = gameCamera;
			bossAi.playerTransform = playerTransform;
			bossAi.MoveBossToFirstPosition();
			bossAi.strifeLeftSide = strifeLeftSide;
			bossAi.strifeRightSide = strifeRightSide;
		}
	}
}