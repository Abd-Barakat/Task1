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
    public class Edit_dialog : Base
    {


        private DataTable Type_table = new DataTable();//table will hold the selected question (text ,order ,type)
        private DataTable question_table = new DataTable();//table will hold values related to the selected question

        private void initialize()//initialize events and controls
        {
            Retrive_Data();
            Default_GrouoBox.Controls.Add(control1);
            Default_GrouoBox.Controls.Add(control2);
            Default_GrouoBox.Controls.Add(control3);
            Default_GrouoBox.Controls.Add(control4);
            Default_GrouoBox2.Controls.Add(control5);
            Default_GrouoBox3.Controls.Add(control6);
            Default_GrouoBox3.Controls.Add(question_box);
            ///////////////////////////////////////////
            question_box.Text = "Write a new question here ...";
            ///////////////////////////////////////////
            control1.GotFocus += TextChanged;
            control2.GotFocus += TextChanged;
            control3.GotFocus += TextChanged;
            control4.GotFocus += TextChanged;
            control5.GotFocus += TextChanged;
            control6.GotFocus += TextChanged;
            question_box.GotFocus += TextChanged;
            control1.MouseClick += TextChanged;
            control2.MouseClick += TextChanged;
            control3.MouseClick += TextChanged;
            control4.MouseClick += TextChanged;
            control5.MouseClick += TextChanged;
            control6.MouseClick += TextChanged;
            question_box.MouseClick += TextChanged;
            ///////////////////////////////////////////
            control1.KeyDown += KeyDown;
            control2.KeyDown += KeyDown;
            control3.KeyDown += KeyDown;
            control4.KeyDown += KeyDown;
            ///////////////////////////////////////////
            FORM.Controls.Add(question_box);
            FORM.Controls.Add(Default_GrouoBox);
            FORM.Controls.Add(Default_GrouoBox2);
            FORM.Controls.Add(Default_GrouoBox3);
            FORM.Controls.Add(Dv);
            FORM.Controls.Add(Save);
            ///////////////////////////////////////////
            FORM = form;
            FORM.Visible = true;
            Save.Click += Save_Click;
        }



        public override void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            q.Reset_values();//reset values to default
            Make_boxes_Empty();//clear text boxes
        }

        private void ShowGroupBox(string type, DataRow dataRow)//this method show a specific Groupbox depends on type of question then save old data (before editing ) in variables 
        {

            switch (type)//switch type of question 
            {

                case "Slider":
                    Default_GrouoBox.Visible = true;//show groupbox that hold  text boxes to display and editing question's values
                    q = new Slider(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(Type_table.Rows[0].ItemArray[1]), (int)(Type_table.Rows[0].ItemArray[2]), (int)(Type_table.Rows[0].ItemArray[3]), (int)(Type_table.Rows[0].ItemArray[4]));//create object from slider class and fill it with saved values retrived from database (old values)
                    control1.Text = q.Current_values().ElementAt(0).ToString();//show stored value of Start value
                    control2.Text = q.Current_values().ElementAt(1).ToString();//show stored value of End value
                    control3.Text = q.Current_values().ElementAt(2).ToString();//show stored value of Start_caption
                    control4.Text = q.Current_values().ElementAt(3).ToString();//show stored value of End_caption
                    break;

                case "Smiley":
                    Default_GrouoBox2.Visible = true;//show groupbox that hold  text boxes to display and editing question's values
                    q = new Smiley(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(Type_table.Rows[0].ItemArray[1]));//create object from Smiley and fill it with stored values retrived from database (old value)
                    control5.Text = q.Current_values().ElementAt(0).ToString();//show stored value of Faces
                    break;

                case "Stars":
                    Default_GrouoBox3.Visible = true;//show groupbox that hold  text boxes to display and editing question's values
                    q = new Stars(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(Type_table.Rows[0].ItemArray[1]));//create object from Stars and fill it with stored values retrived from database (old value)
                    control6.Text = q.Current_values().ElementAt(0).ToString();//show stored value of Stars
                    break;

            }

        }

        private void Retrive_Data()//load saved data of selected question from database 
        {
            string type = question_table.Rows[0].ItemArray[2].ToString();//get type of the question from question table 
            ShowGroupBox(type, Type_table.Rows[0]);//call showgroub box
        }

        public  Edit_dialog(int order, DataRow question, DataRow type)//method used to be called in Form1 class (like constructor)
        {

            Next_order = order;//save next order 

            Type_table = type.Table.Clone();//copy  table's headers only
            Type_table.Rows.Add(type.ItemArray);//add row to type table

            question_table = question.Table.Clone();//copy  table's headers only
            question_table.Rows.Add(question.ItemArray);//add row to question table

            Dv.DataSource = question_table;
            initialize();//call method that initialize controls
        }

        public override void Save_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            if (check(q.Current_values()))//call check method to check inserted values before update database 
            {
                if (!question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))//check if question box contain any number or is empty 
                {
                    try
                    {
                        DB.Update(q);//upate database with new edited question
                        DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);//conform that the opeation is done

                        while (result != DialogResult.OK) ;//wait unitl MessageBox closes 
                        FORM.Visible = false;//hide Add dialog that will call event handler in Form1 class to print new data from database to datagridview
                    }
                    catch (Exception ex)//to catch eny problem that may occure
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occure
                        using (StreamWriter stream = new StreamWriter(@"C: \Users\a.barakat\source\repos\Task1\Error.txt", true))//save errors in Error.txt file
                        {
                            stream.WriteLine("-------------------------------------------------------------------\n");
                            stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                            while (ex != null)
                            {
                                stream.WriteLine("Message :\n" + ex.Message  );
                                stream.WriteLine("Stack trace :\n" + ex.StackTrace);
                                ex = ex.InnerException;
                            }
                        }
                            FORM.Visible = false;
                    }
                }
                else if (isEmpty(question_box) || question_box.Text == "")//check what error is 
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question  ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (question_box.Text.Any(char.IsDigit))//check what error is 
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question without numbers   ", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        public override void Make_Empty(TextBox box)//this method for clear all text boxes to default values
        {
            if (ReferenceEquals(box, question_box))//question text box
            {
                question_box.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                question_box.Text = "Write a new question here ...";
            }
            else if (ReferenceEquals(box, control1))//start text box
            {
                control1.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                control1.Text = q.Default_values().ElementAt(0).ToString();//set default value 

            }
            else if (ReferenceEquals(box, control2))//end text box
            {
                control2.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                control2.Text = q.Default_values().ElementAt(1).ToString();//set default value 
            }
            else if (ReferenceEquals(box, control3))//start caption text box
            {
                control3.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                control3.Text = q.Default_values().ElementAt(2).ToString();//set default value 

            }
            else if (ReferenceEquals(box, control4))//end caption text box
            {
                control4.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                control4.Text = q.Default_values().ElementAt(3).ToString();//set default value 

            }

            else if (ReferenceEquals(box, control5))//faces text box
            {
                control5.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                control5.Text = q.Default_values().ElementAt(0).ToString();//set default value 
            }

            else if (ReferenceEquals(box, control6))//stars text box
            {
                control6.ForeColor = System.Drawing.Color.Gray;//make font color gray 
                control6.Text = q.Default_values().ElementAt(0).ToString();//set default value 
            }
        }

        public override bool isEmpty(TextBox box)//this method to check if text box is containing default value
        {
            if (ReferenceEquals(box, question_box))// question text box
            {
                if (question_box.Text == "Write a new question here ...")//check default 
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control1))// start text box
            {
                if (control1.Text == q.Default_values().ElementAt(0).ToString())//check default 
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, control2))// end text box
            {
                if (control2.Text == q.Default_values().ElementAt(1).ToString())//check default 
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control3))// start caption text box
            {
                if (control3.Text == q.Default_values().ElementAt(2).ToString())//check default 
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control4))// end captiono text box
            {
                if (control4.Text == q.Default_values().ElementAt(3).ToString())//check default 
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control5))// faces text box
            {
                if (control5.Text == q.Default_values().ElementAt(0).ToString())//check default 
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control6))// stars text box
            {
                if (control6.Text == q.Default_values().ElementAt(0).ToString())//check default 
                    return true;
                else
                    return false;
            }
            else
                return false;

        }





    }
}
