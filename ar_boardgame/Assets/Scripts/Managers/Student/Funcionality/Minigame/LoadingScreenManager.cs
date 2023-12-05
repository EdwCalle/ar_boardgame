using TMPro;
using UnityEngine;


public class LoadingScreenManager : MonoBehaviour
{
    public TextMeshProUGUI message;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAnimation(string message)
    {
        this.message.text = message;
        gameObject.SetActive(true);

    }
    public void StopAnimation()
    {
        gameObject.SetActive(false);
    }

}
