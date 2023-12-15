using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] characters;
    int index = 0;
    int size;
    void Start()
    {
        size = characters.Length;

    }

    public void NextCharacter()
    {
        if (index == size - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        UpdateCharacter();

    }
    public void BeforeCharacter()
    {
        if (index == 0)
        {
            index = size - 1;
        }
        else
        {
            index--;
        }
        UpdateCharacter();

    }
    void UpdateCharacter()
    {
        GetComponent<SpriteRenderer>().sprite = characters[index];
    }
    public void GoBoard()
    {
        // Singleton.instance.firstSpritePlayer = characters[index];

        SceneManager.LoadScene("board");
    }



    // Update is called once per frame

}
