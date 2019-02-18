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
        private List<int> Slider= new List<int>();
        private int Faces;
        private int Stars;
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
        private TextBox control5 = new TextBox
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        private TextBox control6 = new TextBox
        {
            Location = new System.Drawing.Point(5, 20),
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
            Visible = false,

        };
        private GroupBox Default_GrouoBox2 = new GroupBox
        {
            Location = new System.Drawing.Point(50, 268),
            Size = new System.Drawing.Size(400, 50),
            Text = "Default values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        private GroupBox Default_GrouoBox3 = new GroupBox
        {
            Location = new System.Drawing.Point(50, 268),
            Size = new System.Drawing.Size(400, 50),
            Text = "Default values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

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
                    Slider[0] = Slider_default[0];
                }
                else
                {
                    try
                    {
                        Slider[0] = Int32.Parse(control1.Text);
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Start value should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (ReferenceEquals(sender, control2))
            {
                if (control2.Text == "")
                {
                    control2.ForeColor = System.Drawing.Color.Gray;
                    control2.Text = string.Format("End ={0}", Slider_default[1]);
                    Slider[1] = Slider_default[1];

                }
                else
                {
                    try
                    {
                        Slider[1] = Int32.Parse(control2.Text);
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Start value should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (ReferenceEquals(sender, control3))
            {
                if (control3.Text == "")
                {
                    control3.ForeColor = System.Drawing.Color.Gray;
                    control3.Text = string.Format("Start Caption ={0}", Slider_default[2]);
                    Slider[2] = Slider_default[2];

                }
                else
                {
                    try
                    {
                        Slider[2] = Int32.Parse(control3.Text);
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Start value should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (ReferenceEquals(sender, control4))
            {
                if (control4.Text == "")
                {
                    control4.ForeColor = System.Drawing.Color.Gray;
                    control4.Text = string.Format("End Caption ={0}", Slider_default[3]);
                    Slider[3] = Slider_default[3];

                }
                else
                {
                    try
                    {
                        Slider[3] = Int32.Parse(control4.Text);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Start value should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (ReferenceEquals(sender, control5))
            {
                if (control5.Text == "")
                {
                    control5.ForeColor = System.Drawing.Color.Gray;
                    control5.Text = string.Format("Smiles = {0}", Num_Faces);
                    Faces = Num_Faces;
                }

                else
                {
                    try
                    {
                        Faces = Int32.Parse(control5.Text);
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Number of Smiles should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (ReferenceEquals(sender, control6))
            {
                if (control6.Text == "")
                {
                    control6.ForeColor = System.Drawing.Color.Gray;
                    control6.Text = string.Format("Stars = {0}", Num_Stars);
                    Stars = Num_Stars;
                }
                else
                {
                    try
                    {
                        Stars = Int32.Parse(control6.Text);
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Number of Stars should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
        }
        private void GotFocus(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, SliderButton))
            {
                SliderButton.Checked = true;
                Default_GrouoBox.Visible = true;
            }
            else if (ReferenceEquals(sender, SmileyButton))
            {
                SmileyButton.Checked = true;
                Default_GrouoBox2.Visible = true;
            }
            else if (ReferenceEquals(sender, StarsButton))
            {
                StarsButton.Checked = true;
                Default_GrouoBox3.Visible = true;

            }
        }

        private void initialize()
        {
            Slider.Add(Slider_default[0]);
            Slider.Add(Slider_default[1]);
            Slider.Add(Slider_default[2]);
            Slider.Add(Slider_default[3]);
            Stars = Num_Stars;
            Faces = Num_Faces;
            /////////////////////////////////////////
            question_box.Text = "Write a question here ...";
            control1.Text = string.Format("Start ={0}", Slider_default[0]);
            control2.Text = string.Format("End ={0}", Slider_default[1]);
            control3.Text = string.Format("Start Caption ={0}", Slider_default[2]);
            control4.Text = string.Format("End Caption ={0}", Slider_default[3]);
            control5.Text = string.Format("Smiles = {0}", Num_Faces);
            control6.Text = string.Format("Stars = {0}", Num_Stars);
            /////////////////////////////////////////
            SliderButton.GotFocus += GotFocus;
            SmileyButton.GotFocus += GotFocus;
            StarsButton.GotFocus += GotFocus;
            //////////////////////////////////////////
            question_box.KeyDown += new KeyEventHandler(Dv_RowsAdded_handler);
            SmileyButton.KeyDown += Dv_RowsAdded_handler;
            SliderButton.KeyDown += Dv_RowsAdded_handler;
            StarsButton.KeyDown += Dv_RowsAdded_handler;
            /////////////////////////////////////////
            SmileyButton.CheckedChanged += CheckedChanged;
            SliderButton.CheckedChanged += CheckedChanged;
            StarsButton.CheckedChanged += CheckedChanged;
            //////////////////////////////////////////
            groupBox.Controls.Add(SliderButton);
            groupBox.Controls.Add(SmileyButton);
            groupBox.Controls.Add(StarsButton);
            /////////////////////////////////////////
            control1.GotFocus += TextChanged;
            control2.GotFocus += TextChanged;
            control3.GotFocus += TextChanged;
            control4.GotFocus += TextChanged;
            control5.GotFocus += TextChanged;
            control6.GotFocus += TextChanged;
            question_box.GotFocus += TextChanged;
            /////////////////////////////////////////
            control1.MouseClick += TextChanged;
            control2.MouseClick += TextChanged;
            control3.MouseClick += TextChanged;
            control4.MouseClick += TextChanged;
            control5.MouseClick += TextChanged;
            control6.MouseClick += TextChanged;
            question_box.MouseClick += TextChanged;
            /////////////////////////////////////////
            control1.LostFocus += LostFocus;
            control2.LostFocus += LostFocus;
            control3.LostFocus += LostFocus;
            control4.LostFocus += LostFocus;
            control5.LostFocus += LostFocus;
            control6.LostFocus += LostFocus;
            question_box.LostFocus += LostFocus;
            /////////////////////////////////////////
            Default_GrouoBox.Controls.Add(control1);
            Default_GrouoBox.Controls.Add(control2);
            Default_GrouoBox.Controls.Add(control3);
            Default_GrouoBox.Controls.Add(control4);
            /////////////////////////////////////////
            Default_GrouoBox2.Controls.Add(control5);
            /////////////////////////////////////////
            Default_GrouoBox3.Controls.Add(control6);
            /////////////////////////////////////////
            FORM.Controls.Add(Dv);
            FORM.Controls.Add(question_box);
            FORM.Controls.Add(groupBox);
            FORM.Controls.Add(Default_GrouoBox);
            FORM.Controls.Add(Default_GrouoBox2);
            FORM.Controls.Add(Default_GrouoBox3);
        }

        

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, SliderButton))
            {
                if (!SliderButton.Checked)
                    Default_GrouoBox.Visible = false;
            }
            else if (ReferenceEquals(sender, SmileyButton))
            {
                if (!SmileyButton.Checked)
                    Default_GrouoBox2.Visible = false;
            }
            else if (ReferenceEquals(sender, StarsButton))
            {
                if (!StarsButton.Checked)
                    Default_GrouoBox3.Visible = false;
            }
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



        private bool isEmpty (TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "Write a question here ...")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control1))
            {
                if (control1.Text == string.Format("Start ={0}", Slider_default[0]))
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, control2))
            {
                if (control2.Text == string.Format("End ={0}", Slider_default[1]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control3))
            {
                if (control3.Text == string.Format("Start Caption ={0}", Slider_default[2]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control4))
            {
                if (control4.Text == string.Format("End Caption ={0}", Slider_default[3]))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control5))
            {
                if (control5.Text == string.Format("Smiles = {0}", Num_Faces))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control6))
            {
                if (control6.Text == string.Format("Stars = {0}", Num_Stars))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        private void TextChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, question_box))
            {
                if (isEmpty(question_box))
                {
                    question_box.Text = "";
                    question_box.ForeColor = System.Drawing.Color.Black;
                }
            }
            else if (ReferenceEquals(sender, control1))
            {
                if (isEmpty(control1))
                {
                    control1.Text = "";
                    control1.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, control2))
            {
                if (isEmpty(control2))
                {
                    control2.Text = "";

                    control2.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, control3))
            {
                if (isEmpty(control3))
                {
                    control3.Text = "";
                    control3.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, control4))
            {
                if (isEmpty(control4))
                {
                    control4.Text = "";
                    control4.ForeColor = System.Drawing.Color.Black;
                }

            }

            else if (ReferenceEquals(sender, control5))
            {
                if (isEmpty(control5))
                {
                    control5.Text = "";
                    control5.ForeColor = System.Drawing.Color.Black;
                }

            }

            else if (ReferenceEquals(sender, control6))
            {
                if (isEmpty(control6))
                {
                    control6.Text = "";
                    control6.ForeColor = System.Drawing.Color.Black;
                }

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
                            Dv.Rows[0].Cells[2].Value = Tables[Groupbox_index+1];

                          
                            insert( connection, command);//insert data to a specific table 
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
        private void insert( SqlConnection connection, SqlCommand command)
        {
            int Groupbox_index = Group_Index();
            Open_connection(connection);
            switch (Groupbox_index)
            { 
                case 0:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], question_box.Text, question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("insert into {0} values ({1},{2},{3},{4},{5})", Tables[1], question_order, Slider[0], Slider[1], Slider[2], Slider[3]);
                    command.ExecuteNonQuery();
                    break;
                case 1:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], question_box.Text, question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[2], question_order, Faces);
                    command.ExecuteNonQuery();
                    break;
                case 2:
                    command.Connection = connection;

                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], question_box.Text, question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[3], question_order, Stars);
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
                return 0;
            else if (SmileyButton.Checked)
                return 1;
            else if (StarsButton.Checked)
                return 2;
            else
                return -1;
        }
    }

}

