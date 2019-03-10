using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
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
            Location = new System.Drawing.Point(470, 20),
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


        protected override void Make_Empty(TextBox box)
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

        private void GotFocus(object sender, EventArgs e)//focus event handler for radio buttons in groupBox 
        {
            relese(q);//call method release that release  q object if refere to another object 
            if (ReferenceEquals(sender, SliderButton))//if slider radio button 
            {

                q = new Slider(Next_ID)//create slider object
                {
                    ID = Next_ID,
                };
                SliderButton.Checked = true;//set check property of Slider radio button to true
                control1.Text = string.Format("Start ={0}", q.Default_values().ElementAt(0));//fill textbox with default value of start 
                control2.Text = string.Format("End ={0}", q.Default_values().ElementAt(1));//fill textbox with default value of end 
                control3.Text = string.Format("Start Caption ={0}", q.Default_values().ElementAt(2));//fill textbox with default value of start caption 
                control4.Text = string.Format("End Caption ={0}", q.Default_values().ElementAt(3));//fill textbox with default value of end caption 
                Default_GrouoBox.Controls.Add(QuestionOrderUpDown);
                Default_GrouoBox.Visible = true;//make groupbox that contain above controls visible
            }
            else if (ReferenceEquals(sender, SmileyButton))//if Smiley radio button 
            {
                q = new Smiley(Next_ID)//create smiley object
                {
                    Question_order = Next_ID//sign qustion_order property to next_order field
                };
                SmileyButton.Checked = true;//set check property of Smiley radio button to true
                control5.Text = string.Format("Smiles = {0}", q.Default_values().ElementAt(0));//fill textbox with default value of faces 
                Default_GrouoBox2.Controls.Add(QuestionOrderUpDown);
                Default_GrouoBox2.Visible = true;//make groupbox that contain above controls visible

            }
            else if (ReferenceEquals(sender, StarsButton))//if Stars radio button 
            {
                q = new Stars(Next_ID)//create star object               
                {
                    Question_order = Next_ID//sign qustion_order property to next_order field
                };
                
                StarsButton.Checked = true;//set check property of Stars radio button to true
                control6.Text = string.Format("Stars = {0}", q.Default_values().ElementAt(0));//fill textbox with default value of stars 
                Default_GrouoBox3.Controls.Add(QuestionOrderUpDown);
                Default_GrouoBox3.Visible = true;//make groupbox that contain above controls visible

            }
        }

        private void relese (Question q)
        {
            if (q !=null)//to avoid null refrence exception
            {
                q = null;
            }
        }

        protected override void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            q.Reset_values();//call reset values method in Questions class
            Make_boxes_Empty();//call method that clear all textboxes
        }

        private void initialize()//initialize controls 
        {
            Next_Number_UpDown();
            QuestionOrderUpDown.ValueChanged += QuestionOrderUpDown_Click;
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
            Cancel.Click += Cancel_Click;
            /////////////////////////////////////////
            Default_GrouoBox2.Controls.Add(control5);
            /////////////////////////////////////////
            Default_GrouoBox3.Controls.Add(control6);
            /////////////////////////////////////////
            FORM.Controls.Add(Save);
            FORM.Controls.Add(Cancel);
            FORM.Controls.Add(question_box);
            FORM.Controls.Add(groupBox);
            FORM.Controls.Add(Default_GrouoBox);
            FORM.Controls.Add(Default_GrouoBox2);
            FORM.Controls.Add(Default_GrouoBox3);
        }

        private void QuestionOrderUpDown_Click(object sender, EventArgs e)
        {
            if (QuestionOrderUpDown.Value > oldValue)
            {
                Next_Number_UpDown();
                oldValue =(int) QuestionOrderUpDown.Value;
            }
            else
            {
                Prev_Number_UpDown();
                oldValue = (int)QuestionOrderUpDown.Value;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.FORM.Close();
        }

        protected override void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            if (check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                if (!question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))//check question textbox if contain invalid inputs or default text
                {
                    int Groupbox_index = Group_Index();//return index of selected control in GroupBox

                    if (Groupbox_index != -1)//if no control selected in GroupBox
                    {

                        try
                        {
                            DB.Insert(Groupbox_index, Tables, q);//call Insert method in DBclass to insert the new question into database
                            FORM.Visible = false;//hide Add dialog
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            using (StreamWriter stream = new StreamWriter(@"Error.txt", true))//save errors in Error.txt file
                            {
                                stream.WriteLine("-------------------------------------------------------------------\n");
                                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                                while (ex != null)
                                {

                                    stream.WriteLine("Message :\n" + ex.Message);
                                    stream.WriteLine("Stack trace :\n" + ex.StackTrace);

                                    ex = ex.InnerException;
                                }
                            }
                            FORM.Visible = false;//hide add_dialog form 
                        }
                       
                    }
                    else
                        MessageBox.Show("Please select question type ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if no question type is selected 


                }
                else if (isEmpty(question_box) || question_box.Text == "")//if question textbox is empty or contain default value
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question  ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (question_box.Text.Any(char.IsDigit))//if question textbox  contain a number in it
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question without numbers   ", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void CheckedChanged(object sender, EventArgs e)//check event hanlder of radio buttons to show groupbox that contain related controls 
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

        public  Add_dialog(int Next_id)//constructor to show add_dialog and initialize it's fields
        {
            initialize();
            Next_ID = Next_id;
            FORM.Text = "Add a question";
            FORM.Visible = true;
            
        }

        protected override bool isEmpty(TextBox box)//this method to check if text box is contain default value or not 
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
    
        private int Group_Index()//return a number that represent radio buttons (0 => Slider,1 => Smiley ,2 => Stars)
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

