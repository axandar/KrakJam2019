using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   private Rigidbody2D _rb;
   private float _xAxis;
   private float _yAxis;


   [SerializeField] private float timeToStop = 0.01f;
   [SerializeField] private float _maxVelocity = 3;
   [SerializeField] private float _rotationSpeed = 2;

   private void Start()
   {
      _rb = GetComponent<Rigidbody2D>();
      
   }

   private void Update()
   {
      _xAxis = Input.GetAxis("Vertical");
      _yAxis = Input.GetAxis("Horizontal");
      GetInput();
   }


   private void GetInput()
   {
   
      if(Input.GetKey(KeyCode.W)){        
         MoveForvard(_xAxis);
      }
      
      if(!Input.GetKey(KeyCode.W)) {
         if (_rb.velocity.magnitude != 0) {
            _rb.velocity = Vector2.Lerp(_rb.velocity , Vector2.zero, timeToStop);
         }
         
      }
      if (Input.GetKey(KeyCode.A)) {
         Rotate(transform, _rotationSpeed );
      }

      if (Input.GetKey(KeyCode.D)) {
         Rotate(transform, -  _rotationSpeed);
      }
     // ClampVelocity();
   }

   private void ClampVelocity()
   {
      float x = Mathf.Clamp(_rb.velocity.x, -_maxVelocity , _maxVelocity);
      float y = Mathf.Clamp(_rb.velocity.y, -_maxVelocity , _maxVelocity);
   
      _rb.velocity = new Vector2(x,y);
   }
   private void MoveForvard(float amount)
   {
      Vector2 force = transform.up * amount;
      _rb.AddForce(force); 
   }

   private void Rotate(Transform t, float amount)
   {
      t.Rotate(0, 0, amount);
   }

}
