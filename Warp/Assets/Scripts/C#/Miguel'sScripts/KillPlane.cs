using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            other.GetComponent<PlayerCube>().isDead = true; // Kill player upon colliding with kill plane
        }
    }
}
