using Models;
using UnityEngine;

public class TabManager : MonoBehaviour, ICrudPage
{
    public int page;
    public GameObject modal;
    public GameObject modalWrapper;


    // Start is called before the first frame update
    void Start()
    {
        modal.transform.SetParent(modalWrapper.transform);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ICrudPage.Add()
    {

    }

    public void ShowModal()
    {
        modalWrapper.SetActive(true);
        modal.SetActive(true);
    }
    public void CloseModal()
    {
        modalWrapper.SetActive(false);
        modal.SetActive(false);
    }
    public void Acept()
    {
        //modal.GetComponent<ICrudPage>().Save();
        CloseModal();
    }
    public void Cancel()
    {
        //modal.GetComponent<ICrudPage>().Save();
        CloseModal();
    }






}
