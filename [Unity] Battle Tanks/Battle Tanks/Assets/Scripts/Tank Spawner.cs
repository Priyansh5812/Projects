using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tankPrefab;
    void Start()
    {
        Instantiate(tankPrefab, this.transform.position, transform.rotation);
        //ins
    }


}
