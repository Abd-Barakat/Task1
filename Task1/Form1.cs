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
namespace Task1
{
    public partial class Form1 : Form
    {
        private SqlConnection Connection;
        private SqlCommand command;
        private DataTable[] dataTables = new  DataTable[4];
        private SqlDataAdapter dataAdapter;
        private Add_dialog Add_dialog = null;
        private Edit_dialog edit_Dialog;
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DataBase-Connection"];
        List<Question> questions = new List<Question>();
        

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)//event handler when form is loaded 
        {
            print();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            Add_dialog = new Add_dialog();//define object from class Add_dialog
            int next_order = Next_question_order() ;
            Add_dialog.ShowDialog( dataTables[0].Clone(), next_order);//call ShowDialog method from class Add_dialog

            Add_dialog.FORM.VisibleChanged += Add_Form_VisibleChanged;//define event handler for event visiable change 
        }//event handler for add button 

        private void Add_Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Add_dialog.FORM.Visible == false)
            {
                print();//update data grid view with new data when add dialog close

            }
        }//event handler for visible change event of add form

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            if (!isEmpty())//check if data grid view is empty to return index of selected row 
            {
                edit_Dialog = new Edit_dialog();//define object from class Edit_dialog
                int order = Selected_question_order();//return question order in database 
                edit_Dialog.ShowDialog(order,extract_row(dataTables[0],order),extract_row(dataTables[dataTable_index()],order));//call method ShowDialog that take question order and datatable and index of selected row 
                edit_Dialog.FORM.VisibleChanged += Edit_FORM_VisibleChanged;//define event handler for Visible change event 
            }
            else
                MessageBox.Show("No questions to edit", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if data grid view or database contian no records

        }
        
        private DataRow extract_row(DataTable data , int order )
        {
            return data.Select(string.Format("Convert(question_order,'System.String') LIKE '%{0}%'", order)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , we must convert question_order to string first 
        }

        private  string Qustion_type()
        {
            return dataTables[0].Rows[dataGridView1.CurrentRow.Index].ItemArray[2].ToString();
        }

        private int dataTable_index ()
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
            if (edit_Dialog.FORM.Visible == false)
            {
                print();//update data grid view after Edit dialog disappear
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
                        order = Selected_question_order();//return question order from database 

                        open_connection();//open connection to server

                        command.CommandText = string.Format("delete from {0} where question_order={1}", dataTables[0].Rows[dataGridView1.CurrentRow.Index].ItemArray[2], order);//sql command
                        command.ExecuteNonQuery();//execute command 

                        command.CommandText = string.Format("delete from questions where question_order= {0}", order);//change sql command text 
                        command.ExecuteNonQuery();//execute command 

                        print();//update data gird view 

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occur
                    }
                   
                }
            }
            else
                MessageBox.Show("No questions to delete", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//in case database is empty 
        }//event handler for delete button

        private int Selected_question_order()//this method return question order of selected question in data grid view 
        {
            
            return  (int)dataTables[0].Rows[dataGridView1.CurrentRow.Index].ItemArray[1];
             
        }

        private int Next_question_order()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                return (int)dataTables[0].Rows[dataGridView1.Rows.Count - 1].ItemArray[1]+1;
            }
            else
                return 0;
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
            
            Connection = new SqlConnection();//make connection object 
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
            try
            {
                DataTable temp_datatable = new DataTable();


                open_connection();//call method open_connection()



                dataTables[0] = new DataTable();//define new data table to hold data 
                dataTables[1] = new DataTable();
                dataTables[2] = new DataTable();
                dataTables[3] = new DataTable();


                command = new SqlCommand("select * from questions", Connection);//new command to database 
                dataAdapter = new SqlDataAdapter(command);//execute command and save it in adapter
                dataAdapter.Fill(dataTables[0]);//fill data table with data retrived from database 

                command.CommandText = "select * from Slider";
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTables[1]);

                command.CommandText = "select * from Smiley";
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTables[2]);

                command.CommandText = "select * from Stars";
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTables[3]);


                
                temp_datatable = dataTables[0].DefaultView.ToTable(false, "question_text");//extract one column from data table 

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


