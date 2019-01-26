using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
   Rigidbody2D _rb;
   float _xAxis;
   float _yAxis;

   [SerializeField] float timeToStop = 0.01f;
   [SerializeField] float _maxVelocity = 3;
   [SerializeField] float _rotationSpeed = 2;
   
   void Start() {
      _rb = GetComponent<Rigidbody2D>();
      
   }

   void Update() {
      _xAxis = Input.GetAxis("Vertical");
      _yAxis = Input.GetAxis("Horizontal");
      GetInput();
   }


   void GetInput() {
      if (Input.GetKey(KeyCode.W))
         MoveForvard(_xAxis);
      
      if(!Input.GetKey(KeyCode.W)) {
         if (_rb.velocity.magnitude != 0)
            _rb.velocity = Vector2.Lerp(_rb.velocity , Vector2.zero, timeToStop);
         }
      
      if (Input.GetKey(KeyCode.A))
         Rotate(transform, _rotationSpeed );

      if (Input.GetKey(KeyCode.D))
         Rotate(transform, -  _rotationSpeed);
      // ClampVelocity();
   }

   void ClampVelocity() {
      float x = Mathf.Clamp(_rb.velocity.x, -_maxVelocity , _maxVelocity);
      float y = Mathf.Clamp(_rb.velocity.y, -_maxVelocity , _maxVelocity);
   
      _rb.velocity = new Vector2(x,y);
   }

   void MoveForvard(float amount) {
      Vector2 force = transform.up * amount;
      _rb.AddForce(force); 
   }

   void Rotate(Transform t, float amount) {
      t.Rotate(0, 0, amount);
   }
}
