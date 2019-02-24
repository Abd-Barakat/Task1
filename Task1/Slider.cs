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
                if (value >= 0 && value <= 100)
                    start = value;
                else
                    MessageBox.Show("Start value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                if (value >= 0 && value <= 100)
                    start_caption = value;
                else
                    MessageBox.Show("Start caption should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        public int End_Caption
        {
            get
            {
                return end_caption;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    end_caption = value;
                else
                    MessageBox.Show("End caption should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

<<<<<<< HEAD
        public Slider(string text, int order, int start = 0, int start_caption = 20, int end = 100, int end_caption = 80) : base(text, order, "Slider")
=======
        public Slider(string text ,int order, int start=0 ,int start_caption=20,int end=100,int end_caption=80):base(text,order, "Slider")
>>>>>>> 2bf46c17a169e84dd553e5d7fd4c454bb9892dbc
        {
            Start = start;
            Start_Caption = start_caption;
            End = end;
            End_Caption = end_caption;

        }
        public Slider() : base("", -1, "Slider")
        {
            Start = Slider_default[0];
<<<<<<< HEAD
            End = Slider_default[1];
            Start_Caption = Slider_default[2];
            End_Caption = Slider_default[3];
            
=======
            Start_Caption = Slider_default[1];
            End = Slider_default[2];
            End_Caption = Slider_default[3];
>>>>>>> 2bf46c17a169e84dd553e5d7fd4c454bb9892dbc
        }
        public override List<int> Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Start);
            temp.Add(End);
            temp.Add(Start_Caption);
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
            End_Caption = Slider_default[3];

        }
        public override void Set_values(List<int> Values)
        {
            Start = Values[0];
            End = Values[1];
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
            if (Start >= Start_Caption)
            {
                Reset_values();

                MessageBox.Show("Start value should be lower than Start caption ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (Start_Caption >= End)//Start Caption should be lower than End value 
            {
                MessageBox.Show(Start_Caption.ToString());
                Reset_values();
                MessageBox.Show("Start Caption should be lower than End value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Start_Caption >= End_Caption)
            {
                Reset_values();
                MessageBox.Show("Start Caption should be lower than End caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (End_Caption >= End)//End caption should be lower than End value 
            {
                Reset_values();
                MessageBox.Show("End Caption should be Lower than End value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (End_Caption <= Start_Caption)//End caption should be higer than Start caption
            {
                Reset_values();
                MessageBox.Show("End caption should be higer than Start caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
