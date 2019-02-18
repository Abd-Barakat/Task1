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
        private readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };
        private readonly int[] Slider_default = new int[] { 0, 100, 20, 90 };
        private readonly int Num_Faces = 3;
        private readonly int Num_Stars = 5;

        private int question_order;
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
            TabIndex = 1,
            TabStop = true,

        };

        private TextBox question_box = new TextBox
        {
            Width = 400,
            Location = new System.Drawing.Point(50, 150),
            TabStop = true,
            TabIndex = 0,

            ForeColor = System.Drawing.Color.Gray,
            Text = "Write a question here ...",

        };
        private TextBox control1 = new TextBox
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };

        TextBox control2 = new TextBox
        {
            Location = new System.Drawing.Point(140, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,


        };


        TextBox control3 = new TextBox
        {
            Location = new System.Drawing.Point(275, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,



        };


        TextBox control4 = new TextBox
        {
            Location = new System.Drawing.Point(410, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,



        };

        private GroupBox Default_GrouoBox = new GroupBox
        {
            Location = new System.Drawing.Point(50, 268),
            Size = new System.Drawing.Size(536, 50),
            Text = "Default values",
            TabIndex = 2,
            TabStop = true,
            Visible = true,
        };


        private RadioButton SliderButton = new RadioButton
        {
            Text = "Slider",
            Location = new System.Drawing.Point(10, 30),
            TabIndex = 0,
            TabStop = true

        };
        private RadioButton SmileyButton = new RadioButton
        {
            Text = "Smiley",
            Location = new System.Drawing.Point(10, 60),
            TabIndex = 1,
            TabStop = true
        };
        private RadioButton StarsButton = new RadioButton
        {
            Text = "Stars",
            Location = new System.Drawing.Point(10, 90),
            TabIndex = 2,
            TabStop = true
        };

        private Form form = new Form
        {
            Width = 600,
            Height = 400,
            MaximumSize = new System.Drawing.Size(663, 400),
            MinimumSize = new System.Drawing.Size(663, 400),

        };

        private DataGridView Dv = new DataGridView
        {
            Size = new System.Drawing.Size(400, 50),
            Location = new System.Drawing.Point(50, 50),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToDeleteRows = false,
            AllowUserToAddRows = true,
            AllowDrop = false,
            ReadOnly = true,
            TabStop = false,
            AllowUserToResizeRows = false,
            AllowUserToResizeColumns = false,
        };

        private void LostFocus(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, question_box))
            {
                if (question_box.Text == "")
                {
                    question_box.ForeColor = System.Drawing.Color.Gray;
                    question_box.Text = "Write a question here ...";
                }

            }
            else if (ReferenceEquals(sender, control1))
            {
                if (control1.Text == "")
                {
                    control1.ForeColor = System.Drawing.Color.Gray;
                    control1.Text = string.Format("Start ={0}", Slider_default[0]);
                }
            }
            else if (ReferenceEquals(sender, control2))
            {
                if (control2.Text == "")
                {
                    control2.ForeColor = System.Drawing.Color.Gray;
                    control2.Text = string.Format("End ={0}", Slider_default[1]);
                }

            }
            else if (ReferenceEquals(sender, control3))
            {
                if (control3.Text == "")
                {
                    control3.ForeColor = System.Drawing.Color.Gray;
                    control3.Text = string.Format("Start Caption ={0}", Slider_default[2]);
                }


            }
            else if (ReferenceEquals(sender, control4))
            {
                if (control4.Text == "")
                {
                    control4.ForeColor = System.Drawing.Color.Gray;
                    control4.Text = string.Format("End Caption ={0}", Slider_default[3]);
                }


            }
        }
        private void GotFocus(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, SliderButton))
                SliderButton.Checked = true;
            else if (ReferenceEquals(sender, SmileyButton))
                SmileyButton.Checked = true;
            else if (ReferenceEquals(sender, StarsButton))
                StarsButton.Checked = true;
        }

        private void initialize()
        {
            control1.Text = string.Format("Start ={0}", Slider_default[0]);
            control2.Text = string.Format("End ={0}", Slider_default[1]);
            control3.Text = string.Format("Start Caption ={0}", Slider_default[2]);
            control4.Text = string.Format("End Caption ={0}", Slider_default[3]);
            /////////////////////////////////////////
            SliderButton.GotFocus += GotFocus;
            SmileyButton.GotFocus += GotFocus;
            StarsButton.GotFocus += GotFocus;
            //////////////////////////////////////////
            groupBox.Controls.Add(SliderButton);
            groupBox.Controls.Add(SmileyButton);
            groupBox.Controls.Add(StarsButton);
            /////////////////////////////////////////
            control1.GotFocus += TextChanged;
            control2.GotFocus += TextChanged;
            control3.GotFocus += TextChanged;
            control4.GotFocus += TextChanged;
            question_box.GotFocus += TextChanged;
            /////////////////////////////////////////
            control1.MouseClick += MouseClick;
            control2.MouseClick += MouseClick;
            control3.MouseClick += MouseClick;
            control4.MouseClick += MouseClick;
            question_box.MouseClick += MouseClick;
            /////////////////////////////////////////
            control1.LostFocus += LostFocus;
            control2.LostFocus += LostFocus;
            control3.LostFocus += LostFocus;
            control4.LostFocus += LostFocus;
            question_box.LostFocus += LostFocus;
            /////////////////////////////////////////
            Default_GrouoBox.Controls.Add(control1);
            Default_GrouoBox.Controls.Add(control2);
            Default_GrouoBox.Controls.Add(control3);
            Default_GrouoBox.Controls.Add(control4);
            /////////////////////////////////////////
            question_box.KeyDown += new KeyEventHandler(Dv_RowsAdded_handler);
            SmileyButton.KeyDown += Dv_RowsAdded_handler;
            SliderButton.KeyDown += Dv_RowsAdded_handler;
            StarsButton.KeyDown += Dv_RowsAdded_handler;
            /////////////////////////////////////////
            FORM.Controls.Add(Dv);
            FORM.Controls.Add(question_box);
            FORM.Controls.Add(groupBox);
            FORM.Controls.Add(Default_GrouoBox);

        }

        public void ShowDialog(DataTable Dt)
        {
            initialize();
            Dv.DataSource = Dt.Clone();

            if (Dt.Rows.Count > 0)
                question_order = (int)Dt.Rows[Dt.Rows.Count - 1].ItemArray[1] + 1;
            else
                question_order = 0;

            FORM.Visible = true;

        }



        private void MouseClick(object sender, MouseEventArgs e)
        {
            if (ReferenceEquals(sender, question_box))
            {
                question_box.Text = "";
            }
            else if (ReferenceEquals(sender, control1))
            {
                control1.Text = "";

            }
            else if (ReferenceEquals(sender, control2))
            {
                control2.Text = "";

            }
            else if (ReferenceEquals(sender, control3))
            {
                control3.Text = "";

            }
            else if (ReferenceEquals(sender, control4))
            {
                control4.Text = "";

            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, question_box))
            {
                question_box.Text = "";
                question_box.ForeColor = System.Drawing.Color.Black;
            }
            else if (ReferenceEquals(sender, control1))
            {
                control1.Text = "";
                control1.ForeColor = System.Drawing.Color.Black;

            }
            else if (ReferenceEquals(sender, control2))
            {
                control2.Text = "";

                control2.ForeColor = System.Drawing.Color.Black;

            }
            else if (ReferenceEquals(sender, control3))
            {
                control3.Text = "";

                control3.ForeColor = System.Drawing.Color.Black;

            }
            else if (ReferenceEquals(sender, control4))
            {
                control4.Text = "";

                control4.ForeColor = System.Drawing.Color.Black;

            }
        }

        private void Dv_RowsAdded_handler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (question_box.Text != "" && !question_box.Text.Any(char.IsDigit))
                {
                    int Groupbox_index = Group_Index();//return index of selected control in GroupBox

                    if (Groupbox_index != -1)//if no control selected in GroupBox
                    {

                        SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                        SqlCommand command = new SqlCommand();
                        try
                        {
                            Dv.Rows[0].Cells[0].Value = question_box.Text;
                            Dv.Rows[0].Cells[1].Value = question_order;
                            Dv.Rows[0].Cells[2].Value = Tables[Groupbox_index];

                            insert(0, connection, command);//insert data to question table first 
                            insert(Groupbox_index, connection, command);//insert data to a specific table 
                            DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            while (result != DialogResult.OK) ;//wait unitl MessageBox closes 
                            FORM.Visible = false;//hide Add dialog
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void insert(int Groupbox_index, SqlConnection connection, SqlCommand command)
        {
            Open_connection(connection);
            switch (Groupbox_index)
            {

                case 0:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], question_box.Text, question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    break;
                case 1:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ({1},{2},{3},{4},{5})", Tables[1], question_order, Slider_default[0], Slider_default[1], Slider_default[2], Slider_default[3]);
                    command.ExecuteNonQuery();
                    break;
                case 2:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[2], question_order, Num_Faces);
                    command.ExecuteNonQuery();
                    break;
                case 3:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[3], question_order, Num_Stars);
                    command.ExecuteNonQuery();
                    break;
            }
        }
        private void Open_connection(SqlConnection sql)
        {
            if (sql.State == ConnectionState.Closed)
                sql.Open();

        }
        private int Group_Index()
        {

            if (SliderButton.Checked)
                return 1;
            else if (SmileyButton.Checked)
                return 2;
            else if (StarsButton.Checked)
                return 3;
            else
                return -1;
        }
    }

}

