using System;
using UnityEngine;
[Serializable]
public class Modal
{
    [SerializeField] private GameObject modalUI;
    // [Range(0, 10)]
    // [SerializeField] private float TimeDelay = 1.050f;
    public GameObject ModalUI { get => modalUI; set => modalUI = value; }

    public void HideModal(GameObject canvasModal, string message)
    {
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvasModal.GetComponent<Animator>().SetTrigger("fadeOut");


        //await Task.Delay((int)(TimeDelay * 1000));
        ModalUI.SetActive(false);

        Debug.Log($"hide:{message}");
    }
    public void ShowModal(GameObject canvasModal, string message)
    {
        // await Task.Delay((int)(TimeDelay * 1000));
        canvasModal.GetComponent<Animator>().SetTrigger("fadeIn");
        canvasModal.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ModalUI.SetActive(true);
        Debug.Log($"show:{message}");
    }
}
