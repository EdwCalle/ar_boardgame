
using System.Collections;
using System.Threading.Tasks;
using Models;
using thisAPP.UIElements;
using UnityEngine;

public class RandomSquareManager : MonoBehaviour, IModalAnimator
{
    public GameManager gameManager;
    public RandomSquareUI uiElements;

    public float defaultTime = 5f;
    public float scaleTime = 0.3f;
    //public float speed = 10;
    public async Task ShowModal(GameObject canvasModal)
    {
        uiElements.ShowModal(canvasModal, "randomSquare");
        await Task.Delay(2000);
    }
    public async Task HideModal(GameObject canvasModal)
    {
        gameManager.turnInfoManager.HideTurn(canvasModal);
        uiElements.HideModal(canvasModal, "randomSquare");
        await Task.Delay(2000);
        gameManager.ShowQuestions(SquareType.normal);
    }
    public async void PrepareRandomSquare(int randomNumber)
    {
        SetTextExtraItem(-1);
        if (Singleton.IsMyTurn())
            gameManager.turnInfoManager.PrepareTurnInfo("Tu turno, espera a tu suerte...");
        else
            gameManager.turnInfoManager.PrepareTurnInfo("Esperando a la casilla random del Player" + Singleton.GetCurrentPlayerTurn());
        await ShowModal(gameManager.canvasModal);
        StartCoroutine(RotateRandomSquare(randomNumber));
    }
    IEnumerator RotateRandomSquare(int randomNumber)
    {
        float currentTime = defaultTime;
        while (currentTime > 0)
        {
            currentTime -= scaleTime;
            uiElements.ImageExtraItem.sprite = uiElements.ExtraItems[Random.Range(0, 3)];
            yield return new WaitForSeconds(scaleTime);
        }
        uiElements.ImageExtraItem.sprite = uiElements.ExtraItems[randomNumber];
        SetTextExtraItem(randomNumber);
        gameManager.playersInfoManager.UpdateScore(Singleton.GetCurrentPlayerTurn());
        uiElements.ExtraItem.GetComponent<Animation>().Play("RandomBoxSelected"); //activate diceselected animation
        //uiElements.TitleNumberDice.SetActive(true);     //activate diceTitleNumberBlink animation
        //uiElements.TitleNumberDice.GetComponent<TextMeshProUGUI>().text = $"{numberDice}"; // set number dice in numberDicetText
        yield return new WaitForSeconds(uiElements.ExtraItem.GetComponent<Animation>()["RandomBoxSelected"].length);
        yield return HideModal(gameManager.canvasModal);

    }
    void SetTextExtraItem(int randomDice)
    {
        switch (randomDice)
        {
            case -1: uiElements.Info.text = "Abriendo la caja ................"; break;
            case 0: uiElements.Info.text = "Caja abierta: 1 COMODÍN EXTRA!!! :)"; break;
            case 1: uiElements.Info.text = "Caja abierta: 2 COMODÍN EXTRA!!! :)"; break;
            case 2: uiElements.Info.text = "Caja abierta: PERDISTE 1 COMODÍN >:("; break;

        }

    }
}
