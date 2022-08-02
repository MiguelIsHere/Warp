using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrap : MonoBehaviour
{

    public GameObject boulder;
    public Transform startPoint;

    public float minimumCountdown;
    public float maximumCountdown; // Determines range of possible cooldown
    float countdown;
    float currentCountdown;

    void Start()
    {
        GetRandomCountdown(); // Get a random value within this range to act as current countdown
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0, 6.0f, 0);
        currentCountdown -= Time.deltaTime;

        if (currentCountdown <= 0)
        {
            // Spawn boulder and reset countdown
            
            Instantiate(boulder, startPoint.transform.position + offset, startPoint.transform.rotation);
            GetRandomCountdown();
        }
    }

    void GetRandomCountdown() // Get a random value within the range of min and maximumCountdown to get the countdown value
    {
        countdown = Random.Range(minimumCountdown, maximumCountdown);
        currentCountdown = countdown;
    }
}
