
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isRotating = false;
    public float swipeSpeed = 5f;
    public float rotationSpeed = 5f;
    public float rotatingTime = 2f;
    public GameObject rotationObject;
    public GameObject limitObject;
    public Sprite[] dices;
    private Vector3 defaultPosition;


    void Start()
    {
        defaultPosition = transform.position;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateDice());
            StartCoroutine(ChangeDice());
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;
        //    print("Up");

    }
    public void OnDrag(PointerEventData eventData)
    {
        MoveObjectWithSwipe(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("EndDrag");
        if (transform.position.y > limitObject.transform.position.y)
        {
            HandleSwipeUp();
        }
        else
        {
            transform.position = defaultPosition;
            rotationObject.transform.rotation = Quaternion.identity;
        }
    }



    void HandleSwipeUp()
    {
        Debug.Log("Swipe up!");
        transform.position = limitObject.transform.position;
        StartCoroutine(RotateDiceTime());
        // Add your custom action here, e.g., triggering an animation, moving an object, etc.
    }

    void MoveObjectWithSwipe(PointerEventData eventData)
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y));
        transform.position = new Vector3(transform.position.x, screenPosition.y, transform.position.z);
    }
    IEnumerator RotateDice()
    {
        while (isRotating)
        {
            rotationObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator RotateDiceTime()
    {
        float currentTime = rotatingTime;
        float currentRotationSpeed = rotationSpeed;
        float totalRotation = 0f;
        while (currentTime >= 0)
        {
            rotationObject.transform.Rotate(Vector3.forward, currentRotationSpeed * Time.deltaTime);
            currentTime -= Time.deltaTime;
            currentRotationSpeed = Mathf.Lerp(0, rotationSpeed, currentTime);
            totalRotation += currentRotationSpeed * Time.deltaTime;
            if (totalRotation >= 360f)
            {
                rotationObject.GetComponent<Image>().sprite = dices[UnityEngine.Random.Range(0, 6)];
                totalRotation = 0f;
            }
            yield return null;
        }
    }
    IEnumerator ChangeDice()
    {
        while (isRotating)
        {
            rotationObject.GetComponent<Image>().sprite = dices[UnityEngine.Random.Range(0, 6)];
            yield return new WaitForSeconds(0.1f);
        }
    }



}
