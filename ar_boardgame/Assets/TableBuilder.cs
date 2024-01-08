
using System;
using Models;
using UnityEngine;

public class TableBuilder : MonoBehaviour
{
    public Transform header;
    public Transform body;
    public GameObject cellPrefab;
    public GameObject rowPrefab;

    public void BuildCell(string[] texts, Transform parent)
    {
        foreach (string text in texts)
        {
            var cell = Instantiate(cellPrefab, parent);
            cell.GetComponent<CellManager>().BuildCell(text: text);
        }
    }
    public void BuidTable(Row[] rows, string[] titles)
    {
        foreach (Row row in rows)
        {
            var instance = Instantiate(rowPrefab, body);
            BuildCell(row.Texts, instance.transform);
        }
        BuildCell(titles, header);
    }

}
