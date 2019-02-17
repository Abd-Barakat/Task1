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
            Add_dialog = new Add_dialog();
            Add_dialog.ShowDialog(dataTable);
            Add_dialog.FORM.VisibleChanged += Add_Form_VisibleChanged;
        }

    

        private void Add_Form_VisibleChanged(object sender, EventArgs e)
        {
           if(Add_dialog.FORM.Visible == false)
            {
                Add_dialog.Clear();
                print();

            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            edit_Dialog = new Edit_dialog();
            int index = dataGridView1.CurrentRow.Index;
            edit_Dialog.ShowDialog(index, dataTable);
            edit_Dialog.FORM.VisibleChanged += Edit_FORM_VisibleChanged;
        }

       
        private void Edit_FORM_VisibleChanged(object sender, EventArgs e)
        {
            if (edit_Dialog.FORM.Visible == false)
            {
                print();
                edit_Dialog.Clear();
            }
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            int? x = null;
            DialogResult result = MessageBox.Show("Are you sure you want to delete question ?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Connection = new SqlConnection(@"Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                try
                {

                    command = new SqlCommand(string.Format("select question_order from questions where question_text='{0}'", dataTable.Rows[dataGridView1.CurrentRow.Index].ItemArray[0]), Connection);
                    open_connection();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        x = dataReader.GetInt32(0);
                    }
                    dataReader.Close();
                    if (x != null)
                    {

                        command.CommandText = string.Format("delete from Slider where question_order={0}", x);
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("delete from Smiley where question_order={0}", x);
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("delete from Stars where question_order= {0}", x);
                        command.ExecuteNonQuery();

                        command.CommandText =string.Format("delete from questions where question_order= {0}", x);
                        command.ExecuteNonQuery();
                    }
                    print();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                }
                finally
                {
                    if (dataReader != null)
                    {
                        ((IDisposable)dataReader).Dispose();
                    }
                }
            }
        }
        private void open_connection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        private void print()
        {
            Connection = new SqlConnection(@"Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
            try
            {
                DataTable temp_datatable = new DataTable();
                command = new SqlCommand("select * from questions", Connection);
                open_connection();
                dataTable = new DataTable();
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                temp_datatable = dataTable.DefaultView.ToTable(false, "question_text");
                dataGridView1.DataSource = temp_datatable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
        }
    }
}


