using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public List<GameObject> listOfLevels = new List<GameObject>();

    [Header("Level-Related")]
    public GameObject lastLevel, currentLevel; // Stores reference of the previous loaded level so it can be destroyed when new level is created
    public GameObject killPlane;
    GameObject levelToLoad;
    int levelID;
    public static GameManager inst;

    float levelsLoaded = 1; // this is 1 and not 0 as it includes the starting level which will always be the same
    Vector3 offset;

    [Header("UI")]
    public TMP_Text playerOneCount, playerTwoCount; // Store reference to the UI Text for the individual players' wins
    public GameObject endScreen;
    public GameObject menuButton, continueButton;
    public GameObject winnerText, loserText, winnerName, loserName, winnerIcon, loserIcon; // Disable all of these at the start of the game and reenable them later
    public Image endScreenBackground;
    public Sprite playerOneIcon, playerTwoIcon; // Store references to the images that will be changed to reflect the winner and loser's icons

    

    Color endScreenColour;
    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        playerOneSpawn = StartPointP1.spawnpoint1;
        playerTwoSpawn = StartPointP2.spawnpoint2;

        endScreenColour = endScreenBackground.GetComponent<Image>().color; // Set this color to be the original colour of the endScreen.
        Color transparent = new Color(0, 0, 0, 0);
        endScreenBackground.GetComponent<Image>().color = transparent; // Set the endScreen to be transparent so we can fade it in later

        endScreen.SetActive(false); winnerName.SetActive(false); // Disable the endScreen and its children which are all related to end of game's UI
        loserName.SetActive(false); winnerIcon.SetActive(false); loserIcon.SetActive(false); winnerText.SetActive(false); loserText.SetActive(false);
        menuButton.SetActive(false); continueButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        playerOneSpawn = StartPointP1.spawnpoint1;
        playerTwoSpawn = StartPointP2.spawnpoint2;
        if (deadPlayers.Count == 0) // If there are no dead players, dont do anything
        {
            return;
        }
        else
        {
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

        StartCoroutine(UpdateWinUI());
    }

    public IEnumerator MovePlayer(GameObject player, GameObject spawnpoint)
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 offset = new Vector3(0, 1, 0); // Add this to transform.position of spawnpoint so player is moved on top of it

        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, spawnpoint.transform.position + offset, t / 6);

            yield return null;
        }

        // Reenable character controller of player if there is a character controller attached
        if (player.GetComponent<CharacterController>())
        {
            player.GetComponent<CharacterController>().enabled = true;
        }
        
        // Reenable the kill plane
        killPlane.SetActive(true);
        yield break;
    }

    public IEnumerator SpawnLevel()
    {
        Vector3 modifier = new Vector3(levelsLoaded, 0, 0); // Multiplies the offset so new levels keep getting created further in the x-axis

        
        while (winner == null)
        {
            // Gets a random level from the level list to load
            levelID = Random.Range(0, listOfLevels.Count - 1); // Substract 1 from Count because final level is the win level; We don't want to load it
                                                               // if no one won. Max is excluded, therefore the highest number correspond to level before final
                                                               // level in list
            break;
        }
        while (winner != null) // If there is a winner, do not load a random level and always load end level instead
        {
            levelID = listOfLevels.Count - 1; // End Level should always be final element in list for this to work
            break;
        }

        Debug.Log("Getting new Level");
        levelToLoad = listOfLevels[levelID];

        // Multiply these two vectors component by component, EX: 100 * modifier.x, 0 * modifier.y, 100 * modifier.z
        offset = Vector3.Scale(new Vector3(60, 0, 0), modifier);
        print("" + offset);

        // Load the new level offset from the previous level in the x-axis
        currentLevel = Instantiate(levelToLoad, offset, Quaternion.identity);
        Debug.Log("New Level Spawned");
        // Increase the number of levels loaded by one
        levelsLoaded += 1;

        yield return null;

        // Disable the kill plane to allow fallen players to move to the next level without dying in the process
        killPlane.SetActive(false);

        // Move the players and their marks to the next level
        StartCoroutine(MovePlayer(player1, playerOneSpawn));
        StartCoroutine(MovePlayer(player1Mark, playerOneSpawn));
        StartCoroutine(MovePlayer(player2, playerTwoSpawn));
        StartCoroutine(MovePlayer(player2Mark, playerTwoSpawn));

        player1.GetComponent<PlayerCube>().won = false;
        player2.GetComponent<PlayerCube>().won = false;

        yield return new WaitForSeconds(1.5f); // Add a delay so theres enough for the players to move away so camera doesnt see lastLevel get destroyed
        Destroy(lastLevel); // Destroys the previous level off-screen
        lastLevel = currentLevel; // Changes last level to the new current level

        while (winner != null) // If there is a winner, switch from regular level music to victory music
        {
            SoundManager.instance.StopPlaying("LevelMusic");
            SoundManager.instance.Play("VictoryMusic");
            break;
        }

        yield break;
    }

    public IEnumerator UpdateWinUI()
    {
        yield return new WaitForSeconds(3.5f); // Wait for first camera transition to zoom in and out

        Debug.Log("Shatter");
        SoundManager.instance.Play("Shatter"); // Play Shatter SFX, followed shortly by ding SFX
        yield return new WaitForSeconds(0.1f);

        Debug.Log("Ding");
        SoundManager.instance.Play("Ding");
        yield return new WaitForSeconds(0.45f);

        // Update the number of wins in the canvas
        playerOneCount.text = "" + playerOneWins;
        playerTwoCount.text = "" + playerTwoWins;

        while (winner != null)
        {
            StartCoroutine(WinScreenReveal(winner)); // If there is winner, start the winScreenReveal

            yield break;
        }
    }

    public IEnumerator WinScreenReveal(GameObject nameOfWinner)
    {
        // Re-enable the endScreen
        endScreen.SetActive(true);

        // Set scales of the images to 0,0,0 so we can scale them up later
        winnerIcon.transform.localScale = Vector3.zero;
        loserIcon.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(2.9f); // Wait a few seconds to give players time to move to the final scene
        // Set the strings and images for the winner and loser's UI
        while (nameOfWinner == player1)
        {
            // If player 1 won, change winner and loser's name to P1 and P2
            winnerName.GetComponent<TMP_Text>().text = "Player 1";
            loserName.GetComponent<TMP_Text>().text = "Player 2";

            // Change the icons as well
            winnerIcon.GetComponent<Image>().sprite = playerOneIcon;
            loserIcon.GetComponent<Image>().sprite = playerTwoIcon;

            break;
        }

        while (nameOfWinner == player2)
        {
            // If player 2 won, change winner and loser's name to P2 and P1
            winnerName.GetComponent<TMP_Text>().text = "Player 2";
            loserName.GetComponent<TMP_Text>().text = "Player 1";

            // Change the icons as well
            winnerIcon.GetComponent<Image>().sprite = playerTwoIcon;
            loserIcon.GetComponent<Image>().sprite = playerOneIcon;

            break;
        }

        // Get the current color of endScreen to use for the transition
        Color transitionColor = endScreen.GetComponent<Image>().color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime) // Over one second, fade in the endScreen
        {
            transitionColor = Color.Lerp(transitionColor, endScreenColour, t); // Lerp the transitionColor torwards the originalColour over time
            endScreen.GetComponent<Image>().color = transitionColor; // Set the new colour of endScreen

            yield return null;
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Re-enable the winner and loser text, these are not the names of the players who won/lost
        winnerText.SetActive(true); loserText.SetActive(true);

        winnerIcon.SetActive(true);
        for (float t = 0.0f; t < 0.5f; t += Time.deltaTime)
        {
            // Lerp the scale of the winnerIcon first up to 1,1,1
            winnerIcon.transform.localScale = Vector3.Lerp(winnerIcon.transform.localScale, Vector3.one,  2 * t); // Multiply t by 2 since t < 0.5

            yield return null;
        }
        
        // Re-enable the winner's name after the icon of the winner has popped up
        winnerName.SetActive(true);

        yield return new WaitForSeconds(0.4f);
        loserIcon.SetActive(true);
        for (float t = 0.0f; t < 0.5f; t += Time.deltaTime)
        {
            // Lerp the scale of the loserIcon next, up to 1,1,1
            loserIcon.transform.localScale = Vector3.Lerp(loserIcon.transform.localScale, Vector3.one, 2 * t);

            yield return null;
        }

        // Re-enable the loser's name after the icon of the loser has popped up
        loserName.SetActive(true);

        SoundManager.instance.Play("Applause");

        yield return new WaitForSeconds(3f); // Show completed winScreen for at least 3s before the player can close it

        menuButton.SetActive(true); continueButton.SetActive(true);
    }
}
