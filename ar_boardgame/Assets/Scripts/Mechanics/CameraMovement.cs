using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Speed of player movement
    public float mouseSensitivity = 2.0f; // Sensitivity of mouse movement
    public float ascentSpeed = 5.0f; // Speed of ascending
    public float descentSpeed = 2.0f; // Speed of descending

    private CharacterController characterController;
    private float verticalRotation = 0;
    private Camera playerCamera;
    private bool isRotating;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main; // Access the main camera in the scene
                                    //  Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the game window
    }

    void Update()
    {
        // Player flying
        float forwardMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float strafeMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float ascent = Input.GetKey(KeyCode.Space) ? ascentSpeed : 0;
        float descent = Input.GetKey(KeyCode.LeftShift) ? -descentSpeed : 0;

        Vector3 movement = (transform.forward * forwardMovement) + (transform.right * strafeMovement) + (transform.up * (ascent + descent));
        characterController.Move(movement);

        // Player rotation (looking around)
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
