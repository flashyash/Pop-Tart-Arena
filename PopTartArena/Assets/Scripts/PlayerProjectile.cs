using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 1;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 4.0f;
      public float SelfDestructVFX = 0.5f;
      public SpriteRenderer projectileArt;
      public GameObject handler;
      void Start(){
           
           projectileArt = GetComponentInChildren<SpriteRenderer>();
           selfDestruct();
      }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnColliderEnter2D(Collider2D other){
            handler.GetComponent<GameHandler>().playerGetHit(damage, findPlayer(other.tag));
            Destroy(gameObject);
      }

      IEnumerator selfDestructHit(GameObject VFX){
            yield return new WaitForSeconds(SelfDestructVFX);
            Destroy (VFX);
            Destroy (gameObject);
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy(gameObject);
      }

      int findPlayer(string tag) {
            if(tag == "player1"){
                  return 1;
            }
            else if(tag == "player2"){
                  return 2;
            }
            else if(tag == "player3"){
                  return 3;
            }
            else return 4;
      }
}