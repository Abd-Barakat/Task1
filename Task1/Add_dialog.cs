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
        private readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };//hold tables names
        private int Next_ID;//hold id for next question
        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="Next_ID">The next question's id.</param>
        public Add(int Next_ID)
        {
            InitializeComponent();
            DB = new DBclass();
            this.Next_ID = Next_ID;
            Path = System.IO.Directory.GetParent(@"..\..\..\").FullName;
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
                Next_Number_UpDown(QuestionOrderUpDown1);
                Show_controls();
            }
            else if (ReferenceEquals(sender, Smiley_Radio))//if Smiley radio button 
            {
                q = new Smiley(Next_ID)//create smiley object
                {
                    Question_order = Next_ID//sign qustion_order property to next_order field
                };
                Smiley_Radio.Checked = true;//set check property of Smiley radio button to true
                Next_Number_UpDown(QuestionOrderUpDown3);
                Show_controls();
            }
            else if (ReferenceEquals(sender, Stars_Radio))//if Stars radio button 
            {
                q = new Stars(Next_ID)//create star object               
                {
                    Question_order = Next_ID//sign qustion_order property to next_order field
                };
                Stars_Radio.Checked = true;//set check property of Stars radio button to true
                Next_Number_UpDown(QuestionOrderUpDown2);
                Show_controls();
            }
        }
        private void Show_controls()
        {
            int index = Group_Index();
            switch (index)
            {
                case 0:
                    Start_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(0));//fill textbox with default value of start 
                    End_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(1));//fill textbox with default value of end 
                    Start_caption_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(2));//fill textbox with default value of start caption 
                    End_caption_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(3));//fill textbox with default value of end caption
                    Slider_GroupBox.Visible = true;//make groupbox that contain above controls visible
                    break;
                case 1:
                    Smile_textBox.Text = string.Format("{0}", q.Default_values().ElementAt(0));//fill textbox with default value of faces 
                    Smiley_GroupBox.Visible = true;//make groupbox that contain above controls visible
                    break;
                case 2:
                    Stars_textbox.Text = string.Format("{0}", q.Default_values().ElementAt(0));//fill textbox with default value of stars 
                    Stars_GroupBox.Visible = true;//make groupbox that contain above controls visible
                    break;
            }
        }
        /// <summary>
        /// Handles the Click event of the Save control, save the new question in database after validation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            try
            {
                int Groupbox_index = Group_Index();//return index of selected control in GroupBox
                if (Groupbox_index != -1)//if no control selected in GroupBox
                {
                    if (Check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
                    {
                        DB.Insert(Groupbox_index, Tables, q);//call Insert method in DBclass to insert the new question into database
                        this.Close();//hide Add dialog
                    }
                }
                else
                {
                    MessageBox.Show("Please select question type ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if no question type is selected 
                    Print_Errors("Please select question type ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors(ex.Message, ex);
                this.Close();//hide add_dialog form 
            }
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
    }
}
