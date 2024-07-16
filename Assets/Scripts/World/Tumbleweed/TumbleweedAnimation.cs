using UnityEngine;
using System;

public class TumbleweedAnimation : MonoBehaviour
{
    public Vector3 startPoint; // Starting point of the tumbleweed
    public Vector3 endPoint; // Ending point of the tumbleweed
    public float moveDuration = 10f; // Duration of the movement from start to end
    public float rollSpeed = 50f; // Speed of the rolling effect
    public AnimationCurve jumpCurve; // Curve defining the jump motion
    public int jumpCount = 3; // Number of jumps

    public event Action OnAnimationEnd;

    private float moveTime = 0f;
    private Vector3 initialLocalPosition;
    private bool animationEnded = false;

    void Start()
    {
        // Record the initial local position to use as the starting point
        initialLocalPosition = transform.localPosition;
        startPoint = initialLocalPosition;
    }

    void Update()
    {
        if (!animationEnded)
        {
            // Calculate the normalized time of the movement (0 to 1)
            moveTime += Time.deltaTime / moveDuration;
            if (moveTime > 1f)
            {
                moveTime = 1f; // Clamp to 1 to stop moving
                animationEnded = true;
                OnAnimationEnd?.Invoke();
            }

            // Move the tumbleweed from start to end
            Vector3 localEndPoint = initialLocalPosition + endPoint;
            transform.localPosition = Vector3.Lerp(startPoint, localEndPoint, moveTime);

            // Rotate the tumbleweed to create the rolling effect
            transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime);

            // Handle jumping using the jump curve
            float jumpProgress = (moveTime * jumpCount) % 1f; // Repeat the curve over jumpCount times
            float jumpHeight = jumpCurve.Evaluate(jumpProgress);
            transform.localPosition = new Vector3(transform.localPosition.x, initialLocalPosition.y + jumpHeight, transform.localPosition.z);
        }
    }
}
