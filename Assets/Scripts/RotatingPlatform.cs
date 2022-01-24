using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    
    public Vector3 rotation;
    public Vector3 force;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
