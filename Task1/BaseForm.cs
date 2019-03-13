using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Task1
{
    public partial class BaseForm : Form
    {
        protected string Path;
        protected Question q;
        protected DBclass DB;
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
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Stars_textbox))
            {
                if (Stars_textbox.Text == string.Format("{0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;

            }
            else if (ReferenceEquals(box, End_textBox))
            {
                if (End_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(1)))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_caption_textBox))
            {
                if (Start_caption_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(2)))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, End_caption_textBox))
            {
                if (End_caption_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(3)))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, Smile_textBox))
            {
                if (Smile_textBox.Text == string.Format("{0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }

            else if (ReferenceEquals(box, Stars_textbox))
            {
                if (Stars_textbox.Text == string.Format("{0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// Checks the specified values if they are correct then save them in question object.
        /// </summary>
        /// <param name="Values">The values.</param>
        /// <returns></returns>
        protected bool Check(List<string> Values)//this function check if entered values are correct and within thier ranges 
        {
            try
            {
                if (!Values_Changed(Values))//if no values entered then no need to check 
                {
                    return false;
                }
                q.Validate();
                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Start value should be lower than End value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors("Start value should be lower than End value", ex);
                return false;
            }
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
                else
                {
                    MessageBox.Show("Please write a question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Please write a question");
                    return false;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Question should not contain a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors("Question should not contain a number", ex);
                return false;
            }
            if (q.Question_type == "Slider")
            {
                try
                {
                    q.Question_order = (int)QuestionOrderUpDown1.Value;
                    if (Start_textBox.Text != "")
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
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Start value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Start value should be integer number", ex);
                    return false;
                }
                try
                {
                    if (End_textBox.Text != "")
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
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("End value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("End value should be integer number", ex);
                    return false;
                }
                try
                {
                    if (Start_caption_textBox.Text != "")
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
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Start Caption should be text only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Start Caption should be text only", ex);
                    return false;
                }
                try
                {
                    if (End_caption_textBox.Text != "")
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
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("End Caption should be text only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("End Caption should be text only", ex);
                    return false;
                }
                try
                {
                    q.Set_values(Values);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Both of Start and End values should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Both of Start and End values should be between 0-100", ex);
                    return false;
                }
            }
            else if (q.Question_type == "Smiley")
            {
                try
                {
                    q.Question_order = (int)QuestionOrderUpDown3.Value;
                    if (Smile_textBox.Text != "")
                    {
                        if (!IsEmpty(Smile_textBox))
                        {
                            if (Smile_textBox.Text.All(char.IsDigit))
                            {
                                Values[0] = Smile_textBox.Text;
                                q.Set_values(Values);
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Number of faces should be between 2-5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Number of faces should be between 2-5", ex);
                    return false;
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Number of Smiles should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Number of Smiles should be integer number", ex);
                    return false;
                }
            }
            else if (q.Question_type == "Stars")
            {
                try
                {
                    q.Question_order = (int)QuestionOrderUpDown2.Value;
                    if (Stars_textbox.Text != "")
                    {
                        if (!IsEmpty(Stars_textbox))
                        {
                            if (Stars_textbox.Text.All(char.IsDigit))
                            {
                                Values[0] = Stars_textbox.Text;
                                q.Set_values(Values);
                            }
                            else
                            {
                                throw new FormatException();
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Number of stars  should be between 0-10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Number of stars  should be between 0-10", ex);
                    return false;
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Number of Stars should be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Number of Stars should be integer", ex);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Handles the ValueChanged event of the QuestionOrderUpDown control, call Prev_Number_UpDown or Next_Number_UpDown depends on up or down button that pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void QuestionOrderUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, QuestionOrderUpDown1))
            {
                if (QuestionOrderUpDown1.Value > oldValue)
                {
                    Next_Number_UpDown(QuestionOrderUpDown1);
                    oldValue = (int)QuestionOrderUpDown1.Value;
                }
                else
                {
                    Prev_Number_UpDown(QuestionOrderUpDown1);
                    oldValue = (int)QuestionOrderUpDown1.Value;
                }
            }
            else if (ReferenceEquals(sender, QuestionOrderUpDown2))
            {
                if (QuestionOrderUpDown2.Value > oldValue)
                {
                    Next_Number_UpDown(QuestionOrderUpDown2);
                    oldValue = (int)QuestionOrderUpDown2.Value;
                }
                else
                {
                    Prev_Number_UpDown(QuestionOrderUpDown2);
                    oldValue = (int)QuestionOrderUpDown2.Value;
                }
            }
            else if (ReferenceEquals(sender, QuestionOrderUpDown3))
            {
                if (QuestionOrderUpDown3.Value > oldValue)
                {
                    Next_Number_UpDown(QuestionOrderUpDown3);
                    oldValue = (int)QuestionOrderUpDown3.Value;
                }
                else
                {
                    Prev_Number_UpDown(QuestionOrderUpDown3);
                    oldValue = (int)QuestionOrderUpDown3.Value;
                }
            }

        }
        /// <summary>
        /// increment question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        protected void Next_Number_UpDown(NumericUpDown QuestionOrderUpDown)
        {
            if (question_order == null)
            {
                question_order = DB.Orders();
                foreach (DataRow row in question_order.Rows)
                {
                    Reserved_orders.Add((int)row.ItemArray[0]);
                }
            }
            while (Reserved_orders.Contains((int)QuestionOrderUpDown.Value))
            {
                QuestionOrderUpDown.Value++;
            }
            q.Question_order = (int)QuestionOrderUpDown.Value;
        }

        /// <summary>
        /// decrement question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        private void Prev_Number_UpDown(NumericUpDown QuestionOrderUpDown)
        {
            int Order = (int)QuestionOrderUpDown.Value;
            if (question_order == null)
            {
                question_order = DB.Orders();
                foreach (DataRow row in question_order.Rows)
                {
                    Reserved_orders.Add((int)row.ItemArray[0]);
                }
            }
            while (Reserved_orders.Contains(Order) && Order >= 0)
            {
                Order--;
                if (Order == -1)
                {
                    Next_Number_UpDown(QuestionOrderUpDown);
                    return;
                }
            }
            QuestionOrderUpDown.Value = Order;
            q.Question_order = (int)QuestionOrderUpDown.Value;
        }
        /// <summary>
        /// Prints the errors in Error.txt file.
        /// </summary>
        /// <param name="Message">The message.</param>
        protected void Print_Errors(string Message, Exception ex)
        {
            string Error_file = string.Format(@Path + @"\Error.txt");
            using (StreamWriter stream = new StreamWriter(@Error_file, true))//save errors in Error.txt file
            {
                if (stream != null)
                {
                    stream.WriteLine("-------------------------------------------------------------------\n");
                    stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                    while (ex != null)
                    {
                        stream.WriteLine("Message     : " + Message);
                        stream.WriteLine("Stack trace : " + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
                else
                {
                    MessageBox.Show("File not found !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Prints the errors in Error.txt file.
        /// </summary>
        /// <param name="Message">The message.</param>
        protected void Print_Errors(string Message)
        {
            string Error_file = string.Format(@Path + @"\Error.txt");
            using (StreamWriter stream = new StreamWriter(@Error_file, true))//save errors in Error.txt file
            {
                if (stream != null)
                {
                    stream.WriteLine("-------------------------------------------------------------------\n");
                    stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                    stream.WriteLine("Message :\n" + Message);
                }
                else
                {
                    MessageBox.Show("File not found !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
