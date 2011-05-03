namespace MapEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fileMenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newButton = new System.Windows.Forms.ToolStripButton();
            this.loadButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foregroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.viewBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.viewForeground = new System.Windows.Forms.ToolStripMenuItem();
            this.viewObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCollision = new System.Windows.Forms.ToolStripMenuItem();
            this.foreType = new System.Windows.Forms.ToolStripSplitButton();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Random = new System.Windows.Forms.ToolStripMenuItem();
            this.stripeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erasertoolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tilePanel = new System.Windows.Forms.Panel();
            this.tileBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.Panel();
            this.mapBox = new System.Windows.Forms.PictureBox();
            this.selectedTileBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tilePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileBox)).BeginInit();
            this.picturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTileBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.aboutMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1019, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MainMenu";
            this.menuStrip1.Click += new System.EventHandler(this.erase);
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuNew,
            this.fileMenuLoad,
            this.toolStripSeparator1,
            this.fileMenuSave,
            this.toolStripSeparator2,
            this.fileMenuClose});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            // 
            // fileMenuNew
            // 
            this.fileMenuNew.Name = "fileMenuNew";
            this.fileMenuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileMenuNew.Size = new System.Drawing.Size(141, 22);
            this.fileMenuNew.Text = "New";
            this.fileMenuNew.Click += new System.EventHandler(this.fileMenuNew_Click);
            // 
            // fileMenuLoad
            // 
            this.fileMenuLoad.Name = "fileMenuLoad";
            this.fileMenuLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.fileMenuLoad.Size = new System.Drawing.Size(141, 22);
            this.fileMenuLoad.Text = "Load";
            this.fileMenuLoad.Click += new System.EventHandler(this.fileMenuLoad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // fileMenuSave
            // 
            this.fileMenuSave.Name = "fileMenuSave";
            this.fileMenuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileMenuSave.Size = new System.Drawing.Size(141, 22);
            this.fileMenuSave.Text = "Save";
            this.fileMenuSave.Click += new System.EventHandler(this.fileMenuSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // fileMenuClose
            // 
            this.fileMenuClose.Name = "fileMenuClose";
            this.fileMenuClose.Size = new System.Drawing.Size(141, 22);
            this.fileMenuClose.Text = "Close";
            this.fileMenuClose.Click += new System.EventHandler(this.fileMenuClose_Click);
            // 
            // aboutMenu
            // 
            this.aboutMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuHelp,
            this.aboutMenuInformation});
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aboutMenu.Size = new System.Drawing.Size(52, 20);
            this.aboutMenu.Text = "About";
            // 
            // aboutMenuHelp
            // 
            this.aboutMenuHelp.Name = "aboutMenuHelp";
            this.aboutMenuHelp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.aboutMenuHelp.Size = new System.Drawing.Size(174, 22);
            this.aboutMenuHelp.Text = "Help";
            this.aboutMenuHelp.Click += new System.EventHandler(this.aboutMenuHelp_Click);
            // 
            // aboutMenuInformation
            // 
            this.aboutMenuInformation.Name = "aboutMenuInformation";
            this.aboutMenuInformation.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.aboutMenuInformation.Size = new System.Drawing.Size(174, 22);
            this.aboutMenuInformation.Text = "Information";
            this.aboutMenuInformation.Click += new System.EventHandler(this.aboutMenuInformation_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.loadButton,
            this.saveButton,
            this.toolStripSplitButton1,
            this.toolStripSplitButton2,
            this.foreType,
            this.erasertoolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newButton
            // 
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(51, 22);
            this.newButton.Text = "New";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Image = ((System.Drawing.Image)(resources.GetObject("loadButton.Image")));
            this.loadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(53, 22);
            this.loadButton.Text = "Load";
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(51, 22);
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundToolStripMenuItem,
            this.foregroundToolStripMenuItem,
            this.objectsToolStripMenuItem,
            this.collisionToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(59, 22);
            this.toolStripSplitButton1.Text = "Edit";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.Checked = true;
            this.backgroundToolStripMenuItem.CheckOnClick = true;
            this.backgroundToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.backgroundToolStripMenuItem.Text = "Background";
            this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.backgroundToolStripMenuItem_Click);
            // 
            // foregroundToolStripMenuItem
            // 
            this.foregroundToolStripMenuItem.CheckOnClick = true;
            this.foregroundToolStripMenuItem.Name = "foregroundToolStripMenuItem";
            this.foregroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.foregroundToolStripMenuItem.Text = "Foreground";
            this.foregroundToolStripMenuItem.Click += new System.EventHandler(this.foregroundToolStripMenuItem_Click);
            // 
            // objectsToolStripMenuItem
            // 
            this.objectsToolStripMenuItem.CheckOnClick = true;
            this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            this.objectsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.objectsToolStripMenuItem.Text = "Objects";
            this.objectsToolStripMenuItem.Click += new System.EventHandler(this.objectsToolStripMenuItem_Click);
            // 
            // collisionToolStripMenuItem
            // 
            this.collisionToolStripMenuItem.CheckOnClick = true;
            this.collisionToolStripMenuItem.Name = "collisionToolStripMenuItem";
            this.collisionToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.collisionToolStripMenuItem.Text = "Collision";
            this.collisionToolStripMenuItem.Click += new System.EventHandler(this.collisionToolStripMenuItem_Click);
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewBackground,
            this.viewForeground,
            this.viewObjects,
            this.viewCollision});
            this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(64, 22);
            this.toolStripSplitButton2.Text = "View";
            // 
            // viewBackground
            // 
            this.viewBackground.Checked = true;
            this.viewBackground.CheckOnClick = true;
            this.viewBackground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewBackground.Name = "viewBackground";
            this.viewBackground.Size = new System.Drawing.Size(138, 22);
            this.viewBackground.Text = "Background";
            this.viewBackground.Click += new System.EventHandler(this.viewBackground_Click);
            // 
            // viewForeground
            // 
            this.viewForeground.Checked = true;
            this.viewForeground.CheckOnClick = true;
            this.viewForeground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewForeground.Name = "viewForeground";
            this.viewForeground.Size = new System.Drawing.Size(138, 22);
            this.viewForeground.Text = "Foreground";
            this.viewForeground.Click += new System.EventHandler(this.viewForeground_Click);
            // 
            // viewObjects
            // 
            this.viewObjects.Checked = true;
            this.viewObjects.CheckOnClick = true;
            this.viewObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewObjects.Name = "viewObjects";
            this.viewObjects.Size = new System.Drawing.Size(138, 22);
            this.viewObjects.Text = "Objects";
            this.viewObjects.Click += new System.EventHandler(this.viewObjects_Click);
            // 
            // viewCollision
            // 
            this.viewCollision.Checked = true;
            this.viewCollision.CheckOnClick = true;
            this.viewCollision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewCollision.Name = "viewCollision";
            this.viewCollision.Size = new System.Drawing.Size(138, 22);
            this.viewCollision.Text = "Collision";
            this.viewCollision.Click += new System.EventHandler(this.viewCollision_Click);
            // 
            // foreType
            // 
            this.foreType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.solidToolStripMenuItem,
            this.Random,
            this.stripeToolStripMenuItem,
            this.barToolStripMenuItem});
            this.foreType.Enabled = false;
            this.foreType.Image = ((System.Drawing.Image)(resources.GetObject("foreType.Image")));
            this.foreType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.foreType.Name = "foreType";
            this.foreType.Size = new System.Drawing.Size(93, 22);
            this.foreType.Text = "Fore-Type";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Checked = true;
            this.noneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // solidToolStripMenuItem
            // 
            this.solidToolStripMenuItem.Name = "solidToolStripMenuItem";
            this.solidToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.solidToolStripMenuItem.Text = "Solid";
            this.solidToolStripMenuItem.Click += new System.EventHandler(this.foreType_Solid);
            // 
            // Random
            // 
            this.Random.Name = "Random";
            this.Random.Size = new System.Drawing.Size(154, 22);
            this.Random.Text = "Random";
            this.Random.Click += new System.EventHandler(this.foreType_Random);
            // 
            // stripeToolStripMenuItem
            // 
            this.stripeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.stripeToolStripMenuItem.Name = "stripeToolStripMenuItem";
            this.stripeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.stripeToolStripMenuItem.Text = "Diagonal Stripe";
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.foreType_DiagL);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.foreType_DiagR);
            // 
            // barToolStripMenuItem
            // 
            this.barToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem,
            this.verticalToolStripMenuItem});
            this.barToolStripMenuItem.Name = "barToolStripMenuItem";
            this.barToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.barToolStripMenuItem.Text = "Bar";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.foreType_BarH);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.foreType_BarV);
            // 
            // erasertoolStripButton1
            // 
            this.erasertoolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("erasertoolStripButton1.Image")));
            this.erasertoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.erasertoolStripButton1.Name = "erasertoolStripButton1";
            this.erasertoolStripButton1.Size = new System.Drawing.Size(58, 22);
            this.erasertoolStripButton1.Text = "Eraser";
            this.erasertoolStripButton1.Click += new System.EventHandler(this.erase);
            // 
            // tilePanel
            // 
            this.tilePanel.AutoScroll = true;
            this.tilePanel.Controls.Add(this.tileBox);
            this.tilePanel.Location = new System.Drawing.Point(12, 107);
            this.tilePanel.Name = "tilePanel";
            this.tilePanel.Size = new System.Drawing.Size(192, 436);
            this.tilePanel.TabIndex = 5;
            // 
            // tileBox
            // 
            this.tileBox.Location = new System.Drawing.Point(3, 3);
            this.tileBox.Name = "tileBox";
            this.tileBox.Size = new System.Drawing.Size(159, 485);
            this.tileBox.TabIndex = 3;
            this.tileBox.TabStop = false;
            this.tileBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tileBox_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selected Tile:";
            // 
            // picturePanel
            // 
            this.picturePanel.Controls.Add(this.mapBox);
            this.picturePanel.Location = new System.Drawing.Point(207, 69);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(800, 480);
            this.picturePanel.TabIndex = 9;
            // 
            // mapBox
            // 
            this.mapBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mapBox.BackgroundImage = global::MapEditor.Properties.Resources.grid;
            this.mapBox.Enabled = false;
            this.mapBox.Image = global::MapEditor.Properties.Resources.grid;
            this.mapBox.InitialImage = global::MapEditor.Properties.Resources.grid;
            this.mapBox.Location = new System.Drawing.Point(0, 0);
            this.mapBox.Margin = new System.Windows.Forms.Padding(0);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(800, 480);
            this.mapBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.mapBox.TabIndex = 8;
            this.mapBox.TabStop = false;
            this.mapBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapBox_MouseDown);
            this.mapBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapBox_MouseMove);
            this.mapBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapBox_MouseUp);
            // 
            // selectedTileBox
            // 
            this.selectedTileBox.Location = new System.Drawing.Point(87, 69);
            this.selectedTileBox.Name = "selectedTileBox";
            this.selectedTileBox.Size = new System.Drawing.Size(32, 32);
            this.selectedTileBox.TabIndex = 7;
            this.selectedTileBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1019, 565);
            this.Controls.Add(this.picturePanel);
            this.Controls.Add(this.selectedTileBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tilePanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RPG Map Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_Keydown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tilePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tileBox)).EndInit();
            this.picturePanel.ResumeLayout(false);
            this.picturePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTileBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuNew;
        private System.Windows.Forms.ToolStripMenuItem fileMenuLoad;
        private System.Windows.Forms.ToolStripMenuItem fileMenuSave;
        private System.Windows.Forms.ToolStripMenuItem aboutMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuInformation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foregroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
        private System.Windows.Forms.ToolStripMenuItem viewBackground;
        private System.Windows.Forms.ToolStripMenuItem viewForeground;
        private System.Windows.Forms.ToolStripMenuItem viewObjects;
        private System.Windows.Forms.ToolStripMenuItem viewCollision;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem fileMenuClose;
        private System.Windows.Forms.ToolStripButton newButton;
        private System.Windows.Forms.ToolStripButton loadButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.PictureBox tileBox;
        private System.Windows.Forms.Panel tilePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox selectedTileBox;
        private System.Windows.Forms.PictureBox mapBox;
        private System.Windows.Forms.Panel picturePanel;
        private System.Windows.Forms.ToolStripSplitButton foreType;
        private System.Windows.Forms.ToolStripMenuItem solidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Random;
        private System.Windows.Forms.ToolStripMenuItem stripeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton erasertoolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
    }
}

