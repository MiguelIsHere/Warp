using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

    }

    public void Credits()
    {

    }

    public void BackToMainMenu()
    {
        StartCoroutine(LoadScene(0)); // Load the mainmenu
    }
    public IEnumerator LoadScene(int sceneToLoad)
    {

        //SoundManager.instance.Play("StartGame");
        yield return null;
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator CreditsTransition()
    {
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
