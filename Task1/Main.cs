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

        private Add_dialog Add_dialog;
        private Edit_dialog edit_Dialog;
        private DBclass DB = new DBclass();

        public Main()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)//event handler when form is loaded 
        {
            Upload();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            int next_id = Next_question_id();
            Add_dialog = new Add_dialog(next_id);//call constructor

            Add_dialog.FORM.VisibleChanged += Add_Form_VisibleChanged;//define event handler for event visiable change 
        }

        private void Add_Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Add_dialog.FORM.Visible == false)
            {
                Add_dialog.FORM.Close();
                Upload();//update data grid view with new data when add dialog close

            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            if (!IsEmpty())//check if data grid view is empty to return index of selected row 
            {
                int Next_order = Next_question_order();
                int Question_id = Selected_question_id();
                if (DatabaseListBox.SelectedIndex != -1)
                {
                    DataRow[] rows = DB.extract_row(Question_id, dataTable_index());

                    edit_Dialog = new Edit_dialog(Next_order, rows[0], rows[1]);//call method ShowDialog that take question order and datatable and index of selected row 
                    edit_Dialog.FORM.VisibleChanged += Edit_FORM_VisibleChanged;//define event handler for Visible change event 

                }

            }
            else
                MessageBox.Show("No questions to edit", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if data grid view or database contian no records

        }

        private DataRow extract_row(DataTable data)//method used to extract a row from data table 
        {
            return data.Select(string.Format("Convert(question_order,'System.String') LIKE '%{0}%'", Selected_question_id())).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , we must convert question_order to string first 
        }

        private string Qustion_type()//method return question type as string 
        {
            return DB.question_table().Rows[DatabaseListBox.SelectedIndex].ItemArray[2].ToString();
        }

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

        private void Edit_FORM_VisibleChanged(object sender, EventArgs e)//event handler for visible change event of edit form
        {
            if (edit_Dialog.FORM.Visible == false)//if edit dialog is not visible 
            {
                edit_Dialog.FORM.Close();
                Upload();//update data grid view with new data from database 
            }
        }

        private void Delete_Button_Click(object sender, EventArgs e)//click event  handler for delete button
        {
            if (!IsEmpty())//check if data grid view contain any row or not 
            {
                if (DatabaseListBox.SelectedIndex != -1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete question ?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//to make sure user want to delete 
                    if (result == DialogResult.Yes)//if user is sure to delete the selected  row 
                    {
                        try
                        {
                            int order = Selected_question_id();//return selected question order from database 
                            DB.Delete(Qustion_type(), order);//call method in class DBclass
                            Upload();//update data grid view with new data from database 
                        }
                        catch (Exception ex)
                        {
                            using (StreamWriter stream = new StreamWriter(@"C: \Users\a.barakat\source\repos\Task1\Error.txt", true))
                            {
                                stream.WriteLine("-------------------------------------------------------------------\n");
                                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                                while (ex != null)
                                {
                                    stream.WriteLine("Message :\n" + ex.Message + "\nStack trace :\n" + ex.StackTrace);
                                    ex = ex.InnerException;
                                }
                            }
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occur
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No question selected", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                }
            }
            else
                MessageBox.Show("No questions to delete", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
        }

        private int Selected_question_id()//this method return question order of selected question in data grid view 
        {
            int current_row_index = DatabaseListBox.SelectedIndex;//return current row index from data grid view
            if (DatabaseListBox.SelectedIndex != -1)
                return (int)DB.question_table().Rows[current_row_index].ItemArray[3];//return question order from selected question
            else
            {
                MessageBox.Show("No question selected ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
                return -1;
            }
        }
        private int Next_question_order()
        {
            if (DatabaseListBox.Items.Count > 0)//if theres rows in data grid view 
            {
                int number_of_rows = DatabaseListBox.Items.Count;
                return (int)DB.question_table().Rows[number_of_rows - 1].ItemArray[1] + 1;//return question order from last question in data grid view and add 1 to it 
            }
            else//no rows found 
                return 0;
        }
        private int Next_question_id()
        {
            if (DatabaseListBox.Items.Count > 0)//if theres rows in data grid view 
            {
                int number_of_rows = DatabaseListBox.Items.Count;
                return (int)DB.question_table().Rows[number_of_rows - 1].ItemArray[3] + 1;//return question order from last question in data grid view and add 1 to it 
            }
            else//no rows found 
                return 0;
        }



        private bool IsEmpty()//this method for check if there data in database
        {
            if (DatabaseListBox.Items.Count == 0)//if count of rows =0
                return true;
            else
                return false;
        }

        private void Upload()//this method for update data grid  view with data from database 
        {
            try
            {
                DatabaseListBox.Items.Clear();
                DataTable temp = DB.load();
                foreach (DataRow row in temp.Rows)
                {
                    DatabaseListBox.Items.Add(row.ItemArray[0].ToString());
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter stream = new StreamWriter(@"C: \Users\a.barakat\source\repos\Task1\text.txt", true))
                {
                    stream.Write("-------------------------------------------------------------------");
                    stream.WriteLine("Date :" + DateTime.Now.ToLongDateString());
                    while (ex != null)
                    {
                        stream.WriteLine("Message :\n" + ex.Message + "\nStack trace :\n" + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);//to show any error could occur
            }

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (Add_dialog != null)
            {
                if (!Add_dialog.FORM.IsDisposed)
                {
                    Add_dialog.FORM.Select();
                }
            }
            if (edit_Dialog != null)
            {
                if (!edit_Dialog.FORM.IsDisposed)
                {
                    edit_Dialog.FORM.Select();
                }
            }
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


