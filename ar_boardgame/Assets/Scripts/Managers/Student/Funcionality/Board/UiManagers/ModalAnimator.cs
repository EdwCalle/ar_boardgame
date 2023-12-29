using System;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]
public class ModalAnimator
{
    [SerializeField] private GameObject modal;
    [Range(0, 10)]
    [SerializeField] private float TimeDelay = 1.050f;
    public GameObject Modal { get => modal; set => modal = value; }

    public async Task HideModal(GameObject canvasModal)
    {
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvasModal.GetComponent<Animator>().SetTrigger("fadeOut");
        await Task.Delay((int)(TimeDelay * 1000));
        Modal.SetActive(false);

    }
    public async Task ShowModal(GameObject canvasModal)
    {
        await Task.Delay((int)(TimeDelay * 1000));
        canvasModal.GetComponent<Animator>().SetTrigger("fadeIn");
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Modal.SetActive(true);

    }
}
