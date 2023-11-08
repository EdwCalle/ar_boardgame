using Api;
using Models;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace Pages
{

    public class GeneralGamePage : MonoBehaviour, ICrud

    {
        public TMP_InputField title;
        // public TMP_InputField roundNumber;
        public SelectorManager selectorRounds;
        public TextMeshProUGUI titleModal;
        Models.GeneralGame generalGame;
        public List<Round> selectedRounds;
        private EditMode editMode;




        void Start()
        {

        }
        void Update()
        {

        }
        void ICrud.Save()
        {
            if (editMode == EditMode.create)
            {
                generalGame = new Models.GeneralGame
                {
                    Title = title.text,
                    Rounds = selectedRounds,
                };
                CrudGeneralGame.Create(generalGame);
            }
            else
            {
                generalGame.Title = title.text;
                generalGame.Rounds = selectedRounds;

                CrudGeneralGame.Edit(generalGame);
            }

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

                //value.AddComponent<int>().
                // 
            }

        }
        void PrepareRow(GameObject row, int pos)
        {

            Models.GeneralGame generalGame = CrudGeneralGame.GetGeneralGames()[pos];
            row.GetComponent<RowManager>().code = generalGame.Id;
            row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = generalGame.Title;
            row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{generalGame.Rounds.Count}";
        }

        void ICrud.PrepareModal(EditMode editMode, int code)
        {
            this.editMode = editMode;
            if (this.editMode == EditMode.create)
            {
                //  titleModal.text = "Crear Partidas";
                selectedRounds = new List<Round>();

                selectorRounds.CreateList(CrudRounds.GetRounds(), new List<Round>(), selectedRounds);
            }
            if (this.editMode == EditMode.edit)
            {

                generalGame = CrudGeneralGame.GetById(code);
                title.text = generalGame.Title;
                //titleModal.text = $"Editar partida:{generalGame.Title}";
                // selectedRounds.Clear();
                selectedRounds = generalGame.Rounds.ToList<Round>();
                selectorRounds.CreateList(CrudGeneralGame.GetNoSelectedRounds(code), generalGame.Rounds, selectedRounds);

            }
            if (this.editMode == EditMode.delete)
            {
                CrudGeneralGame.Delete(code);
            }
        }
        void ICrud.ClearModal()
        {
            title.text = "";
            selectorRounds.ClearSelector();
            // Select
        }


        // public int GetSizeList()
        // {
        //     return CrudGeneralGame.GetGeneralGames().Count;
        // }
    }
}