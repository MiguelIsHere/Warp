using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player1, player2; // Stores references to the two players

    public GameObject focusPoint; // The camera will follow this gameObject, not either of the players under normal conditions
    public GameObject target; // Allows camera to toggle between following the focus to following a player by switching this

    [Header("Variables")]
    public float calculatedSize;
    public float minimumSize;
    public float zoomSize; // This value is used instead of minimumSize when a player is killed to create a kill camera.
    public float distance;

    public bool isZooming;

    Camera theCamera;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();

        focusPoint = GameObject.Find("FocusPoint");
        target = focusPoint;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 v = (player1.transform.position + player2.transform.position) / 2f; // Calculates the midpoint between the two players

        // Calculate cameraSize to use
        Vector3 p = new Vector3(player1.transform.position.x, 0, player1.transform.position.z);
        Vector3 P = new Vector3(player2.transform.position.x, 0, player2.transform.position.z);

        distance = Vector3.Distance(p, P); // Calculates distance between the players
        calculatedSize = distance * 1.20f;

        focusPoint.transform.position = v; // Sets the position of the focusPoint to this position

        if (isZooming) return; // If we are zooming in during the deathCam, do not set position of the camera in the transform

        float s = Mathf.Clamp(calculatedSize, minimumSize, calculatedSize);
        theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, s, Time.deltaTime * 3f);
        // If calculatedSize is greater than minimumSize, use the original value. If calculatedSize is smaller than minimumSize, use minimumSize instead.

        Vector3 d = new Vector3(
            focusPoint.transform.position.x + (-17.6f),
            21f, //focusPoint.transform.position.y + 21f, y-value is constant so camera doesnt follow player or change size down when they die.
            focusPoint.transform.position.z + (-16.5f)); // Move camera to 

        // Smoothly lerp to focusPoint's position.
        transform.position = Vector3.Lerp(transform.position, d, Time.deltaTime * 3f);

    }

    public IEnumerator DeathCam(GameObject zoomTarget)
    {
        if (isZooming) yield break;
        float t = 0f;
        float duration = 10f; //float duration = 300

        target = zoomTarget;
        isZooming = true;

        //bool isZoomingOut = false;
        Vector3 destination = new Vector3(
            zoomTarget.transform.position.x + (-17.6f),
            21f, //zoomTarget.transform.position.y + 21f,
            zoomTarget.transform.position.z + (-16.5f)); // The position to lerp towards, this is a point above the dead player


        while (t < duration)
        {
            //Debug.Log("Lerping");
            //if (isZoomingOut == false) 
            transform.position = Vector3.Lerp(transform.position, destination, t / duration); // Moves the camera over the dead player
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, zoomSize, t / duration); // Zoom in the camera on the player's corpse

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //yield return new WaitUntil(() => transform.position == destination); // Once the camera has reached the destination, zoom back out
        yield return new WaitForEndOfFrame();

        target = focusPoint; // Set the target back to the focusPoint of the camera, which is midpoint between two players
        destination = new Vector3(
            target.transform.position.x + (-17.6f),
            21f,
            target.transform.position.z + (-16.5f)); // Change destination to be based off focusPoint position

        t = 0f; // Reset t

        //Return camera to target position and regular size
        while (t < duration)
        {
            transform.position = Vector3.Lerp(transform.position, destination, t / duration);
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, calculatedSize, t / duration);

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
