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
using Questions;
using DataBase;
namespace Task1
{
    public partial class QuestionAttributes : Form
    {
        private readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };//hold tables names
        private List<string> Default_values = new List<string>();
        private int Next_ID;
        private string path = System.IO.Directory.GetParent(@"..\..\..\").FullName;
        private Question q;
        private int oldValue = 0;
        private DataTable question_order;
        private List<int> Reserved_orders = new List<int>();
        private DBclass DB = new DBclass();

        public QuestionAttributes(int id)
        {
            InitializeComponent();
            Next_ID = id;
            Default_values.Add("0");
            Default_values.Add("100");
            Default_values.Add("Not satisfied");
            Default_values.Add("Extremely statisfied");
            Default_values.Add("3");
            Default_values.Add("5");
        }
        public QuestionAttributes(DataRow[] rows)
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int Table_Index = QuestionType_ComboBox.SelectedIndex;
                if (Table_Index != 0)//if no control selected in GroupBox
                {
                    if (Change_Values())//change question's properties if they are correct
                    {
                        DB.Insert(Table_Index, q);//call Insert method in DBclass to insert the new question into database
                        this.Close();//hide Add dialog
                    }
                }
                else
                {
                    MessageBox.Show("Please select question type ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);//if no question type is selected 
                    Print_Errors("Please select question type ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Print_Errors(ex.Message, ex);
                this.Close();//hide dialog
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Handles the ValueChanged event of the QuestionOrderUpDown control, call Prev_Number_UpDown or Next_Number_UpDown depends on up or down button that pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void QuestionOrderUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (QuestionOrder_UpDown.Value > oldValue)
            {
                Next_Number_UpDown(QuestionOrder_UpDown);
                oldValue = (int)QuestionOrder_UpDown.Value;
            }
            else
            {
                Prev_Number_UpDown(QuestionOrder_UpDown);
                oldValue = (int)QuestionOrder_UpDown.Value;
            }
        }
        private void QuestionType_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relese(q);//call method release that release  q object if refere to another object 
            Next_Number_UpDown(QuestionOrder_UpDown);
            switch (QuestionType_ComboBox.SelectedIndex)
            {
                case 1:
                    q = new Slider(Next_ID)//create slider object
                    {
                        ID = Next_ID,
                    };
                    break;
                case 2:
                    q = new Smiley(Next_ID)//create smiley object
                    {
                        ID = Next_ID//sign qustion_order property to next_order field
                    };
                    break;
                case 3:
                    q = new Stars(Next_ID)//create star object               
                    {
                        ID = Next_ID//sign qustion_order property to next_order field
                    };
                    break;
            }
            Show_controls();
        }
        /// <summary>
        /// Determines whether the specified box is empty.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
        private bool IsEmpty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Shared_textbox))
            {
                switch (QuestionType_ComboBox.SelectedIndex)
                {
                    case 1:
                        if (Shared_textbox.Text == string.Format("{0}", Default_values[0]) || Shared_textbox.Text == "")
                            return true;
                        else
                            return false;
                    case 2:
                        if (Shared_textbox.Text == string.Format("{0}", Default_values[4]) || Shared_textbox.Text == "")
                            return true;
                        else
                            return false;
                    case 3:
                        if (Shared_textbox.Text == string.Format("{0}", Default_values[5]) || Shared_textbox.Text == "")
                            return true;
                        else
                            return false;
                    default:
                        return false;
                }
            }
            else if (ReferenceEquals(box, End_textBox) || End_textBox.Text == "")
            {
                if (End_textBox.Text == string.Format("{0}", Default_values[1]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_caption_textBox) || Start_caption_textBox.Text == "")
            {
                if (Start_caption_textBox.Text == string.Format("{0}", Default_values[2]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, End_caption_textBox) || End_caption_textBox.Text == "")
            {
                if (End_caption_textBox.Text == string.Format("{0}", Default_values[3]))
                    return true;
                else
                    return false;
            }
            else
                return false;
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
        /// change question values if they are correct.
        /// </summary>
        /// <param name="Values">The values.</param>
        /// <returns></returns>
        /// <exception cref="FormatException">
        /// </exception>
        private bool Change_Values()//check if values are changed or not 
        {

            try
            {
                q.Question_order = (int)QuestionOrder_UpDown.Value;
                if (!IsEmpty(question_box))
                {
                    q.Question_text = question_box.Text;//validate user input 
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
                Slider slider = (Slider)q;
                try
                {
                    if (!IsEmpty(Shared_textbox))
                    {
                        if (Shared_textbox.Text.All(char.IsDigit))
                        {
                            slider.Start = Int32.Parse(Shared_textbox.Text);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Start value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Start value should be integer number", ex);
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Start values should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Start values should be between 0-100", ex);
                    return false;
                }
                try
                {
                    if (!IsEmpty(End_textBox))
                    {
                        if (End_textBox.Text.All(char.IsDigit))
                        {
                            slider.End = Int32.Parse(End_textBox.Text);

                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("End value should be integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("End value should be integer number", ex);
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("End value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("End value should be between 0-100", ex);
                    return false;
                }
                try
                {

                    if (!IsEmpty(Start_caption_textBox))
                    {
                        slider.Start_Caption = Start_caption_textBox.Text;
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

                    if (!IsEmpty(End_caption_textBox))
                    {
                        slider.End_Caption = End_caption_textBox.Text;
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
                    return slider.Validate();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Start value should be lower than End value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Start value should be lower than End value", ex);
                    return false;
                }
            }
            else if (q.Question_type == "Smiley")
            {
                Smiley smiley = (Smiley)q;
                try
                {

                    if (!IsEmpty(Shared_textbox))
                    {
                        if (Shared_textbox.Text.All(char.IsDigit))
                        {
                            smiley.Faces = Int32.Parse(Shared_textbox.Text);
                        }
                        else
                        {
                            throw new FormatException();
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
                Stars stars = (Stars)q;
                try
                {


                    if (!IsEmpty(Shared_textbox))
                    {
                        if (Shared_textbox.Text.All(char.IsDigit))
                        {
                            stars.Star = Int32.Parse(Shared_textbox.Text);
                        }
                        else
                        {
                            throw new FormatException();
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
        /// Show related controls depending on question type.
        /// </summary>
        private void Show_controls()
        {
            Shared_textbox.Visible = true;
            int index = QuestionType_ComboBox.SelectedIndex;
            switch (index)
            {
                case 1:

                    Shared_label.Text = "Start :";
                    Shared_textbox.Text =Default_values[0];//fill textbox with default value of start 
                    End_textBox.Text = Default_values[1];//fill textbox with default value of end 
                    Start_caption_textBox.Text =Default_values[2];//fill textbox with default value of start caption 
                    End_caption_textBox.Text = Default_values[3];//fill textbox with default value of end caption

                    Slider_panel.Visible = true;
                    break;
                case 2:
                    Shared_label.Text = "Smiles :";
                    Shared_textbox.Text = Default_values[4];//fill textbox with default value of faces 
                    Slider_panel.Visible = false;
                    break;
                case 3:
                    Shared_label.Text = "Stars :";
                    Shared_textbox.Text = Default_values[5];//fill textbox with default value of stars 
                    Slider_panel.Visible = false;
                    break;
            }
        }
        /// <summary>
        /// Releses the specified question.
        /// </summary>
        /// <param name="q">The q.</param>
        private void Relese(Question q)
        {
            if (q != null)//to avoid null refrence exception
            {
                q = null;
            }
        }
        /// <summary>
        /// Prints the errors in Error.txt file.
        /// </summary>
        /// <param name="Message">The message.</param>
        protected void Print_Errors(string Message, Exception ex)
        {
            string Error_file = string.Format(@path + @"\Error.txt");
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
            string Error_file = string.Format(@path + @"\Error.txt");
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
