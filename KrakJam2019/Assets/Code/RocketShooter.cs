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
			if(!isShootingDisabled && Input.GetMouseButton(0) && timeFromLastShoot >= delayBetweenShoots){
				Debug.Log("entering shoot manager");
				if (_shootingSoundsManager != null){
					_shootingSoundsManager.PlayShootingSound();
				}
				Debug.Log("exciting shoot manager");
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
					return rocket01;
				case 2:
					return rocket02;
				case 3:
					return rocket03;
				default:
					return rocket01;
			}
		}
	}
}