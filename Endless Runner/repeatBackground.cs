using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatBackground : MonoBehaviour
{
    private Vector3 startpos;
    private float repeatrate;
    void Start()
    {
        startpos = transform.position;
        repeatrate = GetComponent <BoxCollider>().size.x/2; // half of length of background in x axis
    }

    
    void Update()
    {
        /*if(transform.position.x <= -11.32f)
         {
             transform.position = new Vector3(45f, transform.position.y, transform.position.z);
         }
        */


        if (transform.position.x < startpos.x - repeatrate)  // if current position is less than half of the starting position of x
        {
            transform.position = startpos;
        }
    }
}
