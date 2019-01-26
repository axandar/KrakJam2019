using UnityEngine;

namespace Code{
    public class CameraController : MonoBehaviour{
        [SerializeField] private Transform jakisTamTransform;

        private void FixedUpdate(){
            var position = transform.position;
            jakisTamTransform.position = new Vector3(position.x , position.y, -10f);
        }
    }
}