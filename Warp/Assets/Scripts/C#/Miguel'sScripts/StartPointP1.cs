using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointP1 : MonoBehaviour
{
    public string playerName;
    public static GameObject spawnpoint1 = null;

    GameObject player;
    GameManager theGameManager;
    void Start()
    {
        player = GameObject.Find(playerName); // Finds corresponding player using this string variable
        theGameManager = FindObjectOfType<GameManager>();

        if (spawnpoint1 = null) // If there is no player 1 spawnpoint instance and this spawnpoint corresponds to player 1, set instance to this gameobject
        {
            if (this.playerName == "Player 1")
            {
                spawnpoint1 = this.gameObject;
                theGameManager.StartCoroutine(theGameManager.MovePlayer(player, spawnpoint1));
            }
        }
        else if (spawnpoint1 != this) // Else, destroy the old instance make this the new instance of player 1 spawnpoint
        {
            if (this.playerName == "Player 1")
            {
                Destroy(spawnpoint1);
                spawnpoint1 = this.gameObject;
            }

        }
    }
}
