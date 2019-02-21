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
        public Stars ():base ("",-1)
        {

        }
        public Stars(string text, int order, int star=5):base(text,order)
        {
            Star = star;
        }
        public override List<int> Default_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Star);
            return temp;
        }
        public override void Reset_values()
        {
            Star = 5;
        }
        public override List<int> Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Star);
            return temp;
        }
        public override void Set_values(List<int> Values)
        {
            Star = Values[0];
        }
    }
}
