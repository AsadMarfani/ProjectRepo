namespace scyntaxProject
{
    partial class syntaxPad
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
            this.CompileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TokenTextBox = new System.Windows.Forms.TextBox();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CompileButton
            // 
            this.CompileButton.Location = new System.Drawing.Point(210, 483);
            this.CompileButton.Name = "CompileButton";
            this.CompileButton.Size = new System.Drawing.Size(75, 23);
            this.CompileButton.TabIndex = 8;
            this.CompileButton.Text = "Compile";
            this.CompileButton.UseVisualStyleBackColor = true;
            this.CompileButton.Click += new System.EventHandler(this.CompileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(658, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Token Viewer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "CodePad";
            // 
            // TokenTextBox
            // 
            this.TokenTextBox.Location = new System.Drawing.Point(555, 47);
            this.TokenTextBox.Multiline = true;
            this.TokenTextBox.Name = "TokenTextBox";
            this.TokenTextBox.ReadOnly = true;
            this.TokenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TokenTextBox.Size = new System.Drawing.Size(279, 411);
            this.TokenTextBox.TabIndex = 5;
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeTextBox.Location = new System.Drawing.Point(12, 47);
            this.CodeTextBox.Multiline = true;
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CodeTextBox.Size = new System.Drawing.Size(536, 412);
            this.CodeTextBox.TabIndex = 0;
            this.CodeTextBox.WordWrap = false;
            // 
            // syntaxPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 535);
            this.Controls.Add(this.CompileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TokenTextBox);
            this.Controls.Add(this.CodeTextBox);
            this.Name = "syntaxPad";
            this.Text = "syntaxPad";
            this.Load += new System.EventHandler(this.syntaxPad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CompileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TokenTextBox;
        private System.Windows.Forms.TextBox CodeTextBox;
    }
}