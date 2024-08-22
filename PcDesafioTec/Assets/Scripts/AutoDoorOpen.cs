using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AutoDoorOpen : MonoBehaviour
{
    public Animator animDoor;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animDoor.SetBool("character_nearby", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        animDoor.SetBool("character_nearby", false);
    }

}
