namespace AutoID
{
    partial class mainForm
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
            this.LoadButton = new System.Windows.Forms.Button();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tileSizeBox = new System.Windows.Forms.TextBox();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.optionsGroup = new System.Windows.Forms.GroupBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.saveLocationBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.optionsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(16, 20);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(99, 23);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Select Image";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(121, 21);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.Size = new System.Drawing.Size(128, 20);
            this.fileNameBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tile Size (px):";
            // 
            // tileSizeBox
            // 
            this.tileSizeBox.Location = new System.Drawing.Point(340, 21);
            this.tileSizeBox.Name = "tileSizeBox";
            this.tileSizeBox.Size = new System.Drawing.Size(38, 20);
            this.tileSizeBox.TabIndex = 3;
            this.tileSizeBox.Text = "32";
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Location = new System.Drawing.Point(12, 162);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(212, 209);
            this.originalPictureBox.TabIndex = 4;
            this.originalPictureBox.TabStop = false;
            // 
            // GenerateButton
            // 
            this.GenerateButton.AutoSize = true;
            this.GenerateButton.Location = new System.Drawing.Point(16, 87);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(369, 43);
            this.GenerateButton.TabIndex = 5;
            this.GenerateButton.Text = "Generate IDs!";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // optionsGroup
            // 
            this.optionsGroup.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.optionsGroup.Controls.Add(this.saveLocationBox);
            this.optionsGroup.Controls.Add(this.SaveButton);
            this.optionsGroup.Controls.Add(this.GenerateButton);
            this.optionsGroup.Controls.Add(this.tileSizeBox);
            this.optionsGroup.Controls.Add(this.label1);
            this.optionsGroup.Controls.Add(this.fileNameBox);
            this.optionsGroup.Controls.Add(this.LoadButton);
            this.optionsGroup.Location = new System.Drawing.Point(8, 9);
            this.optionsGroup.Name = "optionsGroup";
            this.optionsGroup.Size = new System.Drawing.Size(392, 147);
            this.optionsGroup.TabIndex = 6;
            this.optionsGroup.TabStop = false;
            this.optionsGroup.Text = "Options";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(16, 53);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(98, 23);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save To";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // saveLocationBox
            // 
            this.saveLocationBox.Location = new System.Drawing.Point(123, 55);
            this.saveLocationBox.Name = "saveLocationBox";
            this.saveLocationBox.Size = new System.Drawing.Size(254, 20);
            this.saveLocationBox.TabIndex = 7;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 407);
            this.Controls.Add(this.optionsGroup);
            this.Controls.Add(this.originalPictureBox);
            this.MinimumSize = new System.Drawing.Size(425, 445);
            this.Name = "mainForm";
            this.Text = "Auto ID Generator - Generates IDs for your tile sets";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            this.optionsGroup.ResumeLayout(false);
            this.optionsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tileSizeBox;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.GroupBox optionsGroup;
        private System.Windows.Forms.TextBox saveLocationBox;
        private System.Windows.Forms.Button SaveButton;

    }
}

