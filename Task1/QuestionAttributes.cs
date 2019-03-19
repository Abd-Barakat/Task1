using DataBase;
using Questions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace Task1
{
    public partial class QuestionAttributes : Form
    {
        private string path = System.IO.Directory.GetParent(@"..\..\..\").FullName;
        private Question question;
        private bool IsEdit;
        private int oldValue = 0;
        private int Q_order;
        private List<int> Reserved_orders;
        private DBclass DataBase;
        /// <summary>
        /// initialize Add dialog's variables.
        /// </summary>
        /// <param name="id"></param>
        public QuestionAttributes(DBclass database)
        {
            InitializeComponent();
            DataBase = database;
            IsEdit = false;
            Next_Order(QuestionOrder_UpDown);
            SaveButton.Click += Save_new_question_Click;
        }
        /// <summary>
        /// initialize Edit dialog's variables.
        /// </summary>
        /// <param name="rows"></param>
        public QuestionAttributes(Question question, DBclass database)
        {

            InitializeComponent();
            DataBase = database;
            this.question = question;
            IsEdit = true;
            QuestionOrder_UpDown.Value = question.Question_order;
            SaveButton.Click += Save_Updates_Click;
            switch (question.Question_type)
            {
                case "Slider":
                    QuestionType_ComboBox.SelectedIndex = 0;
                    QuestionType_ComboBox.Enabled = false;
                    break;
                case "Smiley":
                    QuestionType_ComboBox.SelectedIndex = 1;
                    QuestionType_ComboBox.Enabled = false;
                    break;
                case "Stars":
                    QuestionType_ComboBox.SelectedIndex = 2;
                    QuestionType_ComboBox.Enabled = false;
                    break;
            }
        }
        /// <summary>
        /// Handles the Click event of the Save control that save updates to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Save_Updates_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            try
            {
                if (Change_Values())//call check method to check inserted values before update database 
                {
                    DataBase.Update(question);//upate database with new edited question
                    this.Close();
                }
            }
            catch (Exception ex)//to catch eny problem that may occure
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//show any error could occure
                Print_Errors(ex.Message, ex);
                this.Close();
            }
        }
        /// <summary>
        /// Handles the Click event of the Save control that save new question to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Save_new_question_Click(object sender, EventArgs e)
        {
            try
            {
                int Table_Index = QuestionType_ComboBox.SelectedIndex;
                if (Table_Index >= 0)//if no control selected in GroupBox
                {
                    if (Change_Values())//change question's properties if they are correct
                    {
                        DataBase.Insert(question);//call Insert method in DBclass to insert the new question into database
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
        /// <summary>
        /// Close Question attributes dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                Next_Order(QuestionOrder_UpDown);
                oldValue = (int)QuestionOrder_UpDown.Value;
            }
            else
            {
                Prev_Order(QuestionOrder_UpDown);
                oldValue = (int)QuestionOrder_UpDown.Value;
            }
        }
        /// <summary>
        /// Event hander for question type selection
        /// Create question object depends on selection of question type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionType_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relese(question);//call method release that release  q object if refere to another object 
            Next_Order(QuestionOrder_UpDown);//initialze QuestionOrder compobox  to next question order
            switch (QuestionType_ComboBox.SelectedIndex)
            {
                case 0:
                    question = IsEdit? question : new Slider("", Q_order);//create slider object
                    break;
                case 1:
                    question = IsEdit? question : new Smiley("", Q_order);//create smiley object
                    break;
                case 2:
                    question = IsEdit? question : new Stars("", Q_order);//create star object               
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
        private bool IsEmpty(object box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Shared_numeric))
            {
                switch (question.Question_type)
                {
                    case "Slider":
                        Slider slider = (Slider)question;
                        if (Shared_numeric.Value == Int32.Parse(slider.Slider_default[0]))
                            return true;
                        else
                            return false;
                    case "Smiley":
                        Smiley smiley = (Smiley)question;
                        if (Shared_numeric.Value ==smiley.Smiles_default)
                            return true;
                        else
                            return false;
                    case "Stars":
                        Stars stars = (Stars)question;
                        if (Shared_numeric.Value == stars.Stars_default)
                            return true;
                        else
                            return false;
                    default:
                        return false;
                }

            }
            else if (ReferenceEquals(box, End_numeric))
            {
                Slider slider = (Slider)question;
                if (End_numeric.Value == Int32.Parse(slider.Slider_default[1]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_caption_textBox) || Start_caption_textBox.Text == "")
            {
                Slider slider = (Slider)question;
                if (Start_caption_textBox.Text ==  slider.Slider_default[2])
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, End_caption_textBox) || End_caption_textBox.Text == "")
            {
                Slider slider = (Slider)question;
                if (End_caption_textBox.Text == slider.Slider_default[3])
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
        protected void Next_Order(NumericUpDown QuestionOrderUpDown)
        {
            if (Reserved_orders == null)
            {
                Reserved_orders = DataBase.Orders();
            }
            if (IsEdit)
            {
                Reserved_orders.Remove(question.Question_order);
            }
            while (Reserved_orders.Contains((int)QuestionOrderUpDown.Value))
            {
                QuestionOrderUpDown.Value++;
            }
            Q_order = (int)QuestionOrderUpDown.Value;
        }

        /// <summary>
        /// decrement question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        private void Prev_Order(NumericUpDown QuestionOrderUpDown)
        {
            int Order = (int)QuestionOrderUpDown.Value;
            if (Reserved_orders == null)
            {
                Reserved_orders = DataBase.Orders();
            }
            if (IsEdit)
            {
                Reserved_orders.Remove(question.Question_order);
            }
            while (Reserved_orders.Contains(Order) && Order >= 0)
            {
                Order--;
                if (Order == -1)
                {
                    Next_Order(QuestionOrderUpDown);
                    return;
                }
            }
            QuestionOrderUpDown.Value = Order;
            Q_order = (int)QuestionOrderUpDown.Value;
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
                question.Question_order = Q_order;
                if (!IsEmpty(question_box))
                {
                    question.Question_text = question_box.Text;//validate user input 
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
            if (question.Question_type == "Slider")
            {
                Slider slider = (Slider)question;
                try
                {
                    if (!IsEmpty(Shared_numeric))
                    {
                        slider.Start = Int32.Parse(Shared_numeric.Text);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Start values should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Start values should be between 0-100", ex);
                    return false;
                }
                try
                {
                    if (!IsEmpty(End_numeric))
                    {

                        slider.End = Int32.Parse(End_numeric.Text);


                    }
                    if (!IsEmpty(End_caption_textBox))
                    {
                        slider.End_Caption = End_caption_textBox.Text;
                    }
                    if (!IsEmpty(Start_caption_textBox))
                    {
                        slider.Start_Caption = Start_caption_textBox.Text;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("End value should be between 0-100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("End value should be between 0-100", ex);
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
            else if (question.Question_type == "Smiley")
            {
                Smiley smiley = (Smiley)question;
                try
                {
                    if (!IsEmpty(Shared_numeric))
                    {
                        smiley.Smiles = Int32.Parse(Shared_numeric.Text);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Number of faces should be between 2-5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Number of faces should be between 2-5", ex);
                    return false;
                }
            }
            else if (question.Question_type == "Stars")
            {
                Stars stars = (Stars)question;
                try
                {
                    if (!IsEmpty(Shared_numeric))
                    {
                        stars.Star = Int32.Parse(Shared_numeric.Text);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Number of stars  should be between 0-10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Print_Errors("Number of stars  should be between 0-10", ex);
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
            question_box.Text = IsEdit == true ? question.Question_text : question_box.Text;
            Shared_numeric.Visible = true;
            int index = QuestionType_ComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    Slider slider = (Slider)question;
                    Shared_label.Text = "Start :";
                    Shared_label.Visible = true;
                    QuestionTip.SetToolTip(Shared_numeric, "Enter start value");

                    Shared_numeric.Value = Int32.Parse(slider.Slider_default[0]);//fill textbox with default value of start 
                    End_numeric.Value = Int32.Parse(slider.Slider_default[1]);//fill textbox with default value of end 
                    Start_caption_textBox.Text = slider.Slider_default[2];//fill textbox with default value of start caption 
                    End_caption_textBox.Text =slider.Slider_default[3];//fill textbox with default value of end caption

                    Slider_panel.Visible = true;
                    break;
                case 1:
                    Smiley smiley = (Smiley)question;
                    Shared_label.Text = "Smiles :";
                    Shared_label.Visible = true;
                    QuestionTip.SetToolTip(Shared_numeric, "Enter number of smiles");

                    Shared_numeric.Value = smiley.Smiles_default;//fill textbox with default value of faces 
                    Slider_panel.Visible = false;
                    break;
                case 2:
                    Stars stars = (Stars)question;
                    Shared_label.Text = "Stars :";
                    Shared_label.Visible = true;
                    QuestionTip.SetToolTip(Shared_numeric, "Enter number of stars");

                    Shared_numeric.Value = stars.Stars_default;//fill textbox with default value of stars 
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
            // if (q != null)//to avoid null refrence exception
            //  {
            q = null;
            // }
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

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }
    }
}