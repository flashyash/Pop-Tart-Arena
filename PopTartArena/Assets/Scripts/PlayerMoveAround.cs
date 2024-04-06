using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MultiPlayerMoveAround : MonoBehaviour
{
    // public float minX, maxX, minY, maxY; // for setting boundaries 

    //public Animator anim;
    //public AudioSource WalkSFX;
    public Rigidbody2D rb2D;
    private bool FaceRight = false; // determine which way player is facing.
    public static float runSpeed = 10f;
    public float startSpeed = 10f;
    public bool isAlive = true;
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;
    public bool isPlayer3 = false;
    public bool isPlayer4 = false;

    private Vector3 hvMove;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
        if (isPlayer1)
        {
            hvMove = new Vector3(Input.GetAxis("p1Horiz"), 0.0f, 0.0f);
        }
        else if (isPlayer2)
        {
            hvMove = new Vector3(Input.GetAxis("p2Horiz"), 0.0f, 0.0f);
        }
        else if (isPlayer3)
        {
            hvMove = new Vector3(Input.GetAxis("p3Horiz"), 0.0f, 0.0f);
        }
        else if (isPlayer4)
        {
            hvMove = new Vector3(Input.GetAxis("p4Horiz"), 0.0f, 0.0f);
        }

        if (isAlive == true)
        {
            // float horizontalInput = Input.GetAxis("Horizontal");
            // float verticalInput = Input.GetAxis("Vertical");

            // Vector2 movement = new Vector2(horizontalInput, verticalInput) * runSpeed * Time.deltaTime;
            // Vector2 newPosition = rb2D.position + movement;

            // // Clamp the player's position within the boundaries
            // newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            // newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            // rb2D.MovePosition(newPosition);

            transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

            if ((Input.GetAxis("Horizontal") != 0))
            {
                //     anim.SetBool ("Walk", true);
                //     if (!WalkSFX.isPlaying){
                //           WalkSFX.Play();
                //     }
            }
            else
            {
                //     anim.SetBool ("Walk", false);
                //     WalkSFX.Stop();
            }

            // Turning. Reverse if input is moving the Player right and Player faces left.
            if ((hvMove.x < 0 && !FaceRight) || (hvMove.x > 0 && FaceRight))
            {
                playerTurn();
            }
        }
    }

    private void playerTurn()
    {
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}