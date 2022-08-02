using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 5f;
    public string target;
    public string markName; // Use this to differentiate between the two player marks
    public GameObject markObject;
    Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, 7f); // Destroy this projectile after 10s
        rb = GetComponent<Rigidbody>();
        markObject = GameObject.Find(markName);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet by translating it to the right in local space
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if this projectile hit a player
        {
            if (other.name.StartsWith(target)) // Check if this projectile hit the enemy player
            {
                StartCoroutine(Teleport(other.gameObject));
            }
        }
    }

    IEnumerator Teleport(GameObject player)
    {
        player.GetComponent<PlayerCube>().isDisabled = true;
        yield return new WaitForSeconds(0.01f); // Need a small delay, otherwise teleportion won't work as movement is not physics-based AKA rb.velocity
        player.transform.position = markObject.transform.position;
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Teleporting " + target);
        player.GetComponent<PlayerCube>().isDisabled = false;
        Destroy(gameObject); // Teleport player before destroying projectile
    }
}
