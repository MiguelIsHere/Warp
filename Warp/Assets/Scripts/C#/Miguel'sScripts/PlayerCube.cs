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
    public float speed = 5f;
    public LayerMask whatIsGround;
    public string player;

    public float rotationRate = 60f; // This is only meant for rotating the aiming line of the player2, as aiming of player1 is based on mousePosition

    Vector3 velocity;
    //Vector3 offset;
    [Header("References")]
    public GameObject playerMesh;
    public GameObject corpse;
    public GameObject bullet; // P1 and P2 have different bullets
    public Transform bulletSpawn;
    public Transform aimStart, aimEnd; // Store reference to start and end points of the aiming lines
    public GameObject mark; // P1 and P2 have different marks

    CameraController theCamera;
    //Rigidbody rb;
    CharacterController cc;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        cc = GetComponent<CharacterController>();
        lr = GetComponent<LineRenderer>();

        mark.transform.position = transform.position; // Set default position of mark to be player's position
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            PlayerDeath();
            //GameManager.inst.deadPlayer = gameObject; // Set the dead Player to be this. This cannot be changed until the next level is loaded
            if (GameManager.inst.deadPlayers.Contains(gameObject)) return; // If this player is already part of the deadPlayers list, return
            GameManager.inst.deadPlayers.Add(gameObject); // Add this to the list of deadPlayers
            return;
        }

        if (!isDisabled)
        {
            UpdateMovement(); 
            Aim();
            UpdateAimingLine();
            //Fire();
            if (Input.GetButtonDown("Fire1 " + player)) // Shoot bullets
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            }

            //PlaceMark();
            if (Input.GetButtonDown("Fire2 " + player)) // Place Marks that set teleport destination for enemy player
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

    public void PlayerDeath() // This is for death to traps
    {
        if (!playerMesh.activeSelf) return; // If player mesh is already deactived, do not run rest of coroutine

        Instantiate(corpse, transform.position, transform.rotation); // Spawns the corpse at the location of the player
        playerMesh.SetActive(false); // Disables the player afterwards, giving off the illusion of player falling apart
    }

    void UpdateMovement()
    {
        velocity += Physics.gravity * Time.deltaTime; // g ~= 10m/s^2; Causes player to fall
        if (cc.isGrounded) velocity = Vector3.zero; // If player is grounded, set acceleration due to gravity to 0

        Vector3 v = new Vector3(Input.GetAxis("Horizontal " + player), 0, Input.GetAxis("Vertical " + player));
        //Debug.Log(player + ": " + Input.GetAxis("Horizontal " + player));
        //Debug.Log(player + ": " + v);
        cc.Move((transform.TransformDirection(v.normalized) * speed + velocity) * Time.deltaTime); // Moves player using inputs
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
                Vector3 rot = Quaternion.LookRotation(h - bulletSpawn.transform.position).eulerAngles; 

                rot.x = rot.z = 0; // Prevents rotation in x- or z-axis so bullets do not go into the ground; bulletSpawn only rotates in y-axis
                bulletSpawn.transform.rotation = Quaternion.Euler(rot); // Finally, set the new rotation of bulletSpawn

                //Vector3 h = new Vector3(hit.point.x, 0.5f, hit.point.z);
                //bulletSpawn.LookAt(h);
            }
        }
        else if (player == "P2") // Aiming for player 2
        {
            if (Input.GetButton("ClockwiseAim"))
            {
                bulletSpawn.transform.Rotate(0, rotationRate * Time.deltaTime, 0); // Aim clockwise at 180 degrees/s
            }
            
            if (Input.GetButton("CounterclockwiseAim"))
            {
                bulletSpawn.transform.Rotate(0, -rotationRate * Time.deltaTime, 0); // Aim counterclockwise at 180 degrees/s
            }
        }
    }

    void UpdateAimingLine()
    {
        lr.SetPosition(0, aimStart.position);
        lr.SetPosition(1, aimEnd.position);
    }

    //void Fire()
    //{
    //    if (Input.GetButtonDown("Fire1 " + player))
    //    {
    //        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    //    }
    //}
}
