using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Task1
{
    class Add_dialog :Base
    {
        private readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };

        private GroupBox groupBox = new GroupBox//define groupbox that contain 3 Radio buttons 
        {
            Text = "Question type",
            Size = new System.Drawing.Size(116, 120),
            AutoSize = false,
            Location = new System.Drawing.Point(470, 90),
            Dock = DockStyle.Right & DockStyle.Bottom,
            TabIndex = 1,
            TabStop = true,

        };

        private RadioButton SliderButton = new RadioButton
        {
            Text = "Slider",
            Location = new System.Drawing.Point(10, 30),
            TabIndex = 0,
            TabStop = true

        };

        private RadioButton SmileyButton = new RadioButton
        {
            Text = "Smiley",
            Location = new System.Drawing.Point(10, 60),
            TabIndex = 1,
            TabStop = true
        };

        private RadioButton StarsButton = new RadioButton
        {
            Text = "Stars",
            Location = new System.Drawing.Point(10, 90),
            TabIndex = 2,
            TabStop = true
        };
       
        public override void Make_Empty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                question_box.ForeColor = System.Drawing.Color.Gray;
                question_box.Text = "Write a question here ...";
            }
            else if (ReferenceEquals(box, control1))
            {
                control1.ForeColor = System.Drawing.Color.Gray;
                control1.Text = string.Format("Start ={0}", q.Default_values().ElementAt(0));

            }
            else if (ReferenceEquals(box, control2))
            {
                control2.ForeColor = System.Drawing.Color.Gray;
                control2.Text = string.Format("End ={0}", q.Default_values().ElementAt(1));
            }
            else if (ReferenceEquals(box, control3))
            {
                control3.ForeColor = System.Drawing.Color.Gray;
                control3.Text = string.Format("Start Caption ={0}", q.Default_values().ElementAt(2));

            }
            else if (ReferenceEquals(box, control4))
            {
                control4.ForeColor = System.Drawing.Color.Gray;
                control4.Text = string.Format("End Caption ={0}", q.Default_values().ElementAt(3));

            }

            else if (ReferenceEquals(box, control5))
            {
                control5.ForeColor = System.Drawing.Color.Gray;
                control5.Text = string.Format("Smiles = {0}", q.Default_values().ElementAt(0));
            }

            else if (ReferenceEquals(box, control6))
            {
                control6.ForeColor = System.Drawing.Color.Gray;
                control6.Text = string.Format("Stars = {0}", q.Default_values().ElementAt(0));
            }
        } 

        private void GotFocus(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, SliderButton))
            {
                relese(q);
                q = new Slider();
                q.Question_order = Next_order;
                SliderButton.Checked = true;
                control1.Text = string.Format("Start ={0}", q.Default_values().ElementAt(0));
                control2.Text = string.Format("End ={0}", q.Default_values().ElementAt(1));
                control3.Text = string.Format("Start Caption ={0}", q.Default_values().ElementAt(2));
                control4.Text = string.Format("End Caption ={0}", q.Default_values().ElementAt(3));
                Default_GrouoBox.Visible = true;
            }
            else if (ReferenceEquals(sender, SmileyButton))
            {
                relese(q);
                q = new Smiley();
                q.Question_order = Next_order;
                SmileyButton.Checked = true;
                control5.Text = string.Format("Smiles = {0}", q.Default_values().ElementAt(0));
                Default_GrouoBox2.Visible = true;

            }
            else if (ReferenceEquals(sender, StarsButton))
            {
                relese(q);
                q = new Stars();
                q.Question_order = Next_order;
                StarsButton.Checked = true;
                control6.Text = string.Format("Stars = {0}", q.Default_values().ElementAt(0));
                Default_GrouoBox3.Visible = true;

            }
        }

        private void relese (Question q)
        {
            if (q !=null)
            {
                q = null;
            }
        }

        public override void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            q.Reset_values();
            Make_boxes_Empty();
        }

        private void initialize()
        {

            /////////////////////////////////////////
            question_box.Text = "Write a question here ...";
            /////////////////////////////////////////
            SliderButton.GotFocus += GotFocus;
            SmileyButton.GotFocus += GotFocus;
            StarsButton.GotFocus += GotFocus;
            //////////////////////////////////////////
            SmileyButton.CheckedChanged += CheckedChanged;
            SliderButton.CheckedChanged += CheckedChanged;
            StarsButton.CheckedChanged += CheckedChanged;
            //////////////////////////////////////////
            groupBox.Controls.Add(SliderButton);
            groupBox.Controls.Add(SmileyButton);
            groupBox.Controls.Add(StarsButton);
            /////////////////////////////////////////
            control1.GotFocus += TextChanged;
            control2.GotFocus += TextChanged;
            control3.GotFocus += TextChanged;
            control4.GotFocus += TextChanged;
            control5.GotFocus += TextChanged;
            control6.GotFocus += TextChanged;
            question_box.GotFocus += TextChanged;
            /////////////////////////////////////////
            control1.KeyDown += KeyDown;
            control2.KeyDown += KeyDown;
            control3.KeyDown += KeyDown;
            control4.KeyDown += KeyDown;
            /////////////////////////////////////////
            control1.MouseClick += TextChanged;
            control2.MouseClick += TextChanged;
            control3.MouseClick += TextChanged;
            control4.MouseClick += TextChanged;
            control5.MouseClick += TextChanged;
            control6.MouseClick += TextChanged;
            question_box.MouseClick += TextChanged;
            /////////////////////////////////////////
            Default_GrouoBox.Controls.Add(control1);
            Default_GrouoBox.Controls.Add(control2);
            Default_GrouoBox.Controls.Add(control3);
            Default_GrouoBox.Controls.Add(control4);
            /////////////////////////////////////////
            Save.Click += Save_Click;
            /////////////////////////////////////////
            Default_GrouoBox2.Controls.Add(control5);
            /////////////////////////////////////////
            Default_GrouoBox3.Controls.Add(control6);
            /////////////////////////////////////////
            FORM.Controls.Add(Save);
            FORM.Controls.Add(Dv);
            FORM.Controls.Add(question_box);
            FORM.Controls.Add(groupBox);
            FORM.Controls.Add(Default_GrouoBox);
            FORM.Controls.Add(Default_GrouoBox2);
            FORM.Controls.Add(Default_GrouoBox3);
        }

        public override void Save_Click(object sender, EventArgs e)
        {
            if (check(q.Current_values()))
            {
                if (!question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))
                {
                    int Groupbox_index = Group_Index();//return index of selected control in GroupBox

                    if (Groupbox_index != -1)//if no control selected in GroupBox
                    {

                        try
                        {

                            Dv.Rows[0].Cells[0].Value = question_box.Text;
                            Dv.Rows[0].Cells[1].Value = q.Question_order;
                            Dv.Rows[0].Cells[2].Value = Tables[Groupbox_index + 1];

                            DB.Insert(Groupbox_index, Tables, q);
                            DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            while (result != DialogResult.OK) ;//wait unitl MessageBox closes 
                            FORM.Visible = false;//hide Add dialog
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FORM.Visible = false;
                        }
                       
                    }
                    else
                        MessageBox.Show("Please select question type ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                else if (isEmpty(question_box) || question_box.Text == "")
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question  ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (question_box.Text.Any(char.IsDigit))
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question without numbers   ", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, SliderButton))
            {
                if (!SliderButton.Checked)
                    Default_GrouoBox.Visible = false;
            }
            else if (ReferenceEquals(sender, SmileyButton))
            {
                if (!SmileyButton.Checked)
                    Default_GrouoBox2.Visible = false;
            }
            else if (ReferenceEquals(sender, StarsButton))
            {
                if (!StarsButton.Checked)
                    Default_GrouoBox3.Visible = false;
            }
        }

        public void ShowDialog(DataTable Dt ,int order)
        {
            initialize();
            Dv.DataSource = Dt.Clone();
            Next_order =  order;
            FORM.Visible = true;
        }

        public override bool isEmpty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "Write a question here ...")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control1))
            {
                if (control1.Text == string.Format("Start ={0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, control2))
            {
                if (control2.Text == string.Format("End ={0}", q.Default_values().ElementAt(1)))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control3))
            {
                if (control3.Text == string.Format("Start Caption ={0}", q.Default_values().ElementAt(2)))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control4))
            {
                if (control4.Text == string.Format("End Caption ={0}", q.Default_values().ElementAt(3) ))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control5))
            {
                if (control5.Text == string.Format("Smiles = {0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control6))
            {
                if (control6.Text == string.Format("Stars = {0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
    
        private int Group_Index()
        {

            if (SliderButton.Checked)
                return 0;
            else if (SmileyButton.Checked)
                return 1;
            else if (StarsButton.Checked)
                return 2;
            else
                return -1;
        }
    }

}

