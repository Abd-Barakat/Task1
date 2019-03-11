using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Task1
{
    class Stars:Question
    {
        private int star =5;
        private int Entered_star;
        /// <summary>
        /// Gets or sets the star.
        /// </summary>
        /// <value>
        /// The star.
        /// </value>
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
                    throw new ArgumentOutOfRangeException();
                }

            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Stars"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Stars (int id ):base ("",-1, "Stars",id)//constructor used to add question with empty fields
        {
            Star = 5;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Stars"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="order">The order.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="star">The star.</param>
        public Stars(string text, int order,int id, int star=5):base(text,order, "Stars",id)//constructor used to initialize  fields and send text,order and stars to question constuctor
        {
            Star = star;
        }
        /// <summary>
        /// Return default values.
        /// </summary>
        /// <returns>
        /// question's default values
        /// </returns>
        public override List<string> Default_values()
        {
            List<string> temp = new List<string>();
            temp.Add(Star.ToString());
            return temp;
        }
        /// <summary>
        /// Return current values.
        /// </summary>
        /// <returns>
        /// question's current values
        /// </returns>
        public override List<string> Current_values()
        {
            List<string> temp = new List<string>();
            temp.Add(Star.ToString());
            return temp;
        }
        /// <summary>
        /// Sets the question's values.
        /// </summary>
        /// <param name="Values">The values.</param>
        public override void Set_values(List<string> Values)
        {

            Star =Int32.Parse( Values[0]);
        }
        /// <summary>
        /// Validates this qustion's values.
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
