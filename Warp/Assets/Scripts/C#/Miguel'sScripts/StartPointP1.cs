using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointP1 : MonoBehaviour
{
    public string playerName;
    public static GameObject spawnpoint1 = null;

    GameObject player;

    //void Awake()
    void Start()
    {
        player = GameObject.Find(playerName); // Finds corresponding player using this string variable

        if (spawnpoint1 = null) // If there is no player 1 spawnpoint instance and this spawnpoint corresponds to player 1, set instance to this gameobject
        {
            if (this.playerName == "Player 1")
            {
                //print("Setting spawn");
                spawnpoint1 = this.gameObject;
                //GameManager.inst.StartCoroutine(GameManager.inst.MovePlayer(player, spawnpoint1));
            }
        }
        else if (spawnpoint1 != this) // Else, destroy the old instance and make this the new instance of player 1 spawnpoint
        {
            if (this.playerName == "Player 1")
            {
                print("Destroying old spawn");
                Destroy(spawnpoint1);
                spawnpoint1 = this.gameObject;
            }

        }
    }
}
