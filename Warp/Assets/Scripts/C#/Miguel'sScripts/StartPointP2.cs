using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointP2 : MonoBehaviour
{
    public string playerName;
    public static GameObject spawnpoint2 = null;

    //GameObject player;
    //GameManager theGameManager;
    void Start()
    {
        //player = GameObject.Find(playerName); // Finds corresponding player using this string variable
        //theGameManager = FindObjectOfType<GameManager>();

        if (spawnpoint2 = null)
        {
            if (this.playerName == "Player 2")
            {
                spawnpoint2 = this.gameObject;
            }
        }
        else if (spawnpoint2 != this)
        {
            if (this.playerName == "Player 2")
            {
                Destroy(spawnpoint2);
                spawnpoint2 = this.gameObject;
            }
        }
    }
}
