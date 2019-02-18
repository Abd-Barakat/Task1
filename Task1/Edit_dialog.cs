using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace Task1
{
    public  class Edit_dialog
    {
        private Form form;
        private DataTable Dt2 ;
        private int Question_order;
        
        public Form FORM
        {
            private set
            {
                form = value;
            }
            get
            {
                return form;
            }
        }
        public void Clear()
        {
            FORM = null;
        }
        private DataGridView Dv = new DataGridView
        {


            Width = 500,
            Height = 50,
            Location = new System.Drawing.Point(50, 50),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToDeleteRows = false,
            AllowUserToAddRows = false,
            AllowDrop = false,
            ReadOnly = false,
            AllowUserToResizeRows= false,
            AllowUserToResizeColumns =false,

        };


 
      

        public void ShowDialog(int order, DataTable Dt,int Row_index)
        {
            Question_order = order;
            FORM = new Form();
            FORM.Width = 500;
            FORM.Height = 300;
            FORM.MaximumSize = new System.Drawing.Size(663, 250);
            FORM.MinimumSize = form.MaximumSize;
            Dt2 = new DataTable();
            Dt2 = Dt.Clone();
            Dv.KeyDown += Dv_KeyPress;
            Dt2.Rows.Add(Dt.Rows[Row_index].ItemArray[0]);
            Dv.DataSource = Dt2.DefaultView.ToTable(false,"question_text");
            FORM.Controls.Add(Dv);
            FORM.Visible = true;
        }

        private  void Dv_KeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode ==Keys.Enter )
            {
                string Q_text;
                if (Dv.CurrentCell.Value != DBNull.Value)
                Q_text = (string)Dv.CurrentCell.Value;
                else
                    Q_text = "";
                if (Q_text != "" && !Q_text.Any(char.IsDigit) )
                {
                    SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                    SqlCommand command = new SqlCommand(  string.Format("update questions set question_text = '{0}' where question_order = {1}", Q_text, Question_order)   , connection);
                    try
                    { 

                        connection.Open();
                        command.ExecuteNonQuery();
                        DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        while (result != DialogResult.OK) ;
                        FORM.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (connection != null)
                        {
                            ((IDisposable)connection).Dispose();
                            ((IDisposable)command).Dispose();
                        }
                    }

                }
                else if (Q_text == "")
                {
                    MessageBox.Show("Please Write a question  ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Q_text = "";
                }
                else if (Q_text.Any(char.IsDigit))
                {
                    MessageBox.Show("Please Write a question without any number ", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Q_text = "";
                }
            }
        }
    }
}
