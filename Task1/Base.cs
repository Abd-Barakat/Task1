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
        public int Next_order;
        public Question q;
        public DBclass DB = new DBclass();
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
        public TextBox control1 = new TextBox //textbox for start value in Slider questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 0,
        };
        public TextBox control2 = new TextBox//textbox for end value in Slider questions
        {
            Location = new System.Drawing.Point(140, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 1,

        };
        public TextBox control3 = new TextBox//textbox for start value caption in Slider questions
        {
            Location = new System.Drawing.Point(275, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 2,

        };
        public TextBox control4 = new TextBox//textbox for End value caption in Slider questions
        {
            Location = new System.Drawing.Point(410, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
            TabIndex = 3,

        };
        public TextBox control5 = new TextBox//textbox for smile Faces in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        public TextBox control6 = new TextBox//textbox for stars number  in smiley questions
        {
            Location = new System.Drawing.Point(5, 20),
            Size = new System.Drawing.Size(100, 20),
            ForeColor = System.Drawing.Color.Gray,
        };
        public TextBox question_box = new TextBox//text box to write a new question within 
        {
            Location = new System.Drawing.Point(50, 98),
            TabStop = true,
            TabIndex = 0,
            Size = new System.Drawing.Size(400, 50),
            ForeColor = System.Drawing.Color.Gray,
            Multiline = true,

        };
        public Form form = new Form//make form to show controls 
        {
            Width = 600,
            Height = 400,
            MaximumSize = new System.Drawing.Size(663, 400),
            MinimumSize = new System.Drawing.Size(663, 400),

        };
        public Button Save = new Button//button to save changes to database 
        {
            Text = "Save",
            Location = new System.Drawing.Point(50, 300),
        };
        public DataGridView Dv = new DataGridView//grid view to show new data 
        {
            Size = new System.Drawing.Size(536, 50),
            Location = new System.Drawing.Point(50, 20),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToDeleteRows = false,
            AllowUserToAddRows = true,
            AllowDrop = false,
            ReadOnly = true,
            AllowUserToResizeRows = false,
            AllowUserToResizeColumns = false,
            TabStop = false,
        };
        public GroupBox Default_GrouoBox = new GroupBox //to hold a slider question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(536, 50),
            Text = "Current values",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        public GroupBox Default_GrouoBox2 = new GroupBox//to hold a smiley question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(400, 50),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };
        public GroupBox Default_GrouoBox3 = new GroupBox//to hold a star question controls
        {
            Location = new System.Drawing.Point(50, 220),
            Size = new System.Drawing.Size(400, 50),
            Text = "Current value",
            TabIndex = 2,
            TabStop = true,
            Visible = false,

        };




        public abstract void Make_Empty(TextBox box);//this function to fill each  textboxes in the dialog with default values 
        public abstract bool isEmpty(TextBox box);//this function check the passed textbox if contain default value or it's empty 
        public abstract void Save_Click(object sender, EventArgs e);//event handler for click event on save button 
        public abstract void Reset();//reset values and then call Make_Empty method to print them in textboxes




        public bool check(List<int> Values)//this function check if entered values are correct and within thier ranges 
        {
            if (!Values_Changes(Values))//if no values entered then no need to check 
            {
                return false;
            }
            return q.Validate();
        }

        public void Make_boxes_Empty()//call Make_Empty method to all textboxes
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

        public bool Correct_Format(TextBox box)
        {
            if (box.Text.Any(char.IsPunctuation))
                return false;
            else
                return true;
        }
        public bool Values_Changes(List<int> Values)//check if values are changed or not 
        {

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
                        if (q.Question_text =="")
                        {
                            MessageBox.Show("Questions  shouldn't  contain any punctuation  mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Questions  shouldn't  contain number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (q.Question_type == "Slider")
            {
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
                                
                                Values[0] = Int32.Parse(control1.Text);//validate user input 
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
                                Values[1] = Int32.Parse(control2.Text);

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
                                Values[2] = Int32.Parse(control3.Text);

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
                                Values[3] = Int32.Parse(control4.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("End caption should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
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
                            Values[0] = Int32.Parse(control5.Text);
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
                            Values[0] = Int32.Parse(control6.Text);

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

        public void TextChanged(object sender, EventArgs e)//event handler to change color or each text in textboxes in dialog and make them Empty 
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

        public void KeyDown(object sender, KeyEventArgs e)//to move to next control 
        {
            if (e.KeyCode == Keys.Enter)
            {
                Default_GrouoBox.SelectNextControl((TextBox)sender, true, false, false, true);
            }
        }
    }
}
