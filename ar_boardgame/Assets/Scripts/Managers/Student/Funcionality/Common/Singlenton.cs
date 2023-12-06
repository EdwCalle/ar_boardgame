using System.Collections.Generic;
using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    public Sprite firstSpritePlayer;
    [HideInInspector]
    // for game funcionality
    public Minigame currentMinigame = new Minigame();

    public GeneralGame currentGeneralGame;

    public Item[] exampleModels;
    public Score playerScore;
    // for teacher testing
    public List<GeneralGame> generalGamesT = new List<GeneralGame>();
    public List<Round> roundsT = new List<Round>();
    public List<Minigame> minigamesT = new List<Minigame>();
    //public List<Round> roundsT = new List<Round>();


    //for minimage
    private int currentQuestion = 0;
    // for board funcionality
    [SerializeField]
    private int numberPlayers = 2;
    // [SerializeField]
    private int myTurn = 0;
    //  [SerializeField]
    private int currentRound = 0;
    //[SerializeField]
    private int[] currentBox = { 0, 0, 0, 0 };
    //  [SerializeField]
    private int currentTurn = 0;
    [SerializeField]
    private bool isSinglePlayerMode = false;
    public int NumberPlayers { get => numberPlayers; set => numberPlayers = value; }
    public int MyTurn { get => myTurn; set => myTurn = value; }
    public int CurrentRound { get => currentRound; set => currentRound = value; }
    public int[] CurrentBox { get => currentBox; set => currentBox = value; }
    public int CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public bool IsSinglePlayerMode { get => isSinglePlayerMode; set => isSinglePlayerMode = value; }
    public int CurrentQuestion { get => currentQuestion; set => currentQuestion = value; }

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
    List<Item> exampleModels2 = new List<Item>();
    List<Round> exampleRounds = new List<Round>();
    public void models()
    {

    }
    public void eRounds()
    {

        Round round = new Round
        {
            Id = 1,
            Title = "Tema 1: Órganos del Cuerpo Humano",
            NumberRound = 1
        };
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "gato", "perro", "cerebro", "camiseta", 2));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "árbol", "manzana", "cerebro", "garras", 2));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "brazo", "perro", "pata", "cajón", 2));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una parte del cuerpo humano?", "pierna", "zapatos", "camiseta", "garra", 0));
        LoadMinigame1(round);
        currentGeneralGame.Rounds.Add(round);
        round = new Round
        {
            Id = 2,
            Title = "Tema 2: Cuerpo saludable",
            // QuestionForA = new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "cereal", "hamburguesa", "manzana", "cabeza", 0),
            // QuestionForB = new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "pan", "lechuga", "papas fritas", "vino", 0),
            // QuestionForC = new Question("¿Cuál de las siguientes opciones es una fruta?", "manzana", "perro", "pata", "cajón", 0),
            // QuestionForD = new Question("¿Cuál de las siguientes opciones es una fruta?", "pan", "hamburguesa", "sandia", "queso", 2),
            NumberRound = 2
        };
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "cereal", "hamburguesa", "manzana", "cabeza", 0));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "pan", "lechuga", "papas fritas", "vino", 0));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una fruta?", "manzana", "perro", "pata", "cajón", 0));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una fruta?", "pan", "hamburguesa", "sandia", "queso", 2));
        LoadMinigame2(round);

        // currentGeneralGame.Rounds.Add(round);
        round = new Round
        {
            Id = 3,
            Title = "Tema 3: Cuerpo saludable",
            // QuestionForA = new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "cereal", "hamburguesa", "manzana", "cabeza", 0),
            // QuestionForB = new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "pan", "lechuga", "papas fritas", "vino", 0),
            // QuestionForC = new Question("¿Cuál de las siguientes opciones es una fruta?", "manzana", "perro", "pata", "cajón", 0),
            // QuestionForD = new Question("¿Cuál de las siguientes opciones es una fruta?", "pan", "hamburguesa", "sandia", "queso", 2),
            NumberRound = 3
        };
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "cereal", "hamburguesa", "manzana", "cabeza", 0));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es un carbohidrato?", "pan", "lechuga", "papas fritas", "vino", 0));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una fruta?", "manzana", "perro", "pata", "cajón", 0));
        round.Questions.Add(new Question("¿Cuál de las siguientes opciones es una fruta?", "pan", "hamburguesa", "sandia", "queso", 2));
        LoadMinigame3(round);
        //  currentGeneralGame.Rounds.Add(round);
    }
    public void eQuestionsModel()
    {

    }
    public void eQuestionsRounds()
    {

    }
    // public Item[] getModels()
    // {
    //     //return Item[];
    // }
    public void PreLoadDataStudent()
    {
        exampleModels = new Item[3];
        //  round.Minigame1.Models.Add(new Item(1, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(1, -0.5f, 2), new Vector3(0.2f, 0.2f, 0.2f), true, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        //round.Minigame1.Models.Add(new Item(2, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/AntiqueCamera/glTF-Binary/AntiqueCamera.glb", new Vector3(0, -0.5f, 2.5f), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        // round.Minigame1.Models.Add(new Item(3, "pato", "description", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf", new Vector3(-1, -0.5f, 3), new Vector3(0.2f, 0.2f, 0.2f), false, false, new Models.Collider(new Vector3(-0.1f, 0.8f, 0), new Vector3(1.7f, 1.8f, 1))));
        exampleModels[0] = new Item
        {
            Code = 1,
            Name = "Margarita",
            Description = "some description",
            Url = "Models3D/margarita.glb",
            Position = new Vector3(1f, 0.5f, -1.5f),
            // Scale = new Vector3(0.2f, 0.2f, 0.2f),
            IsSolution = true,
            IsSelected = false,
            ModelInBox = new ModelInBox { Position = new Vector3(0, -0.15f, -0.5f), Scale = new Vector3(1f, 1f, 1f) },
            ColliderScale = new Vector3(0.5f, 0.5f, 0.5f),
            Question = new Question { Title = "Pregunta 1", OptionA = "1", OptionB = "2", OptionC = "3", OptionD = "4", Solution = 1 }

        };
        exampleModels[1] = new Item
        {
            Code = 2,
            Name = "Arbusto",
            Description = "some description",
            Url = "Models3D/arbusto.glb",
            Position = new Vector3(0, 0.5f, 0f),
            // Scale = new Vector3(0.2f, 0.2f, 0.2f),
            IsSolution = true,
            IsSelected = false,
            ModelInBox = new ModelInBox { Position = new Vector3(0, -0.15f, -0.5f), Scale = new Vector3(1f, 1f, 1f) },
            ColliderScale = new Vector3(0.5f, 0.5f, 0.5f),
            Question = new Question { Title = "Pregunta 2", OptionA = "1a", OptionB = "2b", OptionC = "3c", OptionD = "4d", Solution = 2 }
        };
        exampleModels[2] = new Item
        {
            Code = 3,
            Name = "Cactus",
            Description = "some description",
            Url = "Models3D/cactus.glb",
            Position = new Vector3(-1.5f, 0.5f, 1.5f),
            // Scale = new Vector3(0.2f, 0.2f, 0.2f),
            IsSolution = false,
            IsSelected = false,
            ModelInBox = new ModelInBox { Position = new Vector3(0, -0.15f, -0.5f), Scale = new Vector3(1f, 1f, 1f) },
            ColliderScale = new Vector3(0.5f, 0.5f, 0.5f),
            Question = new Question { Title = "Pregunta 3", OptionA = "a", OptionB = "b", OptionC = "c", OptionD = "d", Solution = 0 }
        };

        currentGeneralGame = new GeneralGame(01, "Bloque 1:Las Plantas", "Ciencias Naturales");
        eRounds();// crear rondas


        //currentGeneralGame.Rounds.Add(exampleRound);
    }

    public void SetMinigame()
    {  //if (level == 1)
        currentMinigame = currentGeneralGame.Rounds[CurrentRound].MyMinigame;
        Debug.Log("Vengo Con:" + currentGeneralGame.Rounds[CurrentRound].ToString());
        Debug.Log("Vengo Con:" + currentMinigame.ToString());
    }
    public void LoadMinigame1(Round round)
    {
        round.MyMinigame = new Minigame
        {
            Id = 1,
            Level = 1,
            ExtraInfo = new ExtraInfo("image"),
            Title = "MG01:selecionar la planta Correcta Según sus hojas",
            InteractionTime = 6,
            ResolveTime = 5,
            FeedbackTime = 4,
            TimerState = TimerState.none
        };
        round.MyMinigame.Models.Add(exampleModels[0]);
        round.MyMinigame.Models.Add(exampleModels[1]);
        round.MyMinigame.Models.Add(exampleModels[2]);
    }
    public void LoadMinigame2(Round round)
    {
        round.MyMinigame = new Minigame
        {
            Id = 2,
            Level = 2,
            ExtraInfo = new ExtraInfo("image"),
            Title = "MG02:selecionar la planta Correcta Según sus hojas",
            InteractionTime = 6,
            ResolveTime = 5,
            FeedbackTime = 4,
            TimerState = TimerState.none
        };
        round.MyMinigame.Models.Add(exampleModels[0]);
        round.MyMinigame.Models.Add(exampleModels[1]);
        round.MyMinigame.Models.Add(exampleModels[2]);
    }
    public void LoadMinigame3(Round round)
    {
        round.MyMinigame = new Minigame
        {
            Id = 3,
            Level = 3,
            ExtraInfo = new ExtraInfo("image"),
            Title = "MG03:selecionar la planta Correcta Según sus hojas",
            InteractionTime = 6,
            ResolveTime = 5,
            FeedbackTime = 4,
            TimerState = TimerState.none
        };
        round.MyMinigame.Models.Add(exampleModels[0]);
        round.MyMinigame.Models.Add(exampleModels[1]);
        round.MyMinigame.Models.Add(exampleModels[2]);
    }


}