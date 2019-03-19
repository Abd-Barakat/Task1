using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using DataBase;
using Questions;
namespace Task1
{
    public partial class Main : Form
    {


        private string Path = System.IO.Directory.GetParent(@"..\..\..\").FullName;
        private DBclass DataBase = new DBclass();
        private Question question;
        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles the Load event of the Form1 control, call Upload method.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Update();
        }
        /// <summary>
        /// Handles the Click event of the Add_Button control, create Add dialog object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Add_Button_Click(object sender, EventArgs e)
        {
            QuestionAttributes Add = new QuestionAttributes(DataBase);
            Add.ShowDialog();
            Update();//update data grid view with new data when add dialog close
        }
        private void  Create_Question()
        {
            int Question_id = Selected_question_id();//return selected question's id
            DataRow[] Rows = DataBase.Extract_row(Question_id);//extract rows related to that question
            string Type = Rows[0].ItemArray[2].ToString();
            switch (Type)
            {
                case "Slider":
                    question = new Slider(Rows[0].ItemArray[0].ToString(), (int)Rows[0].ItemArray[1], (int)Rows[0].ItemArray[3], (int)Rows[1].ItemArray[2], (int)Rows[1].ItemArray[3], Rows[1].ItemArray[4].ToString(), Rows[1].ItemArray[5].ToString());
                    break;
                case "Smiley":
                    question = new Smiley(Rows[0].ItemArray[0].ToString(), (int)Rows[0].ItemArray[1], (int)Rows[0].ItemArray[3], (int)Rows[1].ItemArray[2]);
                    break;
                case "Stars":
                    question = new Stars(Rows[0].ItemArray[0].ToString(), (int)Rows[0].ItemArray[1], (int)Rows[0].ItemArray[3], (int)Rows[1].ItemArray[2]);
                    break;
            }
        }
        /// <summary>
        /// Handles the Click event of the Edit_Button control, create Edit dialog object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Edit_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsListEmpty())//check if list box is empty to return index of selected row 
                {
                    if (DatabaseListBox.SelectedIndices.Count == 1)
                    {

                        Create_Question();
                        QuestionAttributes Edit = new QuestionAttributes(question, DataBase);
                        Edit.ShowDialog();
                        Update();//update data grid view with new data when add dialog close
                    }
                    else if (DatabaseListBox.SelectedIndices.Count == 0)
                    {
                        MessageBox.Show("No question selected", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                        Print_Errors("No question selected");
                    }
                    else
                    {
                        MessageBox.Show("Please select one question to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Print_Errors("Please select one question to edit");
                    }
                }
                else
                {
                    MessageBox.Show("No questions to edit", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if data grid view or database contian no records
                    Print_Errors("No questions to edit");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors(ex.Message, ex);
            }
        }
        /// <summary>
        /// this method return question type from database.
        /// </summary>
        /// <returns></returns>
        private string Qustion_type(int Index)//method return question type as string 
        {
            return DataBase.Question_table().Rows[Index].ItemArray[2].ToString();
        }
        /// <summary>
        /// pair question types with indexes.
        /// </summary>
        /// <returns></returns>
        private int DataTable_index()//return a number that represent question type (used to determine which data table to be retrived)
        {
            switch (Qustion_type(DatabaseListBox.SelectedIndex))
            {
                case "Slider":
                    return 1;
                case "Smiley":
                    return 2;
                case "Stars":
                    return 3;
            }
            return -1;
        }
        /// <summary>
        /// Handles the Click event of the Delete_Button control that delete the selected question.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Delete_Button_Click(object sender, EventArgs e)//click event  handler for delete button
        {
            try
            {
                if (!IsListEmpty())//check if list box contain any row or not 
                {
                    if (DatabaseListBox.SelectedIndices.Count > 0)//check if there a selected question
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete question ?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//to make sure user want to delete 
                        if (result == DialogResult.Yes)//if user is sure to delete the selected  row 
                        {
                            for (int Counter = 0; Counter < DatabaseListBox.SelectedIndices.Count;)
                            {
                                int Index = DatabaseListBox.SelectedIndices[Counter];
                                int id = Selected_question_id(Index);//return selected question order from database 
                                DataBase.Delete(id);//call method in class DBclass
                                Counter++;
                            }
                            Update();//update data grid view with new data from database 
                            if (IsListEmpty())
                            {
                                DataBase.Reset_IDs();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select any question to delete", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                        Print_Errors("Please select any question to delete");
                    }
                }
                else
                {
                    MessageBox.Show("No questions to delete", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                    Print_Errors("No questions to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occur
                Print_Errors(ex.Message, ex);
            }
        }

        /// <summary>
        /// return id for selected question from database.
        /// </summary>
        /// <returns>
        /// Id for selected question
        /// </returns>
        private int Selected_question_id()//this method return question order of selected question in data grid view 
        {
            try
            {
                int current_row_index = DatabaseListBox.SelectedIndex;//return current row index from data grid view
                return (int)DataBase.Question_table().Rows[current_row_index].ItemArray[3];//return question order from selected question
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors(ex.Message, ex);
                return -1;
            }
        }
        /// <summary>
        /// return id for selected question from database.
        /// </summary>
        /// <returns>
        /// id for selection question
        /// </returns>
        private int Selected_question_id(int index)//this method return question order of selected question in data grid view 
        {
            try
            {
                return (int)DataBase.Question_table().Rows[index].ItemArray[3];//return question order from selected question
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors(ex.Message, ex);
                return -1;
            }
        }
        /// <summary>
        /// Determines whether the database is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        private bool IsListEmpty()//this method for check if there data in database
        {
            if (DatabaseListBox.Items.Count == 0)//if count of rows =0
                return true;
            else
                return false;
        }
        /// <summary>
        /// Uploads list box with new questions from database.
        /// </summary>
        private void Update()//this method for update List box with data from database 
        {
            try
            {
                DatabaseListBox.Items.Clear();//clear list box from questions
                DataTable temp = DataBase.Load();
                foreach (DataRow row in temp.Rows)
                {
                    DatabaseListBox.Items.Add(row.ItemArray[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);//to show any error could occur
                Print_Errors(ex.Message, ex);
            }

        }
        /// <summary>
        /// Handles the Click event of the Close_Button control that close the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Prints the errors in Error.txt file.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="ex">The ex.</param>
        protected void Print_Errors(string Message, Exception ex)
        {
            string Error_file = string.Format(@Path + @"\Error.txt");
            using (StreamWriter stream = new StreamWriter(Error_file, true))//save errors in Error.txt file
            {
                stream.WriteLine("-------------------------------------------------------------------\n");
                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                while (ex != null)
                {
                    stream.WriteLine("Message :\n" + Message);
                    stream.WriteLine("Stack trace :\n" + ex.StackTrace);
                    ex = ex.InnerException;
                }
            }
        }
        /// <summary>
        /// Prints the errors in Error.txt file.
        /// </summary>
        /// <param name="Message">The message.</param>
        protected void Print_Errors(string Message)
        {
            string Error_file = string.Format(@Path + @"\Error.txt");
            using (StreamWriter stream = new StreamWriter(Error_file, true))//save errors in Error.txt file
            {
                stream.WriteLine("-------------------------------------------------------------------\n");
                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                stream.WriteLine("Message :\n" + Message);
            }
        }

    }
}


