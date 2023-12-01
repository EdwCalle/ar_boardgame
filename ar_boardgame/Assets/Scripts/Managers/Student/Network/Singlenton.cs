using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    [HideInInspector]
    // public int currentPosition = 0;
    public Sprite firstSpritePlayer;
    [HideInInspector]
    public Minigame currentMinigame = new Minigame();

    public GeneralGame currentGeneralGame;

    public Item[] exampleModels;


    // for teacher testing
    public List<GeneralGame> generalGamesT = new List<GeneralGame>();
    public List<Round> roundsT = new List<Round>();
    public List<Minigame> minigamesT = new List<Minigame>();
    //public List<Round> roundsT = new List<Round>();


    [SerializeField]
    private int numberPlayers = 4;
    [SerializeField]
    private int myTurn = 0;
    [SerializeField]
    private int currentRound = 0;
    //[SerializeField]
    private int[] currentBox = { 0, 0, 0, 0 };
    [SerializeField]
    private int currentTurn = 0;
    [SerializeField]
    private bool isSinglePlayerMode = false;
    public int NumberPlayers { get => numberPlayers; set => numberPlayers = value; }
    public int MyTurn { get => myTurn; set => myTurn = value; }
    public int CurrentRound { get => currentRound; set => currentRound = value; }
    public int[] CurrentBox { get => currentBox; set => currentBox = value; }
    public int CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public bool IsSinglePlayerMode { get => isSinglePlayerMode; set => isSinglePlayerMode = value; }

    private void Awake()
    {
        CreateSingleton();
        PreLoadDataStudent();
        // PreLoadDataTeacher();
        GetComponent<Transform>();
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
        exampleModels = new Item[3];
        //  round.Minigame1.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        //round.Minigame1.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        // round.Minigame1.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        exampleModels[0] = new Item
        {
            Code = 1,
            Name = "pato",
            Description = "some description",
            Url = "Models3D/margarita.glb",
            Position = new Vector3(1, -0.5f, 2),
            Scale = new Vector3(0.2f, 0.2f, 0.2f),
            IsSolution = true,
            IsSelected = false,
            ModelInBox = new ModelInBox { Position = new Vector3(0, -0.15f, -0.5f), Scale = new Vector3(1f, 1f, 1f) },
            ColliderScale = new Vector3(0.5f, 0.5f, 0.5f),
        };
        exampleModels[1] = new Item
        {
            Code = 2,
            Name = "pato",
            Description = "some description",
            Url = "Models3D/arbusto.glb",
            Position = new Vector3(0, -0.5f, 2.5f),
            Scale = new Vector3(0.2f, 0.2f, 0.2f),
            IsSolution = true,
            IsSelected = false,
            ModelInBox = new ModelInBox { Position = new Vector3(0, -0.15f, -0.5f), Scale = new Vector3(1f, 1f, 1f) },
            ColliderScale = new Vector3(0.5f, 0.5f, 0.5f),
        };
        exampleModels[2] = new Item
        {
            Code = 3,
            Name = "pato",
            Description = "some description",
            Url = "Models3D/cactus.glb",
            Position = new Vector3(-1, -0.5f, 3),
            Scale = new Vector3(0.2f, 0.2f, 0.2f),
            IsSolution = true,
            IsSelected = false,
            ModelInBox = new ModelInBox { Position = new Vector3(0, -0.15f, -0.5f), Scale = new Vector3(1f, 1f, 1f) },
            ColliderScale = new Vector3(0.5f, 0.5f, 0.5f),
        };

        currentGeneralGame = new GeneralGame(01, "Bloque 1:Las Plantas", "Ciencias Naturales");
        Round exampleRound = new Round
        {
            Id = 1,
            Title = "Tema 1: Órganos del Cuerpo Humano",
            NumberRound = 1
        };
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "gato", "perro", "cerebro", "camiseta", 2));
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "árbol", "manzana", "cerebro", "garras", 2));
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "brazo", "perro", "pata", "cajón", 2));
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "pierna", "zapatos", "camiseta", "garra", 0));
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        currentGeneralGame.Rounds.Add(exampleRound);
        exampleRound = new Round
        {
            Id = 2,
            Title = "Tema 2: Cuerpo saludable",
            // QuestionForA = new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "cereal", "hamburguesa", "manzana", "cabeza", 0),
            // QuestionForB = new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "pan", "lechuga", "papas fritas", "vino", 0),
            // QuestionForC = new Question("¿Cuál de las siguientes opciones es una fruta?", "manzana", "perro", "pata", "cajón", 0),
            // QuestionForD = new Question("¿Cuál de las siguientes opciones es una fruta?", "pan", "hamburguesa", "sandia", "queso", 2),
            NumberRound = 2
        };
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "cereal", "hamburguesa", "manzana", "cabeza", 0));
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "pan", "lechuga", "papas fritas", "vino", 0));
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es una fruta?", "manzana", "perro", "pata", "cajón", 0));
        exampleRound.Questions.Add(new Question("¿Cuál de las siguientes opciones es una fruta?", "pan", "hamburguesa", "sandia", "queso", 2));
        LoadMinigame1(exampleRound);
        LoadMinigame2(exampleRound);
        LoadMinigame3(exampleRound);
        currentGeneralGame.Rounds.Add(exampleRound);
        // exampleRound = new Round(3, "Unidad 3: Tipos de plantas", 3);
        //currentGeneralGame.Rounds.Add(exampleRound);




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



    }

    public void SetMinigame(int level)
    {
        if (level == 1)

            currentMinigame = currentGeneralGame.Rounds[CurrentRound].Minigame1;

        if (level == 2)

            currentMinigame = currentGeneralGame.Rounds[CurrentRound].Minigame2;

        if (level == 3)

            currentMinigame = currentGeneralGame.Rounds[CurrentRound].Minigame3;

    }
    public void LoadMinigame1(Round round)
    {
        round.Minigame1 = new Minigame();
        round.Minigame1.Id = 1;
        round.Minigame1.Level = 1;
        round.Minigame1.ExtraInfo = new ExtraInfo("image");
        round.Minigame1.Title = "MG01:selecionar la planta Correcta Según sus hojas";
        round.Minigame1.InteractionTime = 15;
        round.Minigame1.ResolveTime = 5;
        round.Minigame1.FeedbackTime = 5;
        round.Minigame1.TimerState = TimerState.none;
        //round.Minigame1.Function = "seleccionar el modelo correcto";

        round.Minigame1.Models.Add(exampleModels[0]);
        round.Minigame1.Models.Add(exampleModels[1]);
        round.Minigame1.Models.Add(exampleModels[2]);
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

        round.Minigame2.Models.Add(exampleModels[0]);
        round.Minigame2.Models.Add(exampleModels[1]);
        round.Minigame2.Models.Add(exampleModels[2]);
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

        round.Minigame3.Models.Add(exampleModels[0]);
        round.Minigame3.Models.Add(exampleModels[1]);
        round.Minigame3.Models.Add(exampleModels[2]);
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

        round.Minigame1.Models.Add(exampleModels[0]);
        round.Minigame1.Models.Add(exampleModels[1]);
        round.Minigame1.Models.Add(exampleModels[2]);
    }


}