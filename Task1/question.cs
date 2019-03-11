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
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
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
        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        /// <value>
        /// The question text.
        /// </value>
        public string Question_text
        {
            set
            {
                if (value.Contains("'"))
                {
                   value= value.Replace("'", "''");//replace each ' with '' 
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
        /// <summary>
        /// Gets or sets the question order.
        /// </summary>
        /// <value>
        /// The question order.
        /// </value>
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
        /// <summary>
        /// Gets or sets the type of the question.
        /// </summary>
        /// <value>
        /// The type of the question.
        /// </value>
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



        /// <summary>
        /// Initializes a new instance of the <see cref="Question"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="order">The order.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        public Question(string text, int order,string type,int id)
        {
            Question_text = text;
            Question_order = order;
            Question_type = type;
            ID = id;
        }
        /// <summary>
        /// Return default values.
        /// </summary>
        /// <returns>
        /// question's default values
        /// </returns>
        public abstract List<string> Default_values();
        /// <summary>
        /// Return current values.
        /// </summary>
        /// <returns>
        /// question's current values
        /// </returns>
        public abstract List<string> Current_values();
        /// <summary>
        /// Sets the question's values.
        /// </summary>
        /// <param name="Values">The values.</param>
        public abstract void Set_values(List<string> Values);

        /// <summary>
        /// Validates this qustion's values.
        /// </summary>
        /// <returns></returns>
        public abstract bool Validate();
       
    }
}
