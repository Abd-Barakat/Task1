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

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
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
        /// <summary>
        /// Gets or sets the start caption.
        /// </summary>
        /// <value>
        /// The start caption.
        /// </value>
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
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
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
        /// <summary>
        /// Gets or sets the end caption.
        /// </summary>
        /// <value>
        /// The end caption.
        /// </value>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="order">The order.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="start_caption">The start caption.</param>
        /// <param name="end_caption">The end caption.</param>
        public Slider(string text, int order, int id ,int start = 0, int end = 100 , string start_caption = "Not satisfied", string end_caption ="Extremely statisfied" ) : base(text, order, "Slider",id)
        {
            Start = start;
            Start_Caption = start_caption;
            End = end;
            End_Caption = end_caption;

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Slider(int id) : base("", -1, "Slider",id)
        {
            Start = Int32.Parse( Slider_default[0]);
            End = Int32.Parse(Slider_default[1]);
            Start_Caption = Slider_default[2];
            End_Caption = Slider_default[3];
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
            temp.Add(Start.ToString());
            temp.Add(End.ToString());
            temp.Add(Start_Caption);
            temp.Add(End_Caption);
            return temp;
        }
        /// <summary>
        /// Return default values.
        /// </summary>
        /// <returns>
        /// question's default values
        /// </returns>
        public override List<string> Default_values()
        {
            return Slider_default;
        }
        /// <summary>
        /// Resets question's values.
        /// </summary>
        public void Reset_values()
        {
            Start = Int32.Parse(Slider_default[0]);
            Start_Caption = Slider_default[2];
            End = Int32.Parse(Slider_default[1]);
            End_Caption = Slider_default[3];

        }
        /// <summary>
        /// Sets the question's values.
        /// </summary>
        /// <param name="Values">The values.</param>
        public override void Set_values(List<string> Values)
        {
            Start = Int32.Parse( Values[0]);
            End = Int32.Parse(Values[1]);
            Start_Caption = Values[2];
            End_Caption = Values[3];
         
        }
        /// <summary>
        /// Validates this qustion's values.
        /// </summary>
        /// <returns></returns>
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
