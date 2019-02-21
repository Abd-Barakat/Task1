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


        private DataTable Type=new DataTable();
        private DataTable question_table = new DataTable();
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
          //  Dv.DataSource = Dt2.DefaultView.ToTable(false, "question_text");
            Save.Click += Save_Click;
        }

        public override string Question_Type()
        {
            return question_table.Rows[0].ItemArray[2].ToString();
        }

        public override void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            q.Reset_values();
            Make_boxes_Empty();
        }

        private void ShowGroupBox(string type,DataRow dataRow)//this method show a specific Groupbox depends on type of question then save old data (before editing ) in variables 
        {
            switch (type)
            {
                case "Slider":
                    Default_GrouoBox.Visible = true;
                    control1.Text = dataRow.ItemArray[1].ToString();
                    control2.Text = dataRow.ItemArray[2].ToString();
                    control3.Text = dataRow.ItemArray[3].ToString();
                    control4.Text = dataRow.ItemArray[4].ToString();
                    q = new Slider(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], Int32.Parse(control1.Text), Int32.Parse(control2.Text), Int32.Parse(control3.Text), Int32.Parse(control4.Text));
                    break;

                case "Smiley":
                    Default_GrouoBox2.Visible = true;
                    control5.Text = dataRow.ItemArray[1].ToString();
                    q = new Smiley(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], Int32.Parse(control5.Text));
                  
                        break;
                case "Stars":
                    Default_GrouoBox3.Visible = true;
                    control6.Text = dataRow.ItemArray[1].ToString();
                    q = new Smiley(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], Int32.Parse(control6.Text));
                    
                    break;
            }

        }

        private void Retrive_Data()//load saved data of selected question from database 
        {
            string type = Question_Type();
            ShowGroupBox(type,Type.Rows[0]);
        }
       
        public void ShowDialog(int order , DataRow question , DataRow type )//method used to be called in Form1 class (like constructor)
        {
            Question_order = order;
            Type=type.Table.Clone();
            Type.Rows.Add(type.ItemArray);
            this.question_table = question.Table.Clone();
            this.question_table.Rows.Add(question.ItemArray);
            Dv.DataSource =this.question_table;
            initialize();
        }

        public override void Save_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            if (check(q.Current_values()))//call check method to check inserted values before update database 
            {
                if (!question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))
                {
                   
                        SqlConnection connection = new SqlConnection();
                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                        SqlCommand command = new SqlCommand();
                        try
                        {
                           
                            Update(connection, command);//Update data to a database
                            DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            while (result != DialogResult.OK) ;//wait unitl MessageBox closes 
                            FORM.Visible = false;//hide Add dialog that will call event handler in Form1 class to print new data from database to datagridview
                        }
                        catch (Exception ex)//to catch eny problem that may occure
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FORM.Visible = false;
                        }
                        finally
                        {
                            if (connection != null)//check to avoid null reference exception
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
                control1.Text = q.Default_values().ElementAt(0).ToString();

            }
            else if (ReferenceEquals(box, control2))
            {
                control2.ForeColor = System.Drawing.Color.Gray;
                control2.Text = q.Default_values().ElementAt(1).ToString();
            }
            else if (ReferenceEquals(box, control3))
            {
                control3.ForeColor = System.Drawing.Color.Gray;
                control3.Text = q.Default_values().ElementAt(2).ToString();

            }
            else if (ReferenceEquals(box, control4))
            {
                control4.ForeColor = System.Drawing.Color.Gray;
                control4.Text = q.Default_values().ElementAt(3).ToString();

            }

            else if (ReferenceEquals(box, control5))
            {
                control5.ForeColor = System.Drawing.Color.Gray;
                control5.Text = q.Default_values().ElementAt(0).ToString();
            }

            else if (ReferenceEquals(box, control6))
            {
                control6.ForeColor = System.Drawing.Color.Gray;
                control6.Text = q.Default_values().ElementAt(0).ToString();
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
                if (control1.Text == q.Default_values().ElementAt(0).ToString())
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, control2))
            {
                if (control2.Text == q.Default_values().ElementAt(1).ToString())
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control3))
            {
                if (control3.Text == q.Default_values().ElementAt(2).ToString())
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, control4))
            {
                if (control4.Text == q.Default_values().ElementAt(3).ToString())
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control5))
            {
                if (control5.Text == q.Default_values().ElementAt(0).ToString())
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, control6))
            {
                if (control6.Text == q.Default_values().ElementAt(0).ToString())
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        private void Update(SqlConnection connection, SqlCommand command) //update database with the new values 
        {
            command.Connection = connection;
            command.CommandText = string.Format("update questions set question_text ='{0}' where question_order ={1}", question_box.Text, Question_order);
            Open_connection(connection);
            command.ExecuteNonQuery();
            switch (Question_Type())
            {
                case "Slider":
                    command.CommandText = string.Format("update Slider set Start_Value ={0},End_Value ={1},Start_Value_Caption ={2},End_Value_Caption ={3} where question_order={4}", control1.Text, control2.Text, control3.Text, control4.Text, Question_order);
                    Open_connection(connection);
                    command.ExecuteNonQuery();
                    break;
                case "Smiley":

                    command.CommandText = string.Format("update Smiley set Num_Faces ={0} where question_order={1}", control5.Text, Question_order);
                    Open_connection(connection);
                    command.ExecuteNonQuery();
                    break;
                case "Stars":
                    command.CommandText = string.Format("update Stars set Num_Stars ={0} where question_order={1}", control6.Text, Question_order);
                    Open_connection(connection);
                    command.ExecuteNonQuery();
                    break;
            }
        }


    }
}
