using System.Collections.Generic;
using System.IO;
using Api;
using ExitGames.Client.Photon.StructWrapping;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrudMinigameManager : MonoBehaviour
{
    public Minigame currentMinigame = new();
    public int Level = 1;
    public GameObject[] modallevels = new GameObject[3];
    public GameObject modalSelected;
    public GameObject modalNoSelected;
    public GameObject modalModels;
    public Transform contentRowModel;
    public GameObject rowModelPrefab;

    public List<Model3D> selectedModels = new();
    public List<Model3D> Allmodels = new();

    public List<GameObject> selectedModelsGM;
    public GameObject modelGLTFPrefab;
    public int LastIndexModal = -1;
    public int LastIndexModels = -1;
    // Start is called before the first frame update

    public TextMeshProUGUI nameLevel;
    public TMP_InputField[] times = new TMP_InputField[2];
    void Start()
    {
        //  ShowMinigame();
        Allmodels = CrudModels.GetModels3D();


        nameLevel.text = $"Datos del Minijuego (Nivel {Level})";
        times[0].text = currentMinigame.ResolveTime + "";
        times[1].text = currentMinigame.FeedbackTime + "";
    }
    void ShowMinigame()
    {
        modalNoSelected.SetActive(false);
        modalSelected.SetActive(true);
        modallevels[Level - 1].SetActive(true);
    }
    void HideMinigame()
    {
        modalNoSelected.SetActive(true);
        modalSelected.SetActive(false);
        modallevels[Level - 1].SetActive(false);
    }
    // Minigame info
    public TextMeshProUGUI nameModel;

    public RawImage miniature;
    public Toggle isSolution;
    public TMP_InputField questionTitle;
    public TMP_Dropdown optionSelected;
    public TMP_InputField[] options = new TMP_InputField[4];
    public async void AddModel(Model3D model3D)
    {
        LoadDataModel(model3D);
        GameObject instance = Instantiate(modelGLTFPrefab);
        instance.GetComponent<Model3DTManager>().ItemManagerBuilder(model3D.Code, model3D, modelInBox: model3D.ModelInBox, colliderSize: model3D.ColliderScale, url: model3D.Url, SelectModel);
        await instance.GetComponent<Model3DTManager>().LoadModel();
        selectedModels.Add(model3D);
        selectedModelsGM.Add(instance);
        SelectModel(model3D.Code);

        if (LastIndexModal != -1)
            ShowMinigame();
        HideModels();
    }
    public void LoadDataModel(Model3D model3D)
    {
        nameModel.text = "Datos del Modelo:" + model3D.Name;
        miniature.texture = LoadTextureFromFile(model3D.UrlMiniature);
        // times[0].text = currentMinigame.ResolveTime + "";
        //times[1].text = currentMinigame.FeedbackTime + "";
        if (Level == 1)
        {
            isSolution.isOn = model3D.IsSolution;
        }
        if (Level == 3)
        {
            questionTitle.text = model3D.Question.Title;
            options[0].text = model3D.Question.OptionA;
            options[1].text = model3D.Question.OptionB;
            options[2].text = model3D.Question.OptionC;
            options[3].text = model3D.Question.OptionD;
            optionSelected.value = model3D.Question.Solution;
        }
    }
    public void DeleteSelectedModel()
    {
        if (LastIndexModels != -1)
        {
            int index = selectedModels.FindIndex(ob => ob.Code == LastIndexModels);
            selectedModels.RemoveAt(index);

            GameObject instance = selectedModelsGM.Find(ob => ob.GetComponent<Model3DTManager>().code == LastIndexModels);
            Destroy(instance);
            selectedModelsGM.Remove(instance);

        }
        LastIndexModels = -1;
        HideMinigame();
        ClearModelInfo();
    }
    public void ClearModelInfo()
    {
        nameLevel.text = "Datos del Modelo:";
        miniature.texture = null;
        // times[0].text = currentMinigame.ResolveTime + "";
        //times[1].text = currentMinigame.FeedbackTime + "";
        // if (Level == 1)
        // {
        //     isSolution.isOn = false;
        // }
        // if (Level == 3)
        // {
        //     questionTitle.text = "";
        //     options[0].text = "";
        //     options[1].text = "";
        //     options[2].text = "";
        //     options[3].text = "";
        //     optionSelected.value = 0;
        // }
    }
    public void SelectModel(int code)
    {
        ShowMinigame();
        print("what's going on?" + code);
        if (LastIndexModels == code)
            return;
        if (LastIndexModels == -1)
        {
            selectedModelsGM.Find(ob => ob.GetComponent<Model3DTManager>().code == code).GetComponent<Model3DTManager>().SelectModel(); ;
        }
        else
        {
            //selectedModelsGM[LastIndexModels].GetComponent<Model3DTManager>().UnselectModel();
            selectedModelsGM.Find(ob => ob.GetComponent<Model3DTManager>().code == LastIndexModels).GetComponent<Model3DTManager>().UnselectModel();
            selectedModelsGM.Find(ob => ob.GetComponent<Model3DTManager>().code == code).GetComponent<Model3DTManager>().SelectModel(); ;

        }
        LoadDataModel(selectedModels.Find(ob => ob.Code == code));
        LastIndexModels = code;
    }

    public void ShowModels()
    {
        ListModels();
        // ListModels();
        modalModels.SetActive(true);
    }
    public void HideModels()
    {
        modalModels.SetActive(false);
    }
    public void ListModels()
    {
        for (int i = 0; i < contentRowModel.childCount; i++)
        {
            Destroy(contentRowModel.transform.GetChild(i).gameObject);
        }
        //  selectedRounds = rightRounds;
        foreach (Model3D model3D in Allmodels)
        {
            GameObject instance = Instantiate(rowModelPrefab, contentRowModel);
            instance.GetComponent<RowModelManager>().SetData(model3D, LoadTextureFromFile(model3D.UrlMiniature));
            if (selectedModels.Find(ob => ob.Code == model3D.Code) != null)
            {
                instance.GetComponent<Button>().interactable = false;
            }
            else
            {
                instance.GetComponent<Button>().onClick.AddListener(() => AddModel(model3D));
            }

            //    instance.GetComponent<RoundItemManager>().PrepareData(, this, direction);
            //CreateElement(item, false);
        }
        // contentRowModel
    }
    //common
    public VectorValues modelPosition;
    public void OnPositionChanged()
    {
        if (LastIndexModels != -1)
            selectedModelsGM.Find(ob => ob.GetComponent<Model3DTManager>().code == LastIndexModels).transform.position = modelPosition.getVectorValue(); ;
        //  selectedModelsGM[LastIndexModels].transform.position = modelPosition.getVectorValue();
    }
    public void OnOptionChanged(int value)
    {

        if (LastIndexModels != -1)
        {
            if (value == 0)
                selectedModels.Find(ob => ob.Code == LastIndexModels).Question.OptionA = options[value].text;
            if (value == 1)
                selectedModels.Find(ob => ob.Code == LastIndexModels).Question.OptionB = options[value].text;
            if (value == 2)
                selectedModels.Find(ob => ob.Code == LastIndexModels).Question.OptionC = options[value].text;
            if (value == 3)
                selectedModels.Find(ob => ob.Code == LastIndexModels).Question.OptionD = options[value].text;
        }
    }
    public void OnTitleChanged()
    {

        if (LastIndexModels != -1)
            selectedModels.Find(ob => ob.Code == LastIndexModels).Question.Title = questionTitle.text;
    }
    public void OnOptionSelectedChanged()
    {
        if (LastIndexModels != -1)
            selectedModels.Find(ob => ob.Code == LastIndexModels).Question.Solution = optionSelected.value;
    }
    public void OnTimeResolveChanged(TMP_InputField inputField)
    {
        currentMinigame.ResolveTime = float.Parse(inputField.text);
    }
    public void OnTimeFeedbackChanged(TMP_InputField inputField)
    {
        currentMinigame.FeedbackTime = float.Parse(inputField.text);
        //  print(currentMinigame.FeedbackTime);
    }


    // Sprite LoadSpriteFromFile(string path)
    // {
    //     // Load the texture from the file
    //     Texture2D texture = LoadTextureFromFile(path);

    //     // Create a sprite from the loaded texture
    //     Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

    //     return sprite;
    // }
    Texture2D LoadTextureFromFile(string path)
    {
        // Read the bytes from the file
        byte[] fileData = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, path));

        // Create a new texture and load the image data
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // This automatically resizes the texture to match the image size

        return texture;
    }

    // Minigame 1

}
