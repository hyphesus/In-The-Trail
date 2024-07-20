using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform player; // Reference to the camera's transform
    public float jumpForce = 5f; // Jump force
    public float groundDistance = 0.2f; // Distance to check for the ground
    public LayerMask groundMask; // Layer mask to specify what is ground
    public float speed = 0.06f; // Movement speed
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded; // Is the player grounded
    public bool isDashing; // Is the player dashing
    
    public bool isMoving;
    public float dashDistance = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.5f; //dash time
    public LayerMask collisionMask;
    public float cameraDistance = 0.35f;

    public float dashCooldown = 0.5f; // Cooldown duration
    private float lastDashTime; // Last time the dash was executed
    public bool isPaused = false;
    public GameObject escMenu;
    public bool easyMode = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        escMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    void FixedUpdate()
    {
        //isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        //print(isGrounded);

        if(!isDashing && !isPaused){
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Combine the inputs to create a direction vector
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            
            if (direction.magnitude >= 0.1f)
            {
                // Calculate the angle to rotate the player based on the camera's rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.eulerAngles.y;
    
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // Move the player
                //transform.Translate(moveDir.normalized * speed, Space.World);
                rb.MovePosition(transform.position + moveDir.normalized * speed );
                isMoving = true;
            }
            else{
                isMoving = false;
            }
        }
        else{
            isMoving = false;
        }   
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            /*if(Input.GetKeyDown(KeyCode.W)){
                Vector3 frontJump = transform.forward + Vector3.up;
                rb.AddForce(frontJump * jumpForce, ForceMode.Impulse);
            }
            else{*/
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            //}
            isGrounded =false;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            // Check combinations of keys for dash direction
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(player.forward + player.right)); // Right-Forward
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(player.forward - player.right)); // Left-Forward
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(player.right)); // Just Right
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(-player.right)); // Just Left
            }
            else
            {
                StartCoroutine(Dash(player.forward)); // Just Forward
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        AdjustCameraPosition();
    }

    IEnumerator Dash(Vector3 direction)
    {
        isDashing = true;

        // Normalize the direction to ensure it only affects X and Z axis
        direction = new Vector3(direction.x, 0, direction.z).normalized;

        // Store the initial velocity
        Vector3 initialVelocity = rb.velocity;

        // Apply a high force for the duration of the dash
        float dashEndTime = Time.time + dashDuration;
        while (Time.time < dashEndTime)
        {
            rb.velocity = direction * dashSpeed;
            yield return null;
        }

        // Restore initial velocity after dash
        rb.velocity = initialVelocity;

        // Stop dashing
        isDashing = false;
        lastDashTime = Time.time;
    }
    void AdjustCameraPosition()
    {
        RaycastHit hit;

        if (Physics.Raycast(rb.position, player.forward, out hit, cameraDistance, collisionMask))
        {
            print("ray cast hit");
            rb.MovePosition(rb.position - player.forward * cameraDistance); // Adjust to a small offset from the hit point
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item")){
            rb.velocity = Vector3.zero;
            
        }
        else if (collision.gameObject.CompareTag("Terrain")){
            isGrounded=true;
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            print("damage taken");
            //HealthManager.instance.TakeDamage(1); // Adjust damage value as needed
        }
    }
    public void Resume()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        escMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }
    /*void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")){
            isGrounded=true;
        }
    }*/
}

