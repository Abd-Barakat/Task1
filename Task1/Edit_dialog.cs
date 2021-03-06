﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Linq;
namespace Task1
{
    public partial class Edit_dialog : Task1.BaseForm
    {
        private DataTable Type_table = new DataTable();//table will hold the selected question (text ,order ,type)
        private DataTable question_table = new DataTable();//table will hold values related to the selected question
        /// <summary>
        /// Show groupbox that contain controls to show current values and initializes a new instance of the <see cref="Edit_dialog"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public Edit_dialog(DataRow[] rows)
        {
            Path = System.IO.Directory.GetParent(@"..\..\..\").FullName;
            DB = new DBclass();
            InitializeComponent();
            Type_table = rows[1].Table.Clone();//copy  table's headers only
            Type_table.Rows.Add(rows[1].ItemArray);//add row to type table
            question_table = rows[0].Table.Clone();//copy  table's headers only
            question_table.Rows.Add(rows[0].ItemArray);//add row to question table
            string type = question_table.Rows[0].ItemArray[2].ToString();//get type of the question from question table 
            ShowGroupBox(type);//call showgroub box
        }
        /// <summary>
        /// Handles the Click event of the Save control that save updates to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Save_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            try
            {
                if (Check(q.Current_values()))//call check method to check inserted values before update database 
                {

                    DB.Update(q);//upate database with new edited question
                    this.Close();
                }
            }
            catch (Exception ex)//to catch eny problem that may occure
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occure
                Print_Errors(ex.Message, ex);
                this.Close();
            }
        }
        /// <summary>
        /// Determines whether the specified box is empty.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsEmpty(TextBox box)//this method to check if text box is containing default value
        {
            if (ReferenceEquals(box, question_box))// question text box
            {
                if (question_box.Text == "")//check default 
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_textBox))// start text box
            {
                if (Start_textBox.Text == q.Current_values().ElementAt(0).ToString())//check default 
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, End_textBox))// end text box
            {
                if (End_textBox.Text == q.Current_values().ElementAt(1).ToString())//check default 
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_caption_textBox))// start caption text box
            {
                if (Start_caption_textBox.Text == q.Current_values().ElementAt(2).ToString())//check default 
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, End_caption_textBox))// end captiono text box
            {
                if (End_caption_textBox.Text == q.Current_values().ElementAt(3).ToString())//check default 
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, Smile_textBox))//smiles text box
            {
                if (Smile_textBox.Text == q.Current_values().ElementAt(0).ToString())//check default 
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, Stars_textbox))//stars text box
            {
                if (Stars_textbox.Text == q.Current_values().ElementAt(0).ToString())//check default 
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        /// <summary>
        /// Shows a specific group box depends on selected question's type.
        /// </summary>
        /// <param name="type">The type.</param>
        private void ShowGroupBox(string type)//this method show a specific Groupbox depends on type of question then save old data (before editing ) in variables 
        {
            switch (type)//switch type of question 
            {

                case "Slider":
                    q = new Slider(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(question_table.Rows[0].ItemArray[3]), (int)(Type_table.Rows[0].ItemArray[1]), (int)(Type_table.Rows[0].ItemArray[2]), Type_table.Rows[0].ItemArray[3].ToString(), Type_table.Rows[0].ItemArray[4].ToString());//create object from slider class and fill it with saved values retrived from database (old values)
                    Start_textBox.Text = q.Current_values().ElementAt(0).ToString();//show stored value of Start value
                    End_textBox.Text = q.Current_values().ElementAt(1).ToString();//show stored value of End value
                    Start_caption_textBox.Text = q.Current_values().ElementAt(2).ToString();//show stored value of Start_caption
                    End_caption_textBox.Text = q.Current_values().ElementAt(3).ToString();//show stored value of End_caption
                    Next_Number_UpDown(QuestionOrderUpDown1);
                    Slider_GroupBox.Visible = true;//show groupbox that hold  text boxes to display and editing question's values
                    break;

                case "Smiley":
                    q = new Smiley(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(question_table.Rows[0].ItemArray[3]), (int)(Type_table.Rows[0].ItemArray[1]));//create object from Smiley and fill it with stored values retrived from database (old value)
                    Smile_textBox.Text = q.Current_values().ElementAt(0).ToString();//show stored value of Faces
                    Next_Number_UpDown(QuestionOrderUpDown3);
                    Smiley_GroupBox.Visible = true;//show groupbox that hold  text boxes to display and editing question's values
                    break;

                case "Stars":
                    q = new Stars(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(question_table.Rows[0].ItemArray[3]), (int)(Type_table.Rows[0].ItemArray[1]));//create object from Stars and fill it with stored values retrived from database (old value)
                    Stars_textbox.Text = q.Current_values().ElementAt(0).ToString();//show stored value of Stars
                    Next_Number_UpDown(QuestionOrderUpDown2);
                    Stars_GroupBox.Visible = true;//show groupbox that hold  text boxes to display and editing question's values

                    break;

            }

        }

    }
}
