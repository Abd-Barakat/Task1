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
    public partial class Form1 : Form
    {
      
        private Add_dialog Add_dialog = null;
        private Edit_dialog edit_Dialog;
        private DBclass DB = new DBclass();

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)//event handler when form is loaded 
        {
            reload();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            int next_order = Next_question_order() ;
           Add_dialog=  new  Add_dialog( DB.question_table().Clone(), next_order);//call constructor

            Add_dialog.FORM.VisibleChanged += Add_Form_VisibleChanged;//define event handler for event visiable change 
        }

        private void Add_Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Add_dialog.FORM.Visible == false)
            {
                reload();//update data grid view with new data when add dialog close

            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            if (!isEmpty())//check if data grid view is empty to return index of selected row 
            {
                int order = Selected_question_order();

                DataRow []rows = DB.extract_row(order, dataTable_index());

                edit_Dialog=new Edit_dialog(order,rows[0],rows[1]);//call method ShowDialog that take question order and datatable and index of selected row 
                edit_Dialog.FORM.VisibleChanged += Edit_FORM_VisibleChanged;//define event handler for Visible change event 
            }
            else
                MessageBox.Show("No questions to edit", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if data grid view or database contian no records

        }
        
        private DataRow extract_row(DataTable data )//method used to extract a row from data table 
        {
            return data.Select(string.Format("Convert(question_order,'System.String') LIKE '%{0}%'", Selected_question_order())).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , we must convert question_order to string first 
        }

        private  string Qustion_type()//method return question type as string 
        {
            return DB.question_table().Rows[dataGridView1.CurrentRow.Index].ItemArray[2].ToString();
        }

        private int dataTable_index ()//return a number that represent question type (used to determine which data table to be retrived)
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
                reload();//update data grid view with new data from database 
            }
        }

        private void Delete_Button_Click(object sender, EventArgs e)//click event  handler for delete button
        {
            if (!isEmpty())//check if data grid view contain any row or not 
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete question ?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//to make sure user want to delete 
                if (result == DialogResult.Yes)//if user is sure to delete the selected  row 
                {
                    try
                    {
                        int order = Selected_question_order();//return selected question order from database 
                        DB.Delete(Qustion_type(), order);//call method in class DBclass
                        reload();//update data grid view with new data from database 
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
                MessageBox.Show("No questions to delete", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
        }

        private int Selected_question_order()//this method return question order of selected question in data grid view 
        {
            int current_row_index = dataGridView1.CurrentRow.Index;//return current row index from data grid view
            return  (int) DB.question_table().Rows[current_row_index].ItemArray[1];//return question order from selected question
        }

        private int Next_question_order()
        {
            if (dataGridView1.Rows.Count > 0)//if theres rows in data grid view 
            {
                int number_of_rows = dataGridView1.Rows.Count;
                return (int)DB.question_table().Rows[ number_of_rows - 1 ].ItemArray[1]+1;//return question order from last question in data grid view and add 1 to it 
            }
            else//no rows found 
                return 0;
        }

      

        private bool isEmpty()//this method for check if there data in database
        {
            if (dataGridView1.Rows.Count == 0)//if count of rows =0
                return true;
            else
                return false;
        }
       
        private void reload()//this method for update data grid  view with data from database 
        {
            try
            {
                dataGridView1.DataSource = DB.load();//extract one column from data table 
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


    }
}


