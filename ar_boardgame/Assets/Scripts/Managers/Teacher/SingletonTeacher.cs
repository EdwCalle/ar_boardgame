using System.Collections.Generic;
using Models;
using UnityEngine;

public class SingletonTeacher : MonoBehaviour
{
    public static SingletonTeacher instance;
    private List<Match> generalGamesT = new();
    private List<Round> roundsT = new();
    private List<Minigame> minigamesT = new();
    public List<Match> GeneralGamesT { get => generalGamesT; set => generalGamesT = value; }
    public List<Round> RoundsT { get => roundsT; set => roundsT = value; }
    public List<Minigame> MinigamesT { get => minigamesT; set => minigamesT = value; }
    // Start is called before the first frame update
    private void Awake()
    {
        CreateSingleton();
    }
    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
