using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Store refernces to these buttons and background images
    public GameObject backButton, instructionsButton, creditsButton, playButton, quitButton;
    public GameObject instructionsBackground, creditsBackground;

    public GameObject[] instructionsUI, creditsUI;
    RectTransform instructionsTransform, creditsTransform;

    // Save the last UI that was opened so that backButton can close it using animations
    RectTransform lastTransform;
    GameObject lastBackground;
    GameObject[] lastUI;
    // Start is called before the first frame update
    void Start()
    {
        // Get the RectTransform components of instructions and credits button
        instructionsTransform = instructionsButton.GetComponent<RectTransform>();
        creditsTransform = creditsButton.GetComponent<RectTransform>();

        SoundManager.instance.Play("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene(1)); // This is a coroutine to allow button click sfx to play before loading game
    }

    public void Instructions()
    {
        lastTransform = instructionsTransform; lastBackground = instructionsBackground; lastUI = instructionsUI;
        instructionsBackground.SetActive(true); // Enable instructionsBackground

        playButton.SetActive(false); quitButton.SetActive(false); creditsButton.SetActive(false); // Disable these buttons before the transition
        StartCoroutine(UIOpenTransition(instructionsTransform, instructionsBackground, instructionsUI));
    }

    public void Credits()
    {
        lastTransform = creditsTransform; lastBackground = creditsBackground; lastUI = creditsUI; // Set the last rectTransform, BG and UI elements to these
        creditsBackground.SetActive(true); // Enable credits Background

        playButton.SetActive(false); quitButton.SetActive(false); instructionsButton.SetActive(false); // Disable all these buttons before the transition
        StartCoroutine(UIOpenTransition(creditsTransform, creditsBackground, creditsUI));
    }

    public void CloseScreen()
    {
        StartCoroutine(UICloseTransition());
    }

    public void BackToMainMenu()
    {
        SoundManager.instance.StopPlaying("VictoryMusic"); // Stop playing the victoryMusic
        SoundManager.instance.Play("MenuMusic");
        StartCoroutine(LoadScene(0)); // Load the mainmenu
    }

    public void Quit()
    {
        Application.Quit();
    }
    public IEnumerator LoadScene(int sceneToLoad)
    {

        //SoundManager.instance.Play("StartGame");
        yield return null;
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator UIOpenTransition(RectTransform buttonToMove, GameObject screenToShow, GameObject[] screenUI)
    {
        // Move to the right of the canvas by 400 units
        Vector2 targetPosition = new Vector2(400, buttonToMove.anchoredPosition.y); 

        // Scale the Background up so it fills the canvas
        Vector3 targetScale = new Vector3(3.4f, screenToShow.transform.localScale.y, screenToShow.transform.localScale.z);

        for (float t = 0f; t < 2.0f; t += Time.deltaTime) // Do the above over 1s
        {
            buttonToMove.anchoredPosition = Vector2.Lerp(buttonToMove.anchoredPosition, targetPosition, t / 2);
            screenToShow.transform.localScale = Vector3.Lerp(screenToShow.transform.localScale, targetScale, t / 2);

            yield return null;
        }

        // Afterwards, enable all the screenUI
        foreach (GameObject go in screenUI)
        {
            go.SetActive(true);
        }

        // After transition is over, enable the backButton
        backButton.SetActive(true);
        yield break;
    }

    public IEnumerator UICloseTransition()
    {
        // Disable the backButton
        backButton.SetActive(false);
        // Disable all gameObject in lastUI
        foreach (GameObject go in lastUI)
        {
            go.SetActive(false);
        }

        // Move button back to its original place
        Vector2 originalPosition = new Vector2(0, lastTransform.anchoredPosition.y);

        // Scale down the background to its original scale at 1,1,1
        Vector3 originalScale = Vector3.one;

        for (float t = 0.0f; t < 2.0f; t += Time.deltaTime)
        {
            lastTransform.anchoredPosition = Vector2.Lerp(lastTransform.anchoredPosition, originalPosition, t / 2);
            lastBackground.transform.localScale = Vector3.Lerp(lastBackground.transform.localScale, originalScale, t / 2);

            yield return null;
        }

        lastBackground.SetActive(false); // Disable the background
        playButton.SetActive(true); quitButton.SetActive(true); // Enable play and quit once the screen transition is over

        // If credits or instructions button is disabled, enable them
        while (!creditsButton.activeSelf) creditsButton.SetActive(true);
        while (!instructionsButton.activeSelf) instructionsButton.SetActive(true);
        yield break;
    }

    public void GrowButton() // When mousing over the button, make it grow in size
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // Set scale of button to be 1.2 in all axis
    }

    public void ShrinkButton() // When mouse moves away from button, make it shrink in size
    {
        transform.localScale = Vector3.one; // Set scale to 1,1,1
    }
}
