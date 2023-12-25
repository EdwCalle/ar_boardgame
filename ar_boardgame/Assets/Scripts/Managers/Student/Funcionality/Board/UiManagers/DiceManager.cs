
using System.Collections;
using TMPro;
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
    public int numberDice = 1;
    private Vector3 defaultPosition;

    public GameObject swipeAnimation;
    public Image imageDice;
    public GameObject titleNumberDice;

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
    IEnumerator RotateDice()
    {
        while (isRotating)
        {
            rotationObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator ChangeDice()
    {
        while (isRotating)
        {
            imageDice.sprite = dices[Random.Range(0, 6)];
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;

    }
    public void OnDrag(PointerEventData eventData)
    {
        MoveObjectWithSwipe(eventData);
    }
    void MoveObjectWithSwipe(PointerEventData eventData)
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y));
        transform.position = new Vector3(transform.position.x, screenPosition.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("EndDrag");
        if (transform.position.y > limitObject.transform.position.y)
            HandleSwipeUp();
        else
            ResetDice();
    }


    void HandleSwipeUp()
    {
        Debug.Log("Swipe up!");

        transform.position = limitObject.transform.position;
        swipeAnimation.SetActive(false);
        StartCoroutine(RotateDiceTime());
    }
    IEnumerator RotateDiceTime()
    {
        float currentTime = rotatingTime;
        float currentRotationSpeed = rotationSpeed;
        float totalRotation = 0f;
        while (currentTime >= 0) // wait 5 secods to turn the dice
        {
            rotationObject.transform.Rotate(Vector3.forward, currentRotationSpeed * Time.deltaTime);
            currentTime -= Time.deltaTime;
            currentRotationSpeed = Mathf.Lerp(0, rotationSpeed, currentTime);
            totalRotation += currentRotationSpeed * Time.deltaTime;
            if (totalRotation >= 360f)
            {
                numberDice = Random.Range(1, 7);
                imageDice.sprite = dices[numberDice - 1];
                totalRotation = 0f;
            }
            yield return null;
        }
        rotationObject.GetComponent<Animation>().Play("DiceSelected"); //activate diceselected animation
        titleNumberDice.SetActive(true);     //activate diceTitleNumberBlink animation
        titleNumberDice.GetComponent<TextMeshProUGUI>().text = $"{numberDice}"; // set number dice in numberDicetText
        yield return new WaitForSeconds(rotationObject.GetComponent<Animation>()["DiceSelected"].length);
    }
    public void ResetDice()
    {
        transform.position = defaultPosition;
        rotationObject.transform.rotation = Quaternion.identity;
    }



}
