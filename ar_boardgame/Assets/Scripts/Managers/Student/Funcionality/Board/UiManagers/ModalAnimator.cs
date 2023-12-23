using UnityEngine;
[System.Serializable]
public class ModalAnimator
{
    [SerializeField] private GameObject modal;
    public GameObject Modal { get => modal; set => modal = value; }

    public void HideModal(GameObject canvasModal)
    {
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvasModal.GetComponent<Animator>().SetTrigger("fadeOut");
        Modal.SetActive(false);

    }
    public void ShowModal(GameObject canvasModal)
    {
        canvasModal.GetComponent<Animator>().SetTrigger("fadeIn");
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Modal.SetActive(true);
    }
}
