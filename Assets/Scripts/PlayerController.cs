using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform player; // Reference to the camera's transform
    public float jumpForce = 3f; // Jump force
    public float groundDistance = 0.2f; // Distance to check for the ground
    public LayerMask groundMask; // Layer mask to specify what is ground
    [SerializeField] public float speed = 0.02f; // Movement speed
    
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded; // Is the player grounded



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

    
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
            transform.Translate(moveDir.normalized * speed, Space.World);
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

