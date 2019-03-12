namespace Task1
{
    partial class BaseForm
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
            this.Slider_GroupBox = new System.Windows.Forms.GroupBox();
            this.QuestionOrderUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Order_Label1 = new System.Windows.Forms.Label();
            this.End_caption_Label = new System.Windows.Forms.Label();
            this.Start_caption_Label = new System.Windows.Forms.Label();
            this.End_Label = new System.Windows.Forms.Label();
            this.Start_Label = new System.Windows.Forms.Label();
            this.End_caption_textBox = new System.Windows.Forms.TextBox();
            this.Start_caption_textBox = new System.Windows.Forms.TextBox();
            this.End_textBox = new System.Windows.Forms.TextBox();
            this.Start_textBox = new System.Windows.Forms.TextBox();
            this.question_box = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.Stars_GroupBox = new System.Windows.Forms.GroupBox();
            this.QuestionOrderUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.Order_Label2 = new System.Windows.Forms.Label();
            this.StarsLabel = new System.Windows.Forms.Label();
            this.Stars_textbox = new System.Windows.Forms.TextBox();
            this.Smiley_GroupBox = new System.Windows.Forms.GroupBox();
            this.Order_Label3 = new System.Windows.Forms.Label();
            this.QuestionOrderUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.Smile_Label = new System.Windows.Forms.Label();
            this.Smile_textBox = new System.Windows.Forms.TextBox();
            this.Slider_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown1)).BeginInit();
            this.Stars_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown2)).BeginInit();
            this.Smiley_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // Slider_GroupBox
            // 
            this.Slider_GroupBox.Controls.Add(this.QuestionOrderUpDown1);
            this.Slider_GroupBox.Controls.Add(this.Order_Label1);
            this.Slider_GroupBox.Controls.Add(this.End_caption_Label);
            this.Slider_GroupBox.Controls.Add(this.Start_caption_Label);
            this.Slider_GroupBox.Controls.Add(this.End_Label);
            this.Slider_GroupBox.Controls.Add(this.Start_Label);
            this.Slider_GroupBox.Controls.Add(this.End_caption_textBox);
            this.Slider_GroupBox.Controls.Add(this.Start_caption_textBox);
            this.Slider_GroupBox.Controls.Add(this.End_textBox);
            this.Slider_GroupBox.Controls.Add(this.Start_textBox);
            this.Slider_GroupBox.Location = new System.Drawing.Point(12, 274);
            this.Slider_GroupBox.Name = "Slider_GroupBox";
            this.Slider_GroupBox.Size = new System.Drawing.Size(776, 100);
            this.Slider_GroupBox.TabIndex = 0;
            this.Slider_GroupBox.TabStop = false;
            this.Slider_GroupBox.Text = "Current values";
            this.Slider_GroupBox.Visible = false;
            // 
            // QuestionOrderUpDown1
            // 
            this.QuestionOrderUpDown1.Location = new System.Drawing.Point(79, 74);
            this.QuestionOrderUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.QuestionOrderUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.QuestionOrderUpDown1.Name = "QuestionOrderUpDown1";
            this.QuestionOrderUpDown1.ReadOnly = true;
            this.QuestionOrderUpDown1.Size = new System.Drawing.Size(82, 20);
            this.QuestionOrderUpDown1.TabIndex = 9;
            this.QuestionOrderUpDown1.ValueChanged += new System.EventHandler(this.QuestionOrderUpDown_ValueChanged);
            // 
            // Order_Label1
            // 
            this.Order_Label1.AutoSize = true;
            this.Order_Label1.Location = new System.Drawing.Point(28, 74);
            this.Order_Label1.Name = "Order_Label1";
            this.Order_Label1.Size = new System.Drawing.Size(39, 13);
            this.Order_Label1.TabIndex = 8;
            this.Order_Label1.Text = "Order :";
            // 
            // End_caption_Label
            // 
            this.End_caption_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.End_caption_Label.Location = new System.Drawing.Point(600, 39);
            this.End_caption_Label.Name = "End_caption_Label";
            this.End_caption_Label.Size = new System.Drawing.Size(73, 20);
            this.End_caption_Label.TabIndex = 7;
            this.End_caption_Label.Text = "End caption :";
            // 
            // Start_caption_Label
            // 
            this.Start_caption_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_caption_Label.Location = new System.Drawing.Point(397, 39);
            this.Start_caption_Label.Name = "Start_caption_Label";
            this.Start_caption_Label.Size = new System.Drawing.Size(73, 20);
            this.Start_caption_Label.TabIndex = 6;
            this.Start_caption_Label.Text = "Start caption :";
            // 
            // End_Label
            // 
            this.End_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.End_Label.Location = new System.Drawing.Point(225, 39);
            this.End_Label.Name = "End_Label";
            this.End_Label.Size = new System.Drawing.Size(45, 20);
            this.End_Label.TabIndex = 5;
            this.End_Label.Text = "End :";
            // 
            // Start_Label
            // 
            this.Start_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_Label.Location = new System.Drawing.Point(28, 39);
            this.Start_Label.Name = "Start_Label";
            this.Start_Label.Size = new System.Drawing.Size(45, 20);
            this.Start_Label.TabIndex = 4;
            this.Start_Label.Text = "Start :";
            // 
            // End_caption_textBox
            // 
            this.End_caption_textBox.Location = new System.Drawing.Point(679, 39);
            this.End_caption_textBox.Name = "End_caption_textBox";
            this.End_caption_textBox.Size = new System.Drawing.Size(82, 20);
            this.End_caption_textBox.TabIndex = 3;
            // 
            // Start_caption_textBox
            // 
            this.Start_caption_textBox.Location = new System.Drawing.Point(481, 39);
            this.Start_caption_textBox.Name = "Start_caption_textBox";
            this.Start_caption_textBox.Size = new System.Drawing.Size(82, 20);
            this.Start_caption_textBox.TabIndex = 2;
            // 
            // End_textBox
            // 
            this.End_textBox.Location = new System.Drawing.Point(276, 39);
            this.End_textBox.Name = "End_textBox";
            this.End_textBox.Size = new System.Drawing.Size(88, 20);
            this.End_textBox.TabIndex = 1;
            // 
            // Start_textBox
            // 
            this.Start_textBox.Location = new System.Drawing.Point(79, 39);
            this.Start_textBox.Name = "Start_textBox";
            this.Start_textBox.Size = new System.Drawing.Size(82, 20);
            this.Start_textBox.TabIndex = 0;
            // 
            // question_box
            // 
            this.question_box.Location = new System.Drawing.Point(12, 23);
            this.question_box.Multiline = true;
            this.question_box.Name = "question_box";
            this.question_box.Size = new System.Drawing.Size(563, 116);
            this.question_box.TabIndex = 10;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(610, 415);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 11;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(698, 415);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 12;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Stars_GroupBox
            // 
            this.Stars_GroupBox.Controls.Add(this.QuestionOrderUpDown2);
            this.Stars_GroupBox.Controls.Add(this.Order_Label2);
            this.Stars_GroupBox.Controls.Add(this.StarsLabel);
            this.Stars_GroupBox.Controls.Add(this.Stars_textbox);
            this.Stars_GroupBox.Location = new System.Drawing.Point(12, 274);
            this.Stars_GroupBox.Name = "Stars_GroupBox";
            this.Stars_GroupBox.Size = new System.Drawing.Size(563, 100);
            this.Stars_GroupBox.TabIndex = 13;
            this.Stars_GroupBox.TabStop = false;
            this.Stars_GroupBox.Text = "Current values";
            this.Stars_GroupBox.Visible = false;
            // 
            // QuestionOrderUpDown2
            // 
            this.QuestionOrderUpDown2.Location = new System.Drawing.Point(79, 74);
            this.QuestionOrderUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.QuestionOrderUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.QuestionOrderUpDown2.Name = "QuestionOrderUpDown2";
            this.QuestionOrderUpDown2.ReadOnly = true;
            this.QuestionOrderUpDown2.Size = new System.Drawing.Size(82, 20);
            this.QuestionOrderUpDown2.TabIndex = 14;
            this.QuestionOrderUpDown2.ValueChanged += new System.EventHandler(this.QuestionOrderUpDown_ValueChanged);
            // 
            // Order_Label2
            // 
            this.Order_Label2.AutoSize = true;
            this.Order_Label2.Location = new System.Drawing.Point(28, 74);
            this.Order_Label2.Name = "Order_Label2";
            this.Order_Label2.Size = new System.Drawing.Size(39, 13);
            this.Order_Label2.TabIndex = 13;
            this.Order_Label2.Text = "Order :";
            // 
            // StarsLabel
            // 
            this.StarsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StarsLabel.Location = new System.Drawing.Point(28, 39);
            this.StarsLabel.Name = "StarsLabel";
            this.StarsLabel.Size = new System.Drawing.Size(45, 20);
            this.StarsLabel.TabIndex = 12;
            this.StarsLabel.Text = "Stars :";
            // 
            // Stars_textbox
            // 
            this.Stars_textbox.Location = new System.Drawing.Point(79, 39);
            this.Stars_textbox.Name = "Stars_textbox";
            this.Stars_textbox.Size = new System.Drawing.Size(82, 20);
            this.Stars_textbox.TabIndex = 11;
            // 
            // Smiley_GroupBox
            // 
            this.Smiley_GroupBox.Controls.Add(this.Order_Label3);
            this.Smiley_GroupBox.Controls.Add(this.QuestionOrderUpDown3);
            this.Smiley_GroupBox.Controls.Add(this.Smile_Label);
            this.Smiley_GroupBox.Controls.Add(this.Smile_textBox);
            this.Smiley_GroupBox.Location = new System.Drawing.Point(12, 274);
            this.Smiley_GroupBox.Name = "Smiley_GroupBox";
            this.Smiley_GroupBox.Size = new System.Drawing.Size(563, 100);
            this.Smiley_GroupBox.TabIndex = 14;
            this.Smiley_GroupBox.TabStop = false;
            this.Smiley_GroupBox.Text = "Current values";
            this.Smiley_GroupBox.Visible = false;
            // 
            // Order_Label3
            // 
            this.Order_Label3.AutoSize = true;
            this.Order_Label3.Location = new System.Drawing.Point(28, 74);
            this.Order_Label3.Name = "Order_Label3";
            this.Order_Label3.Size = new System.Drawing.Size(39, 13);
            this.Order_Label3.TabIndex = 16;
            this.Order_Label3.Text = "Order :";
            // 
            // QuestionOrderUpDown3
            // 
            this.QuestionOrderUpDown3.Location = new System.Drawing.Point(79, 74);
            this.QuestionOrderUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.QuestionOrderUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.QuestionOrderUpDown3.Name = "QuestionOrderUpDown3";
            this.QuestionOrderUpDown3.ReadOnly = true;
            this.QuestionOrderUpDown3.Size = new System.Drawing.Size(82, 20);
            this.QuestionOrderUpDown3.TabIndex = 15;
            this.QuestionOrderUpDown3.ValueChanged += new System.EventHandler(this.QuestionOrderUpDown_ValueChanged);
            // 
            // Smile_Label
            // 
            this.Smile_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Smile_Label.Location = new System.Drawing.Point(28, 39);
            this.Smile_Label.Name = "Smile_Label";
            this.Smile_Label.Size = new System.Drawing.Size(45, 20);
            this.Smile_Label.TabIndex = 12;
            this.Smile_Label.Text = "Stars :";
            // 
            // Smile_textBox
            // 
            this.Smile_textBox.Location = new System.Drawing.Point(79, 39);
            this.Smile_textBox.Name = "Smile_textBox";
            this.Smile_textBox.Size = new System.Drawing.Size(82, 20);
            this.Smile_textBox.TabIndex = 11;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 461);
            this.Controls.Add(this.Smiley_GroupBox);
            this.Controls.Add(this.Stars_GroupBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.question_box);
            this.Controls.Add(this.Slider_GroupBox);
            this.MaximumSize = new System.Drawing.Size(810, 500);
            this.MinimumSize = new System.Drawing.Size(810, 500);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.Slider_GroupBox.ResumeLayout(false);
            this.Slider_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown1)).EndInit();
            this.Stars_GroupBox.ResumeLayout(false);
            this.Stars_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown2)).EndInit();
            this.Smiley_GroupBox.ResumeLayout(false);
            this.Smiley_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox Slider_GroupBox;
        protected System.Windows.Forms.Label End_caption_Label;
        protected System.Windows.Forms.Label Start_caption_Label;
        protected System.Windows.Forms.Label End_Label;
        protected System.Windows.Forms.Label Start_Label;
        protected System.Windows.Forms.TextBox End_caption_textBox;
        protected System.Windows.Forms.TextBox Start_caption_textBox;
        protected System.Windows.Forms.TextBox End_textBox;
        protected System.Windows.Forms.TextBox Start_textBox;
        protected System.Windows.Forms.TextBox question_box;
        protected System.Windows.Forms.Button SaveButton;
        protected System.Windows.Forms.Button CancelButton;
        protected System.Windows.Forms.GroupBox Stars_GroupBox;
        protected System.Windows.Forms.Label StarsLabel;
        protected System.Windows.Forms.TextBox Stars_textbox;
        protected System.Windows.Forms.GroupBox Smiley_GroupBox;
        protected System.Windows.Forms.Label Smile_Label;
        protected System.Windows.Forms.TextBox Smile_textBox;
        protected System.Windows.Forms.Label Order_Label1;
        protected System.Windows.Forms.NumericUpDown QuestionOrderUpDown2;
        protected System.Windows.Forms.Label Order_Label2;
        protected System.Windows.Forms.NumericUpDown QuestionOrderUpDown1;
        protected System.Windows.Forms.Label Order_Label3;
        protected System.Windows.Forms.NumericUpDown QuestionOrderUpDown3;
    }
}