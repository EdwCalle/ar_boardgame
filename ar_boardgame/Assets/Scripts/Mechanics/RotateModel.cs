using System.Collections;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public bool isRotating = false;
    private Vector3 lastMousePosition;
    // public GameObject hitbox;
    Quaternion DefaultRotation;
    public float rotationThreshold = 0.1f;

    void Start()
    {
        DefaultRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        // Check for touch or mouse input
        if (Input.GetMouseButtonDown(0) && IsPointerOverObject())
        {
            StartCoroutine(CheckForDrag(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            transform.rotation = DefaultRotation;
        }

        if (isRotating)
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            RotateObjectWithInput(deltaMouse);
            lastMousePosition = Input.mousePosition;
        }
    }
    IEnumerator CheckForDrag(Vector2 startPosition)
    {
        // Wait for a short duration to see if it's a click or a drag
        yield return new WaitForSeconds(0.2f);

        // If the input is still active, it's a drag; otherwise, it's a click
        // if ((Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended) || Input.GetMouseButton(0))
        if (Input.GetMouseButton(0))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            GetComponent<SelectableModel>().SelectModel();
            // Handle single click behavior here
            Debug.Log("Single Click!");
        }
    }


    private void RotateObjectWithInput(Vector3 delta)
    {
        // Adjust the rotation speed as needed
        float rotationSpeed = 1.0f;
        //if (Mathf.Abs(delta.x) > rotationThreshold || Mathf.Abs(delta.y) > rotationThreshold)
        {
            // Rotate the object
            transform.Rotate(Vector3.up, -delta.x * rotationSpeed, Space.World);
            transform.Rotate(Vector3.right, delta.y * rotationSpeed, Space.World);
        }


    }

    private bool IsPointerOverObject()
    {
        // Check if the pointer is over the object using a raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject == transform.gameObject;
        }

        return false;
    }
}
