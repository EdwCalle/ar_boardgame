using System;
using Api;
using Models;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
namespace Pages
{

    public class GeneralGame : MonoBehaviour, ICrud

    {
        public TMP_InputField title;
        // public TMP_InputField roundNumber;
        public SelectorManager selectorRounds;
        Models.GeneralGame generalGame;
        public List<Round> selectedRounds;
        void Start()
        {
            //rounds.options.
            selectedRounds = new List<Round>();
            CrudGeneralGame crud = new CrudGeneralGame();
            selectorRounds.CreateList(crud.GetRounds(), selectedRounds);

        }
        void Update()
        {

        }
        void ICrud.Save()
        {
            generalGame = new Models.GeneralGame
            {
                Title = title.text,
                Rounds = selectedRounds

            };
            Debug.Log("i'm saving game... " + generalGame.ToString());

        }

    }
}