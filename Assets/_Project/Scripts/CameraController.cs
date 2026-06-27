using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float sprintMultiplier = 3f;
    public float scrollSpeed = 5f;

    [Header("Rotation")]
    public float rotateSpeed = 0.5f;

    private Vector3 lastMousePosition;
    private bool isRotating = false;

    void Update()
    {
        HandleZoom();

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            HandleRotation();
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= sprintMultiplier;

        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += transform.forward;
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;
        if (Input.GetKey(KeyCode.A)) move -= transform.right;
        if (Input.GetKey(KeyCode.D)) move += transform.right;
        if (Input.GetKey(KeyCode.E)) move += Vector3.up;
        if (Input.GetKey(KeyCode.Q)) move -= Vector3.up;

        transform.position += move * speed * Time.deltaTime;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scroll * scrollSpeed, Space.Self);
    }

    void HandleRotation()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        
        float mouseX = delta.x * rotateSpeed;
        float mouseY = delta.y * rotateSpeed;

        transform.eulerAngles += new Vector3(-mouseY, mouseX, 0);
        
        lastMousePosition = Input.mousePosition;
    }
}