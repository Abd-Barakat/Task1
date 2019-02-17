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
        private int index;
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
            ReadOnly = false

        };
        private TextBox question_box = new TextBox
        {
            Width = 400,
            Location = new System.Drawing.Point(50, 150),
        };

        public void ShowDialog(int index, DataTable Dt)
        {
            this.index = index;
            FORM = new Form();
            FORM.Width = 500;
            FORM.Height = 300;
            FORM.MaximumSize = new System.Drawing.Size(663, 250);
            FORM.MinimumSize = form.MaximumSize;
            Dt2 = new DataTable();
            Dt2 = Dt.Clone();
            Dv.KeyDown += Dv_KeyPress;
            Dt2.Rows.Add(Dt.Rows[index].ItemArray[0]);
            Dv.DataSource = Dt2.DefaultView.ToTable(false,"question_text");
            FORM.Controls.Add(Dv);
            FORM.Controls.Add(question_box);
            FORM.Visible = true;
        }

        private  void Dv_KeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode ==Keys.Enter )
            {
                string Q_text;
                Q_text =(string) Dv.CurrentCell.Value;
                if (Q_text != "")
                {
                    SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                    SqlCommand command = new SqlCommand(  string.Format("update questions set question_text = '{0}' where question_order = {1}", Q_text,index)   , connection);
                    try
                    { 
                        question_box.Text = command.CommandText;
                        connection.Open();
                        command.ExecuteNonQuery();
                        DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        while (result != DialogResult.OK) ;
                        FORM.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
                else
                    MessageBox.Show("Please Write a question before saving ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
