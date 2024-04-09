using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      //public Animator animator;
      public Transform attackPt;
      public float attackRange = 0.1f;
      public float attackRate = 1f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      public LayerMask enemyLayers;
      public AudioClip shootSound;
      public bool isPlayer1 = false;
      public bool isPlayer2 = false;
      public bool isPlayer3 = false;
      public bool isPlayer4 = false;
      private Animator anim;
      void Start(){
           anim = gameObject.GetComponent<Animator>();
      }

      void Update(){

           if (Time.time >= nextAttackTime){
                 if(isPlayer1) {
                        if (Input.GetAxis("p1Melee") > 0){
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 else if(isPlayer2) {
                        if (Input.GetAxis("p2Melee") > 0){
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 else if(isPlayer3) {
                        if (Input.GetAxis("p3Melee") > 0){
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 else {
                        if (Input.GetAxis("p4Melee") > 0){
                              anim.Play("attack");
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
            }
      }

      void Attack(){
            //animator.SetTrigger ("Melee");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                  string player = determinePlayer();
                  if(enemy.tag != player) {
                  Debug.Log("We hit " + enemy.name);
                  enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
                      // Add an AudioSource component to the GameObject
                    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                    // Play the sound effect
                    audioSource.PlayOneShot(shootSound);
                  }
            }
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmosSelected(){
           if (attackPt == null) {return;}
            Gizmos.DrawWireSphere(attackPt.position, attackRange);
      }
      string determinePlayer() {
            if(isPlayer1) {
                  return "player1";
            }
            else if(isPlayer2) {
                  return "player2";
            }
            else if(isPlayer3) {
                  return "player3";
            }
            else if (isPlayer4) {
                  return "player4";
            }
            else return "null";
      }
}