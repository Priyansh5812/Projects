using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform characterpos;
    public float tempini; // Temporary input Variable
    public float xoffset;
    private float RightXoffset = 4.876568f, LeftXOffset = -5.76f;
    public float t1 = 0;
    public float t2 = 0;

    void Start()
    {
        xoffset = 4.876568f;
        tempini = 1;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && (xoffset == RightXoffset || xoffset == LeftXOffset))
        {
            tempini = Input.GetAxisRaw("Horizontal");
        }

        if (tempini == 1)
        {
            t1 += Time.deltaTime;
            xoffset = Mathf.SmoothStep(LeftXOffset, RightXoffset, t1);
            t2 = 0;
        }
        else if (tempini == -1)
        {
            t2 += Time.deltaTime;
            xoffset = Mathf.SmoothStep(RightXoffset, LeftXOffset, t2);
            t1 = 0;
        }

        t1 = Mathf.Clamp(t1, 0f, 1f);
        t2 = Mathf.Clamp(t2, 0f, 1f);

        transform.position = new Vector3(characterpos.position.x + xoffset, characterpos.position.y + 1.676082f, characterpos.position.z + -14.69612f);
    }

}
