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
        private int id;
        public int ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public string Question_text
        {
            set
            {
                if (value.Contains("'"))
                {
                   value= value.Replace("'", "''");
                    question_text = value;
                }
                else
                {
                    question_text = value;
                }
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
             set
            {
                question_type = value;
            }
            get
            {
                return question_type;
            }
        }




        public Question(string text, int order,string type,int id)
        {
            Question_text = text;
            Question_order = order;
            Question_type = type;
            ID = id;
        }

        public abstract List<string> Default_values();
        public abstract void Reset_values();
        public abstract List<string> Current_values();
        public abstract void Set_values(List<string> Values);


        public abstract bool Validate();
       
    }
}
