using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform player;
    public bool isDashing; // Is the player dashing
    public float dashDistance = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.5f; //dash time
    public float dashCooldown = 0.5f; // Cooldown duration
    private float lastDashTime; // Last time the dash was executed
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            // Check combinations of keys for dash direction
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dashing(player.forward + player.right)); // Right-Forward
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dashing(player.forward - player.right)); // Left-Forward
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dashing(player.right)); // Just Right
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dashing(-player.right)); // Just Left
            }
            else
            {
                StartCoroutine(Dashing(player.forward)); // Just Forward
            }
        }
    }

    IEnumerator Dashing(Vector3 direction)
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
}
