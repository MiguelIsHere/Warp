using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] alivePlayers;
    public GameObject deadPlayer;

    CameraController theCamera;
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
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
        //if (theCamera.player1.GetComponent<PlayerCube>().isDead || theCamera.player2.GetComponent<PlayerCube>().isDead) // If either players have died, zoom cam
        //{
        //    theCamera.target = 
        //}
    }
}
