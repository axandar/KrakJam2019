using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class RocketShooter : MonoBehaviour {

     [SerializeField] GameObject rocketLauncher;
     [SerializeField] GameObject rocket01;
     [SerializeField] GameObject rocket02;
     [SerializeField] GameObject rocket03;
     [SerializeField] GameObject rocket;
     

     [SerializeField] float rocketVelocity;
     [SerializeField] float rocketTimer;
     [SerializeField] float shootingDelay;
     private int randomValues = 2;
     private static int bulletId;

    

     bool enableShooting = true;

     private void Awake()
     {
          StartCoroutine(Kappa());
          rocket = rocket01;
     }

     void Update() {
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
               if (temporaryRocket != null) {
                    Destroy(temporaryRocket, rocketTimer);
               }
          }
     }

     void EnableShooting() {
          enableShooting = true;
     }
    
     private IEnumerator Kappa()
     {
          while (true) {
               randomValues = Random.Range(0, 3);
               bulletId = Random.Range(0, 4);
               Debug.Log(bulletId);
               yield return new WaitForSeconds(randomValues);

               switch (bulletId) {
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
