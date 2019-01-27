using Code.Enemy;
using UnityEngine;

namespace Code{
	public class RocketShooter : MonoBehaviour{
		[SerializeField] private GameObject rocketLauncher;
		[SerializeField] private GameObject rocket01;
		[SerializeField] private GameObject rocket02;
		[SerializeField] private GameObject rocket03;

		[SerializeField] private float rocketVelocity;
		[SerializeField] private float rocketTimer;
		[SerializeField] private float delayBetweenShoots;
		[SerializeField] private ShootingSoundsManager shootingSoundsManager;

		public int boomBoomValue;
		public bool isShootingDisabled;

		private float _timeFromLastShoot;

		void Update(){
			if(!isShootingDisabled && Input.GetMouseButton(0) && _timeFromLastShoot >= delayBetweenShoots){
				if (shootingSoundsManager != null){
					shootingSoundsManager.PlayShootingSound();
				}
				
				ShootRocket();
				_timeFromLastShoot = 0;
			}else{
				_timeFromLastShoot += Time.deltaTime;
			}
		}

		private void ShootRocket(){
			if(rocketLauncher == null){
				return;
			}

			var rocket = GetRocket();
			var temporaryRocket = Instantiate(rocket, rocketLauncher.transform.position, rocketLauncher.transform.rotation);
			var temporaryRigidbody = temporaryRocket.GetComponent<Rigidbody2D>();
		
			temporaryRigidbody.AddForce(transform.up * rocketVelocity);
			Destroy(temporaryRocket, rocketTimer);
		}

		private void OnCollisionEnter2D(Collision2D other){
			if(other.gameObject.CompareTag("Enemy")){
				other.gameObject.GetComponent<EnemyAI>().DamageMeBoi(boomBoomValue);
			}
		}

		private GameObject GetRocket(){
			var bulletId = Random.Range(0, 4);

			switch(bulletId){
				case 1:

					boomBoomValue = 10;
					return rocket01;
				case 2:
					boomBoomValue = 14;
					return rocket02;
				case 3:
					boomBoomValue = 12;
					return rocket03;
				default:
					return rocket01;
			}
		}
	}
}