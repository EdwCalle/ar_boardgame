
using System.Collections;
using System.Collections.Generic;
using Models;

namespace Minigames
{

    public class Minigame
    {
        private int code;


        private string title;
        private string function;
        private ArrayList models = new ArrayList();
        private List<Question> questions = new List<Question>();
        private List<Group> groups = new List<Group>();
        private ExtraInfo extraInfo;


        private float interactionTime;
        private float resolveTime;
        private float feedbackTime;
        //private bool isMultipleSelection;

        public int Code
        { get => code; set => code = value; }
        public string Title { get => title; set => title = value; }
        public string Function { get => function; set => function = value; }
        public ArrayList Models { get => models; set => models = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public List<Group> Groups { get => groups; set => groups = value; }
        public ExtraInfo ExtraInfo { get => extraInfo; set => extraInfo = value; }
        public float InteractionTime { get => interactionTime; set => interactionTime = value; }
        public float ResolveTime { get => resolveTime; set => resolveTime = value; }
        public float FeedbackTime { get => feedbackTime; set => feedbackTime = value; }
        //public bool ExistSelecction { get => existSelecction; set => existSelecction = value; }



    }
}