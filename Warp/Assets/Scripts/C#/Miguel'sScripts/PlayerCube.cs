using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    [Header("Variables")]
    public bool isDead; // Used to determine if this player has died TO TRAPS
    public bool isFalling; // Check if this player died by FALLING OFF the map
    public bool isDisabled = false; // Since movement is not physics based, need to disable player first before teleportion for it to work
    public float speed = 5f;
    public LayerMask whatIsGround;
    public string player;

    public float rotationRate = 60f; // This is only meant for rotating the aiming line of the player

    Vector3 velocity;
    //Vector3 offset;
    [Header("References")]
    public GameObject playerMesh;
    public GameObject corpse;
    public GameObject bullet; // P1 and P2 have different bullets
    public Transform bulletSpawn;
    public GameObject mark; // P1 and P2 have different marks

    CameraController theCamera;
    GameManager theGameManager;
    //Rigidbody rb;
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        theGameManager = FindObjectOfType<GameManager>();
        cc = GetComponent<CharacterController>();

        mark.transform.position = transform.position; // Set default position of mark to be player's position
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDisabled)
        {
            UpdateMovement();
            Aim();
            if (isDead) // If the player has died, run the death method
            {
                PlayerDeath();
                theGameManager.deadPlayer = gameObject; // Upon death, the player sets itself as the dead player in GameManager
            }

            if (Input.GetButtonDown("Fire1")) // Shoot bullets
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            }

            if (Input.GetButtonDown("Fire2")) // Place Marks
            {
                mark.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
        velocity += Physics.gravity * Time.deltaTime; // g ~= 10m/s^2
        if (cc.isGrounded) velocity = Vector3.zero;

        Vector3 v = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cc.Move((transform.TransformDirection(v.normalized) * speed + velocity) * Time.deltaTime);
    }

    void Aim()
    {
        if (player == "Player 1")
        {
            Camera camera = FindObjectOfType<Camera>();
            //Vector3 mouse = camera.ScreenToWorldPoint(Input.mousePosition); This won't work because screenToWorldPoint is not meant for 3D, only has x and y

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // Shoots ray from mousePosition in camera into world space
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, Mathf.Infinity, whatIsGround))
            {
                //float desiredRotation = Mathf.Atan2(hit.point.z - transform.position.z, hit.point.x - transform.position.x) * Mathf.Rad2Deg;
                //Quaternion.Lerp(bulletSpawn.transform.rotation, bulletSpawn.transform.rotation * Quaternion.Euler(0, desiredRotation, 0), Time.deltaTime * 10);
                Vector3 h = new Vector3(hit.point.x, 0.5f, hit.point.z);
                bulletSpawn.LookAt(h);
            }
        }
        else if (player == "Player 2")
        {
            if (Input.GetKey(KeyCode.U))
            {
                //Debug.Log("Aiming");
                //bulletSpawn.transform.Rotate(0, rotationRate * Time.deltaTime, 0); // Aim clockwise

                bulletSpawn.transform.rotation = Quaternion.Euler(new Vector3(0, rotationRate * Time.deltaTime, 0));
            }
            
            if (Input.GetKey(KeyCode.I))
            {
                bulletSpawn.transform.Rotate(0, -rotationRate * Time.deltaTime, 0); // Aim counterclockwise
            }
        }
    }
}
