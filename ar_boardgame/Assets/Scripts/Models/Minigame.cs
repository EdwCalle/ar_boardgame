using System.Collections.Generic;


namespace Models
{
    public class Minigame
    {
        private int id;
        private int level;
        private string title;
        private string function;
        private List<Item> models = new List<Item>();
        private List<Question> questions = new List<Question>();
        private List<Group> groups = new List<Group>();
        private ExtraInfo extraInfo;
        private float interactionTime;
        private float resolveTime;
        private float feedbackTime;
        private HeaderType header;

        private SelectionType selection;

        private ExtraComponentType extraComponent;

        private TimerState timerState;
        public int Id
        { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Function { get => function; set => function = value; }
        public List<Item> Models { get => models; set => models = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public List<Group> Groups { get => groups; set => groups = value; }
        public ExtraInfo ExtraInfo { get => extraInfo; set => extraInfo = value; }
        public float InteractionTime { get => interactionTime; set => interactionTime = value; }
        public float ResolveTime { get => resolveTime; set => resolveTime = value; }
        public float FeedbackTime { get => feedbackTime; set => feedbackTime = value; }
        public HeaderType Header { get => header; set => header = value; }
        public SelectionType Selection { get => selection; set => selection = value; }
        public ExtraComponentType ExtraComponent { get => extraComponent; set => extraComponent = value; }
        public int Level { get => level; set => level = value; }
        public TimerState TimerState { get => timerState; set => timerState = value; }
        public void ConfigureLevel(HeaderType header, SelectionType selection, ExtraComponentType extraComponent)
        {
            //  this.level = level;
            this.header = header;
            this.selection = selection;
            this.extraComponent = extraComponent;
        }
        public override string ToString()
        {
            return $"id:{id}, title:{title}, ";
        }
        //public bool ExistSelecction { get => existSelecction; set => existSelecction = value; }







    }
}