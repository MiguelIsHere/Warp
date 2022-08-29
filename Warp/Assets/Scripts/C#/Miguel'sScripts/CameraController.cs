using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player1, player2; // Stores references to the two players

    public GameObject focusPoint; // The camera will follow this gameObject, not either of the players under normal conditions
    [HideInInspector] public GameObject target; // Allows camera to toggle between following the focus to following a player by switching this

    [Header("Variables")]
    public float calculatedSize;
    public float minimumSize;
    public float zoomSize; // This value is used instead of minimumSize when a player is killed to create a kill camera.
    public float distance;

    Camera theCamera;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();

        focusPoint = GameObject.Find("Target");
        target = focusPoint;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 v = (player1.transform.position + player2.transform.position) / 2f; // Calculates the midpoint between the two players
        distance = Vector3.Distance(player1.transform.position, player2.transform.position); // Calculates distance between the players

        calculatedSize = distance * 1.20f;
        theCamera.orthographicSize = Mathf.Clamp(calculatedSize, minimumSize, calculatedSize);
        // If calculatedSize is greater than minimumSize, use the original value. If calculatedSize is smaller than minimumSize, use minimumSize instead.

        focusPoint.transform.position = v; // Sets the position of the focus of camera to this position
        transform.position = new Vector3(
            focusPoint.transform.position.x + (-17.6f),
            focusPoint.transform.position.y + 21f,
            focusPoint.transform.position.z + (-16.5f)); // Move camera to 

        //if (player1.GetComponent<PlayerCube>().isDead || player2.GetComponent<PlayerCube>().isDead) // If either player has died
        //{
        //    target = player
        //    StartCoroutine(DeathCam());
        //}
    }

    public IEnumerator DeathCam(GameObject zoomTarget)
    {
        float t = 0;
        float duration = 1;

        Vector3 destination = new Vector3(
            zoomTarget.transform.position.x + (-17.6f),
            zoomTarget.transform.position.y + 21f,
            zoomTarget.transform.position.z + (-16.5f)); // The position to lerp towards, this is a point above the dead player

        while (t < duration)
        {
            transform.position = Vector3.Lerp(transform.position, destination, t / duration); // Moves the camera over the dead player
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, zoomSize, t / duration); // Zoom in the camera on the player's corpse

            yield return new WaitForEndOfFrame();
        }
    }
}
