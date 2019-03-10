using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    class Stars:Question
    {
        private int star =5;
        private int Entered_star;

        public int Star//property to get and set field within range[0-100]
        {
            get
            {
                return star;
            }
            set
            {
                Entered_star = value;//save user input what ever if correct or not (used in Validate Method)
                if (value >= 0 && value <= 10)
                {
                    star = value;
                }
                else
                {
                    MessageBox.Show("Number of stars  should be between 0-10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        public Stars (int id ):base ("",-1, "Stars",id)//constructor used to add question with empty fields
        {
            Star = 5;
        }
        public Stars(string text, int order,int id, int star=5):base(text,order, "Stars",id)//constructor used to initialize  fields and send text,order and stars to question constuctor
        {
            Star = star;
        }
        /// <summary>
        /// return default values
        /// </summary>
        /// <returns></returns>
        public override List<string> Default_values()
        {
            List<string> temp = new List<string>();
            temp.Add(Star.ToString());
            return temp;
        }
        /// <summary>
        /// Resets the values.
        /// </summary>
        public override void Reset_values()
        {
            Star = 5;
        }
        /// <summary>
        /// return currents the values.
        /// </summary>
        /// <returns></returns>
        public override List<string> Current_values()
        {
            List<string> temp = new List<string>();
            temp.Add(Star.ToString());
            return temp;
        }
        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="Values"></param>
        public override void Set_values(List<string> Values)
        {

            Star =Int32.Parse( Values[0]);
        }
        /// <summary>
        /// validate user inputs
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (Entered_star < 0|| Entered_star >10)//End caption should be higer than Start caption
            {
                return false;
            }
            return true;
        }
    }
}
