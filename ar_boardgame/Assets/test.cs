using Models;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TextAsset file;

    [System.Serializable]

    public class Message
    {
        [SerializeField] int arbol;
        [SerializeField] int casa;
        [SerializeField] Score score;

        public int Arbol { get => arbol; set => arbol = value; }
        public int Casa { get => casa; set => casa = value; }
        public Score Score { get => score; set => score = value; }


        public Message(int arbol, int casa)
        {
            this.Arbol = arbol;
            this.Casa = casa;
            Score = new();
        }
        public Message()
        {

        }
    }
    [System.Serializable]
    public class Wrapper
    {
        public Model3D[] models;
    }

    // Start is called before the first frame update
    void Start()
    {
        Message message = new(1, 2);
        message.Score.Stars = 12;

        print(JsonUtility.ToJson(message));
        message = JsonUtility.FromJson<Message>(file.text);
        print(JsonUtility.ToJson(message));

        Wrapper tester = JsonUtility.FromJson<Wrapper>(file.text);
        foreach (var item in tester.models)
        {
            print(item);
        }

    }
    public void LoadJsonFromFile(string nameFile)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
