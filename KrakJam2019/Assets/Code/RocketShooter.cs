using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Enemy;
using UnityEngine;

public class RocketShooter : MonoBehaviour{
	[SerializeField] GameObject rocketLauncher;
	[SerializeField] GameObject rocket01;
	[SerializeField] GameObject rocket02;
	[SerializeField] GameObject rocket03;
	[SerializeField] GameObject rocket;


	[SerializeField] float rocketVelocity;
	[SerializeField] float rocketTimer;
	[SerializeField] float shootingDelay;
	private int randomValues;
	private static int bulletId;

	public int boomBoomValue;
	public bool isShootingDisabled;
	bool enableShooting = true;

	void Awake() {
		StartCoroutine(Kappa());
		rocket = rocket01;
	}

	void Update() {
		if (!isShootingDisabled)
			ShootRocket();
	}

	void ShootRocket() {
          if (enableShooting) {
               if (!Input.GetMouseButton(0)) return;
               enableShooting = false;
               var temporaryRocket = Instantiate(rocket, rocketLauncher.transform.position,
                    rocketLauncher.transform.rotation) as GameObject;

			var temporaryRigidbody = temporaryRocket.GetComponent<Rigidbody2D>();
			temporaryRigidbody.AddForce(transform.up * rocketVelocity);
			Invoke(nameof(EnableShooting), shootingDelay);
			if(temporaryRocket != null){
				Destroy(temporaryRocket, rocketTimer);
			}
		}
	}
     void EnableShooting() {
          enableShooting = true;
     }

     void OnCollisionEnter2D(Collision2D other) {
          if (other.gameObject.tag == "Enemy") {
               other.gameObject.GetComponent<EnemyAI>().DamageMeBoi(boomBoomValue);
               Destroy(gameObject);
          }
     }

	IEnumerator Kappa(){
		while(true){
			randomValues++;
			if(randomValues == 3)
				randomValues = 1;
			bulletId = Random.Range(0, 4);
			Debug.Log(bulletId);
			yield return new WaitForSeconds(Random.Range(0, 1));

			switch(bulletId){
				case 1:
					rocket = rocket01;
					break;
				case 2:
					rocket = rocket02;
					break;
				case 3:
					rocket = rocket03;
					break;
			}
		}
	}
}