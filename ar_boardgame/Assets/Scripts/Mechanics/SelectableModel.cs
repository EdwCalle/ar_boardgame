using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class SelectableModel : MonoBehaviour
{
    public GameObject checkbox;
    private ModelObject modelObject;

    public GameObject timer;

    string names;

    public bool isSolution;
    bool stop = false;
    void Start()
    {
        modelObject = new ModelObject();
        modelObject.Name = names;
        modelObject.IsSolution = this.isSolution;
        // checkbox.SetActive(false);
        // checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

    }
    void Update()
    {
        if (timer.GetComponent<CountdownTimer>().isCountingDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        else
        {
            print("go 1");
            if (!stop)
            {
                print("go 2");
                if (timer.GetComponent<CountdownTimer>().showResults)
                {
                    print("go 3");
                    setCorrect();
                }
            }


        }

    }


    public void setCorrect()
    {
        if (modelObject.IsSolution == modelObject.IsSelected)
        {

            checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            checkbox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        }
        stop = true;
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
