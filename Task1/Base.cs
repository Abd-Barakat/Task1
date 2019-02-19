using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace Task1
{
    abstract  public class Base
    {
        public int Question_order;
        public List<int> Slider = new List<int>();
        public readonly int[] Slider_default =new int[] { 0,100,20,80};
        public int Num_Faces;
        public int Num_Stars;
        public int Faces;
        public int Stars;
        public Form FORM
        {
            protected set
            {
                form = value;
            }
            get
            {
                return form;
            }
        }
        public TextBox control1 = new TextBox //textbox for start value in Slider questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 0,
        };

         public TextBox control2 = new TextBox//textbox for end value in Slider questions
        {
            Location = new System.Drawing.Point(140, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 1,

        };


        public TextBox control3 = new TextBox//textbox for start value caption in Slider questions
        {
            Location = new System.Drawing.Point(275, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 2,

        };


        public TextBox control4 = new TextBox//textbox for End value caption in Slider questions
        {
            Location = new System.Drawing.Point(410, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 3,

        };
        public TextBox control5 = new TextBox//textbox for smile Faces in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };

        public TextBox control6 = new TextBox//textbox for stars number  in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        public TextBox question_box = new TextBox//text box to write a new question within 
        {
            Location = new System.Drawing.Point(50, 98),
            TabStop = true,
            TabIndex = 0,
            Size = new System.Drawing.Size(400, 50),
            ForeColor = System.Drawing.Color.Gray,
            Multiline=true,

        };
        public Form form = new Form
        {
            Width = 600,
            Height = 400,
            MaximumSize = new System.Drawing.Size(663, 400),
            MinimumSize = new System.Drawing.Size(663, 400),

        };
        public Button Save = new Button
        {
            Text = "Save",
            Location = new System.Drawing.Point(50, 300),
        };
        public DataGridView Dv = new DataGridView
        {
            Size = new System.Drawing.Size(536, 50),
            Location = new System.Drawing.Point(50, 20),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToDeleteRows = false,
            AllowUserToAddRows = true,
            AllowDrop = false,
            ReadOnly = true,
            AllowUserToResizeRows = false,
            AllowUserToResizeColumns = false,

        };
        public GroupBox Default_GrouoBox = new GroupBox //to hold a slider question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(536, 50),
            Text = "Current values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        public GroupBox Default_GrouoBox2 = new GroupBox//to hold a smiley question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(400, 50),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        public GroupBox Default_GrouoBox3 = new GroupBox//to hold a star question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(400, 50),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };



        







        public abstract void Make_Empty(TextBox box);
        public abstract bool isEmpty(TextBox box);
        public abstract void Save_Click(object sender, EventArgs e);
        public abstract string Question_Type();
        public abstract void Reset();




        public bool check()
        {
            if (!Check_Update())
            {
                return false;
            }
            if (Question_Type() == "Slider")
            {
                if (Slider[0] < 0 || Slider[0] > 100)//validate user input (Start value should be between 0-100)
                {
                    Make_Empty(control1);
                    Reset();//call reset method 
                    MessageBox.Show("Start value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[0] >= Slider[1])
                {
                    Make_Empty(control1);
                    Reset();
                    MessageBox.Show("Start value should be lower than end value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[0] >= Slider[2])
                {
                    Make_Empty(control1);
                    Reset();
                    MessageBox.Show("Start value should be lower than Start caption ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[1] < 0 || Slider[1] > 100)//End value should be between 0-100
                {
                    Reset();
                    Make_Empty(control2);
                    MessageBox.Show("End value should be betweem 0-100 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[2] < 0 || Slider[2] > 100)//Start Caption should be between 0-100 
                {
                    Reset();
                    Make_Empty(control3);
                    MessageBox.Show("Start caption should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[2] >= Slider[1])//Start Caption should be lower than End value 
                {
                    Reset();
                    Make_Empty(control3);
                    MessageBox.Show("Start Caption should be lower than End value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[2] >= Slider[3])
                {
                    Reset();
                    Make_Empty(control3);
                    MessageBox.Show("Start Caption should be lower than End caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[3] < 0 || Slider[3] > 100)//End caption should be between 0-100
                {
                    Reset();
                    Make_Empty(control4);
                    MessageBox.Show("End Caption should between 0-100 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[3] >= Slider[1])//End caption should be lower than End value 
                {
                    Reset();
                    Make_Empty(control4);
                    MessageBox.Show("End Caption should be Lower than End value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[3] <= Slider[2])//End caption should be higer than Start caption
                {
                    Reset();
                    Make_Empty(control4);
                    MessageBox.Show("End caption should be higer than Start caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (Question_Type() == "Smiley")
            {
                if (Faces > 5 || Faces < 0)
                {
                    Reset();
                    Make_Empty(control5);
                    MessageBox.Show("Number of Smiles should between 0-5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (Question_Type() == "Stars")
            {
                if (Stars > 10 || Faces < 0)
                {
                    Reset();
                    Make_Empty(control6);
                    MessageBox.Show("Number of stars  should be between 0-10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;

        }


        public void Make_Empty()
        {
            Make_Empty(control1);
            Make_Empty(control2);
            Make_Empty(control3);
            Make_Empty(control4);
            Make_Empty(control5);
            Make_Empty(control6);
        }

        public bool Check_Update()
        {

            if (question_box.Text == "")
            {
                Make_Empty(question_box);
            }
            if (control1.Text == "")
            {
                Make_Empty(control1);
            }
            else
            {
                try
                {
                    if (!isEmpty(control1))
                        Slider[0] = Int32.Parse(control1.Text);//validate user input 

                }
                catch (FormatException)
                {
                    MessageBox.Show("Start value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control2.Text == "")
            {
                Make_Empty(control2);
            }
            else
            {
                try
                {
                    if (!isEmpty(control2))
                        Slider[1] = Int32.Parse(control2.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("End value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control3.Text == "")
            {
                Make_Empty(control3);
            }
            else
            {
                try
                {
                    if (!isEmpty(control3))
                        Slider[2] = Int32.Parse(control3.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("Start caption should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control4.Text == "")
            {

                Make_Empty(control4);
            }
            else
            {
                try
                {
                    if (!isEmpty(control4))
                        Slider[3] = Int32.Parse(control4.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("End caption should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            if (control5.Text == "")
            {
                Make_Empty(control5);
            }

            else
            {
                try
                {
                    if (!isEmpty(control5))
                        Faces = Int32.Parse(control5.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Number of Smiles should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control6.Text == "")
            {
                Make_Empty(control6);
            }
            else
            {
                try
                {
                    if (!isEmpty(control6))
                        Stars = Int32.Parse(control6.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Number of Stars should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            return true;
        }

        public void TextChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, question_box))
            {
                if (isEmpty(question_box))
                {
                    question_box.Text = "";
                    question_box.ForeColor = System.Drawing.Color.Black;
                }
            }
            else if (ReferenceEquals(sender, control1))
            {
                if (isEmpty(control1))
                {
                    control1.Text = "";
                    control1.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, control2))
            {
                if (isEmpty(control2))
                {
                    control2.Text = "";

                    control2.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, control3))
            {
                if (isEmpty(control3))
                {
                    control3.Text = "";
                    control3.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, control4))
            {
                if (isEmpty(control4))
                {
                    control4.Text = "";
                    control4.ForeColor = System.Drawing.Color.Black;
                }

            }

            else if (ReferenceEquals(sender, control5))
            {
                if (isEmpty(control5))
                {
                    control5.Text = "";
                    control5.ForeColor = System.Drawing.Color.Black;
                }

            }

            else if (ReferenceEquals(sender, control6))
            {
                if (isEmpty(control6))
                {
                    control6.Text = "";
                    control6.ForeColor = System.Drawing.Color.Black;
                }

            }
        }

        public void Open_connection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        public void KeyDown(object sender, KeyEventArgs e)//to move to next control 
        {
            if (e.KeyCode == Keys.Enter)
            {
                Default_GrouoBox.SelectNextControl((TextBox)sender, true, false, false, true);
            }
        }
    }
}
