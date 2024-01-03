
using System.Collections;
using System.Threading.Tasks;
using thisAPP.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DiceManager : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IModalAnimator
{
    public DiceUi uiElements;
    public float swipeSpeed = 5f;
    public float rotationSpeed = 5f;
    public float rotatingTime = 2f;
    public int numberDice = 1;
    private bool isRotating = false;
    private Vector3 defaultPosition;
    public GameManager gameManager;

    void Awake()
    {
        //uiElements.Dice.transform.localPosition = defaultPosition;
        defaultPosition = uiElements.Dice.transform.position;
        //  print("no se que estoy haciendo mal" + defaultPosition);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRotating && Singleton.IsMyTurn() && uiElements.Dice.GetComponent<BoxCollider2D>().enabled)
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

        if (Singleton.IsMyTurn() && uiElements.Dice.GetComponent<BoxCollider2D>().enabled)
            isRotating = false;
    }
    public void OnDrag(PointerEventData eventData)
    {

        if (Singleton.IsMyTurn() && uiElements.Dice.GetComponent<BoxCollider2D>().enabled)
            MoveObjectWithSwipe(eventData);
    }
    void MoveObjectWithSwipe(PointerEventData eventData)
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y));
        uiElements.Dice.transform.position = new Vector3(uiElements.Dice.transform.position.x, screenPosition.y, uiElements.Dice.transform.position.z);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Singleton.IsMyTurn() && uiElements.Dice.GetComponent<BoxCollider2D>().enabled)
        {
            if (uiElements.Dice.transform.position.y > uiElements.LimitObject.transform.position.y)
                HandleSwipeUp();
            else
                RestartDice();
        }
        //print("EndDrag");
    }
    void HandleSwipeUp()
    {
        Debug.Log("Swipe up!");
        uiElements.Dice.transform.position = uiElements.LimitObject.transform.position; // center dice 
        uiElements.Dice.GetComponent<BoxCollider2D>().enabled = false;                  // one throw dice
        uiElements.SwipeAnimation.SetActive(false);                                       // disable animation of title "throw dice"
        numberDice = Random.Range(1, 7);
        gameManager.RollDice(numberDice);
    }
    public IEnumerator RotateDiceTime(int definedDice)
    {
        uiElements.Dice.transform.position = uiElements.LimitObject.transform.position; // center dice 
        numberDice = definedDice;
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
                uiElements.ImageDice.sprite = uiElements.Dices[Random.Range(0, 6)];
                totalRotation = 0f;
            }
            yield return null;
        }
        uiElements.ImageDice.sprite = uiElements.Dices[numberDice - 1];// set all dice for every one
        uiElements.RotationObject.GetComponent<Animation>().Play("DiceSelected"); //activate diceselected animation
        uiElements.TitleNumberDice.SetActive(true);     //activate diceTitleNumberBlink animation
        uiElements.TitleNumberDice.GetComponent<TextMeshProUGUI>().text = $"{numberDice}"; // set number dice in numberDicetText
        yield return new WaitForSeconds(uiElements.RotationObject.GetComponent<Animation>()["DiceSelected"].length);
        gameManager.ThrowDice(numberDice);
    }
    public void RestartDice()
    {
        uiElements.Dice.transform.position = defaultPosition;
        uiElements.RotationObject.transform.rotation = Quaternion.identity;
    }
    public void ResetDice()
    {
        RestartDice();
        uiElements.TitleNumberDice.SetActive(false);
        uiElements.TitleNumberDice.GetComponent<TextMeshProUGUI>().text = $"-";
        Color color = uiElements.RotationObject.GetComponent<Image>().color;
        color.a = 0;
        uiElements.RotationObject.GetComponent<Image>().color = color;
    }

    public void SetDiceInfo()
    {
        ResetDice();
        if (Singleton.IsMyTurn())
        {
            uiElements.TitleTurn.text = "Es tu turno";
            uiElements.SwipeAnimation.SetActive(true);
        }
        else
        {
            uiElements.SwipeAnimation.SetActive(false);
            uiElements.TitleTurn.text = $"Player{Singleton.GetCurrentPlayerTurn()} está lanzando el dado";
        }
    }

    public async Task ShowModal(GameObject canvasModal)
    {

        uiElements.ShowModal(canvasModal);
        SetDiceInfo();
        await Task.Delay(1050);
    }

    public async Task HideModal(GameObject canvasModal)
    {
        uiElements.HideModal(canvasModal);
        await Task.Delay(1050);
    }
}
