using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    public bool isTriggered = false; // Controls whether the trapdoor shall be triggered
    public float rotationSpeed = 360f;

    Vector3 desiredRotation;
    void Start()
    {
        // Used to rotate the trapdoor around a pivot, 75 degrees counterclockwise in z-axis
        desiredRotation = new Vector3(transform.rotation.x, transform.rotation.y, 75); 
    }

    // Update is called once per frame
    void Update()
    {
        // If the trapdoor was triggered and the trapdoor has not been rotated so its opened, rotate the trapdoor towards desiredRotation
        if (isTriggered && transform.rotation.eulerAngles != desiredRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(desiredRotation), rotationSpeed * Time.deltaTime);
        }
        // Else if trapdoor has been untriggered and the trapdoor is still open, rotate the trapdoor towards (0,0,0) to close it
        else if (!isTriggered && transform.rotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), rotationSpeed * Time.deltaTime);
        }
    }
}
