using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform cameraTransform;
    public Transform Flashlight;
    public float cam_sensitivity = 2f;
    public GameObject Stopped;
    private float camera_vertical_rot = 0f;
    private float camera_horizontal_rot = 0f;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (!Stopped.GetComponent<PlayerController>().isPaused)
        {
            float inputX = Input.GetAxis("Mouse X") * cam_sensitivity;
            float inputY = Input.GetAxis("Mouse Y") * cam_sensitivity;

            camera_vertical_rot -= inputY;
            camera_vertical_rot = Mathf.Clamp(camera_vertical_rot, -80f, 80f);
            cameraTransform.transform.localEulerAngles = Vector3.right * camera_vertical_rot;

            camera_horizontal_rot += inputX;
            player.eulerAngles = new Vector3(0, camera_horizontal_rot, 0);
            cameraTransform.eulerAngles = new Vector3(camera_vertical_rot, camera_horizontal_rot, 0.0f);

            if (Flashlight != null)
            {
                Flashlight.transform.localEulerAngles = Vector3.right * camera_vertical_rot;
                Flashlight.eulerAngles = new Vector3(camera_vertical_rot, camera_horizontal_rot, 0.0f);
            }
        }
    }

    public void ShakeCamera()
    {
        // Implement your camera shake logic here
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = cameraTransform.localPosition;
        float duration = 0.1f;
        float elapsed = 0f;
        float magnitude = 0.1f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }
}
