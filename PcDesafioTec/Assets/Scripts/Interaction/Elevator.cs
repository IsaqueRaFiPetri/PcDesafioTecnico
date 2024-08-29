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

    private void OnTriggerStay(Collider other)
    {
        gravityScale *= -1;

        Vector3 gravity = gravityScale * Physics.gravity;
        elevatorRb.AddForce(gravity, ForceMode.Acceleration);

    }
    private void OnTriggerExit()
    {
        gravityScale *= -1;

        Vector3 gravity = gravityScale * Physics.gravity;
        elevatorRb.AddForce(gravity, ForceMode.Acceleration);
    }
}
