using UnityEngine;

public class MediatorMinigame : MonoBehaviour
{
    [SerializeField] private HeaderManager headerManager;
    [SerializeField] private ItemsManager itemsManager;


    // Start is called before the first frame update
    void Start()
    {
        // headerManager.Configure(this); is not necessary right now
        // prepara el header, no olvidar implementar personalizadamente
        headerManager.PrepareLevel();
        itemsManager.PrepareLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
