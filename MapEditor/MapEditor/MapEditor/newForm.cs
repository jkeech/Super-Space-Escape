/*******************************************************************************
  MapEditor v1.00
  Developed by: Zach Pollock, Michael Perkins, and John Keech
  Copyright 2011 - All rights reserved
 
  Disclaimer: The following software is provided "as-is" for educational
               purposes and is the intellectual property of Texas A&M University
               created by the forementioned developers for the Computer Science
               and Engineering Department Programming Studio course.
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MapEditor
{
    public partial class newForm : Form
    {
        int width = 0, height = 0;
        string txtName;

        /***********************************************************************
                                New Form Event Handlers
        ***********************************************************************/

        public newForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /* Creates a default template level file of the correct
         * dimensions and saves it to disk. This file is then
         * loaded into the TileEngine by the main form. */
        private void createButton_Click(object sender, EventArgs e)
        {
            txtName = newName.Text;
            int.TryParse(newWidth.Text, out width);
            int.TryParse(newHeight.Text, out height);
            // error handling - bounds checking
            if (txtName == "")
            {
                MessageBox.Show("You must have a file name", "Invalid Map parameters", MessageBoxButtons.OK);
                return;
            }
            if (width < 25 || height < 15)
            {
                MessageBox.Show("Map too small! The dimensions must be at least 25x15", "Invalid Map parameters", MessageBoxButtons.OK);
                return;
            }
            if (width > 200 || height > 200)
            {
                MessageBox.Show("Map too large! The dimensions must not exceed 200x200", "Invalid Map parameters", MessageBoxButtons.OK);
                return;
            }

            // Write the file to disk
            FileStream fs1 = File.Open(txtName + ".lvl", FileMode.OpenOrCreate, FileAccess.Write);
            fs1.Close();
            FileStream fs = File.Open(txtName + ".lvl", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(txtName);
            sw.WriteLine(width + " " + height);
            sw.WriteLine("");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j > 0)
                        sw.Write(" ");
                    sw.Write("72");
                }
                sw.Write("\r\n");
            }
            sw.WriteLine("");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j > 0)
                        sw.Write(" ");
                    sw.Write("-1");
                }
                sw.Write("\r\n");
            }
            sw.WriteLine("");

            for (int i = 0; i < height * 2; i++)
            {
                for (int j = 0; j < width * 2; j++)
                {
                    if (j > 0)
                        sw.Write(" ");
                    sw.Write("-1");
                }
                sw.Write("\r\n");
            }
            sw.WriteLine("");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j > 0)
                        sw.Write(" ");
                    sw.Write("-1");
                }
                sw.Write("\r\n");
            }
            sw.WriteLine("");
            sw.Close();
            fs.Close();
            this.Hide();
        }

        /***********************************************************************
                          Functions to be called from MainForm
        ***********************************************************************/

        public string GetName()
        {
            return txtName;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        private void newForm_Load(object sender, EventArgs e)
        {

        }
    }
}
