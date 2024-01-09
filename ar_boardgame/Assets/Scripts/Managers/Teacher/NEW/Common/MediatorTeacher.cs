
using System;
using Models;
using Pages;
using UnityEngine;

public class MediatorTeacher : MonoBehaviour
{
    [Serializable]
    public class Tab
    {
        public GameObject tab;
        public GameObject tabItem;
        public GameObject content;

        private GameObject table;
        public GameObject modal;
        public string[] titles;
        public CellProperties[] cellProperties;
        public CellProperties[] cellPropertiesRow;
        private ICrud crud;

        public GameObject Table { get => table; set => table = value; }
        public ICrud Crud { get => crud; set => crud = value; }
        public void ShowModal()
        {

            modal.SetActive(true);
        }
        public void HideModal()
        {
            modal.SetActive(false);
        }
        public void BuildHeader()
        {
            Table.GetComponent<TableBuilder>().BuildHeader(titles, cellProperties);
        }
        public void List()
        {

            Row[] rows = crud.List();
            if (cellPropertiesRow.Equals(null))
                Table.GetComponent<TableBuilder>().BuildBody(rows, Edit, Delete, cellPropertiesRow);
            else
                Table.GetComponent<TableBuilder>().BuildBody(rows, Edit, Delete, cellProperties);
        }
        public void Edit(int code)
        {
            crud.ClearModal();
            crud.PrepareModal(EditMode.edit, code);
            ShowModal();
        }
        public void Delete(int code)
        {
            crud.PrepareModal(EditMode.delete, code);
            List();
        }
    }
    int selectedTab = 0;
    public Tab[] tabs;
    public GameObject tablePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Test();
        tabs[0].Crud = tabs[0].tabItem.GetComponent<CrudMatch>();
        tabs[0].List();
    }
    public void OnSelectTab(int value)
    {
        tabs[selectedTab].tabItem.SetActive(false);
        tabs[value].tabItem.SetActive(true);
        selectedTab = value;
    }
    public void OnAddButton()
    {
        tabs[selectedTab].Crud.ClearModal();
        tabs[selectedTab].Crud.PrepareModal(EditMode.create);
        tabs[selectedTab].ShowModal();
    }
    public void OnSaveButton()
    {
        tabs[selectedTab].Crud.Save();
        OnCloseButton();
    }
    public void OnCloseButton()
    {
        tabs[selectedTab].Crud.ClearModal();
        tabs[selectedTab].List();
        tabs[selectedTab].HideModal();
    }
    void Test()
    {

        foreach (var tab in tabs)
        {

            tab.Table = Instantiate(tablePrefab, tab.content.transform);
            if (tab.titles != null)
                tab.BuildHeader();
        }

    }

}
