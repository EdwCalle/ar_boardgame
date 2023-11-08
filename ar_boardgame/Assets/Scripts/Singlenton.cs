using System.Collections.Generic;
using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    [HideInInspector]
    public int currentPosition = 0;
    public int currentRound = 0;
    public Sprite firstSpritePlayer;
    [HideInInspector]
    public Minigame currentMinigame = new Minigame();

    public GeneralGame currentGeneralGame;


    // for teacher testing
    public List<GeneralGame> generalGamesT = new List<GeneralGame>();
    public List<Round> roundsT = new List<Round>();
    public List<Minigame> minigamesT = new List<Minigame>();
    //public List<Round> roundsT = new List<Round>();




    private void Awake()
    {
        CreateSingleton();
        PreLoadDataStudent();
        // PreLoadDataTeacher();

    }

    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    public void PreLoadDataStudent()
    {

        currentGeneralGame = new GeneralGame(01, "Bloque 1:Las Plantas", "Ciencias Naturales");

        Round exampleRound;
        exampleRound = new Round(1, "Unidad 1: Las partes de la planta", 1);
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        currentGeneralGame.Rounds.Add(exampleRound);
        exampleRound = new Round(2, "Unidad 2: El ciclo de vida la planta", 2);
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        // currentGeneralGame.Rounds.Add(exampleRound);
        // exampleRound = new Round(3, "Unidad 3: Tipos de plantas", 3);

        currentGeneralGame.Rounds.Add(exampleRound);

        // currentMinigame.Id = 1;
        // currentMinigame.ExtraInfo = new ExtraInfo("image", "las aves vuelan");
        // currentMinigame.Title = "selecionar el pato correcto";
        // currentMinigame.InteractionTime = 15;
        // currentMinigame.ResolveTime = 30;
        // currentMinigame.FeedbackTime = 10;
        // currentMinigame.TimerState = TimerState.none;
        // currentMinigame.Function = "seleccionar el modelo correcto";

        // currentMinigame.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        // currentMinigame.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        // currentMinigame.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));

        // currentMinigame.Questions.Add(new Question("blanco", "negro", "rojo", "verde", "¿De que color es el casco?"));
        // currentMinigame.Questions.Add(new Question("una ", "dos ", "tres", "cuatro", "¿Cuántas ruedas tiene el carro?"));
        // currentMinigame.Questions.Add(new Question("blanco", "amarillo", "rojo", "verde", "¿De que color es el pato?"));
    }
    public void PreLoadDataTeacher()
    {

        Round exampleRound;
        exampleRound = new Round(1, "tema 1: Las partes de la planta", 1);
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        roundsT.Add(exampleRound);
        exampleRound = new Round(2, "tema 2: El ciclo de vida la planta", 2);
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        roundsT.Add(exampleRound);
        exampleRound = new Round(3, "tema 3: La fotosíntesis", 3);
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        roundsT.Add(exampleRound);

        GeneralGame generalGame = new GeneralGame(1, "Unidad 1:Las Plantas", "Ciencias Naturales");
        generalGame.Rounds.Add(exampleRound); // example 3
        generalGamesT.Add(generalGame);

        minigamesT.Add(exampleRound.Minigame1);
        minigamesT.Add(exampleRound.Minigame2);
        minigamesT.Add(exampleRound.Minigame3);

        LoadMinigame1_4(exampleRound);
        minigamesT.Add(exampleRound.Minigame1);
        //  minigamesT.Add(exampleRound.Minigame4);

        // currentMinigame.Id = 1;
        // currentMinigame.ExtraInfo = new ExtraInfo("image", "las aves vuelan");
        // currentMinigame.Title = "selecionar el pato correcto";
        // currentMinigame.InteractionTime = 15;
        // currentMinigame.ResolveTime = 30;
        // currentMinigame.FeedbackTime = 10;
        // currentMinigame.TimerState = TimerState.none;
        // currentMinigame.Function = "seleccionar el modelo correcto";

        // currentMinigame.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        // currentMinigame.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        // currentMinigame.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));

        // currentMinigame.Questions.Add(new Question("blanco", "negro", "rojo", "verde", "¿De que color es el casco?"));
        // currentMinigame.Questions.Add(new Question("una ", "dos ", "tres", "cuatro", "¿Cuántas ruedas tiene el carro?"));
        // currentMinigame.Questions.Add(new Question("blanco", "amarillo", "rojo", "verde", "¿De que color es el pato?"));
    }

    public void SetMinigame(int level)
    {
        if (level == 1)

            currentMinigame = currentGeneralGame.Rounds[currentRound].Minigame1;

        if (level == 2)

            currentMinigame = currentGeneralGame.Rounds[currentRound].Minigame2;

        if (level == 3)

            currentMinigame = currentGeneralGame.Rounds[currentRound].Minigame3;

    }
    public void LoadMinigame1(Round round)
    {
        round.Minigame1 = new Minigame();
        round.Minigame1.Id = 1;
        round.Minigame1.Level = 1;
        round.Minigame1.ExtraInfo = new ExtraInfo("image");
        round.Minigame1.Title = "MG01:selecionar la planta Correcta Según sus hojas";
        round.Minigame1.InteractionTime = 15;
        round.Minigame1.ResolveTime = 30;
        round.Minigame1.FeedbackTime = 10;
        round.Minigame1.TimerState = TimerState.none;
        //round.Minigame1.Function = "seleccionar el modelo correcto";

        round.Minigame1.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame1.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame1.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
    }
    public void LoadMinigame2(Round round)
    {
        round.Minigame2 = new Minigame();
        round.Minigame2.Id = 2;
        round.Minigame2.Level = 2;
        round.Minigame2.ExtraInfo = new ExtraInfo("image");
        round.Minigame2.Title = "MG02:Explora el Área y selecciona la opcion correcte";
        round.Minigame2.InteractionTime = 15;
        round.Minigame2.ResolveTime = 30;
        round.Minigame2.FeedbackTime = 10;
        round.Minigame2.TimerState = TimerState.none;
        //round.Minigame1.Function = "seleccionar el modelo correcto";

        round.Minigame2.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame2.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame2.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
    }
    public void LoadMinigame3(Round round)
    {
        round.Minigame3 = new Minigame();
        round.Minigame3.Id = 3;
        round.Minigame3.Level = 3;
        round.Minigame3.ExtraInfo = new ExtraInfo("image");
        round.Minigame3.Title = "MG03:selecionar las plantas correctas";
        round.Minigame3.InteractionTime = 15;
        round.Minigame3.ResolveTime = 30;
        round.Minigame3.FeedbackTime = 10;
        round.Minigame3.TimerState = TimerState.none;
        //round.Minigame1.Function = "seleccionar el modelo correcto";

        round.Minigame3.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame3.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame3.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
    }
    public void LoadMinigame1_4(Round round)
    {
        round.Minigame1 = new Minigame();
        round.Minigame1.Id = 4;
        round.Minigame1.Level = 1;
        round.Minigame1.ExtraInfo = new ExtraInfo("image");
        round.Minigame1.Title = "MG01:selecionar la CASA inCorrecta Según sus hojas";
        round.Minigame1.InteractionTime = 15;
        round.Minigame1.ResolveTime = 30;
        round.Minigame1.FeedbackTime = 10;
        round.Minigame1.TimerState = TimerState.none;
        //round.Minigame1.Function = "seleccionar el modelo correcto";

        round.Minigame1.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame1.Models.Add(new Item(2, "pata", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        round.Minigame1.Models.Add(new Item(3, "perro", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
    }


}