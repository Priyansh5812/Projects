using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public Transform orientation;
    private float Horizontalinput;
    private float Verticalinput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    public bool isgrounded = false;
    public GameObject checksphere;
    public LayerMask GroundMask;
    public float SpeedLimit;
    public float jumpforce;
    public float jumpCooldown;
    public float AirMultiplier;
    public bool canjump;
    private KeyCode Jumpkey = KeyCode.Space;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canjump = true;
    }

    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics.CheckSphere(checksphere.transform.position, 0.25f, GroundMask);
        MyInput();
        if(isgrounded)
        {
            rb.drag = 15.6f;
        }
        else
        {
            rb.drag = 0f;
        }
        if(rb.velocity.magnitude > SpeedLimit)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, SpeedLimit);
        }
        if(isgrounded && Input.GetKey(KeyCode.LeftShift))
        {
            rb.drag = 9.5f;
        }
        
    }

    void FixedUpdate()
    {
        Move();
        transform.rotation = orientation.rotation;
    }

    

    private void MyInput()
    {
        Horizontalinput = Input.GetAxisRaw("Horizontal");
        Verticalinput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(Jumpkey) && isgrounded && canjump)
        {   
            Jump();
            Invoke("ResetJump", jumpCooldown);
        }
    }

    private void Move()
    {
        //calculate Movement Direction
        moveDirection = orientation.forward * Verticalinput + orientation.right * Horizontalinput;
        if(isgrounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);
        }
        else if(!isgrounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * 10f * AirMultiplier, ForceMode.Force);
        }
        

    }
    private void Jump()
    {
        canjump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //Reseting the velocity.
        rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canjump = true;
    }
}
