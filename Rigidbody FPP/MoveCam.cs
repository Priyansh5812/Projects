using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraholder;

    // Update is called once per frame
    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }
    void Update()
    {
        transform.position = cameraholder.position;

    }
}
