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

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead) // If the player has died, run the death method
        {
            PlayerDeath();
        }
    }

    void PlayerDeath() // This is for death to traps
    {
        Instantiate(corpse, transform.position, transform.rotation); // Spawns the corpse at the location of the player
        playerMesh.SetActive(false); // Disables the player afterwards, giving off the illusion of player falling apart

        theCamera.target = this.gameObject; // Sets the target of the camera to the dead player
        StartCoroutine(theCamera.DeathCam(this.gameObject));
    }
}
