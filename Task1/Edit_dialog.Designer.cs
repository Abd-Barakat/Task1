namespace Task1
{
    partial class Edit_dialog
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
            SaveButton.Click += Save_Click;
            //////////////////////////////////////////////////////////////////////////////////////
            this.Slider_GroupBox.SuspendLayout();
            this.Stars_GroupBox.SuspendLayout();
            this.Smiley_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // Edit_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Name = "Edit_dialog";
            this.Text = "Edit the question";
            this.Slider_GroupBox.ResumeLayout(false);
            this.Slider_GroupBox.PerformLayout();
            this.Stars_GroupBox.ResumeLayout(false);
            this.Stars_GroupBox.PerformLayout();
            this.Smiley_GroupBox.ResumeLayout(false);
            this.Smiley_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestionOrderUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
