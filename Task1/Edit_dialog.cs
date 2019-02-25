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


        private DataTable Type_table=new DataTable();
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
            Save.Click += Save_Click;
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
                    q = new Slider(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int) (Type_table.Rows[0].ItemArray[1]), (int)(Type_table.Rows[0].ItemArray[2]), (int)(Type_table.Rows[0].ItemArray[3]), (int)(Type_table.Rows[0].ItemArray[4]));
                    control1.Text = q.Current_values().ElementAt(0).ToString();
                    control2.Text = q.Current_values().ElementAt(1).ToString();
                    control3.Text = q.Current_values().ElementAt(2).ToString();
                    control4.Text = q.Current_values().ElementAt(3).ToString();
                    break;

                case "Smiley":
                    q = new Smiley(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(Type_table.Rows[0].ItemArray[1]));
                    Default_GrouoBox2.Visible = true;
                    control5.Text = q.Current_values().ElementAt(0).ToString();
                    break;

                case "Stars":
                    q = new Stars(question_table.Rows[0].ItemArray[0].ToString(), (int)question_table.Rows[0].ItemArray[1], (int)(Type_table.Rows[0].ItemArray[1]));
                    Default_GrouoBox3.Visible = true;
                    control6.Text = q.Current_values().ElementAt(0).ToString();
                    break;

            }

        }

        private void Retrive_Data()//load saved data of selected question from database 
        {
            string type = question_table.Rows[0].ItemArray[2].ToString();//get type of the question from question table 
            ShowGroupBox(type,Type_table.Rows[0]);
        }
       
        public void ShowDialog(int order , DataRow question , DataRow type )//method used to be called in Form1 class (like constructor)
        {

            Next_order = order;

            Type_table=type.Table.Clone();
            Type_table.Rows.Add(type.ItemArray);

            question_table = question.Table.Clone();
            question_table.Rows.Add(question.ItemArray);

            Dv.DataSource =this.question_table;
            initialize();
        }

        public override void Save_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            if (check(q.Current_values()))//call check method to check inserted values before update database 
            {
                if (!question_box.Text.Any(char.IsDigit) && !isEmpty(question_box))
                {
                        try
                        {
                            DB.Update(q);
                            DialogResult result = MessageBox.Show("Done !!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            while (result != DialogResult.OK) ;//wait unitl MessageBox closes 
                            FORM.Visible = false;//hide Add dialog that will call event handler in Form1 class to print new data from database to datagridview
                        }
                        catch (Exception ex)//to catch eny problem that may occure
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FORM.Visible = false;
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

     



    }
}
