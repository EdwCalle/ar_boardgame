using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TableBuilder : MonoBehaviour
{
    public Transform header;
    public Transform body;
    public GameObject cellPrefab;
    public GameObject rowPrefab;
    public GameObject editButtonPrefab;
    public CellProperties[] cellProperties;

    public Sprite[] sprites = new Sprite[2];

    public void BuildRow(string[] texts, Transform parent)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            var cell = Instantiate(cellPrefab, parent);
            cell.GetComponent<CellManager>().BuildCell(text: texts[i], cellProperties[i].FontSize, cellProperties[i].Width);
        }
    }
    public void BuildButton(Transform parent, string type, UnityAction<int> action, int id)
    {
        var button = Instantiate(editButtonPrefab, parent);
        button.GetComponent<Button>().onClick.AddListener(() => action.Invoke(id));
        if (type == "edit")
        {
            button.GetComponent<Image>().sprite = sprites[0];
        }
        if (type == "delete")
        {
            button.GetComponent<Image>().sprite = sprites[1];
        }
    }
    public void BuildBody(Row[] rows, UnityAction<int> editAction, UnityAction<int> deleteAction, CellProperties[] cellProperties)
    {
        this.cellProperties = cellProperties;
        for (int i = 0; i < body.childCount; i++)
        {
            Destroy(body.transform.GetChild(i).gameObject);
        }
        foreach (Row row in rows)
        {
            var instance = Instantiate(rowPrefab, body);
            BuildRow(row.Cells, instance.transform);
            BuildButton(instance.transform, "edit", editAction, row.Id);
            BuildButton(instance.transform, "delete", deleteAction, row.Id);
        }
    }
    public void BuildHeader(string[] titles, CellProperties[] cellProperties)
    {
        this.cellProperties = cellProperties;
        BuildRow(titles, header);
    }

}
