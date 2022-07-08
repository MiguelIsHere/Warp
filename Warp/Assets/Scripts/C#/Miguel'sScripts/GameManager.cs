using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] alivePlayers;

    CameraController theCamera;
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (theCamera.player1.GetComponent<PlayerCube>().isDead || theCamera.player2.GetComponent<PlayerCube>().isDead) // If either players have died, zoom cam
        //{
        //    theCamera.target = 
        //}
    }
}
