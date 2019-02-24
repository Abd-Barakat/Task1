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
        private string question_type;
        private int question_order;

        public string Question_text
        {
            set
            {
                if (!value.Any(char.IsPunctuation))
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
        public string Question_type
        {
<<<<<<< HEAD
            private set
=======
             set
>>>>>>> 2bf46c17a169e84dd553e5d7fd4c454bb9892dbc
            {
                question_type = value;
            }
            get
            {
                return question_type;
            }
        }
<<<<<<< HEAD




=======
>>>>>>> 2bf46c17a169e84dd553e5d7fd4c454bb9892dbc
        public Question(string text, int order,string type)
        {
            Question_text = text;
            Question_order = order;
            Question_type = type;
        }

        public abstract List<int> Default_values();
        public abstract void Reset_values();
        public abstract List<int> Current_values();
        public abstract void Set_values(List<int> Values);


        public abstract bool Validate();
       
    }
}
