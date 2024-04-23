using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

      public int damage = 1;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 4.0f;
      public float SelfDestructVFX = 0.5f;
      public SpriteRenderer projectileArt;
      public GameObject handler;
      private int playerNum;
      public int belongsToPlayerNum = 0;

      void Start()
      {
            handler = GameObject.FindWithTag("GameHandler");
            projectileArt = GetComponentInChildren<SpriteRenderer>();
            StartCoroutine(selfDestruct());
      }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      public void OnCollisionEnter2D(Collision2D other)
      {
            if (other.gameObject.layer == 6) //check if the other gameObject is in the enemies layer (6)
            {
                  playerNum = int.Parse(other.gameObject.tag.Substring(6, 1)); // read the number of the player tag
                  if(playerNum != belongsToPlayerNum)
                  {
                        Debug.Log("Hit " + other.gameObject.tag);
                        handler.GetComponent<GameHandler>().playerGetHit(damage, playerNum);
                        Destroy(gameObject); // change to the function below if we have a hit animation
                  }

            }
            else Destroy(gameObject);

      }

      IEnumerator selfDestructHit(GameObject VFX)
      {
            yield return new WaitForSeconds(SelfDestructVFX);
            Destroy(VFX);
            Destroy(gameObject);
      }

      IEnumerator selfDestruct()
      {
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy(gameObject);
      }
}