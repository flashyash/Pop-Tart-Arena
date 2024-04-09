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
    private bool jumpButtonDown = false;
    public GameObject platforms;

    private Animator anim;
    //public AudioSource JumpSFX;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
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
        }
        else if (isPlayer2)
        {
            jumpButtonDown = Input.GetButtonDown("p2Jump");
        }
        else if (isPlayer3)
        {
            jumpButtonDown = Input.GetButtonDown("p3Jump");
        }
        else if (isPlayer4)
        {
            jumpButtonDown = Input.GetButtonDown("p4Jump");
        }


        if(jumpButtonDown && canJump && isAlive)
        {
            Jump(Vector2.up);
            anim.Play("jump");
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
        jumpTimes += 1;
        rb.velocity = direction * jumpForce;
        // anim.SetTrigger("Jump");
        // JumpSFX.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;

        // Iterate over all child game objects of the parent
        foreach (Transform child in platforms.transform)
        {
            // Check if the child has a collider component
            Collider2D collider = child.GetComponent<Collider2D>();
            if (collider != null)
            {
                // Set the collider to be a trigger
                collider.isTrigger = true;
            }
        }

        // Start coroutine to re-enable collider after a short delay
        StartCoroutine(EnableColliderAfterDelay());
    }

    private IEnumerator EnableColliderAfterDelay()
    {
        // Wait for a short duration (adjust as needed)
        yield return new WaitForSeconds(0.2f);

        // Re-enable collider's trigger
        // Iterate over all child game objects of the parent
        foreach (Transform child in platforms.transform)
        {
            // Check if the child has a collider component
            Collider2D collider = child.GetComponent<Collider2D>();
            if (collider != null)
            {
                // Set the collider to be a trigger
                collider.isTrigger = false;
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