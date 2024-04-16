using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour{

      public Animator animator;
      public Transform firePoint;
      public GameObject projectilePrefab;
      public float projectileSpeed = 10f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public AudioClip shootSound;


      void Start(){
           animator = gameObject.GetComponent<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                 if (gameObject.tag == "player1") {
                        if (Input.GetAxis("p1Shoot") > 0){
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
            if(gameObject.tag == "player1"){
                  animator.Play("shoot");
            }
            
            Vector2 fwd = (firePoint.position - this.transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(shootSound);
      }
}
