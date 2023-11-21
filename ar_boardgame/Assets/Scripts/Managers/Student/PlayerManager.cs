using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public TextMeshProUGUI nicknmame;

    private int turn;
    private int currentSquare = 0;

    public int Turn { get => turn; set => turn = value; }
    public int CurrentSquare { get => currentSquare; set => currentSquare = value; }

    // Start is called before the first frame update


    void Start()
    {

    }
    public void setNickame(string nicknmame)
    {

    }
    public void PrepareData(string nicknmame, int turn)
    {
        this.nicknmame.text = nicknmame;
        this.turn = turn;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
