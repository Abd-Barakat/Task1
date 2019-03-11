using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class BaseForm : Form
    {

        protected Question q;
        protected int oldValue = 0;
        protected DataTable question_order;
        protected List<int> Reserved_orders = new List<int>();
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseForm"/> class.
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles the Click event of the Cancel control that close the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Determines whether the specified box is empty.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsEmpty(TextBox box)
        {
            return false;
        }
        /// <summary>
        /// Checks the specified values if they are correct then save them in question object.
        /// </summary>
        /// <param name="Values">The values.</param>
        /// <returns></returns>
        protected bool Check(List<string> Values)//this function check if entered values are correct and within thier ranges 
        {
            if (!Values_Changed(Values))//if no values entered then no need to check 
            {
                return false;
            }
            return q.Validate();

        }
        /// <summary>
        /// change question values if they are correct.
        /// </summary>
        /// <param name="Values">The values.</param>
        /// <returns></returns>
        /// <exception cref="FormatException">
        /// </exception>
        protected bool Values_Changed(List<string> Values)//check if values are changed or not 
        {
            
            try
            {
                if (!IsEmpty(question_box))
                {
                    q.Question_text = question_box.Text;//validate user input 
                    if (q.Question_text == "")
                    {
                        throw new FormatException();
                    }
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Questions  shouldn't  contain number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (q.Question_type == "Slider")
            {
                q.Question_order = (int)QuestionOrderUpDown1.Value;
                if (Start_textBox.Text != "")
                {
                    try
                    {
                        if (!IsEmpty(Start_textBox))
                        {
                            if (Start_textBox.Text.All(char.IsDigit))
                            {
                                Values[0] = Start_textBox.Text;
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
                if (End_textBox.Text != "")
                {
                    try
                    {
                        if (!IsEmpty(End_textBox))
                        {
                            if (End_textBox.Text.All(char.IsDigit))
                            {
                                Values[1] = End_textBox.Text;
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
                if (Start_caption_textBox.Text != "")
                {
                    try
                    {
                        if (!IsEmpty(Start_caption_textBox))
                        {
                            if (Start_caption_textBox.Text.Any(char.IsPunctuation))
                            {
                                throw new FormatException();
                            }
                            if (Start_caption_textBox.Text.Any(char.IsDigit))
                            {
                                throw new FormatException();
                            }
                            Values[2] = Start_caption_textBox.Text;
                        }

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Start Caption should be text only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (End_caption_textBox.Text != "")
                {
                    try
                    {
                        if (!IsEmpty(End_caption_textBox))
                        {
                            if (End_caption_textBox.Text.Any(char.IsPunctuation))
                            {
                                throw new FormatException();
                            }
                            if (End_caption_textBox.Text.Any(char.IsDigit))
                            {
                                throw new FormatException();
                            }
                            Values[3] = End_caption_textBox.Text;

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
                q.Question_order = (int)QuestionOrderUpDown2.Value;

                if (Smile_textBox.Text != "")
                {
                    try
                    {
                        if (!IsEmpty(Smile_textBox))
                        {
                            if (Smile_textBox.Text.All(char.IsDigit))
                            {
                                Values[0] = Smile_textBox.Text;
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
                    q.Set_values(Values);
                }

            }
            else if (q.Question_type == "Stars")
            {
                q.Question_order = (int)QuestionOrderUpDown3.Value;

                if (Stars_textbox.Text != "")
                {
                    try
                    {
                        if (!IsEmpty(Stars_textbox))

                        {
                            if (Stars_textbox.Text.All(char.IsDigit))
                            {
                                Values[0] = Stars_textbox.Text;
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
                    q.Set_values(Values);
                }

            }

            return true;
        }
       
        
    }
}
