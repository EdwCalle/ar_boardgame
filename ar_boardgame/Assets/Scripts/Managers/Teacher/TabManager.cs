using System.Collections.Generic;
using Models;
using UnityEngine;
public class TabManager : MonoBehaviour
{
    public int page;
    public GameObject modal;
    public GameObject modalWrapper;

    public GameObject table;
    public GameObject row;

    public List<GameObject> rows;


    // Start is called before the first frame update
    void Start()
    {
        modal.transform.SetParent(modalWrapper.transform);
        List();

    }

    // Update is called once per frame
    void Update()
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
        List();
    }
    public void Acept()
    {
        modal.GetComponent<ICrud>().Save();
        CloseModal();
    }
    public void Cancel()
    {
        modal.GetComponent<ICrud>().Save();
        CloseModal();
    }
    public void EditModal()
    {
        ShowModal();
    }
    public void DeleteMotal()
    {
        ShowModal();
    }
    public void List()
    {
        for (int i = 1; i < table.transform.childCount; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);
        }
        modal.GetComponent<ICrud>().List(table, row);
    }








}
