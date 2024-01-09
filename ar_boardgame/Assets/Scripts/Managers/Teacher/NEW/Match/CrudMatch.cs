using Api;
using Models;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
namespace Pages
{

    public class CrudMatch : MonoBehaviour, ICrud

    {
        public TMP_InputField title;
        public TMP_Dropdown numberPlayer;
        public SelectorManager selectorManager;
        public TextMeshProUGUI titleModal;
        Match match;
        // public List<Round> selectedRounds;
        // public List<Question> selectedAngelQuestions;
        // public List<Question> selectedDemonsQuestions;
        private EditMode editMode;

        /// <summary>
        /// 
        /// </summary>
        public QuestionCrudManager angelQuestionManager;
        public QuestionCrudManager demonsQuestionManager;
        void ICrud.Save()
        {
            var selectedRounds = new List<Round>(selectorManager.SelectedRounds);
            var selectedAngelQuestions = new List<Question>(angelQuestionManager.SelectedQuestions);
            var selectedDemonsQuestions = new List<Question>(demonsQuestionManager.SelectedQuestions);
            if (editMode == EditMode.create)
            {
                match = new Match
                {
                    Title = title.text,
                    Rounds = selectedRounds,
                    AngelQuestions = selectedAngelQuestions,
                    DemonQuestions = selectedDemonsQuestions,

                    NumberPlayers = numberPlayer.value + 1
                };
                CrudMatchGame.Create(match);
            }
            else
            {
                match.Title = title.text;
                match.Rounds = selectedRounds;
                match.AngelQuestions = selectedAngelQuestions;
                match.DemonQuestions = selectedDemonsQuestions;
                match.NumberPlayers = numberPlayer.value + 1;
                CrudMatchGame.Edit(match);
            }

            Debug.Log("i'm saving game... " + match.ToString());
        }
        Row[] ICrud.List()
        {
            Row[] rows = new Row[CrudMatchGame.GetMatches().Count];
            for (int i = 0; i < CrudMatchGame.GetMatches().Count; i++)
            {
                Match item = CrudMatchGame.GetMatches()[i];
                Row row = new()
                {
                    Id = item.Id,
                    Cells = new string[3]
                };
                row.Cells[0] = item.Title;
                row.Cells[1] = $"{item.Rounds.Count}";
                row.Cells[2] = $"{item.NumberPlayers}";
                rows[i] = row;
            }
            return rows;
        }
        void ICrud.PrepareModal(EditMode editMode, int code)
        {

            this.editMode = editMode;
            //    selectorManager.get.normalizedPosition = new Vector2(0f, 1f);

            if (this.editMode == EditMode.create)
            {

                titleModal.text = "Crear Partidas";
                // var selectedRounds = new List<Round>();
                // var selectedAngelQuestions = new List<Question>();
                // var selectedDemonsQuestions = new List<Question>();
                angelQuestionManager.ListQuestions(CrudExtraQuestions.GetAngelQuestions(), new List<Question>());
                demonsQuestionManager.ListQuestions(SingletonTeacher.GetDemonsQuestions(), new List<Question>());
                selectorManager.CreateList(CrudRounds.GetRounds(), new List<Round>());
            }
            if (this.editMode == EditMode.edit)
            {
                match = CrudMatchGame.GetById(code);
                title.text = match.Title;
                titleModal.text = $"Editar partida:{match.Title}";
                numberPlayer.value = match.NumberPlayers - 1;
                // var selectedRounds = new List<Round>(match.Rounds);
                // var selectedAngelQuestions = new List<Question>(match.AngelQuestions);
                // var selectedDemonsQuestions = new List<Question>(match.DemonQuestions);
                selectorManager.CreateList(CrudMatchGame.GetNoSelectedRounds(code), match.Rounds);
                angelQuestionManager.ListQuestions(CrudMatchGame.GetNoSelectedQuestions(code, "angels"), match.AngelQuestions);
                demonsQuestionManager.ListQuestions(CrudMatchGame.GetNoSelectedQuestions(code, "demons"), match.DemonQuestions);
            }
            if (this.editMode == EditMode.delete)
            {
                CrudMatchGame.Delete(code);
            }
        }
        void ICrud.ClearModal()
        {
            title.text = "";
            angelQuestionManager.ClearQuestions();
            demonsQuestionManager.ClearQuestions();
            selectorManager.ClearSelector();

        }
    }
}