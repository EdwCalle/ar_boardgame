
using System.Collections;
using System.Collections.Generic;
using Models;

namespace Minigames
{

    public class Minigame
    {
        private int code;
        private int timer;

        private string title;
        private string function;
        private ArrayList models = new ArrayList();
        private List<Question> questions = new List<Question>();
        private List<Group> groups = new List<Group>();

        private ExtraInfo extraInfo;
        //private bool isMultipleSelection;

        public int Code
        { get => code; set => code = value; }
        public int Timer { get => timer; set => timer = value; }
        public string Title { get => title; set => title = value; }
        public string Function { get => function; set => function = value; }
        public ArrayList Models { get => models; set => models = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public List<Group> Groups { get => groups; set => groups = value; }
        public ExtraInfo ExtraInfo { get => extraInfo; set => extraInfo = value; }

    }
}