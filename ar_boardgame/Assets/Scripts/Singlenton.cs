using System.Collections;
using Minigames;
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
        currentMinigame.Code = 4;
        currentMinigame.Title = "selecionar el pato correcto";
        currentMinigame.Timer = 30;
        currentMinigame.Function = "seleccionar el modelo correcto";
        currentMinigame.Models.Add(new ModelObject(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", "1;-0,5;2", "0.2,0.2,0.2", true, false, "2;1,8;1", "-0,21;0,77;0,05"));
        currentMinigame.Models.Add(new ModelObject(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", "0;-0,5;2,5", "0.2,0.2,0.2", false, false, "2;1,8;1", "-0,21;0,77;0,05"));
        currentMinigame.Models.Add(new ModelObject(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", "-1;-0,5;3", "0,2;0,2;0,2", false, false, "2;1,8;1", "-0,21;0,77;0,05"));

        currentMinigame.Questions.Add(new Question("blanco", "negro", "rojo", "verde", "¿De que color es el casco?"));
        currentMinigame.Questions.Add(new Question("una ", "dos ", "tres", "cuatro", "¿Cuántas ruedas tiene el carro?"));
        currentMinigame.Questions.Add(new Question("blanco", "amarillo", "rojo", "verde", "¿De que color es el pato?"));


    }


}