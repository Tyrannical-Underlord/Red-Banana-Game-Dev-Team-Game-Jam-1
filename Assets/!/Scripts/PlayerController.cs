using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Init variables we want to be able to access from other scripts
    public float speed;
    public float jumpForce;
    public int extraJumps;
    


    //Init Variables that should only be accessed within this script
    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight = true;

    //Ground check variables. The public variables can be accessed outside this scripts and can be changed in the inspector
    //To alter the location and checking size of the ground check circle
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int jumps;




    // Start is called before the first frame update
    void Start()
    {
        //Gets the rigid body component
        rb = GetComponent<Rigidbody2D>();

        //Set jumps to starting extrajumps + 1
        jumps = extraJumps + 1;
    }

    private void Update()
    {
        //If the player hits the jumpbutton and has jumps left, add upwards velocity
        if(Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            //add upwards velocity
            rb.velocity = Vector2.up * jumpForce;

            //subtract a jump
            jumps--;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //Gets the horizontal axis player input and multiplies the players velocity by their direction and speed
        moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);

        //Sprite flipper according to velocity direction (Means we only need animations in one direction so the artists' job will be easier)
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    //Flips the sprite by scaling it to the opposite local x scale
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
