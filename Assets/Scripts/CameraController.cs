using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    public float cam_sensitivity = 2f;

    private float camera_vertical_rot = 0f;
    private float camera_horizontal_rot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X")*cam_sensitivity;
        float inputY = Input.GetAxis("Mouse Y")*cam_sensitivity;

        camera_vertical_rot -= inputY;
        camera_vertical_rot = Mathf.Clamp(camera_vertical_rot, -80f, 80f);
        transform.localEulerAngles = Vector3.right*camera_vertical_rot;

        camera_horizontal_rot += inputX;
        transform.eulerAngles = new Vector3(camera_vertical_rot, camera_horizontal_rot, 0.0f);
    }
}
