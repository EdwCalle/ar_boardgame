using System.Collections.Generic;
using Api;
using Models;
using TMPro;
using UnityEngine;
namespace Pages
{

    public class GeneralGame : MonoBehaviour, ICrudPage

    {
        public TMP_InputField title;
        // public TMP_InputField roundNumber;
        public TMP_Dropdown rounds;
        Models.GeneralGame generalGame;
        void Start()
        {
            //rounds.options.

            CrudGeneralGame crud = new CrudGeneralGame();

            foreach (Models.Round item in crud.GetRounds())
            {
                //  TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();

                //rounds.options.Add(new TMP_Dropdown.OptionData(item.Title));
            }


        }
        void Update()
        {

        }
        void ICrudPage.Save()
        {
            generalGame = new Models.GeneralGame
            {
                Title = title.text
            };
            Debug.Log("i'm saving game... " + generalGame.ToString());

        }

    }
}