using UnityEngine;

public class MediatorMinigame : MonoBehaviour
{
    public HeaderManager headerManager;

    // Start is called before the first frame update
    void Start()
    {
        headerManager.Configure(this);
        headerManager.PrepareLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
