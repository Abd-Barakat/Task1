using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.IO;
namespace Task1
{
    public partial class Add : Task1.BaseForm
    {
        private readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };
        DBclass DB = new DBclass();
        private int Next_ID;
        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="Next_ID">The next question's id.</param>
        public Add(int Next_ID)
        {
            InitializeComponent();
            this.Next_ID = Next_ID;
        }

        /// <summary>
        /// Handles the ValueChanged event of the QuestionOrderUpDown control, call Prev_Number_UpDown or Next_Number_UpDown depends on up or down button that pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void QuestionOrderUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, QuestionOrderUpDown1))
            {
                if (QuestionOrderUpDown1.Value > oldValue)
                {
                    Next_Number_UpDown(QuestionOrderUpDown1);
                    oldValue = (int)QuestionOrderUpDown1.Value;
                }
                else
                {
                    Prev_Number_UpDown(QuestionOrderUpDown1);
                    oldValue = (int)QuestionOrderUpDown1.Value;
                }
            }
            else if (ReferenceEquals(sender, QuestionOrderUpDown2))
            {
                if (QuestionOrderUpDown2.Value > oldValue)
                {
                    Next_Number_UpDown(QuestionOrderUpDown2);
                    oldValue = (int)QuestionOrderUpDown2.Value;
                }
                else
                {
                    Prev_Number_UpDown(QuestionOrderUpDown2);
                    oldValue = (int)QuestionOrderUpDown2.Value;
                }
            }
            else if (ReferenceEquals(sender, QuestionOrderUpDown3))
            {
                if (QuestionOrderUpDown3.Value > oldValue)
                {
                    Next_Number_UpDown(QuestionOrderUpDown3);
                    oldValue = (int)QuestionOrderUpDown3.Value;
                }
                else
                {
                    Prev_Number_UpDown(QuestionOrderUpDown3);
                    oldValue = (int)QuestionOrderUpDown3.Value;
                }
            }

        }
        /// <summary>
        /// increment question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        private void Next_Number_UpDown(NumericUpDown QuestionOrderUpDown)
        {
            if (question_order == null)
            {
                question_order = DB.Orders();
                foreach (DataRow row in question_order.Rows)
                {
                    Reserved_orders.Add((int)row.ItemArray[0]);
                }
            }
            while (QuestionOrderUpDown.Value == -1 || Reserved_orders.Contains((int)QuestionOrderUpDown.Value))
            {
                QuestionOrderUpDown.Value++;
            }
            q.Question_order = (int)QuestionOrderUpDown.Value;
        }

        /// <summary>
        /// decrement question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        private void Prev_Number_UpDown(NumericUpDown QuestionOrderUpDown)
        {
            if (question_order == null)
            {
                question_order = DB.Orders();
                foreach (DataRow row in question_order.Rows)
                {
                    Reserved_orders.Add((int)row.ItemArray[0]);
                }
            }
            while (Reserved_orders.Contains((int)QuestionOrderUpDown.Value) && QuestionOrderUpDown.Value >= 0)
            {
                QuestionOrderUpDown.Value--;
                if (QuestionOrderUpDown.Value == -1)
                {
                    Next_Number_UpDown(QuestionOrderUpDown);
                }
            }
            q.Question_order = (int)QuestionOrderUpDown.Value;
        }
        /// <summary>
        /// Releses the specified question.
        /// </summary>
        /// <param name="q">The q.</param>
        private void Relese(Question q)
        {
            if (q != null)//to avoid null refrence exception
            {
                q = null;
            }
        }
        /// <summary>
        /// Handles the CheckedChanged event of the Radio controls, show groupboxes depends on selection of radio buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, Slider_Radio))
            {
                if (!Slider_Radio.Checked)
                    Slider_GroupBox.Visible = false;
            }
            else if (ReferenceEquals(sender, Smiley_Radio))
            {
                if (!Smiley_Radio.Checked)
                    Smiley_GroupBox.Visible = false;
            }
            else if (ReferenceEquals(sender, Stars_Radio))
            {
                if (!Stars_Radio.Checked)
                    Stars_GroupBox.Visible = false;
            }

        }
        /// <summary>
        /// Handles the Click event of the Radio control, create question object and fill controls with default values of that question.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Radio_Click(object sender, EventArgs e)
        {
            Relese(q);//call method release that release  q object if refere to another object 
            if (ReferenceEquals(sender, Slider_Radio))//if slider radio button 
            {

                q = new Slider(Next_ID)//create slider object
                {
                    ID = Next_ID,
                };
                Slider_Radio.Checked = true;//set check property of Slider radio button to true
                Start_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(0));//fill textbox with default value of start 
                End_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(1));//fill textbox with default value of end 
                Start_caption_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(2));//fill textbox with default value of start caption 
                End_caption_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(3));//fill textbox with default value of end caption 
                Next_Number_UpDown(QuestionOrderUpDown1);
                Slider_GroupBox.Visible = true;//make groupbox that contain above controls visible
            }
            else if (ReferenceEquals(sender, Smiley_Radio))//if Smiley radio button 
            {
                q = new Smiley(Next_ID)//create smiley object
                {
                    Question_order = Next_ID//sign qustion_order property to next_order field
                };
                Smiley_Radio.Checked = true;//set check property of Smiley radio button to true
                Smile_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(0));//fill textbox with default value of faces 
                Next_Number_UpDown(QuestionOrderUpDown3);
                Smiley_GroupBox.Visible = true;//make groupbox that contain above controls visible
            }
            else if (ReferenceEquals(sender, Stars_Radio))//if Stars radio button 
            {
                q = new Stars(Next_ID)//create star object               
                {
                    Question_order = Next_ID//sign qustion_order property to next_order field
                };

                Stars_Radio.Checked = true;//set check property of Stars radio button to true
                Stars_textbox.Text = string.Format("{0}", q.Default_values().ElementAt(0));//fill textbox with default value of stars 
                Next_Number_UpDown(QuestionOrderUpDown2);
                Stars_GroupBox.Visible = true;//make groupbox that contain above controls visible

            }
        }
        /// <summary>
        /// Handles the Click event of the Save control, save the new question in database after validation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            int Groupbox_index = Group_Index();//return index of selected control in GroupBox

            if (Groupbox_index != -1)//if no control selected in GroupBox
            {
                if (!question_box.Text.Any(char.IsDigit) && !IsEmpty(question_box))//check question textbox if contain invalid inputs or default text
                {

                    if (Check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
                    {
                        try
                        {
                            DB.Insert(Groupbox_index, Tables, q);//call Insert method in DBclass to insert the new question into database
                            this.Close();//hide Add dialog
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
                            this.Close();//hide add_dialog form 
                        }

                    }

                }
                else if (IsEmpty(question_box) || question_box.Text == "")//if question textbox is empty or contain default value
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
            else
                MessageBox.Show("Please select question type ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if no question type is selected 
        }
        /// <summary>
        /// Groups the index.
        /// </summary>
        /// <returns>
        /// number that represent radio buttons (0 => Slider,1 => Smiley ,2 => Stars)
        /// </returns>
        private int Group_Index()//return a number that represent radio buttons (0 => Slider,1 => Smiley ,2 => Stars)
        {

            if (Slider_Radio.Checked)
                return 0;
            else if (Smiley_Radio.Checked)
                return 1;
            else if (Stars_Radio.Checked)
                return 2;
            else
                return -1;
        }
        /// <summary>
        /// Determines whether the specified box is empty, consider that the default value is empty too.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsEmpty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Stars_textbox))
            {
                if (Stars_textbox.Text == string.Format("{0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, End_textBox))
            {
                if (End_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(1)))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_caption_textBox))
            {
                if (Start_caption_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(2)))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, End_caption_textBox))
            {
                if (End_caption_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(3)))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, Smile_textBox))
            {
                if (Smile_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, Stars_textbox))
            {
                if (Stars_textbox.Text == string.Format("{0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }


    }
}
