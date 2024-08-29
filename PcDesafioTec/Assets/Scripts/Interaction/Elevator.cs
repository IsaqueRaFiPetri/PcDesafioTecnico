using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Rigidbody elevatorRb;
    public float gravityScale = 1.0f; // Adjust this to change the gravity scale

    private void Start()
    {
        elevatorRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        elevatorRb.velocity = new Vector3(0, gravityScale, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gravityScale *= -1;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gravityScale *= -1;
        }
    }
}