using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Smiley:Question
    {
        
        private int faces =3;
        public int Faces
        {
            get
            {
                return faces;
            }
            set
            {
                if (value >= 2 && value <= 5)
                {
                    faces = value;
                }
            }
        }

        public Smiley(string text, int order, int faces=3):base(text,order)
        {
            Faces = faces;
        }
        public Smiley() : base("", -1)
        {

        }
        public override  List<int> Default_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Faces);
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
    }
}
