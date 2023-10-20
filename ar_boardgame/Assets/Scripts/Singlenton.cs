using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    [HideInInspector]
    public int currentPosition = 0;
    [HideInInspector]
    public Sprite firstSpritePlayer;
    [HideInInspector]
    public Minigame currentMinigame = new Minigame();

    private void Awake()
    {
        CreateSingleton();
        PreLoadData();

    }

    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    public void PreLoadData()
    {
        currentMinigame.Code = 1;
        currentMinigame.ExtraInfo = new ExtraInfo("image", "las aves vuelan");
        currentMinigame.Title = "selecionar el pato correcto";
        currentMinigame.InteractionTime = 15;
        currentMinigame.ResolveTime = 30;
        currentMinigame.FeedbackTime = 10;
        currentMinigame.TimerState = TimerState.none;
        currentMinigame.Function = "seleccionar el modelo correcto";

        currentMinigame.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        currentMinigame.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        currentMinigame.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));

        currentMinigame.Questions.Add(new Question("blanco", "negro", "rojo", "verde", "¿De que color es el casco?"));
        currentMinigame.Questions.Add(new Question("una ", "dos ", "tres", "cuatro", "¿Cuántas ruedas tiene el carro?"));
        currentMinigame.Questions.Add(new Question("blanco", "amarillo", "rojo", "verde", "¿De que color es el pato?"));


    }


}