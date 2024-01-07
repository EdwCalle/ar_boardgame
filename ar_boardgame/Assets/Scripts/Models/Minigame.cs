using System.Collections.Generic;
using UnityEngine;
namespace Models
{
    [System.Serializable]
    public class Minigame
    {
        [SerializeField] private int id;
        [SerializeField] private int level;
        [SerializeField] private string title;
        [SerializeField] private string function;
        [SerializeField] private List<Model3D> models = new();
        [SerializeField] private List<Question> questions = new();
        [SerializeField] private ExtraInfo extraInfo;
        [SerializeField] private float interactionTime;
        [SerializeField] private float resolveTime;
        [SerializeField] private float feedbackTime;
        //  [SerializeField] private HeaderType headerType;
        [SerializeField] private SelectionType selectionType;
        [SerializeField] private ExtraComponentType extraComponentType;
        [SerializeField] private TimerState timerState;
        public int Id
        { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Function { get => function; set => function = value; }
        public List<Model3D> Models { get => models; set => models = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public ExtraInfo ExtraInfo { get => extraInfo; set => extraInfo = value; }
        public float InteractionTime { get => interactionTime; set => interactionTime = value; }
        public float ResolveTime { get => resolveTime; set => resolveTime = value; }
        public float FeedbackTime { get => feedbackTime; set => feedbackTime = value; }
        //public HeaderType HeaderType { get => headerType; set => headerType = value; }
        public SelectionType SelectionType { get => selectionType; set => selectionType = value; }
        public ExtraComponentType ExtraComponentType { get => extraComponentType; set => extraComponentType = value; }
        public int Level { get => level; set => level = value; }
        public TimerState TimerState { get => timerState; set => timerState = value; }
        public void ConfigureLevel(SelectionType selection, ExtraComponentType extraComponent)
        {
            //  this.level = level;
            //this.headerType = header;
            this.selectionType = selection;
            this.extraComponentType = extraComponent;
        }
        public override string ToString()
        {
            return $"id:{id}, title:{title}, ";
        }
        //public bool ExistSelecction { get => existSelecction; set => existSelecction = value; }







    }
}