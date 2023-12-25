
using System.Collections;
using thisAPP.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public DiceUi uiElements;

    public float swipeSpeed = 5f;
    public float rotationSpeed = 5f;
    public float rotatingTime = 2f;
    public int numberDice = 1;
    private bool isRotating = false;
    private Vector3 defaultPosition;
    // public GameObject rotationObject;
    // public GameObject limitObject;
    // public Sprite[] dices;
    // public GameObject swipeAnimation;
    // public Image imageDice;
    // public GameObject titleNumberDice;

    void Start()
    {
        defaultPosition = uiElements.Dice.transform.position;

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
            uiElements.RotationObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator ChangeDice()
    {
        while (isRotating)
        {
            uiElements.ImageDice.sprite = uiElements.Dices[Random.Range(0, 6)];
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
        uiElements.Dice.transform.position = new Vector3(uiElements.Dice.transform.position.x, screenPosition.y, uiElements.Dice.transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("EndDrag");
        if (uiElements.Dice.transform.position.y > uiElements.LimitObject.transform.position.y)
            HandleSwipeUp();
        else
            ResetDice();
    }
    void HandleSwipeUp()
    {
        Debug.Log("Swipe up!");

        uiElements.Dice.transform.position = uiElements.LimitObject.transform.position;
        uiElements.SwipeAnimation.SetActive(false);
        StartCoroutine(RotateDiceTime());
    }
    IEnumerator RotateDiceTime()
    {
        float currentTime = rotatingTime;
        float currentRotationSpeed = rotationSpeed;
        float totalRotation = 0f;
        while (currentTime >= 0) // wait 5 secods to turn the dice
        {
            uiElements.RotationObject.transform.Rotate(Vector3.forward, currentRotationSpeed * Time.deltaTime);
            currentTime -= Time.deltaTime;
            currentRotationSpeed = Mathf.Lerp(0, rotationSpeed, currentTime);
            totalRotation += currentRotationSpeed * Time.deltaTime;
            if (totalRotation >= 360f)
            {
                numberDice = Random.Range(1, 7);
                uiElements.ImageDice.sprite = uiElements.Dices[numberDice - 1];
                totalRotation = 0f;
            }
            yield return null;
        }
        uiElements.RotationObject.GetComponent<Animation>().Play("DiceSelected"); //activate diceselected animation
        uiElements.TitleNumberDice.SetActive(true);     //activate diceTitleNumberBlink animation
        uiElements.TitleNumberDice.GetComponent<TextMeshProUGUI>().text = $"{numberDice}"; // set number dice in numberDicetText
        yield return new WaitForSeconds(uiElements.RotationObject.GetComponent<Animation>()["DiceSelected"].length);
    }
    public void ResetDice()
    {
        uiElements.Dice.transform.position = defaultPosition;
        uiElements.RotationObject.transform.rotation = Quaternion.identity;
    }



}
