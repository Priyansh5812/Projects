using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator playerAnim;
    public ParticleSystem explosion , dirtsplatter ,coincollect;
    public float jumpvel;
    public float gravityModifier;
    public bool isgrounded= true;
    public bool gameover = false;
    public AudioClip jumpS, crashS, coinaudio, DiamondAudio;
    private AudioSource playeraudio;
    public int c = 0 , cc = 0;
    public static int Highscore;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playeraudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space) && isgrounded && !gameover)
        {
            dirtsplatter.Stop();
            rb.AddForce(Vector3.up * jumpvel, ForceMode.Impulse);
            isgrounded = false;
            playerAnim.SetTrigger("Jump_trig");
            playeraudio.PlayOneShot(jumpS, 1.5f);
        }*/

        if(Input.GetKeyDown(KeyCode.Space) && !gameover && c<=1)
        {
            dirtsplatter.Stop();
            if(c==0)
            {
                rb.AddForce(Vector3.up * jumpvel, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                playeraudio.PlayOneShot(jumpS, 1.5f);
            }
            else if(c!=0)
            {
                rb.AddForce(Vector3.up * jumpvel/2f, ForceMode.Impulse);
                playeraudio.PlayOneShot(jumpS, 1.5f);
                //Debug.Log("Jumped twice");
            }
            
            isgrounded = false;
            c++;
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.CompareTag("Coin"))
        {
            playeraudio.PlayOneShot(coinaudio, 1f);
            coincollect.Play();
            //Debug.Log("Coin Collected");
            Destroy(other.gameObject);
            cc++;
        }
        else if(other.gameObject.CompareTag("Note"))
        {
            playeraudio.PlayOneShot(DiamondAudio, 1f);
            coincollect.Play();
            //Debug.Log("Coin Collected");
            Destroy(other.gameObject);
            cc++;
        }
        else if(other.gameObject.CompareTag("Diamond"))
        {
            playeraudio.PlayOneShot(DiamondAudio, 1f);
            coincollect.Play();
            //Debug.Log("Coin Collected");
            Destroy(other.gameObject);
            cc++;
        }
             
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("ground"))
        {
            playerAnim.StopPlayback();
            isgrounded = true;
            if(!gameover)
            {
                dirtsplatter.Play();
            }  
            c = 0;
        }
        else if(collision.gameObject.CompareTag("obstacle"))
        {
            dirtsplatter.Stop(); 
            gameover = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int" , 1);
            explosion.Play();
            playeraudio.PlayOneShot(crashS, 1.0f);
            if(Highscore <= cc)
            {
                Highscore = cc;
            }
            
          
        }
    }


}
