using System;
using UnityEngine;

namespace Code{
	public class PlayerController : MonoBehaviour{
		private Rigidbody2D _rb;
		private float _xAxis;
		private float _yAxis;


		[SerializeField] private float timeToStop = 0.01f;
		[SerializeField] private float maxVelocity = 3;
		[SerializeField] private float rotationSpeed = 2;

		public static float Acceleration = 5;

		private void Start(){
			_rb = GetComponent<Rigidbody2D>();
		}

		private void Update(){
			_xAxis = Input.GetAxis("Vertical");
			_yAxis = Input.GetAxis("Horizontal");
			GetInput();
		}


		private void GetInput(){
			if(Input.GetKey(KeyCode.W)){
				MoveForvard(_xAxis * Acceleration);
			}

			if(!Input.GetKey(KeyCode.W)){
				if(Math.Abs(_rb.velocity.magnitude) > 0.001f){
					_rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, timeToStop);
				}
			}

			if(Input.GetKey(KeyCode.A)){
				Rotate(transform, rotationSpeed);
			}

			if(Input.GetKey(KeyCode.D)){
				Rotate(transform, -rotationSpeed);
			}

			// ClampVelocity();
		}

		void ClampVelocity(){
			var x = Mathf.Clamp(_rb.velocity.x, -maxVelocity, maxVelocity);
			var y = Mathf.Clamp(_rb.velocity.y, -maxVelocity, maxVelocity);

			_rb.velocity = new Vector2(x, y);
		}

		private void MoveForvard(float amount){
			Vector2 force = transform.up * amount;
			_rb.AddForce(force);
		}

		static void Rotate(Transform t, float amount){
			t.Rotate(0, 0, amount);
		}
	}
}