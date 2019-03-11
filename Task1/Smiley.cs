using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Task1
{
    class Smiley:Question
    {
        
        private int faces;
        private int Entered_faces;
        /// <summary>
        /// Gets or sets the faces.
        /// </summary>
        /// <value>
        /// The faces.
        /// </value>
        public int Faces
        {
            get
            {
                return faces;
            }
            set
            {
                Entered_faces = value;
                if (value >= 2 && value <= 5)
                {
                    faces = value;
                }
                else
                    MessageBox.Show("Number of faces should be between 2-5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Smiley"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="order">The order.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="faces">The faces.</param>
        public Smiley(string text, int order, int id, int faces=3):base(text,order, "Smiley",id)
        {
            Faces = faces;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Smiley"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Smiley(int id) : base("", -1, "Smiley",id)
        {
            Faces = 3;
        }
        /// <summary>
        /// Return default values.
        /// </summary>
        /// <returns>
        /// question's default values
        /// </returns>
        public override  List<string> Default_values()
        {
            List<string> temp = new List<string>();
            temp.Add("3");
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
            temp.Add(Faces.ToString());
            return temp;
        }
        /// <summary>
        /// Sets the question's values.
        /// </summary>
        /// <param name="Values">The values.</param>
        public override void Set_values( List<string> Values)
        {
            Faces = Int32.Parse( Values[0]);
        }
        /// <summary>
        /// Validates this qustion's values.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (Entered_faces < 2 || Entered_faces > 5)//End caption should be higer than Start caption
            {
                return false;
            }
            return true;
        }
    }
}
