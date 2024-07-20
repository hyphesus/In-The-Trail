using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{
    public Text messageText; // Reference to the UI Text component
    public float displayDuration = 4f; // Duration to display the message

    private void Start()
    {
        // Ensure the message is not visible initially
        messageText.gameObject.SetActive(true);
        StartCoroutine(DisplayMessageCoroutine());
    }

    private IEnumerator DisplayMessageCoroutine()
    {
        // Show the message
        messageText.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the message
        messageText.gameObject.SetActive(false);
    }
}
