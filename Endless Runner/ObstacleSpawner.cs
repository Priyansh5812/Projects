using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] shortobs, longobs,collectible;
    private PlayerMovement playerMovementScript;
    void Start()
    {
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        InvokeRepeating("Spawn", 2f, 1.49f);
    }


    void Spawn()
    {   if(playerMovementScript.gameover == false)
        {   transform.position = new Vector3(Random.Range(28f, 33f), transform.position.y, transform.position.z);
            int temp = Random.Range(1, 12);
            if (temp % 2 != 0)
            {
                int t = Random.Range(0, 4);
                Instantiate(shortobs[t], transform.position, shortobs[t].transform.rotation);
                int ch = Random.Range(0, 3);
                if(ch== 0)
                {
                    
                    Instantiate(collectible[1], new Vector3(transform.position.x, 2.8f, transform.position.z), transform.rotation);

                }
                else if(ch ==1)
                {
                    Instantiate(collectible[0], new Vector3(transform.position.x, 2.8f, transform.position.z), transform.rotation);
                }
                

                // collectible is to be instantiated
            }
            else // else if
            {
                int t = Random.Range(0, 2);
                Instantiate(longobs[t], transform.position, shortobs[t].transform.rotation);
                Instantiate(collectible[Random.Range(0,2)], new Vector3(transform.position.x, 4.8f, transform.position.z), transform.rotation);

                // collectible is to be instantiated
            }
        }
        
        
    }
}
