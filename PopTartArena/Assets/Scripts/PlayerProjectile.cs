using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 1;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 4.0f;
      public float SelfDestructVFX = 0.5f;
      public SpriteRenderer projectileArt;

      void Start(){
           projectileArt = GetComponentInChildren<SpriteRenderer>();
           selfDestruct();
      }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnTriggerEnter2D(Collider2D other){
            Destroy(gameObject);
      }

      IEnumerator selfDestructHit(GameObject VFX){
            yield return new WaitForSeconds(SelfDestructVFX);
            Destroy (VFX);
            Destroy (gameObject);
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy (gameObject);
      }
}