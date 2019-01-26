using UnityEngine;

namespace Code{
   public class AnimationAutoDestroy : MonoBehaviour{
      public void DestroyThisObject(){
         gameObject.SetActive(false);
      }
   }
}
