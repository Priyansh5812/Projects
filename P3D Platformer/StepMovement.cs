using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepMovement : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject stepRayUpper;
    public GameObject stepRayLower;


    [Header("floats")]
    public PlayerMovement PlayerMovementScript;
    public bool onSlope;
    private Rigidbody rb;
    private float t;
    public bool canstep;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementScript = GameObject.Find("Capsule").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        canstep = false;

    }

    // Update is called once per frame
    private void Update()
    {

        if (PlayerMovementScript.sloped == false && PlayerMovementScript.state == PlayerMovement.MovementState.walking)
        {
            canstep = true;
        }
        else
        {
            canstep = false;
        }


    }
    private void FixedUpdate()
    {
        if (canstep)
            StepClimb();
    }

    private void StepClimb()
    {
        RaycastHit hitLower;

        if (Physics.Raycast(stepRayLower.transform.position, stepRayLower.transform.forward, out hitLower, 0.25f))
        {
            RaycastHit hitUpper;

            if (!Physics.Raycast(stepRayUpper.transform.position, stepRayUpper.transform.forward, out hitUpper, 0.5f))
            {

                rb.AddForce(Vector3.up * 2.85f, ForceMode.Impulse);
                Debug.Log("Yes");


            }
        }

    }
}