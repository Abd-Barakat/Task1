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
namespace Task1
{
    public partial class Main : Form
    {


        private string Path = System.IO.Directory.GetParent(@"..\..\..\").FullName;
        private DBclass DB = new DBclass();
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
            Upload();
        }
        /// <summary>
        /// Handles the Click event of the Add_Button control, create Add dialog object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Add_Button_Click(object sender, EventArgs e)
        {
            int next_id = Next_question_id();
            if (next_id != -1)
            {
                Add _Add = new Add(next_id);
                _Add.ShowDialog();
                Upload();//update data grid view with new data when add dialog close
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
                if (!IsEmpty())//check if list box is empty to return index of selected row 
                {
                    int Question_id = Selected_question_id();//return selected question's id
                    if (DatabaseListBox.SelectedIndex != -1)//check if a question is selected
                    {
                        DataRow[] rows = DB.extract_row(Question_id, dataTable_index());//extract rows related to that question
                        Edit_dialog _Edit = new Edit_dialog(rows);
                        _Edit.ShowDialog();
                        Upload();//update data grid view with new data when add dialog close
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
        private string Qustion_type()//method return question type as string 
        {
            return DB.question_table().Rows[DatabaseListBox.SelectedIndex].ItemArray[2].ToString();
        }
        /// <summary>
        /// pair question types with indexes.
        /// </summary>
        /// <returns></returns>
        private int dataTable_index()//return a number that represent question type (used to determine which data table to be retrived)
        {
            switch (Qustion_type())
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
                if (!IsEmpty())//check if list box contain any row or not 
                {
                    if (DatabaseListBox.SelectedIndex != -1)//check if there a selected question
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete question ?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//to make sure user want to delete 
                        if (result == DialogResult.Yes)//if user is sure to delete the selected  row 
                        {
                            int id = Selected_question_id();//return selected question order from database 
                            DB.Delete(Qustion_type(), id);//call method in class DBclass
                            Upload();//update data grid view with new data from database 
                        }
                    }
                    else
                    {
                        MessageBox.Show("No question selected", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                        Print_Errors("No question selected");
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
        /// return id for selected question.
        /// </summary>
        /// <returns></returns>
        private int Selected_question_id()//this method return question order of selected question in data grid view 
        {
            try
            {
                int current_row_index = DatabaseListBox.SelectedIndex;//return current row index from data grid view
                if (DatabaseListBox.SelectedIndex != -1)
                {
                    return (int)DB.question_table().Rows[current_row_index].ItemArray[3];//return question order from selected question
                }
                else
                {
                    MessageBox.Show("No question selected ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                    Print_Errors("No question selected ");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors(ex.Message, ex);
                return -1;
            }
        }
        /// <summary>
        /// increment question id.
        /// </summary>
        /// <returns>
        /// next id
        /// </returns>
        private int Next_question_id()
        {
            try
            {
                if (DatabaseListBox.Items.Count > 0)//if theres rows in data grid view 
                {
                    int number_of_rows = DatabaseListBox.Items.Count;
                    return (int)DB.question_table().Rows[number_of_rows - 1].ItemArray[3] + 1;//return question order from last question in data grid view and add 1 to it 
                }
                else//no rows found 
                    return 0;
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
        private bool IsEmpty()//this method for check if there data in database
        {
            if (DatabaseListBox.Items.Count == 0)//if count of rows =0
                return true;
            else
                return false;
        }
        /// <summary>
        /// Uploads list box with new questions from database.
        /// </summary>
        private void Upload()//this method for update data grid  view with data from database 
        {
            try
            {
                DatabaseListBox.Items.Clear();//clear list box from questions
                DataTable temp = DB.load();
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


