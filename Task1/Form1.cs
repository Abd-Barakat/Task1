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
namespace Task1
{
    public partial class Form1 : Form
    {
        private SqlConnection Connection;
        private SqlCommand command;
        private DataTable dataTable;
        private SqlDataAdapter dataAdapter;
        private Add_dialog Add_dialog = null;
        private SqlDataReader dataReader;
        private Edit_dialog edit_Dialog;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            print();
        }
        private void Add_Button_Click(object sender, EventArgs e)
        {
            Add_dialog = new Add_dialog();//define object from class Add_dialog
            Add_dialog.ShowDialog(dataTable);//call ShowDialog method from class Add_dialog
            Add_dialog.FORM.VisibleChanged += Add_Form_VisibleChanged;//define event handler for event visiable change 
        }



        private void Add_Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Add_dialog.FORM.Visible == false)
            {
                Add_dialog.Clear();//call method from class Add_dialog
                print();//update data grid view with new data when add dialog close

            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            if (!isEmpty())//check if data grid view is empty to return index of selected row 
            {
                edit_Dialog = new Edit_dialog();//define object from class Edit_dialog
                int order = Question_order();//return question order in database  
                edit_Dialog.ShowDialog(order, dataTable, dataGridView1.CurrentCell.RowIndex);//call method ShowDialog that take question order and datatable and index of selected row 
                edit_Dialog.FORM.VisibleChanged += Edit_FORM_VisibleChanged;//define event handler for Visible change event 
            }
            else
                MessageBox.Show("No questions to edit", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if data grid view or database contian no records

        }


        private void Edit_FORM_VisibleChanged(object sender, EventArgs e)//event handler 
        {
            if (edit_Dialog.FORM.Visible == false)
            {
                print();//update data grid view after Edit dialog disappear
                edit_Dialog.Clear();//call Clear method from class EditDialog
            }
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            if (!isEmpty())//check if data grid view contain any row or not 
            {

                DialogResult result = MessageBox.Show("Are you sure you want to delete question ?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//to make sure user want to delete 
                if (result == DialogResult.Yes)//if user is sure to delete the selected  row 
                {
                    try
                    {
                        int order;
                        order = Question_order();//return question order from database 

                        open_connection();//open connection to server

                        command.CommandText = string.Format("delete from {0} where question_O={1}", dataTable.Rows[dataGridView1.CurrentRow.Index].ItemArray[2], order);//sql command
                        command.ExecuteNonQuery();//execute command 

                        command.CommandText = string.Format("delete from questions where question_order= {0}", order);//change sql command text 
                        command.ExecuteNonQuery();//execute command 

                        print();//update data gird view 

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occur
                    }
                    finally
                    {
                        if (dataReader != null)//to prevent Null exception 
                        {
                            ((IDisposable)dataReader).Dispose();//relese dataRader object 
                        }
                    }
                }
            }
            else
                MessageBox.Show("No questions to delete", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
        }

        private int Question_order()//this method return question order of selected question in data grid view 
        {
            int order = -1;
            open_connection();//open connection to server
            command.CommandText = string.Format("select question_order from questions where question_text='{0}'", dataTable.Rows[dataGridView1.CurrentRow.Index].ItemArray[0]);//return question from data gird view 
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                order = dataReader.GetInt32(0);//read all data retrived from database one per time 
            }
            dataReader.Close();//should close or it will be open always 
            return order;
        }

        private void open_connection()//this method for open connection
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        private bool isEmpty()//this method for check if there data in database
        {
            if (dataGridView1.Rows.Count <= 0)
                return true;
            else
                return false;
        }
       
        private void print()//this method for update data grid  view with data from database 
        {
            Connection = new SqlConnection(@"Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");//make connection object 
            try
            {
                DataTable temp_datatable = new DataTable();
                command = new SqlCommand("select * from questions", Connection);//new command to database 
                open_connection();//call method open_connection()
                dataTable = new DataTable();//define new data table to hold data 
                dataAdapter = new SqlDataAdapter(command);//execute command and save it in adapter
                dataAdapter.Fill(dataTable);//fill data table with data retrived from database 
                temp_datatable = dataTable.DefaultView.ToTable(false, "question_text");//extract one column from data table 
                dataGridView1.DataSource = temp_datatable;//show one column from data table 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);//to show any error could occur
            }
            finally
            {
                if (Connection != null)//to prevent Null Exception 
                    Connection.Close();//to release connection object 
            }
        }

        
    }
}


