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
        private readonly string[] Tables = new string[] { "Slider", "Smiley", "Stars" };
        private readonly int[] Slider_default = new int[] { 0, 100, 20, 90 };
        private readonly int Num_Faces = 3;
        private readonly int Num_Stars = 5;

        private int question_order;
        public Form form;
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
        private GroupBox groupBox = new GroupBox
        {
            Text = "Question type",
            Size = new System.Drawing.Size(116, 120),
            AutoSize = false,
            Location = new System.Drawing.Point(470, 50),
            Dock = DockStyle.Right & DockStyle.Bottom,
            
        };

        private TextBox question_box = new TextBox
        {
            Width = 400,
            Location = new System.Drawing.Point(50, 150),
        };

       


        private RadioButton SliderButton = new RadioButton
        {
            Text = "Slider",
            Location = new System.Drawing.Point (10,30) 
            
        };
        private RadioButton SmileyButton = new RadioButton
        {
            Text = "Smiley",
            Location = new System.Drawing.Point(10, 60)
        };
        private RadioButton StarsButton = new RadioButton
        {
            Text = "Stars",
            Location = new System.Drawing.Point(10, 90)
        };


        private DataGridView Dv = new DataGridView
        {
            Width = 400,
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
            groupBox.Controls.Add(SmileyButton);
            groupBox.Controls.Add(StarsButton);
            groupBox.Controls.Add(SliderButton);
            if (Dt.Rows.Count > 0)
            {
                question_order =   (int)Dt.Rows[Dt.Rows.Count - 1].ItemArray[1] + 1;
            }
            else
                question_order = 0;
            FORM.Width = 600;
            FORM.Height = 400;
            FORM.MaximumSize = new System.Drawing.Size(663, 400);
            FORM.MinimumSize = FORM.MaximumSize;
            question_box.KeyDown += new KeyEventHandler(Dv_RowsAdded_handler);
            Dv.DataSource = Dt.Clone();
            form.Controls.Add(Dv);
            form.Controls.Add(question_box);
            FORM.ActiveControl = question_box;
            FORM.Controls.Add(groupBox);
            FORM.Visible = true;

        }

        private void Dv_RowsAdded_handler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (question_box.Text != "" && !question_box.Text.Any(char.IsDigit))
                {
                    int Groupbox_index = -1;

                    if (SliderButton.Checked)
                        Groupbox_index = 0;
                    else if (SmileyButton.Checked)
                        Groupbox_index = 1;
                    else if (StarsButton.Checked)
                        Groupbox_index = 2;
                    if (Groupbox_index != -1)
                    {
                        DataTable Dt2 = new DataTable();
                        SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                        SqlCommand command = new SqlCommand(string.Format("insert into questions values ('{0}',", question_box.Text) + question_order + ",'"+Tables[Groupbox_index]+"')", connection);
                        try
                        {
                            Dv.Rows[0].Cells[0].Value = question_box.Text;
                            Dv.Rows[0].Cells[1].Value = question_order;
                            Dv.Rows[0].Cells[2].Value = Tables[Groupbox_index];
                            connection.Open();

                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(Dt2);

                            insert(Groupbox_index, connection, command);

                            DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            while (result != DialogResult.OK) ;
                            FORM.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            FORM.Visible = false;
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
                        MessageBox.Show("Please select question type ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);


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
        private void insert (int groud_index,SqlConnection connection,SqlCommand command)
        {
            switch (groud_index)
            {
                case 0:
                    command.CommandText = string.Format("insert into {0} values ({1},{2},{3},{4},{5})", Tables[0], question_order, Slider_default[0], Slider_default[1], Slider_default[2], Slider_default[3]);
                    command.ExecuteNonQuery();
                    break;
                case 1:
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[1], question_order, Num_Faces);
                    command.ExecuteNonQuery();
                    break;
                case 2://hello
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[2], question_order, Num_Stars);
                    command.ExecuteNonQuery();
                    break;
            }
        }
    }

}

