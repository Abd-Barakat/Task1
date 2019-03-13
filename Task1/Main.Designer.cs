namespace Task1
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Add_Button = new System.Windows.Forms.Button();
            this.Delete_Button = new System.Windows.Forms.Button();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.DatabaseListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.Add_Button);
            this.panel1.Controls.Add(this.Delete_Button);
            this.panel1.Controls.Add(this.Edit_Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(583, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 442);
            this.panel1.TabIndex = 0;
            // 
            // Add_Button
            // 
            this.Add_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Add_Button.Location = new System.Drawing.Point(3, 28);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(91, 23);
            this.Add_Button.TabIndex = 0;
            this.Add_Button.Text = "Add";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // Delete_Button
            // 
            this.Delete_Button.Location = new System.Drawing.Point(3, 86);
            this.Delete_Button.Name = "Delete_Button";
            this.Delete_Button.Size = new System.Drawing.Size(91, 23);
            this.Delete_Button.TabIndex = 2;
            this.Delete_Button.Text = "Delete";
            this.Delete_Button.UseVisualStyleBackColor = true;
            this.Delete_Button.Click += new System.EventHandler(this.Delete_Button_Click);
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(3, 57);
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(91, 23);
            this.Edit_Button.TabIndex = 1;
            this.Edit_Button.Text = "Edit";
            this.Edit_Button.UseVisualStyleBackColor = true;
            this.Edit_Button.Click += new System.EventHandler(this.Edit_Button_Click);
            // 
            // DatabaseListBox
            // 
            this.DatabaseListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DatabaseListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatabaseListBox.FormattingEnabled = true;
            this.DatabaseListBox.HorizontalScrollbar = true;
            this.DatabaseListBox.IntegralHeight = false;
            this.DatabaseListBox.ItemHeight = 16;
            this.DatabaseListBox.Location = new System.Drawing.Point(12, 28);
            this.DatabaseListBox.MinimumSize = new System.Drawing.Size(100, 100);
            this.DatabaseListBox.Name = "DatabaseListBox";
            this.DatabaseListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DatabaseListBox.Size = new System.Drawing.Size(565, 402);
            this.DatabaseListBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Question list :";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(705, 442);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatabaseListBox);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(564, 429);
            this.Name = "Main";
            this.Text = "Survey Configurator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.Button Delete_Button;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.ListBox DatabaseListBox;
        private System.Windows.Forms.Label label1;
    }
}

