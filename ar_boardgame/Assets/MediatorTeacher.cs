
using Api;
using Models;
using Pages;
using UnityEngine;

public class MediatorTeacher : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public GameObject tab;
        public GameObject tabItem;
        public GameObject content;
        private GameObject table;
        public GameObject modal;

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
    }
    int selectedTab = 0;
    public Tab[] tabs;
    public GameObject tablePrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tab in tabs)
        {
            tab.Table = Instantiate(tablePrefab, tab.content.transform);
            string[] titles = { "prueba 1" + tab.tab.name, "prueba 2" + tab.tab.name };
            Row[] rows = new Row[4];
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new Row
                {
                    Texts = new string[] { $"{tab.tab.name}{i}", $"{tab.tab.name}{i + 1}" }
                };
            }
            tab.Table.GetComponent<TableBuilder>().BuidTable(rows, titles);
        }
        tabs[0].tabItem.GetComponent<CrudMatch>();
    }
    public void OnSelectTab(int value)
    {
        tabs[selectedTab].tabItem.SetActive(false);
        tabs[value].tabItem.SetActive(true);
        selectedTab = value;
    }
    public void OnAddButton()
    {
        tabs[selectedTab].ShowModal();
    }
    public void OnCloseButton()
    {

        // tabs[selectedTab].Crud.Save();
        tabs[selectedTab].HideModal();
    }
}
