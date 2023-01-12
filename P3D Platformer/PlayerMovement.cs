using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Extras")]
    private Rigidbody rb;
    public float horizontalInput;
    public Vector3 moveDirection;
    public float crouchYscale;
    public float startcrouchYscale;
    public MovementState state;
    public bool iscrouching;
    public float playerheight;

    [Header("Speeds")]
    public float speedlimit;
    private float movespeed;
    public float walkspeed;
    public float RotationSpeed;
    public float CrouchSpeed;

    [Header("GroundChecks")]
    public bool isgrounded;
    public GameObject checksphere;
    public LayerMask GroundMask, SlopeMask;
    public float grounddrag;
    public bool sloped;

    [Header("JumpVars")]
    public float jumpForce , SlopeJumpForce;
    public float JumpCooldown;
    public float airMultiplier;
    public bool readyTojump;


    [Header("KeyBinds")]
    private KeyCode JumpKey = KeyCode.W;
  
    private KeyCode CrouchKey = KeyCode.S;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit SlopeHit;
    public float angle;
    
    public enum MovementState
    {
        walking, aerial, crouch, idle, onslope
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyTojump = true;
        startcrouchYscale = transform.localScale.y;
        Physics.gravity *= 2f;
        iscrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        playerheight = transform.localScale.y;
        isgrounded = Physics.CheckSphere(checksphere.transform.position, 0.5f, GroundMask);
        if (isgrounded)
        {
            rb.drag = grounddrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (OnSlope()) // if on slope set sloped = true ...
        {
            sloped = true;
        }
        else // otherwise set sloped to false
        {
            sloped = false;
        }
        if (sloped) // while moving down from steep slope player will stand automatically
        {
            iscrouching = false;
        }
        
        SpeedControl();
        RotateOnlook();
        StateHandler();


    }

    private void FixedUpdate()
    {
        MyInput();
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(JumpKey) && isgrounded && readyTojump && !iscrouching)
        {
            readyTojump = false;
            Jump();
            Invoke(nameof(ResetJump), JumpCooldown);
        }

        if (iscrouching && state != MovementState.aerial)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            // problem will arise when the player will float in the air when got crouched sice scale will be decremented from top and bottom since origin of the body lies in the center
            // Therefore we will apply downward force to get body quickly on the ground
            rb.AddForce(Vector3.down * 15f, ForceMode.Force);
            
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startcrouchYscale, transform.localScale.z);
        }

        if (transform.localScale.y == startcrouchYscale/2 && Input.GetKeyDown(JumpKey)) // just in case if someone presses jump key to exit from crouch
        {
            iscrouching = false;
        }


    }

    void MovePlayer()
    {
        moveDirection = new Vector3(horizontalInput, 0f, 0f);

        if (OnSlope())
        {
            if (horizontalInput == 0)
            {
                rb.AddForce(Vector3.down * 100f, ForceMode.Force);
                

            }
            else 
            {

                rb.AddForce(GetSlopeMoveDirection() * movespeed * 2.3f, ForceMode.Force);
                
            }  
        }

        if (isgrounded) // Movement of player on ground
        {
            rb.AddForce(moveDirection * movespeed * 10f, ForceMode.Force);  
        }
        else if (!isgrounded) // Movement of player in mid air
        {
            rb.AddForce(moveDirection * movespeed * 10f * airMultiplier, ForceMode.Force);
        }



    }

    private void SpeedControl()
    {
        Vector3 dummy_vel = new Vector3(rb.velocity.x, 0f, 0f); // calculating velocity
        if (dummy_vel.magnitude > speedlimit && isgrounded)  // if calculated velocity is greater than the speedlimit and body is grounded
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedlimit); // then clamp the velocity of the body
        }

    }

    private void RotateOnlook()
    {
        if (moveDirection != Vector3.zero)
        { 
            /*
            // For instantenous Rotation
           transform.forward = moveDirection;
            */

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        // Resetting velocity in y axis first
        rb.velocity = new Vector3(rb.velocity.x / 2, 0f, 0f);
        // then jumping
        //if (OnSlope() && angle >= 40) // for slope
        //{       
        //        rb.AddForce(SlopeHit.normal * jumpForce / 1.18f, ForceMode.Impulse);
        //}
            
        //else
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump() // will be invoked when jump function executed after a certain period of time
    {
        readyTojump = true;
    }

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down * 5f, out SlopeHit, transform.localScale.y * 0.5f + 0.9f))
        {
            sloped = true;
            angle = Vector3.Angle(Vector3.up, SlopeHit.normal);
            return (angle <= maxSlopeAngle && angle != 0);
        }
        else 
        {
            sloped = false;
        }
        return false;
        
    }   

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, SlopeHit.normal).normalized;
    }

    

    private void StateHandler()
    {   // Deciding States and appliying corrosponding movespeed according to the player's state

        // for being idle
        if (isgrounded && horizontalInput == 0 && !iscrouching && !Input.GetKeyDown(CrouchKey) && !sloped) // if player is grounded , no input in either direction , not crouching and as for changing state from idle state on to crouching state,No crouch key pressed 
        {
            state = MovementState.idle;
        }



        // for crouch
        else if (Input.GetKeyDown(CrouchKey) && state != MovementState.aerial && !sloped) // if crouch key is pressed down and state is not aerial
        {   // algorithm of toggling crouch
            if (iscrouching)
                iscrouching = false;
            else
                iscrouching = true;
            
            state = MovementState.crouch; // set state
            movespeed = CrouchSpeed; // set speed
            speedlimit = 7.5f; // set speed limit
        }


        // for walking
        else if (isgrounded && !iscrouching && !sloped) // if player is grounded and not crouched
        {
            state = MovementState.walking; // set state
            movespeed = walkspeed; // set speed
            speedlimit = 15.5f; // set speed limit
        }



        // for aerial state
        else if (!iscrouching && !isgrounded && !sloped)
        {
            state = MovementState.aerial; // set state
            // speed is not being set because it is controlled by Jump() Function
            speedlimit = 8.5f; // set speed limit

        }

        else if(!iscrouching && sloped)
        {   
            state = MovementState.onslope;
            movespeed = walkspeed; // set speed
            speedlimit = 9f; // set speed limit
        }



    }
}