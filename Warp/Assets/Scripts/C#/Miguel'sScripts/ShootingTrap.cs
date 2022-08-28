using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [Header("GOs")]
    public GameObject arrow;
    public GameObject shootPoint;
    [Header("Floats")]
    public float countdown = 3f;
    public float rotationRate = 0f; // If this trap is meant to rotate, change this value in inspector (in degrees/s)
    float currentCountdown;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCountdown = countdown;
    }
    // Update is called once per frame
    void Update()
    {
        // Spin this trap if it is meant to spin
        transform.Rotate(0, 0, rotationRate * Time.deltaTime);

        // Countdown before the next arrow is created
        Fire();
    }

    void Fire()
    {
        currentCountdown -= Time.deltaTime;

        if (currentCountdown <= 0)
        {
            Instantiate(arrow, shootPoint.transform.position, shootPoint.transform.rotation);
            currentCountdown = countdown;
        }
    }
}
