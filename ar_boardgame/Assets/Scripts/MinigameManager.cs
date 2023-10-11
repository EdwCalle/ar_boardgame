
using System.Collections;
using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
using UnityGLTF;

public class MinigameManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject modelPrefab;

    // for level 1
    public GameObject image;

    int counterQuestion = 0;

    List<GameObject> models = new List<GameObject>();

    public TextMeshProUGUI optionA;
    public TextMeshProUGUI optionB;
    public TextMeshProUGUI optionC;
    public TextMeshProUGUI optionD;

    public TextMeshProUGUI counterText;
    public GameObject minigame2;
    public GameObject finalizarButton;

    // Start is called before the first frame update
    void Start()
    {

        if (Singleton.instance.currentMinigame.Code == 1)
        {


            minigame2.SetActive(false);
            counterText.text = "--";
            //preload all models
            foreach (ModelObject item in Singleton.instance.currentMinigame.Models)
            {

                print(item.Name);
                modelPrefab.transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().GLTFUri = item.Url;
                modelPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().center = item.GetColliderCenter();
                modelPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size = item.GetColliderScale();
                Instantiate(modelPrefab, item.GetPosition(), Quaternion.identity);



                //en base al minijuego los modelos 3D se les asignarán los componentes que neseciten
                // un error esperar que se destruyan todos los modelos cuando se cambia de escena
            }
        }

        if (Singleton.instance.currentMinigame.Code == 4)
        {
            modelPrefab.SetActive(false);
            foreach (ModelObject item in Singleton.instance.currentMinigame.Models)
            {

                print(item.Name);
                modelPrefab.transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().GLTFUri = item.Url;
                modelPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().center = item.GetColliderCenter();
                modelPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size = item.GetColliderScale();

                models.Add(Instantiate(modelPrefab, new Vector3(0, 0, 1f), Quaternion.identity));



                //en base al minijuego los modelos 3D se les asignarán los componentes que neseciten
                // un error esperar que se destruyan todos los modelos cuando se cambia de escena
            }
            finalizarButton.SetActive(false);
            image.SetActive(false);
            models[0].SetActive(true);

            setData(0);
        }


        //
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectOption(int option)
    {
        counterText.text = (counterQuestion + 1) + "/3";
        if (counterQuestion == Singleton.instance.currentMinigame.Questions.Count - 1)
        {
            //show finalizar
            finalizarButton.SetActive(true);
            minigame2.SetActive(false);
        }
        else
        {

            models[counterQuestion].SetActive(false);
            counterQuestion++;
            models[counterQuestion].SetActive(true);
            setData(counterQuestion);
        }
    }
    public void setData(int pos)
    {
        title.text = Singleton.instance.currentMinigame.Questions[pos].Title;
        optionA.text = Singleton.instance.currentMinigame.Questions[pos].OptionA;
        optionB.text = Singleton.instance.currentMinigame.Questions[pos].OptionB;
        optionC.text = Singleton.instance.currentMinigame.Questions[pos].OptionC;
        optionD.text = Singleton.instance.currentMinigame.Questions[pos].OptionD;
    }
}
