using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Slider:Question
    {
       
        private int start;
        private int start_caption;
        private int end;
        private int end_caption;
        public readonly List<int> Slider_default = new List<int> { 0, 100, 20, 80 };
        
       
        public int Start
        {
            get
            {
                return start;
            }
            set
            {
                    start = value;
            }
        }
        public int Start_Caption
        {
            get
            {
                return start_caption;
            }
            set
            {
                start_caption = value;
            }
        }
        public int End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }
        public int End_Caption
        {
            get
            {
                return end_caption;
            }
            set
            {
                end_caption = value;
            }
        }

        public Slider(string text ,int order, int start=0 ,int start_caption=20,int end=100,int end_caption=80):base(text,order)
        {
            Start = start;
            Start_Caption = start_caption;
            End = end;
            End_Caption = end_caption;
        }
        public Slider() : base("", -1)
        {

        }
        public override List <int > Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Start);
            temp.Add(Start_Caption);
            temp.Add(End);
            temp.Add(End_Caption);
            return temp;
        }
        public override List<int> Default_values()
        {
            return Slider_default;
        }
        public override void Reset_values()
        {
            Start = Slider_default[0];
            Start_Caption = Slider_default[1];
            End = Slider_default[2];
            End_Caption =Slider_default[3];
        }
        public override void Set_values(List<int> Values)
        {
            Start = Values[0];
            Start_Caption = Values[1];
            End = Values[2];
            End_Caption = Values[3];
        }
    }
}
