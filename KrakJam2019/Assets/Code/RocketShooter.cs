﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShooter : MonoBehaviour {

     [SerializeField] GameObject rocketLauncher;
     [SerializeField] GameObject rocket;
     [SerializeField] float rocketVelocity;
     [SerializeField] float rocketTimer;
     [SerializeField] float shootingDelay;

     bool enableShooting = true;
     
     void Update() {
          ShootRocket();
     }
     
     void ShootRocket() {
          if (enableShooting) {
               if (!Input.GetMouseButton(0)) return;
               enableShooting = false;
               var temporaryRocket = Instantiate(rocket, rocketLauncher.transform.position,
                    rocketLauncher.transform.rotation) as GameObject;

               var temporaryRigidbody = temporaryRocket.GetComponent<Rigidbody>();
               temporaryRigidbody.AddForce(transform.up * rocketVelocity);
               Invoke(nameof(EnableShooting), shootingDelay);
               Destroy(temporaryRocket, rocketTimer);
          }
     }

     void EnableShooting() {
          enableShooting = true;
     }
}
