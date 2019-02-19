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
        private const int Num_Faces = 3;
        private const int Num_Stars = 5;
        private List<int> Slider= new List<int>();

        private int Faces;
        private int Stars;
        private int question_order;

        public Form FORM//property used to modify private data 
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

        private GroupBox groupBox = new GroupBox//define groupbox that contain 3 Radio buttons 
        {
            Text = "Question type",
            Size = new System.Drawing.Size(116, 120),
            AutoSize = false,
            Location = new System.Drawing.Point(470, 50),
            Dock = DockStyle.Right & DockStyle.Bottom,
            TabIndex = 1,
            TabStop = true,

        };

        private TextBox question_box = new TextBox//text box to write a new question within 
        {
            Width = 400,
            Location = new System.Drawing.Point(50, 150),
            TabStop = true,
            TabIndex = 0,

            ForeColor = System.Drawing.Color.Gray,

        };

        private void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            Slider[0] = Slider_default[0];
            Slider[1] = Slider_default[1];
            Slider[2] = Slider_default[2];
            Slider[3] = Slider_default[3];
            Stars = Num_Stars;
            Faces = Num_Faces;
            Make_Empty();
        }

        private TextBox control1 = new TextBox //textbox for start value in Slider questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };

        TextBox control2 = new TextBox//textbox for end value in Slider questions
        {
            Location = new System.Drawing.Point(140, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            Enabled =false,
        };


        TextBox control3 = new TextBox//textbox for start value caption in Slider questions
        {
            Location = new System.Drawing.Point(275, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            Enabled = false,
        };


        TextBox control4 = new TextBox//textbox for End value caption in Slider questions
        {
            Location = new System.Drawing.Point(410, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            Enabled = false,
        };
        private TextBox control5 = new TextBox//textbox for smile Faces in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        private TextBox control6 = new TextBox//textbox for stars number  in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };

        private GroupBox Default_GrouoBox = new GroupBox //to hold a slider question controls
        {
            Location = new System.Drawing.Point(50, 268),
            Size = new System.Drawing.Size(536, 50),
            Text = "Default values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        private GroupBox Default_GrouoBox2 = new GroupBox//to hold a smiley question controls
        {
            Location = new System.Drawing.Point(50, 268),
            Size = new System.Drawing.Size(400, 50),
            Text = "Default values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        private GroupBox Default_GrouoBox3 = new GroupBox//to hold a star question controls
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

        private Form form = new Form//create form to show controls in Add dialog
        {
            Width = 600,
            Height = 400,
            MaximumSize = new System.Drawing.Size(663, 400),
            MinimumSize = new System.Drawing.Size(663, 400),

        };

        private DataGridView Dv = new DataGridView//create data grid view to show data ENTERED in database (show new record only)
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

        private void Make_Empty (TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                question_box.ForeColor = System.Drawing.Color.Gray;
                question_box.Text = "Write a question here ...";
            }
            else if (ReferenceEquals(box, control1))
            {
                control1.ForeColor = System.Drawing.Color.Gray;
                control1.Text = string.Format("Start ={0}", Slider_default[0]);

            }
            else if (ReferenceEquals(box, control2))
            {
                control2.ForeColor = System.Drawing.Color.Gray;
                control2.Text = string.Format("End ={0}", Slider_default[1]);
            }
            else if (ReferenceEquals(box, control3))
            {
                control3.ForeColor = System.Drawing.Color.Gray;
                control3.Text = string.Format("Start Caption ={0}", Slider_default[2]);
            }
            else if (ReferenceEquals(box, control4))
            {
                control4.ForeColor = System.Drawing.Color.Gray;
                control4.Text = string.Format("End Caption ={0}", Slider_default[3]);
            }

            else if (ReferenceEquals(box, control5))
            {
                control5.ForeColor = System.Drawing.Color.Gray;
                control5.Text = string.Format("Smiles = {0}", Num_Faces);
            }

            else if (ReferenceEquals(box, control6))
            {
                control6.ForeColor = System.Drawing.Color.Gray;
                control6.Text = string.Format("Stars = {0}", Num_Stars);
            }
        }

       private void  Make_Empty()
        {
            Make_Empty(control1);
            Make_Empty(control2);
            Make_Empty(control3);
            Make_Empty(control4);
            Make_Empty(control5);
            Make_Empty(control6);

        }

        private void LostFocus(object sender, EventArgs e)//event handler
        {
            if (ReferenceEquals(sender, question_box))//to check if the controls is same of question box 
            {
                if (question_box.Text == "")
                {
                    Make_Empty(question_box);
                }
               

            }
            else if (ReferenceEquals(sender, control1))//to check if the controls is same of control1 
            {
                if (control1.Text == "")
                {
                    Make_Empty(control1);
                    Reset();
                }
                else
                {
                    try
                    {
                        Slider[0] = Int32.Parse(control1.Text);//validate user input 
                        if (Slider[0] <0 || Slider[0] >100)//validate user input (Start value should be between 0-100)
                        {
                            Make_Empty(control1);
                            Reset();//call reset method 
                            throw new  ArgumentOutOfRangeException();
                        }
                        control2.Enabled = true;
                        control2.Focus();
                    }
                    catch ( ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Start value should be a number1111111111", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Start value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (ReferenceEquals(sender, control2))//to check if the controls is same of control2
            {
                if (control2.Text == "")
                {
                    Make_Empty(control2);
                    Reset();
                }
                else
                {
                    try
                    {
                        Slider[1] = Int32.Parse(control2.Text);
                        if (Slider[1] < 0 || Slider[1] > 100)//End value should be between 0-100
                        {
                            Reset();
                            Make_Empty(control2);
                            throw new ArgumentOutOfRangeException();
                        }
                        else if (Slider[1] < Slider[0])//End value should be higher than Start value 
                        {
                            Reset();
                            Make_Empty(control2);
                            MessageBox.Show("End value should be higher than Start value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        control3.Enabled = true;
                        control3.Focus();
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Start value should be integer222222222", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentOutOfRangeException )
                    {
                        MessageBox.Show("End value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (ReferenceEquals(sender, control3))//to check if the controls is same of control3
            {
                if (control3.Text == "")
                {
                    Make_Empty(control3);
                    Reset();
                }
                else
                {
                    try
                    {
                        Slider[2] = Int32.Parse(control3.Text);
                        if (Slider[2] < 0 || Slider[2] > 100)//Start Caption should be between 0-100 
                        {
                            Reset();
                            Make_Empty(control3);
                            throw new ArgumentOutOfRangeException();
                        }
                       else if (Slider[2] < Slider[0])//Start Caption should be higher than Start value 
                        {
                            Reset();
                            Make_Empty(control3);
                            MessageBox.Show("Start Caption should be higher than Start value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        control4.Enabled = true;
                        control4.Focus();

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Start value should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                        MessageBox.Show("Start caption should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

            }
            else if (ReferenceEquals(sender, control4))//to check if the controls is same of control4
            {
                if (control4.Text == "")
                {
                    Make_Empty(control4);
                    Reset();
                }
                else
                {
                    try
                    {
                        Slider[3] = Int32.Parse(control4.Text);
                        if (Slider[3] < 0 || Slider[3] > 100)//End caption should be between 0-100
                        {
                            Reset();
                            Make_Empty(control4);
                            throw new ArgumentOutOfRangeException();
                        }
                       else if (Slider[3] > Slider[1])//End caption should be lower than End value 
                        {
                            Reset();
                            Make_Empty(control4);
                            MessageBox.Show("End Caption should be Lower than End value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Slider[3] <Slider[2])//End caption should be higer than Start caption
                        {

                            Reset();
                            Make_Empty(control4);
                            MessageBox.Show("End caption should be higer than Start caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Start value should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("End Caption should between 0-100 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

            }
            else if (ReferenceEquals(sender, control5))
            {
                if (control5.Text == "")
                {
                    Make_Empty(control5);
                    Reset();
                }

                else
                {
                    try
                    {
                        Faces = Int32.Parse(control5.Text);
                        if (Faces >5 || Faces<0)
                        {
                            Reset();
                            Make_Empty(control5);
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Number of Smiles should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Number of Sniles should between 0-5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (ReferenceEquals(sender, control6))
            {
                if (control6.Text == "")
                {
                    Make_Empty(control6);
                    Reset();
                }
                else
                {
                    try
                    {
                        Stars = Int32.Parse(control6.Text);
                        if (Stars > 10 || Faces < 0)
                        {
                            Reset();
                            Make_Empty(control6);
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (FormatException )
                    {
                        MessageBox.Show("Number of Stars should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Number of stars  should be between 0-10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (question_box.Text != "" && !question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))
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

