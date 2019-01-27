using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Boss{
	public class BossAI : MonoBehaviour{
		public Camera gameCamera;
		public Transform playerTransform;
		public Transform strifeRightSide;
		public Transform strifeLeftSide;
		
		[SerializeField] private float speed;
		[SerializeField] private float minDistanceToPlayer;
		[SerializeField] private float maxDistanceToPlayer;
		[SerializeField] private List<GameObject> bullets;
		[SerializeField] private float reload;
		[SerializeField] private int dmgPerBullet;
		[SerializeField] private int bulletSpeed = 10;
		[SerializeField] private Vector3 inCameraSpawnVector = new Vector3(0.5f, 0.9f, 1f);
		
		private bool _goingRight = true;
		private BossInfo _bossInfo;

		private void Start(){
			_bossInfo = GetComponent<BossInfo>();
			StartCoroutine(ShootThePlayer());
		}

		public void MoveBossToFirstPosition(){
			transform.position = gameCamera.ViewportToWorldPoint(inCameraSpawnVector);
		}

		private void FixedUpdate(){
			var distanceToPlayer = CalculateDistanceToPlayer();
			
			if(distanceToPlayer < maxDistanceToPlayer &&
			   distanceToPlayer > minDistanceToPlayer){
				StrifeNearPlayer();
			}else if(distanceToPlayer > minDistanceToPlayer){
				MoveToPlayer();
			}
		}

		private void DamageReceived(int damageTaken){
			_bossInfo.CurrentHealth -= damageTaken;
		}

		private void OnCollisionEnter2D(Collision2D other){
			if(other.gameObject.CompareTag("Bullet")){
				DamageReceived(dmgPerBullet);
				Destroy(other.gameObject);
			}
		}

		private void MoveToPlayer(){
			var step = speed * Time.deltaTime;
			transform.position = 
				Vector3.MoveTowards(transform.position, playerTransform.position, step);
		}

		private void StrifeNearPlayer(){
			var step = speed * Time.deltaTime;
			if(_goingRight){
				if(IsNearRightWall()){
					_goingRight = false;
				} else{
					transform.position = 
						Vector3.MoveTowards(transform.position, strifeRightSide.position, step);
				}
			}else if(IsNearLeftWall()){
				_goingRight = true;
			}else{
				transform.position = 
					Vector3.MoveTowards(transform.position, strifeLeftSide.position, step);
			}
		}

		private bool IsNearRightWall(){
			return Vector3.Distance(transform.position, strifeRightSide.position) < 10.0f;
		}

		private bool IsNearLeftWall(){
			return Vector3.Distance(transform.position, strifeLeftSide.position) < 10.0f;
		}


		private float CalculateDistanceToPlayer(){
			return Vector3.Distance(transform.position, playerTransform.position);
		}


		private IEnumerator ShootThePlayer(){
			while(true){
				var enemyToShoot = bullets[Random.Range(0, bullets.Count)];
			
				var enemy = Instantiate(enemyToShoot);
				var enemyAi = enemy.GetComponent<EnemyAI>();
				enemyAi.speed = bulletSpeed;
				enemyAi.target = playerTransform;
				enemy.transform.position = transform.position;
			
				yield return new WaitForSeconds(reload);
			}
		}
	}
}