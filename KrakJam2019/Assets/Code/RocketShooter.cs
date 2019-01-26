using Code.Enemy;
using UnityEngine;

namespace Code{
	public class RocketShooter : MonoBehaviour{
		[SerializeField] GameObject rocketLauncher;
		[SerializeField] GameObject rocket01;
		[SerializeField] GameObject rocket02;
		[SerializeField] GameObject rocket03;

		[SerializeField] float rocketVelocity;
		[SerializeField] float rocketTimer;
		[SerializeField] private float delayBetweenShoots;

		public int boomBoomValue;
		public bool isShootingDisabled;

		private float timeFromLastShoot = 0;

		void Update(){
			if(!isShootingDisabled && Input.GetMouseButton(0) && timeFromLastShoot >= delayBetweenShoots){
				ShootRocket();
				timeFromLastShoot = 0;
			}else{
				timeFromLastShoot += Time.deltaTime;
			}
		}

		void ShootRocket(){
			if(rocketLauncher == null){
				return;
			}

			var rocket = GetRocket();
			var temporaryRocket = Instantiate(rocket, rocketLauncher.transform.position, rocketLauncher.transform.rotation);
			var temporaryRigidbody = temporaryRocket.GetComponent<Rigidbody2D>();
		
			temporaryRigidbody.AddForce(transform.up * rocketVelocity);
			Destroy(temporaryRocket, rocketTimer);
		}

		void OnCollisionEnter2D(Collision2D other){
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