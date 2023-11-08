using Api;
using Models;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
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

            selectorRounds.CreateList(CrudGeneralGame.GetRounds(), selectedRounds);

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
        void ICrud.List(GameObject table, GameObject row)
        {

            for (int i = 0; i < CrudGeneralGame.GetGeneralGames().Count; i++)
            {
                GameObject value = Instantiate(row);
                value.SetActive(true);
                PrepareRow(value, i);
                value.transform.SetParent(table.transform);
                value.transform.localScale = new Vector3(1, 1, 1);
                // 
            }

        }
        void PrepareRow(GameObject row, int pos)
        {
            Models.GeneralGame generalGame = CrudGeneralGame.GetGeneralGames()[pos];
            row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = generalGame.Title;
            row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{generalGame.Rounds.Count}";
            //row.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(editAction);
        }
        // public int GetSizeList()
        // {
        //     return CrudGeneralGame.GetGeneralGames().Count;
        // }
    }
}