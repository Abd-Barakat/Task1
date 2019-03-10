using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Task1
{
    class Slider : Question
    {

        private int start;
        private string start_caption;
        private int end;
        private string end_caption;
        public readonly List<string> Slider_default = new List<string> { "0", "100", "Not satisfied", "Extremely statisfied" };


        public int Start
        {
            get
            {
                return start;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    start = value;
                else
                    MessageBox.Show("Start value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public string Start_Caption
        {
            get
            {
                return start_caption;
            }
            set
            {
                if (!value.Any(char.IsPunctuation))
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
                if (value >= 0 && value <= 100)
                    end = value;
                else
                    MessageBox.Show("End value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public string End_Caption
        {
            get
            {
                return end_caption;
            }
            set
            {
                if (!value.Any(char.IsPunctuation))
                    end_caption = value;

            }
        }

        public Slider(string text, int order, int id ,int start = 0, int end = 100 , string start_caption = "Not satisfied", string end_caption ="Extremely statisfied" ) : base(text, order, "Slider",id)
        {
            Start = start;
            Start_Caption = start_caption;
            End = end;
            End_Caption = end_caption;

        }
        public Slider(int id) : base("", -1, "Slider",id)
        {
            Start = Int32.Parse( Slider_default[0]);
            End = Int32.Parse(Slider_default[1]);
            Start_Caption = Slider_default[2];
            End_Caption = Slider_default[3];
        }
        public override List<string> Current_values()
        {
            List<string> temp = new List<string>();
            temp.Add(Start.ToString());
            temp.Add(End.ToString());
            temp.Add(Start_Caption);
            temp.Add(End_Caption);
            return temp;
        }
        public override List<string> Default_values()
        {
            return Slider_default;
        }
        public override void Reset_values()
        {
            Start = Int32.Parse(Slider_default[0]);
            Start_Caption = Slider_default[2];
            End = Int32.Parse(Slider_default[1]);
            End_Caption = Slider_default[3];

        }
        public override void Set_values(List<string> Values)
        {
            Start = Int32.Parse( Values[0]);
            End = Int32.Parse(Values[1]);
            Start_Caption = Values[2];
            End_Caption = Values[3];
         
        }

        public override bool Validate()
        {

            if (Start >= End)
            {
                Reset_values();
                MessageBox.Show("Start value should be lower than end value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
         

         
            return true;
        }
    }
}
