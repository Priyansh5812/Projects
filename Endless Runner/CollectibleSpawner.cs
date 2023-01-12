using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject [] collectible;
    private Transform obs;
    private PlayerMovement PlayerMovementScript;
    // Update is called once per frame
    void Start()
    {
        PlayerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        obs = GameObject.Find("SpawnManager").GetComponent<Transform>();
        InvokeRepeating("SpawnObstacle", 2f, 1.49f);
        InvokeRepeating("SpecialSpawn", 12f, 12f);


    }

    public void SpawnObstacle()
    {
        if(PlayerMovementScript.gameover == false)
        {
            transform.position = new Vector3(Random.Range(29f, 35f), transform.position.y, transform.position.z);
            if (Vector3.Distance(transform.position, obs.position) < 3.5f)
            {   if(transform.position.x > obs.position.x)
                {
                    transform.position = new Vector3(transform.position.x + 4f, transform.position.y, transform.position.z);
                    obs.position = new Vector3(obs.position.x - 4f, obs.position.y, obs.position.z);
                }
                else
                {
                    obs.position = new Vector3(obs.position.x + 4f, obs.position.y, obs.position.z);
                    transform.position = new Vector3(transform.position.x - 4f, transform.position.y, transform.position.z);
                }

            }
            int temp = Random.Range(1, 11);
            if(temp == 7)
            {
                Instantiate(collectible[1], new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
            }

            else
            {
                Instantiate(collectible[0], new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
            }
           
        }
        
        
    }

    public void SpecialSpawn()
    {
        if(PlayerMovementScript.gameover == false)
        {
            Instantiate(collectible[2], new Vector3(transform.position.x, 3f, transform.position.z), transform.rotation);
        }
    }
}
