using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
   public void DestroyThisObject(){
      Destroy(gameObject);
   }
}
