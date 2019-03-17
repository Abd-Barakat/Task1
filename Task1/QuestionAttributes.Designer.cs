namespace Task1
{
    partial class QuestionAttributes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.question_label = new System.Windows.Forms.Label();
            this.question_box = new System.Windows.Forms.TextBox();
            this.Order_label = new System.Windows.Forms.Label();
            this.QuestionType_ComboBox = new System.Windows.Forms.ComboBox();
            this.QuestionType_label = new System.Windows.Forms.Label();
            this.End_label = new System.Windows.Forms.Label();
            this.Shared_label = new System.Windows.Forms.Label();
            this.Start_Caption_Label = new System.Windows.Forms.Label();
            this.End_Caption_label = new System.Windows.Forms.Label();
            this.QuestionOrder_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Shared_textbox = new System.Windows.Forms.TextBox();
            this.End_textBox = new System.Windows.Forms.TextBox();
            this.Start_caption_textBox = new System.Windows.Forms.TextBox();
            this.End_caption_textBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.Slider_panel = new System.Windows.Forms.Panel();
            this.QuestionTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrder_UpDown)).BeginInit();
            this.Slider_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // question_label
            // 
            this.question_label.AutoSize = true;
            this.question_label.Location = new System.Drawing.Point(32, 38);
            this.question_label.Name = "question_label";
            this.question_label.Size = new System.Drawing.Size(34, 13);
            this.question_label.TabIndex = 0;
            this.question_label.Text = "Text :";
            // 
            // question_box
            // 
            this.question_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.question_box.Location = new System.Drawing.Point(115, 35);
            this.question_box.Multiline = true;
            this.question_box.Name = "question_box";
            this.question_box.Size = new System.Drawing.Size(248, 47);
            this.question_box.TabIndex = 0;
            this.QuestionTip.SetToolTip(this.question_box, "Write question here ...");
            // 
            // Order_label
            // 
            this.Order_label.AutoSize = true;
            this.Order_label.Location = new System.Drawing.Point(32, 93);
            this.Order_label.Name = "Order_label";
            this.Order_label.Size = new System.Drawing.Size(39, 13);
            this.Order_label.TabIndex = 2;
            this.Order_label.Text = "Order :";
            // 
            // QuestionType_ComboBox
            // 
            this.QuestionType_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuestionType_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QuestionType_ComboBox.FormattingEnabled = true;
            this.QuestionType_ComboBox.Items.AddRange(new object[] {
            "Slider",
            "Smiley",
            "Stars"});
            this.QuestionType_ComboBox.Location = new System.Drawing.Point(115, 128);
            this.QuestionType_ComboBox.Name = "QuestionType_ComboBox";
            this.QuestionType_ComboBox.Size = new System.Drawing.Size(248, 21);
            this.QuestionType_ComboBox.TabIndex = 2;
            this.QuestionTip.SetToolTip(this.QuestionType_ComboBox, "Select question type");
            this.QuestionType_ComboBox.SelectedIndexChanged += new System.EventHandler(this.QuestionType_ComboBox_SelectedIndexChanged);
            // 
            // QuestionType_label
            // 
            this.QuestionType_label.AutoSize = true;
            this.QuestionType_label.Location = new System.Drawing.Point(32, 128);
            this.QuestionType_label.Name = "QuestionType_label";
            this.QuestionType_label.Size = new System.Drawing.Size(37, 13);
            this.QuestionType_label.TabIndex = 4;
            this.QuestionType_label.Text = "Type :";
            // 
            // End_label
            // 
            this.End_label.AutoSize = true;
            this.End_label.Location = new System.Drawing.Point(7, 13);
            this.End_label.Name = "End_label";
            this.End_label.Size = new System.Drawing.Size(32, 13);
            this.End_label.TabIndex = 3;
            this.End_label.Text = "End :";
            // 
            // Shared_label
            // 
            this.Shared_label.AutoSize = true;
            this.Shared_label.Location = new System.Drawing.Point(32, 163);
            this.Shared_label.Name = "Shared_label";
            this.Shared_label.Size = new System.Drawing.Size(38, 13);
            this.Shared_label.TabIndex = 6;
            this.Shared_label.Text = "Start  :";
            this.Shared_label.Visible = false;
            // 
            // Start_Caption_Label
            // 
            this.Start_Caption_Label.AutoSize = true;
            this.Start_Caption_Label.Location = new System.Drawing.Point(7, 48);
            this.Start_Caption_Label.Name = "Start_Caption_Label";
            this.Start_Caption_Label.Size = new System.Drawing.Size(74, 13);
            this.Start_Caption_Label.TabIndex = 7;
            this.Start_Caption_Label.Text = "Start Caption :";
            // 
            // End_Caption_label
            // 
            this.End_Caption_label.AutoSize = true;
            this.End_Caption_label.Location = new System.Drawing.Point(7, 83);
            this.End_Caption_label.Name = "End_Caption_label";
            this.End_Caption_label.Size = new System.Drawing.Size(71, 13);
            this.End_Caption_label.TabIndex = 8;
            this.End_Caption_label.Text = "End Caption :";
            // 
            // QuestionOrder_UpDown
            // 
            this.QuestionOrder_UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuestionOrder_UpDown.Location = new System.Drawing.Point(115, 94);
            this.QuestionOrder_UpDown.Name = "QuestionOrder_UpDown";
            this.QuestionOrder_UpDown.Size = new System.Drawing.Size(248, 20);
            this.QuestionOrder_UpDown.TabIndex = 1;
            this.QuestionTip.SetToolTip(this.QuestionOrder_UpDown, "Select question order ");
            this.QuestionOrder_UpDown.ValueChanged += new System.EventHandler(this.QuestionOrderUpDown_ValueChanged);
            // 
            // Shared_textbox
            // 
            this.Shared_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Shared_textbox.Location = new System.Drawing.Point(115, 163);
            this.Shared_textbox.Name = "Shared_textbox";
            this.Shared_textbox.Size = new System.Drawing.Size(248, 20);
            this.Shared_textbox.TabIndex = 3;
            this.Shared_textbox.Visible = false;
            // 
            // End_textBox
            // 
            this.End_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.End_textBox.Location = new System.Drawing.Point(90, 12);
            this.End_textBox.Name = "End_textBox";
            this.End_textBox.Size = new System.Drawing.Size(248, 20);
            this.End_textBox.TabIndex = 0;
            this.QuestionTip.SetToolTip(this.End_textBox, "Enter End value");
            // 
            // Start_caption_textBox
            // 
            this.Start_caption_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Start_caption_textBox.Location = new System.Drawing.Point(90, 46);
            this.Start_caption_textBox.Name = "Start_caption_textBox";
            this.Start_caption_textBox.Size = new System.Drawing.Size(248, 20);
            this.Start_caption_textBox.TabIndex = 1;
            this.QuestionTip.SetToolTip(this.Start_caption_textBox, "Write start caption");
            // 
            // End_caption_textBox
            // 
            this.End_caption_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.End_caption_textBox.Location = new System.Drawing.Point(90, 80);
            this.End_caption_textBox.Name = "End_caption_textBox";
            this.End_caption_textBox.Size = new System.Drawing.Size(248, 20);
            this.End_caption_textBox.TabIndex = 2;
            this.QuestionTip.SetToolTip(this.End_caption_textBox, "Write end caption");
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(238, 311);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.QuestionTip.SetToolTip(this.SaveButton, "Save question");
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(319, 311);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.QuestionTip.SetToolTip(this.CancelButton, "Cancel ");
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Slider_panel
            // 
            this.Slider_panel.Controls.Add(this.End_caption_textBox);
            this.Slider_panel.Controls.Add(this.Start_caption_textBox);
            this.Slider_panel.Controls.Add(this.End_textBox);
            this.Slider_panel.Controls.Add(this.End_Caption_label);
            this.Slider_panel.Controls.Add(this.Start_Caption_Label);
            this.Slider_panel.Controls.Add(this.End_label);
            this.Slider_panel.Location = new System.Drawing.Point(25, 185);
            this.Slider_panel.Name = "Slider_panel";
            this.Slider_panel.Size = new System.Drawing.Size(359, 113);
            this.Slider_panel.TabIndex = 4;
            this.Slider_panel.Visible = false;
            // 
            // QuestionAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 340);
            this.Controls.Add(this.Slider_panel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Shared_textbox);
            this.Controls.Add(this.QuestionOrder_UpDown);
            this.Controls.Add(this.Shared_label);
            this.Controls.Add(this.QuestionType_label);
            this.Controls.Add(this.QuestionType_ComboBox);
            this.Controls.Add(this.Order_label);
            this.Controls.Add(this.question_box);
            this.Controls.Add(this.question_label);
            this.MinimumSize = new System.Drawing.Size(367, 379);
            this.Name = "QuestionAttributes";
            this.Text = "Question Properties";
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrder_UpDown)).EndInit();
            this.Slider_panel.ResumeLayout(false);
            this.Slider_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label question_label;
        private System.Windows.Forms.TextBox question_box;
        private System.Windows.Forms.Label Order_label;
        private System.Windows.Forms.ComboBox QuestionType_ComboBox;
        private System.Windows.Forms.Label QuestionType_label;
        private System.Windows.Forms.Label End_label;
        private System.Windows.Forms.Label Shared_label;
        private System.Windows.Forms.Label Start_Caption_Label;
        private System.Windows.Forms.Label End_Caption_label;
        private System.Windows.Forms.NumericUpDown QuestionOrder_UpDown;
        private System.Windows.Forms.TextBox Shared_textbox;
        private System.Windows.Forms.TextBox End_textBox;
        private System.Windows.Forms.TextBox Start_caption_textBox;
        private System.Windows.Forms.TextBox End_caption_textBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Panel Slider_panel;
        private System.Windows.Forms.ToolTip QuestionTip;
    }
}