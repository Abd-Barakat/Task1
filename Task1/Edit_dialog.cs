using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Task1
{
    public class Edit_dialog:Base
    {
        private DataTable Dt2=new DataTable();
        public readonly int[] Slider_def = new int[4];
        public readonly int N_Faces;
        public readonly int N_Stars;
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
            Save.Click += Save_Click;
        }

        public override string Question_Type()
        {
            return Dt2.Rows[0].ItemArray[2].ToString();
        }

        public override void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            Slider[0] = Slider_def[0];
            Slider[1] = Slider_def[1];
            Slider[2] = Slider_def[2];
            Slider[3] = Slider_def[3];
            Stars = Num_Stars;
            Faces = Num_Faces;
            Make_Empty();
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
                    Slider_def[0]=(Int32.Parse(control1.Text));
                    Slider_def[1]=(Int32.Parse(control2.Text));
                    Slider_def[2]=(Int32.Parse(control3.Text));
                    Slider_def[3]=(Int32.Parse(control4.Text));
                    Slider.Add(Slider_def[0]);
                    Slider.Add(Slider_def[1]);
                    Slider.Add(Slider_def[2]);
                    Slider.Add(Slider_def[3]);
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

        private void Retrive_Data()
        {
            string type = Question_Type();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
            SqlCommand command = new SqlCommand(string.Format("select * from {0} where question_O = {1}",type,Dt2.Rows[0].ItemArray[1]),connection);
            Open_connection(connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable temp =new DataTable();
            dataAdapter.Fill(temp);
            ShowGroupBox(type,temp);
        }
       
        public void ShowDialog(int order, DataTable Dt, int Row_index)
        {
            Question_order = order;          
            Dt2 = Dt.Clone();
            Dt2.Rows.Add(Dt.Rows[Row_index].ItemArray);
            initialize();
        }

        public override void Save_Click(object sender, EventArgs e)
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
      
        public override void Make_Empty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                question_box.ForeColor = System.Drawing.Color.Gray;
                question_box.Text = "Write a new question here ...";
            }
            else if (ReferenceEquals(box, control1))
            {
                control1.ForeColor = System.Drawing.Color.Gray;
                control1.Text = Slider_def[0].ToString();

            }
            else if (ReferenceEquals(box, control2))
            {
                control2.ForeColor = System.Drawing.Color.Gray;
                control2.Text = Slider_def[1].ToString();
            }
            else if (ReferenceEquals(box, control3))
            {
                control3.ForeColor = System.Drawing.Color.Gray;
                control3.Text =Slider_def[2].ToString();

            }
            else if (ReferenceEquals(box, control4))
            {
                control4.ForeColor = System.Drawing.Color.Gray;
                control4.Text = Slider_def[3].ToString();

            }

            else if (ReferenceEquals(box, control5))
            {
                control5.ForeColor = System.Drawing.Color.Gray;
                control5.Text = N_Faces.ToString();
            }

            else if (ReferenceEquals(box, control6))
            {
                control6.ForeColor = System.Drawing.Color.Gray;
                control6.Text = N_Stars.ToString();
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

        public override bool isEmpty(TextBox box)
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
                if (control1.Text ==  Slider_def[0].ToString())
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, control2))
            {
                if (control2.Text == Slider_def[1].ToString())
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control3))
            {
                if (control3.Text == Slider_def[2].ToString())
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control4))
            {
                if (control4.Text == Slider_def[3].ToString())
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control5))
            {
                if (control5.Text == N_Faces.ToString())
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control6))
            {
                if (control6.Text == N_Stars.ToString())
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

    }
}
