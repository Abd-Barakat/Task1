using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Smiley:Question
    {
        
        private int faces;
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

    }
}
