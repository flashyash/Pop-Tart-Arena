using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    //public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayer;
    public bool canJump = false;
    public int jumpTimes = 0;
    public bool isAlive = true;
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;
    public bool isPlayer3 = false;
    public bool isPlayer4 = false;
    //public AudioSource JumpSFX;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (isPlayer1 == true){
        if ((IsGrounded()) || (jumpTimes <= 1))
        {
            // if ((IsGrounded()) && (jumpTimes <= 1)){ // for single jump only
            canJump = true;
        }
        else if (jumpTimes > 1)
        {
            // else { // for single jump only
            canJump = false;
        }

        if ((Input.GetButtonDown("Jump")) && canJump && isAlive)
        {
            Jump();
        }
    }

    public void Jump()
    {
        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;
        // anim.SetTrigger("Jump");
        // JumpSFX.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
        if ((groundCheck != null))
        {
            //Debug.Log("I am touching ground!");
            jumpTimes = 0;
            return true;
        }
        return false;
    }
}