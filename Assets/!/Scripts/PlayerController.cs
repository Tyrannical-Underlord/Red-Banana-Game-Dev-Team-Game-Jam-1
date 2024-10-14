using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Init variables we want to be able to access from other scripts
    //Player horizontal movement speed
    public float speed;

    //Jump variables
    public float jumpForce;
    public int extraJumps;

    //Dash variables
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    


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

    //Dashing private variables
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private SpriteTrailRenderer str;




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

        //If the player hits the dash button and can dash, dash the player in the direction they are facing
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the player is dashing, don't do anything this frame like move or jump
        if(isDashing)
        {
            return;
        }


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //Gets the horizontal axis player input and multiplies the players velocity by their direction and speed
        moveInput = Input.GetAxisRaw("Horizontal");
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //str.enabled = true;
        yield return new WaitForSeconds(dashingTime);
        //str.enabled = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
