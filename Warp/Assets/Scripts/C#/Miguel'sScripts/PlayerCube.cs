using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    [Header("Variables")]
    public bool isDead; // Used to determine if this player has died TO TRAPS
    public bool isFalling; // Check if this player died by FALLING OFF the map
    public bool isDisabled = false; // Since movement is not physics based, need to disable player first before teleportion for it to work
    public bool won = false; // Used to ensure win is only added once as the win function is under Update
    public bool isFadingStarted = false;
    public LayerMask whatIsGround;
    public string player;

    public float speed = 5f;
    public float rotationRate = 60f; // This is only meant for rotating the aiming line of the player2, as aiming of player1 is based on mousePosition
    public float fadeCountdown; // Max value of the countdown before aiming circle fades in/out
    public float countdown;

    public Vector3 offset;
    Vector3 rot; // This is the desired rotation for our bullet spawning and aiming circle
    Vector3 velocity;
    

    Color originalColour;
    [Header("References")]
    public GameObject playerMesh;
    public GameObject corpse;
    public GameObject bullet; // P1 and P2 have different bullets
    public Transform bulletSpawn;
    public Transform aimStart, aimEnd; // Store reference to start and end points of the aiming lines
    public GameObject mark; // P1 and P2 have different marks
    public GameObject aimCircle; // This is for changing the alpha of the aimCircle sprite
    public GameObject aimCrosshair; // This is for rotating the crosshair so that it is still somewhat centred on player

    CameraController theCamera;
    //Rigidbody rb;
    CharacterController cc;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        cc = GetComponent<CharacterController>();
        spriteRenderer = aimCircle.GetComponent<SpriteRenderer>(); // Get the sprite renderer of aimCircle

        mark.transform.position = transform.position; // Set default position of mark to be player's position
        originalColour = spriteRenderer.color; // Stores reference to the original colour of crosshair so we can go back to this color later

        //offset = transform.rotation.eulerAngles; // Vector to apply when calculating rotation of character to turn towards
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            PlayerDeath();

            if (GameManager.inst.deadPlayers.Contains(gameObject)) return; // If this player is already part of the deadPlayers list, return
            GameManager.inst.deadPlayers.Add(gameObject); // Add this to the list of deadPlayers
            return;
        }

        if (!isDisabled)
        {
            UpdateMovement(); 
            Aim();
            UpdateAimingCircle();

            // Shoot bullets
            if (Input.GetButtonDown("Fire1 " + player)) 
            {
                SoundManager.instance.Play("Shoot"); // Play Shoot SFX
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            }

            // Place Marks that set teleport destination for enemy player
            if (Input.GetButtonDown("Fire2 " + player)) 
            {
                mark.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            // If this player is not dead, the dead player is not this object and the deadPlayers.Count is 0. deadPlayers != null does not work
            if (!isDead && GameManager.inst.deadPlayers.Count != 0) // GameManager.inst.deadPlayer
            {
                if(won) return;

                won = true; // Sets this to true so win method is called once
                GameManager.inst.AddPlayerWin(gameObject); // Calls this method and declares which player won so a win for corresponding player is added
            }
        }
    }

    public void PlayerDeath() 
    {
        if (!playerMesh.activeSelf) return; // If the player is alr dead, dont run rest of code

        SoundManager.instance.Play("Death"); // Play Death SFX
        Instantiate(corpse, transform.position, transform.rotation); // Spawns the corpse at the location of the player
        playerMesh.SetActive(false); // Disables the player afterwards, giving off the illusion of player falling apart
        cc.enabled = false; // Disable character controller to make movement of player between levels smoother
    }

    void UpdateMovement()
    {
        velocity += Physics.gravity * Time.deltaTime; // g ~= 10m/s^2; Causes player to fall
        if (cc.isGrounded) velocity = Vector3.zero; // If player is grounded, set acceleration due to gravity to 0

        Vector3 v = new Vector3(Input.GetAxis("Horizontal " + player), 0, Input.GetAxis("Vertical " + player));
        //Debug.Log(player + ": " + Input.GetAxis("Horizontal " + player));
        //Debug.Log(player + ": " + v);
        if (v != Vector3.zero)
        {
            // Make the character smoothly rotate to face direction of movement
            playerMesh.transform.rotation = Quaternion.Slerp(playerMesh.transform.rotation, Quaternion.LookRotation(v + offset), 0.25f); 
        }

        // If the character controller is enabled, move the character
        if (cc.enabled)
        {
            cc.Move((transform.TransformDirection(v.normalized) * speed + velocity) * Time.deltaTime); // Moves player using inputs
        }
    }

    void Aim()
    {
        if (player == "P1") // Aiming for player 1
        {
            Camera camera = FindObjectOfType<Camera>();
            //Vector3 mouse = camera.ScreenToWorldPoint(Input.mousePosition); This won't work because screenToWorldPoint is not meant for 3D, only has x and y

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // Shoots ray from mousePosition in camera into world space
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, Mathf.Infinity, whatIsGround))
            {
                Vector3 h = new Vector3(hit.point.x, 0.5f, hit.point.z); // Offset the y of hit so that hit.point and bulletSpawn have the same y value

                // Find vector I need to rotate such that forward Vector of bulletSpawn is pointed towards h
                rot = Quaternion.LookRotation(h - bulletSpawn.transform.position).eulerAngles; 

                rot.x = rot.z = 0; // Prevents rotation in x- or z-axis so bullets do not go into the ground; bulletSpawn only rotates in y-axis
                bulletSpawn.transform.rotation = Quaternion.Euler(rot); // Finally, set the new rotation of bulletSpawn


                // Check if there is any mouse movement, this will be used by UpdateAimCircle
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    countdown = fadeCountdown;
                    isFadingStarted = false;
                }
            }
        }
        else if (player == "P2") // Aiming for player 2
        {
            if (Input.GetButton("ClockwiseAim"))
            {
                bulletSpawn.transform.Rotate(0, rotationRate * Time.deltaTime, 0); // Aim clockwise at 180 degrees/s
                countdown = fadeCountdown;
                isFadingStarted = false;
            }
            
            if (Input.GetButton("CounterclockwiseAim"))
            {
                bulletSpawn.transform.Rotate(0, -rotationRate * Time.deltaTime, 0); // Aim counterclockwise at 180 degrees/s
                countdown = fadeCountdown;
                isFadingStarted = false;
            }
        }
    }

    void UpdateAimingCircle()
    {

        countdown -= Time.deltaTime;
        if (countdown <= 0) // When countdown <= zero, the aimCircle will fade out
        {
            isFadingStarted = false;
            //StartCoroutine(FadeAimingCircle());
            StartCoroutine(FadeTo(0.0f));
        }
        else // When countdown is above 0, which is when the player is currently aiming, make aimCrosshair appear again
        {
            spriteRenderer.color = originalColour;
        }

        if (player == "P1")
        {
            // Apply a 90-degree counterclockwise rotation around world y-axis to rot so bar of crosshair aligns with trajectory of projectiles
            Quaternion crosshairRotation = Quaternion.AngleAxis(-90, Vector3.up) * Quaternion.Euler(rot); 

            // Set the rotation
            aimCrosshair.transform.rotation = crosshairRotation;
        }
        if (player == "P2")
        {
            if (Input.GetButton("ClockwiseAim"))
            {
                aimCrosshair.transform.Rotate(0, rotationRate * Time.deltaTime, 0); // Aim clockwise at 180 degrees/s

            }

            if (Input.GetButton("CounterclockwiseAim"))
            {
                aimCrosshair.transform.Rotate(0, -rotationRate * Time.deltaTime, 0); // Aim counterclockwise at 180 degrees/s

            }
        }
    }

    IEnumerator FadeTo(float aValue)
    {
        float alpha = spriteRenderer.color.a;

        while (isFadingStarted == true) yield break;
        isFadingStarted = true; // Prevents this coroutine from constantly running

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(originalColour.r, originalColour.g, originalColour.b, Mathf.Lerp(alpha, aValue, t));
            spriteRenderer.color = newColor;

            yield return null;
        }
        yield break;
    } 
}
