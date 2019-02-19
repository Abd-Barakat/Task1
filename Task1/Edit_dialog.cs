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
    public class Edit_dialog
    {
        private DataTable Dt2=new DataTable();
        private int Question_order;
        private List<int> Slider = new List<int>();
        private readonly int [] Slider_default = new int [4];
        private int Num_Faces;
        private int Num_Stars;
        private int Faces;
        private int Stars;
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
        private TextBox control1 = new TextBox //textbox for start value in Slider questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 0,
        };

        TextBox control2 = new TextBox//textbox for end value in Slider questions
        {
            Location = new System.Drawing.Point(140, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 1,

        };


        TextBox control3 = new TextBox//textbox for start value caption in Slider questions
        {
            Location = new System.Drawing.Point(275, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 2,

        };


        TextBox control4 = new TextBox//textbox for End value caption in Slider questions
        {
            Location = new System.Drawing.Point(410, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 3,

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
        private TextBox question_box = new TextBox//text box to write a new question within 
        {
            Location = new System.Drawing.Point(50, 150),
            TabStop = true,
            TabIndex = 0,
            Size = new System.Drawing.Size(536, 50),
            ForeColor = System.Drawing.Color.Gray,

        };


        private GroupBox Default_GrouoBox = new GroupBox //to hold a slider question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(536, 50),
            Text = "Current values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        private GroupBox Default_GrouoBox2 = new GroupBox//to hold a smiley question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(400, 50),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        private GroupBox Default_GrouoBox3 = new GroupBox//to hold a star question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(400, 50),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        private Form form = new Form
        {
           Width = 600,
            Height = 400,
            MaximumSize = new System.Drawing.Size(663, 400),
            MinimumSize = new System.Drawing.Size(663, 400),

        };

        private Button Save = new Button
        {
            Text = "Save",
            Location = new System.Drawing.Point(513, 300),
        };
        private DataGridView Dv = new DataGridView
        {
            Size = new System.Drawing.Size(536, 50),
            Location = new System.Drawing.Point(50, 50),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToDeleteRows = false,
            AllowUserToAddRows = false,
            AllowDrop = false,
            ReadOnly = true,
            AllowUserToResizeRows = false,
            AllowUserToResizeColumns = false,

        };
        
        private void initialize()
        {
            Retrive_Data();
            Default_GrouoBox.Controls.Add(control1);
            Default_GrouoBox.Controls.Add(control2);
            Default_GrouoBox.Controls.Add(control3);
            Default_GrouoBox.Controls.Add(control4);
            Default_GrouoBox2.Controls.Add(control5);
            Default_GrouoBox3.Controls.Add(control6);
            Default_GrouoBox3.Controls.Add(question_box);
            ///////////////////////////////////////////
            question_box.Text = "Write a new question here ...";
            ///////////////////////////////////////////
            control1.GotFocus += TextChanged;
            control2.GotFocus += TextChanged;
            control3.GotFocus += TextChanged;
            control4.GotFocus += TextChanged;
            control5.GotFocus += TextChanged;
            control6.GotFocus += TextChanged;
            question_box.GotFocus += TextChanged;
            control1.MouseClick += TextChanged;
            control2.MouseClick += TextChanged;
            control3.MouseClick += TextChanged;
            control4.MouseClick += TextChanged;
            control5.MouseClick += TextChanged;
            control6.MouseClick += TextChanged;
            question_box.MouseClick += TextChanged;
            ///////////////////////////////////////////
             control1.KeyDown += KeyDown;
            control2.KeyDown += KeyDown;
            control3.KeyDown += KeyDown;
            control4.KeyDown += KeyDown;
            ///////////////////////////////////////////
            FORM.Controls.Add(question_box);
            FORM.Controls.Add(Default_GrouoBox);
            FORM.Controls.Add(Default_GrouoBox2);
            FORM.Controls.Add(Default_GrouoBox3);
            FORM.Controls.Add(Dv);
            FORM.Controls.Add(Save);
            ///////////////////////////////////////////
            FORM = form;
            FORM.Visible = true;
            Dv.DataSource = Dt2.DefaultView.ToTable(false, "question_text");
            Dv.KeyDown += Dv_KeyPress;
            Save.Click += Save_Click;
        }

        private string Question_Type()
        {
            return Dt2.Rows[0].ItemArray[2].ToString();
        }
        private void KeyDown(object sender, KeyEventArgs e)//to move to next control 
        {
            if (e.KeyCode == Keys.Enter)
            {
                Default_GrouoBox.SelectNextControl((TextBox)sender, true, false, false, true);
            }
        }

        private void ShowGroupBox(string type,DataTable data)
        {
            switch (type)
            {
                case "Slider":
                    Default_GrouoBox.Visible = true;
                    control1.Text = data.Rows[0].ItemArray[1].ToString();
                    control2.Text = data.Rows[0].ItemArray[2].ToString();
                    control3.Text = data.Rows[0].ItemArray[3].ToString();
                    control4.Text = data.Rows[0].ItemArray[4].ToString();
                    Slider_default[0]=(Int32.Parse(control1.Text));
                    Slider_default[1]=(Int32.Parse(control2.Text));
                    Slider_default[2]=(Int32.Parse(control3.Text));
                    Slider_default[3]=(Int32.Parse(control4.Text));
                    Slider.Add(Slider_default[0]);
                    Slider.Add(Slider_default[1]);
                    Slider.Add(Slider_default[2]);
                    Slider.Add(Slider_default[3]);
                    break;
                case "Smiley":
                    Default_GrouoBox2.Visible = true;
                    control5.Text = data.Rows[0].ItemArray[1].ToString();
                    Num_Faces = Int32.Parse(control5.Text);
                    Faces = Num_Faces;
                        break;
                case "Stars":
                    Default_GrouoBox3.Visible = true;
                    control6.Text = data.Rows[0].ItemArray[1].ToString();
                    Num_Stars = Int32.Parse(control6.Text);
                    Stars = Num_Stars;

                    break;
            }

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

        private void Retrive_Data()
        {
            string type = Question_Type();
            SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
            SqlCommand command = new SqlCommand(string.Format("select * from {0} where question_O = {1}",type,Dt2.Rows[0].ItemArray[1]),connection);
            Open_connection(connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable temp =new DataTable();
            dataAdapter.Fill(temp);
            ShowGroupBox(type,temp);
        }
        private void Open_connection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
       

       

        public void ShowDialog(int order, DataTable Dt, int Row_index)
        {
            Question_order = order;          
            Dt2 = Dt.Clone();
            Dt2.Rows.Add(Dt.Rows[Row_index].ItemArray);
            initialize();
        }

        private void Dv_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Q_text;
                if (Dv.CurrentCell.Value != DBNull.Value)
                    Q_text = (string)Dv.CurrentCell.Value;
                else
                    Q_text = "";
                if (Q_text != "" && !Q_text.Any(char.IsDigit))
                {
                    SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                    SqlCommand command = new SqlCommand(string.Format("update questions set question_text = '{0}' where question_order = {1}", Q_text, Question_order), connection);
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
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool check()
        {
            if (!Update())
            {
                return false;
            }
            if (Question_Type ()== "Slider")
            {
                if (Slider[0] < 0 || Slider[0] > 100)//validate user input (Start value should be between 0-100)
                {
                    Make_Empty(control1);
                    Reset();//call reset method 
                    MessageBox.Show("Start value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[0] >= Slider[1])
                {
                    Make_Empty(control1);
                    Reset();
                    MessageBox.Show("Start value should be lower than end value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[0] >= Slider[2])
                {
                    Make_Empty(control1);
                    Reset();
                    MessageBox.Show("Start value should be lower than Start caption ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[1] < 0 || Slider[1] > 100)//End value should be between 0-100
                {
                    Reset();
                    Make_Empty(control2);
                    MessageBox.Show("End value should be betweem 0-100 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[2] < 0 || Slider[2] > 100)//Start Caption should be between 0-100 
                {
                    Reset();
                    Make_Empty(control3);
                    MessageBox.Show("Start caption should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[2] >= Slider[1])//Start Caption should be lower than End value 
                {
                    Reset();
                    Make_Empty(control3);
                    MessageBox.Show("Start Caption should be lower than End value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[2] >= Slider[3])
                {
                    Reset();
                    Make_Empty(control3);
                    MessageBox.Show("Start Caption should be lower than End caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[3] < 0 || Slider[3] > 100)//End caption should be between 0-100
                {
                    Reset();
                    Make_Empty(control4);
                    MessageBox.Show("End Caption should between 0-100 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[3] >= Slider[1])//End caption should be lower than End value 
                {
                    Reset();
                    Make_Empty(control4);
                    MessageBox.Show("End Caption should be Lower than End value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Slider[3] <= Slider[2])//End caption should be higer than Start caption
                {
                    Reset();
                    Make_Empty(control4);
                    MessageBox.Show("End caption should be higer than Start caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (Question_Type() == "Smiley")
            {
                if (Faces > 5 || Faces < 0)
                {
                    Reset();
                    Make_Empty(control5);
                    MessageBox.Show("Number of Smiles should between 0-5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (Question_Type() == "Stars")
            {
                if (Stars > 10 || Faces < 0)
                {
                    Reset();
                    Make_Empty(control6);
                    MessageBox.Show("Number of stars  should be between 0-10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            
                return true;

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (check())
            {
                if (!question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))
                {
                   
                        SqlConnection connection = new SqlConnection("Data Source=A-BARAKAT;Initial Catalog=Questions;Integrated Security=True");
                        SqlCommand command = new SqlCommand();
                        try
                        {
                           
                            Update(connection, command);//insert data to a specific table 
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
                else if (isEmpty(question_box) || question_box.Text == "")
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
        private void Make_Empty()
        {
            Make_Empty(control1);
            Make_Empty(control2);
            Make_Empty(control3);
            Make_Empty(control4);
            Make_Empty(control5);
            Make_Empty(control6);

        }
        private void Make_Empty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                question_box.ForeColor = System.Drawing.Color.Gray;
                question_box.Text = "Write a new question here ...";
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

        private void Update(SqlConnection connection, SqlCommand command)
        {
            command.Connection = connection;
            command.CommandText = string.Format("update questions set question_text ='{0}' where question_order ={1}", question_box.Text, Question_order);
            Open_connection(connection);
            command.ExecuteNonQuery();
            switch (Question_Type())
            {
                case "Slider":
                    command.CommandText = string.Format("update Slider set Start_Value ={0},End_Value ={1},Start_Value_Caption ={2},End_Value_Caption ={3} where question_O={4}", control1.Text, control2.Text, control3.Text, control4.Text, Question_order);
                    Open_connection(connection);
                    command.ExecuteNonQuery();
                    break;
                case "Smiley":

                    command.CommandText = string.Format("update Smiley set Num_Faces ={0} where question_O={1}", control5.Text, Question_order);
                    Open_connection(connection);
                    command.ExecuteNonQuery();
                    break;
                case "Stars":
                    command.CommandText = string.Format("update Stars set Num_Stars ={0} where question_O={1}", control6.Text, Question_order);
                    Open_connection(connection);
                    command.ExecuteNonQuery();
                    break;
            }
        }

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
        private bool Update()
        {

            if (question_box.Text == "")
            {
                Make_Empty(question_box);
            }
            if (control1.Text == "")
            {
                Make_Empty(control1);
            }
            else
            {
                try
                {
                    if (!isEmpty(control1))
                        Slider[0] = Int32.Parse(control1.Text);//validate user input 

                }
                catch (FormatException)
                {
                    MessageBox.Show("Start value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control2.Text == "")
            {
                Make_Empty(control2);
            }
            else
            {
                try
                {
                    if (!isEmpty(control2))
                        Slider[1] = Int32.Parse(control2.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("End value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control3.Text == "")
            {
                Make_Empty(control3);
            }
            else
            {
                try
                {
                    if (!isEmpty(control3))
                        Slider[2] = Int32.Parse(control3.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("Start caption should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control4.Text == "")
            {

                Make_Empty(control4);
            }
            else
            {
                try
                {
                    if (!isEmpty(control4))
                        Slider[3] = Int32.Parse(control4.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("End caption should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            if (control5.Text == "")
            {
                Make_Empty(control5);
            }

            else
            {
                try
                {
                    if (!isEmpty(control5))
                        Faces = Int32.Parse(control5.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Number of Smiles should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (control6.Text == "")
            {
                Make_Empty(control6);
            }
            else
            {
                try
                {
                    if (!isEmpty(control6))
                        Stars = Int32.Parse(control6.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Number of Stars should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            return true;
        }






        private bool isEmpty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "Write a new question here ...")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control1))
            {
                if (control1.Text ==  Slider_default[0].ToString())
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, control2))
            {
                if (control2.Text == Slider_default[1].ToString())
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control3))
            {
                if (control3.Text == Slider_default[2].ToString())
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control4))
            {
                if (control4.Text == Slider_default[3].ToString())
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control5))
            {
                if (control5.Text == Num_Faces.ToString())
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control6))
            {
                if (control6.Text == Num_Stars.ToString())
                    return true;
                else
                    return false;
            }
            else
                return false;

        }



    }
}
