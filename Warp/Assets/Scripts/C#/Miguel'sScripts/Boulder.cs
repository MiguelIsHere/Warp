using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public float speed = 10f;
    public float rotationRate = 120f;


    Rigidbody rb;
    MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move() // Move the boulder to the right
    {
        rb.velocity = Vector3.right * speed;
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationRate * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If boulder squashes a player, player is dead
        {
            other.GetComponent<PlayerCube>().isDead = true;
        }

        if (other.CompareTag("Endpoint")) // If boulder touches the endPoint, destroy it after 2s
        {
            Destroy(gameObject, 3f);
        }
    }

    //IEnumerator FadeAway()
    //{
    //    float t = 0f;
    //    float duration = 1f;
    //    yield return new WaitForSeconds(1f);

    //    Color color = mesh.material.color;

    //    color.a -= Time.deltaTime;

    //    Color transparent = color;
    //    Vector4 transparent = new Vector4(1f, 1f, 1f, 0f);
    //    while (t < duration)
    //    {
    //        mesh.material.color = Color.Lerp(mesh.material.color, transparent, t / duration); // t / duration = Time.deltaTime

    //        yield return null;
    //    }

    //    yield return null;
    //    Destroy(gameObject);
    //}
}
