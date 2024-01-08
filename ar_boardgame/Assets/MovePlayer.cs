using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Speed of player movement
    public float mouseSensitivity = 2.0f; // Sensitivity of mouse movement
    public float ascentSpeed = 5.0f; // Speed of ascending
    public float descentSpeed = 2.0f; // Speed of descending
    CharacterController characterController;
    private float verticalRotation = 0;
    public GameObject playerCamera;
    //public RawImage screen;

    // Start is called before the first frame update


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // playerCamera.transform.parent = transform;
        // RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // Camera.main.targetTexture = renderTexture;
        // screen.texture = renderTexture;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float strafeMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        float ascent = Input.GetKey(KeyCode.Space) ? ascentSpeed : 0;
        float descent = Input.GetKey(KeyCode.LeftShift) ? -descentSpeed : 0;
        Vector3 movement = (transform.forward * forwardMovement) + (transform.right * strafeMovement) + (transform.up * (ascent + descent));
        characterController.Move(movement);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);


    }
    void FixedUpdate()
    {

    }

}
