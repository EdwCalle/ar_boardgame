using Models;
using TMPro;
using UnityEngine;
namespace Pages
{

    public class GeneralGame : MonoBehaviour, ICrudPage

    {
        public TMP_InputField nameGame;
        void Start()
        {

        }
        void Update()
        {

        }
        void ICrudPage.Save()
        {
            Debug.Log("i'm saving game...");

        }
    }
}