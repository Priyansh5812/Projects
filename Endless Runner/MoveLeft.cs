using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    public static float speed = 15;
    public PlayerMovement PlayerMovementScript;
    public float timef = 6f;
    public float timei = 1f;
    void Start()
    {
        PlayerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*timef += 1 * Time.deltaTime;
        timei = Mathf.RoundToInt(timef);

        if (timei % 5 == 0)
        {
            speed += 2 * Time.deltaTime;
        }

        if (PlayerMovementScript.gameover == false)
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }*/

        timef += 1 * Time.deltaTime;
        timei = Mathf.RoundToInt(timef);
        if(timei%5 == 0)
        {   speed += 0.5f * Time.deltaTime;
        }
        if (PlayerMovementScript.gameover == false)
        {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        




    }
}
