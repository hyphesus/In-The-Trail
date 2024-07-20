using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform player; // Reference to the camera's transform
    public GameObject health;
    public float jumpForce = 5f; // Jump force
    public float groundDistance = 0.2f; // Distance to check for the ground
    public LayerMask groundMask; // Layer mask to specify what is ground
    public float speed = 0.04f; // Movement speed
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded; // Is the player grounded
    
    public bool isMoving;
    
    public LayerMask collisionMask;
    public float cameraDistance = 0.35f;

    
    public bool isPaused = false;
    public GameObject escMenu;
    public bool easyMode;
    private bool isTakingDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        escMenu.SetActive(false);
        Time.timeScale = 1f;
        if(health != null){
            health.SetActive(true);
        }
        
    }

    void FixedUpdate()
    {
        //isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        //print(isGrounded);

        if(!isPaused){
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
        else if (collision.gameObject.CompareTag("Spike") && !isTakingDamage)
        {
            StartCoroutine(ApplyContinuousDamage());
        }

    }
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Spike"))
        {
            print("damage taken");
            StopCoroutine(ApplyContinuousDamage());
            isTakingDamage = false;
        }
    }

    IEnumerator ApplyContinuousDamage()
    {
        isTakingDamage = true;
        while(isTakingDamage){
            health.GetComponent<Health>().TakeDamage(1); 
            yield return new WaitForSeconds(2f);
        }
            
    }
    public void Resume()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        health.SetActive(true);
    }

    void Pause()
    {
        escMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        health.SetActive(false);
    }
    /*void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")){
            isGrounded=true;
        }
    }*/
}

