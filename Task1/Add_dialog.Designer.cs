namespace Task1
{
    partial class Add
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
            this.Radio_GroupBox = new System.Windows.Forms.GroupBox();
            this.Stars_Radio = new System.Windows.Forms.RadioButton();
            this.Smiley_Radio = new System.Windows.Forms.RadioButton();
            this.Slider_Radio = new System.Windows.Forms.RadioButton();
            this.Slider_GroupBox.SuspendLayout();
            this.Stars_GroupBox.SuspendLayout();
            this.Smiley_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown3)).BeginInit();
            this.Radio_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Radio_GroupBox
            // 
            this.Radio_GroupBox.Controls.Add(this.Stars_Radio);
            this.Radio_GroupBox.Controls.Add(this.Smiley_Radio);
            this.Radio_GroupBox.Controls.Add(this.Slider_Radio);
            this.Radio_GroupBox.Location = new System.Drawing.Point(613, 23);
            this.Radio_GroupBox.Name = "Radio_GroupBox";
            this.Radio_GroupBox.Size = new System.Drawing.Size(175, 121);
            this.Radio_GroupBox.TabIndex = 13;
            this.Radio_GroupBox.TabStop = false;
            this.Radio_GroupBox.Text = "Question type";
            // 
            // Stars_Radio
            // 
            this.Stars_Radio.AutoSize = true;
            this.Stars_Radio.Location = new System.Drawing.Point(43, 80);
            this.Stars_Radio.Name = "Stars_Radio";
            this.Stars_Radio.Size = new System.Drawing.Size(49, 17);
            this.Stars_Radio.TabIndex = 2;
            this.Stars_Radio.TabStop = true;
            this.Stars_Radio.Text = "Stars";
            this.Stars_Radio.UseVisualStyleBackColor = true;
            this.Stars_Radio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            this.Stars_Radio.Click += new System.EventHandler(this.Radio_Click);
            // 
            // Smiley_Radio
            // 
            this.Smiley_Radio.AutoSize = true;
            this.Smiley_Radio.Location = new System.Drawing.Point(43, 57);
            this.Smiley_Radio.Name = "Smiley_Radio";
            this.Smiley_Radio.Size = new System.Drawing.Size(55, 17);
            this.Smiley_Radio.TabIndex = 1;
            this.Smiley_Radio.TabStop = true;
            this.Smiley_Radio.Text = "Smiley";
            this.Smiley_Radio.UseVisualStyleBackColor = true;
            this.Smiley_Radio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            this.Smiley_Radio.Click += new System.EventHandler(this.Radio_Click);
            // 
            // Slider_Radio
            // 
            this.Slider_Radio.AutoSize = true;
            this.Slider_Radio.Location = new System.Drawing.Point(43, 34);
            this.Slider_Radio.Name = "Slider_Radio";
            this.Slider_Radio.Size = new System.Drawing.Size(51, 17);
            this.Slider_Radio.TabIndex = 0;
            this.Slider_Radio.TabStop = true;
            this.Slider_Radio.Text = "Slider";
            this.Slider_Radio.UseVisualStyleBackColor = true;
            this.Slider_Radio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            this.Slider_Radio.Click += new System.EventHandler(this.Radio_Click);
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(794, 461);
            this.Controls.Add(this.Radio_GroupBox);
            this.Name = "Add";
            this.Text = "Add a question";
            this.Controls.SetChildIndex(this.Slider_GroupBox, 0);
            this.Controls.SetChildIndex(this.question_box, 0);
            this.Controls.SetChildIndex(this.SaveButton, 0);
            this.Controls.SetChildIndex(this.CancelButton, 0);
            this.Controls.SetChildIndex(this.Stars_GroupBox, 0);
            this.Controls.SetChildIndex(this.Smiley_GroupBox, 0);
            this.Controls.SetChildIndex(this.Radio_GroupBox, 0);
            this.Slider_GroupBox.ResumeLayout(false);
            this.Slider_GroupBox.PerformLayout();
            this.Stars_GroupBox.ResumeLayout(false);
            this.Stars_GroupBox.PerformLayout();
            this.Smiley_GroupBox.ResumeLayout(false);
            this.Smiley_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown3)).EndInit();
            this.Radio_GroupBox.ResumeLayout(false);
            this.Radio_GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
        #endregion

        private System.Windows.Forms.GroupBox Radio_GroupBox;
        private System.Windows.Forms.RadioButton Stars_Radio;
        private System.Windows.Forms.RadioButton Smiley_Radio;
        private System.Windows.Forms.RadioButton Slider_Radio;
    }
}
