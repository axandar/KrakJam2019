using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
   private SpriteRenderer _spriteRenderer;

   private void Start(){
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }

   public void DisableThisObject(){
      _spriteRenderer.enabled = false;
   }
}
