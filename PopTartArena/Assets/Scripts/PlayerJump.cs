using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    public float jumpForce = 10f;
    public Transform feet;
    public LayerMask groundLayer;
    public bool canJump = false;
    public int jumpTimes = 0;
    public bool isAlive = true;
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;
    public bool isPlayer3 = false;
    public bool isPlayer4 = false;
    private bool jumpButtonDown = false;
    private bool dropButtonDown = false;
    private GameObject platforms;

    private Animator anim;
    //public AudioSource JumpSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();
        platforms = GameObject.FindWithTag("Platforms");
    }

    void Update()
    {

        if (IsGrounded() || (jumpTimes <= 1))
        {
            canJump = true;
        }
        else if (jumpTimes > 1)
        {
            // else { // for single jump only
            canJump = false;
        }

        // if ((Input.GetButtonDown("Jump")) && canJump && isAlive)
        // {
        //     Jump(Vector2.up);
        // } 

        if (isPlayer1)
        {
            jumpButtonDown = Input.GetButtonDown("p1Jump");
            dropButtonDown = Input.GetAxis("p1Drop") < -0.9f;
        }
        else if (isPlayer2)
        {
            jumpButtonDown = Input.GetButtonDown("p2Jump");
            dropButtonDown = Input.GetAxis("p2Drop") < -0.9f;
        }
        else if (isPlayer3)
        {
            jumpButtonDown = Input.GetButtonDown("p3Jump");
            dropButtonDown = Input.GetAxis("p3Drop") < -0.9f;
        }
        else if (isPlayer4)
        {
            jumpButtonDown = Input.GetButtonDown("p4Jump");
            dropButtonDown = Input.GetAxis("p4Drop") < -0.9f;
        }


        if (jumpButtonDown && canJump && isAlive)
        {
            Jump(Vector2.up);
            anim.Play("jump");
        }
        else if (dropButtonDown && canJump && isAlive)
        {
            Jump(Vector2.down);
        }

        /* jump down
        
        if (isPlayer1 && Input.GetAxis("p1Vert") < 0 && canJump && isAlive) 
        {
            Jump(Vector2.down);
        }
        if (isPlayer2 && Input.GetAxis("p2Vert") < 0 && canJump && isAlive) 
        {
            Jump(Vector2.down);
        }
        if (isPlayer3 && Input.GetAxis("p3Vert") < 0 && canJump && isAlive) 
        {
            Jump(Vector2.down);
        }
        if (isPlayer4 && Input.GetAxis("p4Vert") < 0 && canJump && isAlive) 
        {
            Jump(Vector2.down);
        }
        
        */
    }

    public void Jump(Vector2 direction)
    {
        if (direction.y > 0)
        {
            jumpTimes += 1;
        }
        else if (direction.y < 0)
        {
            jumpTimes = 2;
            // disable collisions with platforms
            foreach (Transform child in platforms.transform)
            {
                // Check if the child has a collider component
                Collider2D collider = child.GetComponent<Collider2D>();
                if (collider != null)
                {
                    Physics2D.IgnoreCollision(collider, playerCollider, true);
                }
            }
            StartCoroutine(EnableCollisionAfterDelay());
        }

        rb.velocity = direction * jumpForce;
        // anim.SetTrigger("Jump");
        // JumpSFX.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;

        // disable then Start coroutine to re-enable collider after a short delay
    }

    private IEnumerator EnableCollisionAfterDelay()
    {
        // Wait for a short duration (adjust as needed)
        yield return new WaitForSeconds(0.2f);
        // reset collision detection
        foreach (Transform child in platforms.transform)
        {
            // Check if the child has a collider component
            Collider2D collider = child.GetComponent<Collider2D>();
            if (collider != null)
            {
                Physics2D.IgnoreCollision(collider, playerCollider, false);
            }
        }
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
        if (groundCheck != null)
        {
            //Debug.Log("I am touching ground!");
            jumpTimes = 0;
            return true;
        }
        return false;
    }
}