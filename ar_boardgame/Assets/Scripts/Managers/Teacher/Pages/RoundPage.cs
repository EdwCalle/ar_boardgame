
using System.Collections.Generic;
using Api;
using Models;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;
namespace Pages
{
    public class RoundPage : MonoBehaviour, ICrud
    {
        public TMP_InputField title;
        public TMP_Dropdown minigameOne;
        public TMP_Dropdown minigameTwo;
        public TMP_Dropdown minigameThree;
        public GameObject customItem;
        public TMP_Dropdown minigameFinal;
        public TextMeshProUGUI titleModal;
        Round round;
        private EditMode editMode;

        private Minigame minigame1;
        private Minigame minigame2;
        private Minigame minigame3;
        private Minigame finalMinigame;

        Dictionary<int, int> mapMinigame1 = new Dictionary<int, int>();
        Dictionary<int, int> mapMinigame2 = new Dictionary<int, int>();
        Dictionary<int, int> mapMinigame3 = new Dictionary<int, int>();
        Dictionary<int, int> mapFinalMinigame = new Dictionary<int, int>();




        void Start()
        {
            // minigameOne.onValueChanged.AddListener();
        }
        void Update()
        {

        }
        void ICrud.Save()
        {
            if (editMode == EditMode.create)
            {
                minigame1 = new Minigame();
                minigame2 = new Minigame();
                minigame3 = new Minigame();
                //Debug.Log()
                minigame1.Id = mapMinigame1[minigameOne.value];
                minigame2.Id = mapMinigame2[minigameTwo.value];
                minigame3.Id = mapMinigame3[minigameThree.value];

                round = new Round
                {
                    Title = title.text,
                    MyMinigame = minigame1,
                    //  Minigame2 = minigame2,
                    //  Minigame3 = minigame3,
                    // FinalMinigame = finalMinigame,
                };
                CrudRounds.Create(round);
            }
            else
            {
                round.Title = title.text;
                round.MyMinigame = minigame1;
                // round.Minigame2 = minigame2;
                //round.Minigame3 = minigame3;
                round.FinalMinigame = finalMinigame;


                // CrudRounds.Edit(round);
            }

            Debug.Log("i'm saving round... " + round.ToString());

        }
        void ICrud.List(GameObject table, GameObject row)
        {
            for (int i = 0; i < CrudRounds.GetRounds().Count; i++)
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

            Round round = CrudRounds.GetRounds()[pos];
            row.GetComponent<RowManager>().code = round.Id;
            row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = round.Title;
            row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = round.MyMinigame.Title;
            //  row.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = round.Minigame2.Title;
            // row.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = round.Minigame3.Title;
            //  row.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = round.FinalMinigame.Title;
            // row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{round.Rounds.Count}";
        }

        void ICrud.PrepareModal(EditMode editMode, int code)
        {
            this.editMode = editMode;
            if (this.editMode == EditMode.create)
            {
                ShowMinigames(minigameOne, CrudMinigame.GetMinigamesbyLevel(1), mapMinigame1);
                ShowMinigames(minigameTwo, CrudMinigame.GetMinigamesbyLevel(2), mapMinigame2);
                ShowMinigames(minigameThree, CrudMinigame.GetMinigamesbyLevel(3), mapMinigame3);

            }
            if (this.editMode == EditMode.edit)
            {
                ShowMinigames(minigameOne, CrudMinigame.GetMinigamesbyLevel(1), mapMinigame1);
                ShowMinigames(minigameTwo, CrudMinigame.GetMinigamesbyLevel(2), mapMinigame2);
                ShowMinigames(minigameThree, CrudMinigame.GetMinigamesbyLevel(3), mapMinigame3);

                round = CrudRounds.GetById(code);
                title.text = round.Title;
                // minigameOne.round.
                //titleModal.text = $"Editar partida:{generalGame.Title}";
                // selectedRounds.Clear();
                //  selectedRounds = round.Rounds.ToList<Round>();
                // selectorRounds.CreateList(CrudGeneralGame.GetNoSelectedRounds(code), round.Rounds, selectedRounds);
            }
            if (this.editMode == EditMode.delete)
            {
                // Round.Delete(code);
            }
        }
        void ICrud.ClearModal()
        {
            title.text = "";
            minigameOne.ClearOptions();
            minigameTwo.ClearOptions();
            minigameThree.ClearOptions();
            minigameFinal.ClearOptions();
            // selectorRounds.ClearSelector();
            // Select
        }
        void ShowMinigames(TMP_Dropdown dropdown, List<Minigame> minigames, Dictionary<int, int> map)
        {
            dropdown.ClearOptions();
            OptionDataList optionDataList = new OptionDataList();
            int i = 0;
            foreach (Minigame item in minigames)
            {
                map.Add(i++, item.Id);
                OptionData customOption = new OptionData(item.Title);
                dropdown.options.Add(customOption);
            }
        }

        // void SelectMinigame(int index)
        // {
        //     string selectedValue = dropdown.options[index].text;
        //     Debug.Log("Selected value: " + selectedValue);
        // }

    }
}