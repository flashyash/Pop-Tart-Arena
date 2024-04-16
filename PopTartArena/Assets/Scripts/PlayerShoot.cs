using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour{

      public GameObject gameHandler;      
      public Animator animator;
      public Transform firePoint;
      public GameObject projectilePrefab;
      public float projectileSpeed = 10f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public AudioClip shootSound;
      private int playerNum;
      public int attackDamage = 40;
      public LayerMask enemyLayers;


      void Start(){
           animator = gameObject.GetComponent<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                 if (gameObject.tag == "player1") {
                        if (Input.GetAxis("p1Shoot") > 0){
                              animator.Play("shoot");
                              playerFire();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 else if (gameObject.tag == "player2") {
                        if (Input.GetAxis("p2Shoot") > 0){
                              playerFire();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 else if (gameObject.tag == "player3") {
                        if (Input.GetAxis("p3Shoot") > 0){
                              playerFire();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 else if(gameObject.tag == "player4") {
                        if (Input.GetAxis("p4Shoot") > 0){
                              playerFire();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                 }
                 
            }
      }

      void playerFire(){
              
            
            Vector2 fwd = (firePoint.position - this.transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(shootSound);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, 4f, enemyLayers);            
            
            foreach (Collider2D enemy in hitEnemies)
            {
                  playerNum = int.Parse(enemy.name.Substring(6,1));
                  if (enemy.tag != gameObject.tag)
                  {
                        Debug.Log("We hit shoot " + enemy.name);
                        gameHandler.GetComponent<GameHandler>().playerGetHit(attackDamage, playerNum);
                  }
            }
      }
}
