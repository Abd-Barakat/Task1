using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Task1
{
    class Question
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

       
    }
}
