using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] alivePlayers;
    

    [Header("Player-Related")]
    public GameObject player1, player2; // Stores references to the individual players
    //public GameObject deadPlayer;
    public List<GameObject> deadPlayers = new List<GameObject>();
    public GameObject playerOneSpawn, playerTwoSpawn;

    public int playerOneWins = 0, playerTwoWins = 0; // Both win counts start at 0
    CameraController theCamera;

    [Header("Level Prefabs")]
    public GameObject levelPrefab; // Make this into an array that then randomly selects a level prefab later

    public static GameManager inst;

    float levelsLoaded = 1; // this is 1 and not 0 as it includes the starting level which will always be the same
    Vector3 offset;
    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        playerOneSpawn = StartPointP1.spawnpoint1;
        playerTwoSpawn = StartPointP2.spawnpoint2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pressed Space");
            StartCoroutine(MovePlayer(player1, playerOneSpawn));
        }

        playerOneSpawn = StartPointP1.spawnpoint1;
        playerTwoSpawn = StartPointP2.spawnpoint2;
        if (deadPlayers.Count == 0) //(deadPlayer == null)
        {
            return;
        }
        else
        {
            //theCamera.StartCoroutine(theCamera.DeathCam(deadPlayer));
            theCamera.StartCoroutine(theCamera.DeathCam(deadPlayers[0])); // Focuses the death cam on the first player that died in the round
        }

    }

    public void AddPlayerWin(GameObject player) //(GameObject player, int winCount)
    {
        if (player == player1) // If the player that called this method is the same as player1 reference, give player1 1 win
        {
            playerOneWins += 1;
        }
        else if (player == player2) // Else if player that called this method is the same as player2 reference, give player2 1 win
        {
            playerTwoWins += 1;
        }
    }

    public IEnumerator MovePlayer(GameObject player, GameObject spawnpoint)
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Moving");
        Vector3 offset = new Vector3(0, 1, 0); // Add this to transform.position of spawnpoint so player is moved on top of it

        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, spawnpoint.transform.position + offset, t / 6);

            yield return null;
        }
        yield break;
    }

    public IEnumerator SpawnLevel()
    {
        Vector3 modifier = new Vector3(levelsLoaded, 0, 0);

        // Multiply these two vectors component by component, EX: 100 * modifier.x, 0 * modifier.y, 100 * modifier.z
        offset = Vector3.Scale(new Vector3(60, 0, 0), modifier);
        print("" + offset);
        Instantiate(levelPrefab, offset, Quaternion.identity);

        levelsLoaded += 1;


        // Move the players to the next level
        yield return null;
        StartCoroutine(MovePlayer(player1, playerOneSpawn));
        StartCoroutine(MovePlayer(player2, playerTwoSpawn));

        player1.GetComponent<PlayerCube>().won = false;
        player2.GetComponent<PlayerCube>().won = false;
    }
}
