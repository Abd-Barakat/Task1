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
    abstract public class Base
    {
        protected int oldValue = 0;
        protected int Next_ID;
        protected DataTable question_order;
        protected List<int> Reserved_orders = new List<int>();
        protected Question q;
        protected DBclass DB = new DBclass();
        protected NumericUpDown QuestionOrderUpDown = new NumericUpDown
        {
            Size = new System.Drawing.Size(100, 20),
            Location = new System.Drawing.Point(5, 45),
            Increment = 1,
            ReadOnly = true,
            Minimum = -1,
            Maximum=1000,
        };
        public Form FORM //property to edit form 
        {
            protected set
            {
                form = value;
            }
            get
            {
                return form;
            }
        }
        protected TextBox control1 = new TextBox //textbox for start value in Slider questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 0,
        };
        protected TextBox control2 = new TextBox//textbox for end value in Slider questions
        {
            Location = new System.Drawing.Point(140, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 1,

        };
        protected TextBox control3 = new TextBox//textbox for start value caption in Slider questions
        {
            Location = new System.Drawing.Point(275, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 2,

        };
        protected TextBox control4 = new TextBox//textbox for End value caption in Slider questions
        {
            Location = new System.Drawing.Point(410, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 3,

        };
        protected TextBox control5 = new TextBox//textbox for smile Faces in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        protected TextBox control6 = new TextBox//textbox for stars number  in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        protected TextBox question_box = new TextBox//text box to write a new question within 
        {
            Location = new System.Drawing.Point(50, 27),
            TabStop = true,
            TabIndex = 0,
            Size = new System.Drawing.Size(400, 112),
            ForeColor = System.Drawing.Color.Gray,
            Multiline = true,

        };
        protected Form form = new Form//make form to show controls 
        {
            Width = 600,
            Height = 400,
            MaximumSize = new System.Drawing.Size(663, 400),
            MinimumSize = new System.Drawing.Size(663, 400),

        };
        protected Button Save = new Button//button to save changes to database 
        {
            Text = "Save",
            Location = new System.Drawing.Point(430, 300),
        };
        protected Button Cancel = new Button
        {
            Text = "Cancel",
            Location = new System.Drawing.Point(515, 300),
        };
        protected GroupBox Default_GrouoBox = new GroupBox //to hold a slider question controls
        {
            Location = new System.Drawing.Point(50, 160),
            Size = new System.Drawing.Size(536, 70),
            Text = "Current values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        protected GroupBox Default_GrouoBox2 = new GroupBox//to hold a smiley question controls
        {
            Location = new System.Drawing.Point(50, 160),
            Size = new System.Drawing.Size(400, 70),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        protected GroupBox Default_GrouoBox3 = new GroupBox//to hold a star question controls
        {
            Location = new System.Drawing.Point(50, 160),
            Size = new System.Drawing.Size(400, 70),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };




        protected abstract void Make_Empty(TextBox box);//this function to fill each  textboxes in the dialog with default values 
        protected abstract bool isEmpty(TextBox box);//this function check the passed textbox if contain default value or it's empty 
        protected abstract void Save_Click(object sender, EventArgs e);//event handler for click event on save button 
        protected abstract void Reset();//reset values and then call Make_Empty method to print them in textboxes




        protected bool check(List<string> Values)//this function check if entered values are correct and within thier ranges 
        {
            if (!Values_Changed(Values))//if no values entered then no need to check 
            {
                return false;
            }
            return q.Validate();

        }

        protected void Make_boxes_Empty()//call Make_Empty method to all textboxes
        {
            if (q.Question_type == "Slider")
            {
                Make_Empty(control1);
                Make_Empty(control2);
                Make_Empty(control3);
                Make_Empty(control4);
            }
            if (q.Question_type == "Smiley")
            {
                Make_Empty(control5);
            }
            if (q.Question_type == "Stars")
            {
                Make_Empty(control6);
            }
        }

        protected bool Correct_Format(TextBox box)
        {
            if (box.Text.Any(char.IsPunctuation))
                return false;
            else
                return true;
        }

        protected bool Values_Changed(List<string> Values)//check if values are changed or not 
        {
            q.Question_order = (int)QuestionOrderUpDown.Value;
            if (question_box.Text == "")
            {
                Make_Empty(question_box);

            }
            else
            {
                try
                {
                    if (!isEmpty(question_box))
                    {
                        q.Question_text = question_box.Text;//validate user input 
                        if (q.Question_text == "")
                        {
                            MessageBox.Show("Questions  shouldn't  contain any punctuation  mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    q.Question_text = question_box.Text;//validate user input 

                }
                catch (FormatException)
                {
                    MessageBox.Show("Questions  shouldn't  contain number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
           
            if (q.Question_type == "Slider")
            {
               
                if (control1.Text == "")
                {
                    Make_Empty(control1);
                }
                else
                {
                    try
                    {
                        if (!isEmpty(control1))
                        {
                            if (control1.Text.All(char.IsDigit))
                            {
                                Values[0] = control1.Text;
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }

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
                        {
                            if (control2.Text.All(char.IsDigit))
                            {
                                Values[1] = control2.Text;
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }

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
                        {
                            if (control3.Text.Any(char.IsPunctuation))
                            {
                                throw new FormatException();
                            }
                            if (control3.Text.Any(char.IsDigit))
                            {
                                throw new FormatException();
                            }
                            Values[2] = control3.Text;
                        }

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Start Caption should be text only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        {
                            if (control4.Text.Any(char.IsPunctuation))
                            {
                                throw new FormatException();
                            }
                            if (control4.Text.Any(char.IsDigit))
                            {
                                throw new FormatException();
                            }
                            Values[3] = control4.Text;

                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("End Caption should be text only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                q.Set_values(Values);
            }
            else if (q.Question_type == "Smiley")
            {

                if (control5.Text == "")
                {
                    Make_Empty(control5);
                }

                else
                {
                    try
                    {
                        if (!isEmpty(control5))
                        {
                            if (control5.Text.All(char.IsDigit))
                            {
                                Values[0] = control5.Text;
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Number of Smiles should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                q.Set_values(Values);

            }
            else if (q.Question_type == "Stars")
            {

                if (control6.Text == "")
                {
                    Make_Empty(control6);
                }
                else
                {
                    try
                    {
                        if (!isEmpty(control6))

                        {
                            if (control6.Text.All(char.IsDigit))
                            {
                                Values[0] = control6.Text;
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Number of Stars should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                q.Set_values(Values);

            }

            return true;
        }
        public void Next_Number_UpDown()
        {
            if (question_order == null)
            {
                question_order = DB.Orders();
                foreach (DataRow row in question_order.Rows)
                {
                    Reserved_orders.Add((int)row.ItemArray[0]);
                }
            }           
            while (  QuestionOrderUpDown.Value==-1 ||Reserved_orders.Contains((int)QuestionOrderUpDown.Value)  )
            {
                QuestionOrderUpDown.Value++;
            }
        }
        public void Prev_Number_UpDown()
        {
            if (question_order == null)
            {
                question_order = DB.Orders();
                foreach (DataRow row in question_order.Rows)
                {
                    Reserved_orders.Add((int)row.ItemArray[0]);
                }
            }
            while (Reserved_orders.Contains((int)QuestionOrderUpDown.Value)&& QuestionOrderUpDown.Value >=0)
            {
                QuestionOrderUpDown.Value--;
                if (QuestionOrderUpDown.Value ==-1)
                {
                    Next_Number_UpDown();
                }
            }
        }
        protected void TextChanged(object sender, EventArgs e)//event handler to change color or each text in textboxes in dialog and make them Empty 
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

        protected void KeyDown(object sender, KeyEventArgs e)//to move to next control 
        {
            if (e.KeyCode == Keys.Enter)
            {
                Default_GrouoBox.SelectNextControl((TextBox)sender, true, false, false, true);
            }
        }
    }
}
