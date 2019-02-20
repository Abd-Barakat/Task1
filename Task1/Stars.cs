using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Stars:Question
    {
        private int star;
        public int Star
        {
            get
            {
                return star;
            }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    star= value;
                }
            }
        }

        public Stars(string text, int order, int star=5):base(text,order)
        {
            Star = star;
        }

    }
}
