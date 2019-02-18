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
    class Add_dialog
    {

        public Form form ;
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
            question_box.Text = "";
            FORM = null;
        }
        private TextBox question_box = new TextBox
        {
            Width = 400,
            Location = new System.Drawing.Point(50, 150),
        };

        private NumericUpDown question_order = new NumericUpDown
        {
            ReadOnly = true,
            UpDownAlign = LeftRightAlignment.Right,
            Location = new System.Drawing.Point(450, 150),
            Width = 100,
        };

        private DataGridView Dv = new DataGridView
        {
            Width = 500,
            Height = 50,
            Location = new System.Drawing.Point(50, 50),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToDeleteRows = false,
            AllowUserToAddRows = true,
            AllowDrop = false,
            ReadOnly = true
        };
        public void ShowDialog(DataTable Dt)
        {
            FORM = new Form();
            int max_order;
            if (Dt.Rows.Count > 0)
            {
                max_order = (int)Dt.Rows[Dt.Rows.Count - 1].ItemArray[1] + 1;
            }
            else
                max_order = 0;
            FORM.Width = 500;
            FORM.Height = 300;
            FORM.MaximumSize = new System.Drawing.Size(663, 250);
            FORM.MinimumSize = FORM.MaximumSize;
            question_box.KeyDown += new KeyEventHandler(Dv_RowsAdded_handler);
            Dv.DataSource = Dt.Clone();
            question_order.Minimum = max_order;
            form.Controls.Add(Dv);
            form.Controls.Add(question_box);
            FORM.ActiveControl = question_box;
            FORM.Controls.Add(question_order);
            FORM.Visible = true;

        }

        private void Dv_RowsAdded_handler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (question_box.Text != "" && !question_box.Text.Any(char.IsDigit))
                {
                    DataTable Dt2 = new DataTable();
                    SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                    SqlCommand command = new SqlCommand(string.Format("insert into questions values ('{0}',", question_box.Text) + question_order.Value + ")", connection);
                    try
                    {
                        Dv.Rows[0].Cells[0].Value = question_box.Text;
                        Dv.Rows[0].Cells[1].Value = question_order.Value;
                        question_box.Text = command.CommandText;
                        connection.Open();
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        dataAdapter.Fill(Dt2);
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
                else if (question_box.Text == "")
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question  ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (question_box.Text.Any(char.IsDigit))
                {
                    question_box.Text = "";
                    MessageBox.Show("Please Write a question without numbers   ", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

    }

}

