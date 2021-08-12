using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    //GameObject character;

    //Vector2 rotation;
    //Vector2 smooth;

    //public float sensitivity = 40;
    //public float smoothness = 2;

    public float rotateSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;       // Speed of the camera when being panned

    private Vector3 mouseOrigin;
    private bool isPanning;
    private bool isRotating;

    float mouseWheelScroll;

    // Start is called before the first frame update
    void Start()
    {
        //character = this.transform.parent.gameObject;

        //mousePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        bool isMouseOverGameWindow = !(
                                        0 > Input.mousePosition.x || 0 > Input.mousePosition.y
                                        || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y
                                      );

        // Check right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Check middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(1))
            isRotating = false;
        if (!Input.GetMouseButton(2))
            isPanning = false;

        // Get mouse wheel scroll
        mouseWheelScroll = Input.GetAxis("Mouse ScrollWheel");

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * rotateSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * rotateSpeed);
        }

        // Move camera along X and Y axis
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        // Zoom camera using scroll wheel
        if (mouseWheelScroll != 0 && isMouseOverGameWindow)
        {
            float r = mouseWheelScroll * 15;
            float x = Camera.main.transform.eulerAngles.x + 90;
            float y = -1 * (Camera.main.transform.eulerAngles.y - 90);
            x = x / 180 * Mathf.PI;
            y = y / 180 * Mathf.PI;
            float x2 = r * Mathf.Sin(x) * Mathf.Cos(y);
            float z2 = r * Mathf.Sin(x) * Mathf.Sin(y);
            float y2 = r * Mathf.Cos(x);
            float camX = Camera.main.transform.position.x;
            float camY = Camera.main.transform.position.y;
            float camZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(camX + x2, camY + y2, camZ + z2);
        }

        //Vector2 mouseD = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //mouseD = Vector2.Scale(mouseD, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
        //smooth.x = Mathf.Lerp(smooth.x, mouseD.x, 1f / smoothness);
        //smooth.y = Mathf.Lerp(smooth.y, mouseD.y, 1f / smoothness);
        //rotation += smooth;

        //transform.localRotation = Quaternion.AngleAxis(-rotation.y, Vector3.right);
        //character.transform.localRotation = Quaternion.AngleAxis(rotation.x, character.transform.up);
    }
}
