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
<<<<<<< HEAD
        private int Entered_faces;
=======
>>>>>>> 2bf46c17a169e84dd553e5d7fd4c454bb9892dbc
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

        public Smiley(string text, int order, int faces=3):base(text,order, "Smiley")
        {
            Faces = faces;
        }
        public Smiley() : base("", -1, "Smiley")
        {
            Faces = 3;
        }
        public override  List<int> Default_values()
        {
            List<int> temp = new List<int>();
            temp.Add(3);
            return temp;
        }
        public override void Reset_values()
        {
            Faces = 3;
        }
        public override List<int> Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Faces);
            return temp;
        }
        public override void Set_values( List<int> Values)
        {
            Faces = Values[0];
        }
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
