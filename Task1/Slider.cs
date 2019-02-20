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
        public readonly int[] Slider_default = new int[] { 0, 100, 20, 80 };
        
       
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


    }
}
