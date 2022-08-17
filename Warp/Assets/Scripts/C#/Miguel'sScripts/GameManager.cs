using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] alivePlayers;
    

    [Header("Player-Related")]
    public GameObject player1, player2; // Stores references to the individual players
    public List<GameObject> deadPlayers = new List<GameObject>();
    public GameObject playerOneSpawn, playerTwoSpawn;
    public GameObject player1Mark, player2Mark;
    public GameObject winner, loser; // Used to refer to which player was the winner and loser for UI elements

    public int playerOneWins = 0, playerTwoWins = 0; // Both win counts start at 0
    CameraController theCamera;

    [Header("Level Prefabs")]
    //public GameObject levelPrefab; // Make this into an array that then randomly selects a level prefab later
    public GameObject lastLevel, currentLevel; // Stores reference of the previous loaded level so it can be destroyed when new level is created
    public List<GameObject> listOfLevels = new List<GameObject>();

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

    public void AddPlayerWin(GameObject player)
    {
        if (player == player1) // If the player that called this method is the same as player1 reference, give player1 1 win
        {
            playerOneWins += 1;

            if (playerOneWins == 5) // If this player has reached 5 wins, they won
            {
                winner = player1;
                loser = player2;
            }
        }
        else if (player == player2) // Else if player that called this method is the same as player2 reference, give player2 1 win
        {
            playerTwoWins += 1;

            if (playerTwoWins == 5) // If this player has reached 5 wins, they won
            {
                winner = player2;
                loser = player1;
            }
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

        Destroy(lastLevel); // Destroys the previous level off-screen
        lastLevel = currentLevel; // Changes last level to the new current level
        yield break;
    }

    public IEnumerator SpawnLevel()
    {
        Vector3 modifier = new Vector3(levelsLoaded, 0, 0); // Multiplies the offset so new levels keep getting created further in the x-axis

        // Gets a random level from the level list
        int levelID = Random.Range(0, listOfLevels.Count - 1); // Substract 1 from Count because final level is the win level; We don't want to load it
                                                               // if no one won
        GameObject levelToLoad = listOfLevels[levelID];

        // Multiply these two vectors component by component, EX: 100 * modifier.x, 0 * modifier.y, 100 * modifier.z
        offset = Vector3.Scale(new Vector3(60, 0, 0), modifier);
        print("" + offset);
        //Instantiate(levelPrefab, offset, Quaternion.identity);

        currentLevel = Instantiate(levelToLoad, offset, Quaternion.identity);
        levelsLoaded += 1;


        // Move the players and their marks to the next level
        yield return null;

        StartCoroutine(MovePlayer(player1, playerOneSpawn));
        StartCoroutine(MovePlayer(player1Mark, playerOneSpawn));
        StartCoroutine(MovePlayer(player2, playerTwoSpawn));
        StartCoroutine(MovePlayer(player2Mark, playerTwoSpawn));

        player1.GetComponent<PlayerCube>().won = false;
        player2.GetComponent<PlayerCube>().won = false;
    }
}
