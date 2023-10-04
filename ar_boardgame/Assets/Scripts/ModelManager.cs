using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
public class ModelManager : MonoBehaviour
{
    public GameObject checkbox;
    private ModelObject modelObject;
    public Camera camara;

    string names;
    void Start()
    {
        modelObject = new ModelObject();
        modelObject.Name = names;
        // checkbox.SetActive(false);
        // checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camara.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == checkbox)
                {
                    // This object was clicked.
                    // Put your click logic here.
                    print("selected model" + modelObject.Name);
                    selectModel();
                }
            }
        }
    }


    public void setCorrect()
    {
        if (!modelObject.IsSelected)
        {
            checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            modelObject.IsSelected = true;
        }
        else
        {
            checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            modelObject.IsSelected = false;
        }
        //  if (se)
    }
    public void selectModel()
    {
        if (!modelObject.IsSelected)
        {
            checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            modelObject.IsSelected = true;
        }
        else
        {
            checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            modelObject.IsSelected = false;
        }
        //  if (se)
    }


}
