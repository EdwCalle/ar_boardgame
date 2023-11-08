using System.Collections.Generic;
using System.Diagnostics;
using Models;
using UnityEngine;
using UnityEngine.UIElements;
public class TabManager : MonoBehaviour
{
    public int page;
    public GameObject modal;
    public GameObject modalWrapper;

    public GameObject table;
    public GameObject row;


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


    public void SwitchModal(bool value)
    {
        modalWrapper.SetActive(value);
        modal.SetActive(value);
    }
    public void ShowModal(EditMode editMode, int code)
    {
        modal.GetComponent<ICrud>().ClearModal();
        modal.GetComponent<ICrud>().PrepareModal(editMode, code);
        SwitchModal(true);

    }
    public void CloseModal()
    {
        SwitchModal(false);
        List();
    }

    public void CreateModal()
    {
        // if this 
        ShowModal(EditMode.create, 0);
    }
    public void EditModal(RowManager rowManager)
    {
        ShowModal(EditMode.edit, rowManager.code);
    }
    public void DeleteMotal()
    {
        ShowModal(EditMode.delete, 0);
    }
    public void Acept()
    {
        modal.GetComponent<ICrud>().Save();
        CloseModal();
    }
    public void Cancel()
    {
        CloseModal();
    }
    // create a table manager for separate
    public void List()
    {
        for (int i = 1; i < table.transform.childCount; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);
        }
        modal.GetComponent<ICrud>().List(table, row);
    }








}
