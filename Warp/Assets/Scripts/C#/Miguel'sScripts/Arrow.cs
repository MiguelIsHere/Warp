using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 3.5f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 7f); // After spawning, destroy this projectile after 7s

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            player.GetComponent<PlayerCube>().isDead = true;
            Destroy(gameObject);
        }
    }
}
