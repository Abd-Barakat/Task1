using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Task1
{
    public abstract class Question
    {
        private string question_text;

        private int question_order;

        public string Question_text
        {
            set
            {
                question_text = value;
            }
            get
            {
                return question_text;
            }
        }

        public int Question_order
        {
            set
            {
                question_order = value;
            }
            get
            {
                return question_order;
            }
        }

        public Question(string text, int order)
        {
            Question_text = text;
            Question_order = order;
        }

        public abstract List<int> Default_values();
        public abstract void Reset_values();
        public abstract List<int> Current_values();
        public abstract void Set_values(List<int> Values);
    }
}
