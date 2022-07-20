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
        distance = Vector3.Distance(player1.transform.position, player2.transform.position); // Calculates distance between the players

        calculatedSize = distance * 1.20f;
        // If calculatedSize is greater than minimumSize, use the original value. If calculatedSize is smaller than minimumSize, use minimumSize instead.

        focusPoint.transform.position = v; // Sets the position of the focusPoint to this position

        if (isZooming) return; // If we are zooming in during the deathCam, do not set position of the camera in the transform

        theCamera.orthographicSize = Mathf.Clamp(calculatedSize, minimumSize, calculatedSize);
        // If calculatedSize is greater than minimumSize, use the original value. If calculatedSize is smaller than minimumSize, use minimumSize instead.

        transform.position = new Vector3(
            focusPoint.transform.position.x + (-17.6f),
            21f, //focusPoint.transform.position.y + 21f, y-value is constant so camera doesnt follow player or change size down when they die.
            focusPoint.transform.position.z + (-16.5f)); // Move camera to 

    }

    public IEnumerator DeathCam(GameObject zoomTarget)
    {
        float t = 0;
        float duration = 300;

        target = zoomTarget;
        isZooming = true;

        Vector3 destination = new Vector3(
            zoomTarget.transform.position.x + (-17.6f),
            21f, //zoomTarget.transform.position.y + 21f,
            zoomTarget.transform.position.z + (-16.5f)); // The position to lerp towards, this is a point above the dead player

        
        while (t < duration)
        {
            //Debug.Log("Lerping");
            transform.position = Vector3.Lerp(transform.position, destination, t / duration); // Moves the camera over the dead player
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, zoomSize, t / duration); // Zoom in the camera on the player's corpse

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
