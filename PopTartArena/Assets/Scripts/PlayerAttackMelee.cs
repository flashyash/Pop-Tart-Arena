using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
      public bool isAlive = true;
      public Transform attackPt;
      private GameObject gameHandler;
      public float attackRange = 4f;
      public float attackRate = 1f;
      private float nextAttackTime = 0f;
      private int playerNum;
      public int attackDamage = 15;
      public LayerMask enemyLayers;
      public AudioSource shootSound;
      public bool isPlayer1 = false;
      public bool isPlayer2 = false;
      public bool isPlayer3 = false;
      public bool isPlayer4 = false;
      private Animator anim;
      void Start()
      {
            anim = gameObject.GetComponent<Animator>();
            if (GameObject.FindWithTag("GameHandler")!= null){
                  gameHandler = GameObject.FindWithTag("GameHandler");
            }
      }

      void Update()
      {
            if (Time.time >= nextAttackTime && isAlive)
            {
                  if (isPlayer1)
                  {
                        if (Input.GetAxis("p1Melee") > 0)
                        {
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                  }
                  else if (isPlayer2)
                  {
                        if (Input.GetAxis("p2Melee") > 0)
                        {
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                  }
                  else if (isPlayer3)
                  {
                        if (Input.GetAxis("p3Melee") > 0)
                        {
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                  }
                  else
                  {
                        if (Input.GetAxis("p4Melee") > 0)
                        {
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                  }
            }
      }

      void Attack()
      {
            //AudioSource source = gameObject.AddComponent<AudioSource>();
            //source.PlayOneShot(shootSound);
            shootSound.Play();
            //animator.SetTrigger ("Melee");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                  playerNum = int.Parse(enemy.tag.Substring(6, 1)); // read the number of the player tag
                  if (enemy.tag != gameObject.tag) // don't damage self
                  {
                        Debug.Log("We hit melee " + enemy.name);
                        gameHandler.GetComponent<GameHandler>().playerGetHit(attackDamage, playerNum);
                  }
            }

            //NOTE: to help see the attack sphere in editor:
            // void OnDrawGizmosSelected()
            // {
            //       if (attackPt == null) { return; }
            //       Gizmos.DrawWireSphere(attackPt.position, attackRange);
            // }
      }
}