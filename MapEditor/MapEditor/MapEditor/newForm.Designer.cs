namespace MapEditor
{
    partial class newForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.newName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newHeight = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name (.lvl) ";
            // 
            // newName
            // 
            this.newName.Location = new System.Drawing.Point(85, 7);
            this.newName.Name = "newName";
            this.newName.Size = new System.Drawing.Size(288, 20);
            this.newName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width (tiles)";
            // 
            // newWidth
            // 
            this.newWidth.Location = new System.Drawing.Point(85, 47);
            this.newWidth.Name = "newWidth";
            this.newWidth.Size = new System.Drawing.Size(107, 20);
            this.newWidth.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Height (tiles)";
            // 
            // newHeight
            // 
            this.newHeight.Location = new System.Drawing.Point(279, 48);
            this.newHeight.Name = "newHeight";
            this.newHeight.Size = new System.Drawing.Size(94, 20);
            this.newHeight.TabIndex = 5;
            // 
            // createButton
            // 
            this.createButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.createButton.Location = new System.Drawing.Point(19, 82);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(254, 33);
            this.createButton.TabIndex = 6;
            this.createButton.Text = "Create New Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(281, 82);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 32);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // newForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(401, 133);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.newHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "newForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a new map";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.newForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newHeight;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button cancelButton;
    }
}