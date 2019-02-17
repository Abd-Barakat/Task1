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

<<<<<<< HEAD
        public Form form;
=======
        public Form form ;
>>>>>>> e0b5e41de138403cae3fb217d3bff8a4483f8f78
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
<<<<<<< HEAD
            int max_order;
            FORM = new Form();
            FORM.Width = 500;
            FORM.Height = 300;
            FORM.MaximumSize = new System.Drawing.Size(663, 250);
            FORM.MinimumSize = FORM.MaximumSize;
            if (Dt.Rows.Count > 0)
                max_order = (int)Dt.Rows[Dt.Rows.Count - 1].ItemArray[1] + 1;
            else
                max_order = 0;


            question_box.KeyDown += new KeyEventHandler(Dv_RowsAdded_handler);
            question_order.Minimum = max_order;

            Dv.DataSource = Dt.Clone();

            FORM.Controls.Add(Dv);
            FORM.Controls.Add(question_box);
            FORM.ActiveControl = question_box;
            FORM.Controls.Add(question_order);
            FORM.Visible = true;
=======
            FORM = new Form();
            int max_order = (int)Dt.Rows[Dt.Rows.Count - 1].ItemArray[1] + 1;
            form.Width = 500;
            form.Height = 300;
            form.MaximumSize = new System.Drawing.Size(663, 250);
            form.MinimumSize = form.MaximumSize;
            question_box.KeyDown += new KeyEventHandler(Dv_RowsAdded_handler);
            Dv.DataSource = Dt.Clone();
            question_order.Minimum = max_order;
            form.Controls.Add(Dv);
            form.Controls.Add(question_box);
            form.ActiveControl = question_box;
            form.Controls.Add(question_order);
            form.Visible = true;
>>>>>>> e0b5e41de138403cae3fb217d3bff8a4483f8f78

        }

        private void Dv_RowsAdded_handler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
<<<<<<< HEAD
                if (question_box.Text != "" && !question_box.Text.Any(char.IsDigit))
=======
                if (question_box.Text != "")
>>>>>>> e0b5e41de138403cae3fb217d3bff8a4483f8f78
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
<<<<<<< HEAD
                        FORM.Visible = false;
=======
                        form.Visible = false;
>>>>>>> e0b5e41de138403cae3fb217d3bff8a4483f8f78
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
<<<<<<< HEAD
                else if (question_box.Text == "")
                {
                    MessageBox.Show("Please Write a question  ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    question_box.Text = "";
                }
                else if (question_box.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Please Write a question without any number ", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    question_box.Text = "";
                }
=======
                else
                    MessageBox.Show("Please Write a question before saving ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
>>>>>>> e0b5e41de138403cae3fb217d3bff8a4483f8f78
            }

        }

    }

}

