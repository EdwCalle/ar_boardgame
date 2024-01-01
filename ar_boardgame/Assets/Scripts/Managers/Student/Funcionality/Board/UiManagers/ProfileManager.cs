
using Models;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public TextMeshProUGUI nickname;
    public TextMeshProUGUI caps;
    public TextMeshProUGUI stars;
    public TextMeshProUGUI extracoins;
    public GameObject yourTurn;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(Score score)
    {
        caps.text = score.caps.ToString("0#");
        stars.text = score.stars.ToString("0#");
        extracoins.text = score.extracoins.ToString("0#");
    }
    public void SelectPlayer()
    {
        yourTurn.SetActive(true);
    }
    public void UnselectPlayer()
    {
        yourTurn.SetActive(false);
    }
}
