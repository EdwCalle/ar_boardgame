using System.Collections.Generic;
using Models;
using UnityEngine;


public class SingletonTeacher : MonoBehaviour
{
    public static SingletonTeacher instance;
    private List<Match> matchesT = new();
    private List<Round> roundsT = new();
    private List<Minigame> minigamesT = new();
    private List<Model3D> models3DT = new();
    public List<Match> MatchesT { get => matchesT; set => matchesT = value; }
    public List<Round> RoundsT { get => roundsT; set => roundsT = value; }
    public List<Minigame> MinigamesT { get => minigamesT; set => minigamesT = value; }
    public List<Model3D> Models3DT { get => models3DT; set => models3DT = value; }

    // Start is called before the first frame update

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
    [System.Serializable]
    public class Wrapper
    {
        public List<Model3D> models;
        public Minigame[] minigames;
        public Dialogue[] dialogues;
        public List<Round> rounds;
        public List<Question> angelQuestions;
        public List<Question> demonQuestions;
        public List<Match> matches;
    }
    public TextAsset examplesJson;
    Wrapper ExampleData;
    public void PreLoadData()
    {
        ExampleData = JsonUtility.FromJson<Wrapper>(examplesJson.text);
        // generalGamesT[0] = new Match(01, "Bloque 1:Las Plantas", "Ciencias Naturales");

        // SetRoundExample(0, 0);
        // SetRoundExample(1, 1);
        // SetRoundExample(2, 2);
        // SetExtraQuestion();
        RoundsT = ExampleData.rounds;
        MatchesT = ExampleData.matches;
        Models3DT = ExampleData.models;
        // print(JsonUtility.ToJson(currentGeneralGame.Rounds[0]));
        // print(JsonUtility.ToJson(currentGeneralGame.Rounds[1]));
        // print(JsonUtility.ToJson(currentGeneralGame.[DemonQuestions]));
    }
    public void SetExtraQuestion()
    {
        matchesT[0].DemonQuestions = ExampleData.demonQuestions;
        matchesT[0].AngelQuestions = ExampleData.angelQuestions;
    }
    public void SetRoundExample(int numberRound, int idMinigame)
    {
        Round round = ExampleData.rounds[numberRound];
        round.Dialogue = ExampleData.dialogues[0];
        SetMinigameExample(round, idMinigame);
        matchesT[0].Rounds.Add(round);
    }
    public void SetMinigameExample(Round round, int idMinigame)
    {
        round.Minigame = ExampleData.minigames[idMinigame];
        round.Minigame.Models.Add(ExampleData.models[0]);
        round.Minigame.Models.Add(ExampleData.models[1]);
        round.Minigame.Models.Add(ExampleData.models[2]);
    }
    public static List<Question> GetAngelQuestions()
    {
        return instance.ExampleData.angelQuestions;
    }
    public static List<Question> GetDemonsQuestions()
    {
        return instance.ExampleData.demonQuestions;
    }
    public static List<Match> GetMatches()
    {
        return instance.matchesT;
    }

}
