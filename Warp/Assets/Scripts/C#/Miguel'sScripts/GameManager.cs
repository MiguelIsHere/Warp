using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] alivePlayers;
    public GameObject deadPlayer;

    public GameObject player1;

    CameraController theCamera;
    public GameObject playerOneSpawn, playerTwoSpawn;

    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        playerOneSpawn = StartPointP1.spawnpoint1;
        playerTwoSpawn = StartPointP2.spawnpoint2;
        //StartCoroutine(MovePlayer())
    }

    // Update is called once per frame
    void Update()
    {
        if (deadPlayer == null)
        {
            return;
        }
        else
        {
            theCamera.StartCoroutine(theCamera.DeathCam(deadPlayer));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pressed F");
            StartCoroutine(MovePlayer(player1, playerOneSpawn));
        }

    }

    public IEnumerator MovePlayer(GameObject player, GameObject spawnpoint)
    {
        Debug.Log("Moving");
        Vector3 offset = new Vector3(0, 1, 0); // Add this to transform.position of spawnpoint so player is moved on top of it

        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, spawnpoint.transform.position + offset, t);

            yield return null;
        }
    }
}
