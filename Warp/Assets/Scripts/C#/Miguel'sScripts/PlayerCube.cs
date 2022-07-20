using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{

    public bool isDead; // Used to determine if this player has died TO TRAPS
    public bool isFalling; // Check if this player died by FALLING OFF the map

    public GameObject playerMesh;
    public GameObject corpse;

    CameraController theCamera;
    GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        theGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead) // If the player has died, run the death method
        {
            StartCoroutine(PlayerDeath());
            theGameManager.deadPlayer = gameObject; // Upon death, the player sets itself as the dead player in GameManager
        }
    }

    IEnumerator PlayerDeath() // This is for death to traps
    {
        while (!playerMesh.activeSelf) yield break; // If player mesh is already deactived, do not run rest of coroutine

        Instantiate(corpse, transform.position, transform.rotation); // Spawns the corpse at the location of the player
        playerMesh.SetActive(false); // Disables the player afterwards, giving off the illusion of player falling apart

        //theCamera.target = this.gameObject; // Sets the target of the camera to the dead player
        //StartCoroutine(theCamera.DeathCam(this.gameObject));
    }
}
