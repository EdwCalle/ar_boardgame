using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private bool isRotating = false;
    private Vector3 lastMousePosition;
    public GameObject hitbox;

    // Update is called once per frame
    void Update()
    {
        // Check for touch or mouse input
        if (Input.GetMouseButtonDown(0) && IsPointerOverObject())
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            RotateObjectWithInput(deltaMouse);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void RotateObjectWithInput(Vector3 delta)
    {
        // Adjust the rotation speed as needed
        float rotationSpeed = 1.0f;

        // Rotate the object
        transform.Rotate(Vector3.up, -delta.x * rotationSpeed, Space.World);
        transform.Rotate(Vector3.right, delta.y * rotationSpeed, Space.World);
    }

    private bool IsPointerOverObject()
    {
        // Check if the pointer is over the object using a raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject == hitbox;
        }

        return false;
    }
}
