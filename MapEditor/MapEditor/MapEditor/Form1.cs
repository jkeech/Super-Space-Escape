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
    public partial class MainForm : Form
    {
        /* Class Variables */
        int TILE_SIZE = 32;
        TileEngine engine;
        TileLoader tileLoader;
        int selectedTile = 0;
        int mapX = 0, mapY = 0;
        bool isEditing = false;
        bool erasing=false;
        bool editSinceLastSave = false;        

        /***********************************************************************
                      Drawing methods to draw the map on the screen
        ***********************************************************************/

        private void clearFore()
        {
            if (engine.getCurrentLevel() < 0)
            {
                MessageBox.Show("Please load a level first!");
                return;
            }
            editSinceLastSave = true;
            int textured=-1;
            for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
            {
                for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                {
                    engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(-1);
                    tileLoader.addToMapLayer(getEditLayer(), b, a,textured);
                }
            }
            
        }
        private void drawFore()
        {
            if (engine.getCurrentLevel() < 0)
            {
                MessageBox.Show("Please load a level first!");
                return;
            }
            clearFore();
            if (getForeType() == ForeType.SOLID)
                {
                    for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
                    {
                        for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                        {
                            tileLoader.addToMapLayer(getEditLayer(), b, a, selectedTile);
                            engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(selectedTile);
                        }

                    }
                }
                if (getForeType() == ForeType.RANDOM)
                {
                    Random rand = new Random();

                    for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
                    {
                        for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                        {
                            int num = rand.Next(0, 100);
                            if (num > 90)
                            {
                                tileLoader.addToMapLayer(getEditLayer(), b, a, selectedTile);
                                engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(selectedTile);
                            }
                        }

                    }
                }
                if (getForeType() == ForeType.DIAGR)
                {
                    for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
                    {
                        for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                        {
                            if ((b - a) % 5 == 0)
                            {
                                tileLoader.addToMapLayer(getEditLayer(), b, a, selectedTile);
                                engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(selectedTile);
                            }
                        }

                    }
                }
                if (getForeType() == ForeType.DIAGL)
                {
                    for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
                    {
                        for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                        {
                            if ((b + a) % 5 == 0)
                            {
                                tileLoader.addToMapLayer(getEditLayer(), b, a, selectedTile);
                                engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(selectedTile);
                            }
                        }

                    }
                }
                if (getForeType() == ForeType.BARH)
                {
                    for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
                    {
                        for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                        {
                            if (a % 4 == 0)
                            {
                                tileLoader.addToMapLayer(getEditLayer(), b, a, selectedTile);
                                engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(selectedTile);
                            }
                        }

                    }
                }
                if (getForeType() == ForeType.BARV)
                {
                    for (int a = 0; a < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getHeight(); a++)
                    {
                        for (int b = 0; b < engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getWidth(); b++)
                        {
                            if (b % 4 == 0)
                            {
                                tileLoader.addToMapLayer(getEditLayer(), b, a, selectedTile);
                                engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(b, a).setTexture(selectedTile);
                            }
                        }

                    }
                }
                mapBox.Image = null;
                redrawMap();
        }

        private void redrawMap()
        {
            if (engine.getCurrentLevel() < 0)
            {
                mapBox.Image = mapBox.InitialImage;
                MessageBox.Show("Please load a level first!");
                return;
            }
            // Create new image to draw
            if (mapBox.Image == null)
                mapBox.Image = new Bitmap(engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getWidth() * TILE_SIZE, engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getHeight() * TILE_SIZE);
            Graphics canvas = Graphics.FromImage(mapBox.Image);

            // Draw blank grid
            canvas.DrawImage(mapBox.InitialImage, new Point(0, 0));

            // Draw layers if they are visible
            if (viewBackground.Checked == true)
                canvas.DrawImage(tileLoader.getMapLayer(LayerType.BACKGROUND), new Point(0, 0));
            if (viewObjects.Checked == true)
                canvas.DrawImage(tileLoader.getMapLayer(LayerType.OBJECTS), new Point(0, 0));
            if (viewForeground.Checked == true)
                canvas.DrawImage(tileLoader.getMapLayer(LayerType.FOREGROUND), new Point(0, 0));
            if (viewCollision.Checked == true)
                canvas.DrawImage(tileLoader.getMapLayer(LayerType.COLLISION), new Point(0, 0));

            canvas.Dispose();

            // Update image on screen
            mapBox.Refresh();
        }

        private void erase(object sender, EventArgs e)
        {
            erasing = true;
            selectedTile = -1;
            selectedTileBox.Image = tileLoader.getTile(getEditLayer(), selectedTile);
        }

        /***********************************************************************
                 Adds the selected tile to the map at a certain location
        ***********************************************************************/

        private void addTile(int X, int Y)
        {
            editSinceLastSave = true;

            // Convert the click coordinates (in pixels) to the tile coordinates
            int x = X / TILE_SIZE;
            int y = Y / TILE_SIZE;

            // Select the layer we are editing
            LayerType layer = getEditLayer();
            
            // Update the tile on that layer with the new one
            tileLoader.addToMapLayer(layer, x, y, selectedTile);

            // Add the new tile to the TileEngine
            if (layer == LayerType.COLLISION)
            {
                bool hasCollision = false;
                if (selectedTile == 0)
                    hasCollision = true;
                engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getTile(x, y).setCollision(hasCollision);
            }
            else
            {
                if (erasing)
                {
                    if (layer == LayerType.BACKGROUND)
                    {
                        // If you are trying to erase the background, set it to the default "grass" texture
                        engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getTile(x, y).setTexture(72);
                        tileLoader.addToMapLayer(getEditLayer(), x, y, 72);
                    }
                    else
                    {
                        // -1 is the ID for a blank tile
                        engine.getCurrentMap().getLayer(layer).getTile(x, y).setTexture(-1);
                        tileLoader.addToMapLayer(getEditLayer(), x, y, -1);
                    }
                }
                else
                    engine.getCurrentMap().getLayer(layer).getTile(x, y).setTexture(selectedTile);
            }
            // redraw the correct Layers
            redrawMap();
        }

        /***********************************************************************
                       Helper functions to determine current states
        ***********************************************************************/

        // returns the currently selected layer to edit
        private LayerType getEditLayer()
        {
            if (backgroundToolStripMenuItem.Checked == true)
                return LayerType.BACKGROUND;
            else if (foregroundToolStripMenuItem.Checked == true)
                return LayerType.FOREGROUND;
            else if (objectsToolStripMenuItem.Checked == true)
                return LayerType.OBJECTS;
            else
                return LayerType.COLLISION;
        }
        // returns the currently selected layer to edit
        private ForeType getForeType()
        {
            if (Random.Checked == true)
                return ForeType.RANDOM;
            else if (leftToolStripMenuItem.Checked == true)
                return ForeType.DIAGL;
            else if (rightToolStripMenuItem.Checked == true)
                return ForeType.DIAGR;
            else if (horizontalToolStripMenuItem.Checked == true)
                return ForeType.BARH;
            else if (verticalToolStripMenuItem.Checked == true)
                return ForeType.BARV;
            else if (noneToolStripMenuItem.Checked == true)
                return ForeType.NONE;
            else
                return ForeType.SOLID;
        }

        /***********************************************************************
                                Main Form Event Handlers
        ***********************************************************************/

        public MainForm()
        {
            InitializeComponent();
        }

        /* Set up the TileLoader and TileEngine classes */
        private void MainForm_Load(object sender, EventArgs e)
        {
            tileLoader = new TileLoader(TILE_SIZE);
            engine = new TileEngine(TILE_SIZE, tileLoader);

            populateTiles(LayerType.BACKGROUND);
        }

        /* Add initial tiles to the editor on the left side */
        private void populateTiles(LayerType layer)
        {
            tileBox.Image = tileLoader.getTileLayer(layer);
            tileBox.Height = tileBox.Image.Height;
            selectedTileBox.Image = tileLoader.getTile(layer, selectedTile);
        }

        /***********************************************************************
                                   Form close events
        ***********************************************************************/

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editSinceLastSave)
            {
                if (MessageBox.Show("Do you want to save the changes to your level before you close the Map Editor?", "Save your changes?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    saveFile();
                    e.Cancel = true; // Do not close the form
                }
                else
                    e.Cancel = false; // Close the form
            }
            else
                e.Cancel = false; // Close the form
        }

        private void fileMenuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /***********************************************************************
                                   New file events
        ***********************************************************************/

        private void fileMenuNew_Click(object sender, EventArgs e)
        {
            createNewFile();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            createNewFile();
        }

        /* Creates a new temporary level file and loads it into the TileEngine */
        private void createNewFile()
        {
            newForm newForm = new newForm();
            if (newForm.ShowDialog(this) == DialogResult.Yes)
            {
                string filename = newForm.GetName();
                if (filename != "" && newForm.getHeight() >= 15 && newForm.getWidth() >= 25)
                {
                    engine.loadLevel(filename + ".lvl");
                    System.Console.WriteLine("Loaded!");
                    mapBox.Image = null;
                    redrawMap();
                    mapBox.Enabled = true;
                }
            }
        }

        /***********************************************************************
                                   Load file events
        ***********************************************************************/

        private void fileMenuLoad_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        /* Loads an existing level file from disk */
        private void loadFile()
        {
            String file = SelectLoadFile("/");
            if (file != null)
            {
                engine.loadLevel(file);
                mapBox.Image = null;
                redrawMap();
                mapBox.Enabled = true;
            }
        }

        /* Prompts the user to select which level file to load */
        private string SelectLoadFile(string initialDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "Level Files(*.lvl)|*.lvl;";
            dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select an level file";
            return (dialog.ShowDialog() == DialogResult.OK)
               ? dialog.FileName : null;
        }

        /***********************************************************************
                                   Save file events
        ***********************************************************************/

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void fileMenuSave_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        /* Writes the current level data to disk in an easy-to-read, but still compact form */
        private void saveFile()
        {
            String file = SelectSaveFile("/");
            if (file != null)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(file, false);
                    int width = engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getWidth();
                    int height = engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getHeight();
                    sw.WriteLine(engine.getCurrentMap().getName());
                    sw.WriteLine(width + " " + height);
                    sw.WriteLine("");
                    //Save Background
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (j > 0)
                                sw.Write(" ");
                            int ID = engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getTile(j, i).getTexture();
                            sw.Write(ID);
                        }
                        sw.Write("\r\n");
                    }
                    sw.WriteLine("");
                    //save Collision
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (j > 0)
                                sw.Write(" ");
                            bool Col = engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getTile(j, i).hasCollision();//????
                            if (Col)
                                sw.Write("0");
                            else
                                sw.Write("-1");
                        }
                        sw.Write("\r\n");
                    }
                    sw.WriteLine("");
                    //save Foreground
                    for (int i = 0; i < height * 2; i++)
                    {
                        for (int j = 0; j < width * 2; j++)
                        {
                            if (j > 0)
                                sw.Write(" ");
                            int ID = engine.getCurrentMap().getLayer(LayerType.FOREGROUND).getTile(j, i).getTexture();

                            sw.Write(ID);
                        }
                        sw.Write("\r\n");
                    }
                    sw.WriteLine("");
                    //save objects
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (j > 0)
                                sw.Write(" ");
                            int ID = engine.getCurrentMap().getLayer(LayerType.OBJECTS).getTile(j, i).getTexture();
                            sw.Write(ID);
                        }
                        sw.Write("\r\n");
                    }
                    sw.WriteLine("");
                    sw.Close();
                    editSinceLastSave = false;
                }
                catch (Exception e)
                {
                    // Couldn't save the file. Display the error message
                    MessageBox.Show(e.ToString());
                }
            }
        }

        /* Prompts the user to select where to save the level file */
        private string SelectSaveFile(string initialDirectory)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter =
               "Level Files(*.lvl)|*.lvl;";
            dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select an level file";
            return (dialog.ShowDialog() == DialogResult.OK)
               ? dialog.FileName : null;
        }

        /***********************************************************************
                             View and Edit Event Handlers
        ***********************************************************************/

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundToolStripMenuItem.Checked = true;
            foregroundToolStripMenuItem.Checked = false;
            objectsToolStripMenuItem.Checked = false;
            collisionToolStripMenuItem.Checked = false;
            foreType.Enabled = false;

            populateTiles(LayerType.BACKGROUND);
        }

        private void foregroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = true;
            objectsToolStripMenuItem.Checked = false;
            collisionToolStripMenuItem.Checked = false;
            foreType.Enabled = true;
            foreType.ShowDropDown();

            populateTiles(LayerType.FOREGROUND);
        }

        private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = false;
            objectsToolStripMenuItem.Checked = true;
            collisionToolStripMenuItem.Checked = false;
            foreType.Enabled = false;

            populateTiles(LayerType.OBJECTS);
        }

        private void collisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = false;
            objectsToolStripMenuItem.Checked = false;
            collisionToolStripMenuItem.Checked = true;
            foreType.Enabled = false;

            populateTiles(LayerType.COLLISION);
        }

        private void foreType_Solid(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = true;
            Random.Checked = false;
            leftToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            drawFore();
        }

        private void foreType_Random(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = false;
            Random.Checked = true;
            leftToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            drawFore();
        }

        private void foreType_DiagL(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = false;
            Random.Checked = false;
            leftToolStripMenuItem.Checked = true;
            rightToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            drawFore();
        }
        private void foreType_DiagR(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = false;
            Random.Checked = false;
            leftToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = true;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            drawFore();
        }
        private void foreType_BarH(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = false;
            Random.Checked = false;
            leftToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = true;
            verticalToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            drawFore();
        }

        private void foreType_BarV(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = false;
            Random.Checked = false;
            leftToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = true;
            noneToolStripMenuItem.Checked = false;
            drawFore();
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            solidToolStripMenuItem.Checked = false;
            Random.Checked = false;
            leftToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = true;
            clearFore();
            redrawMap();
        }

        private void viewBackground_Click(object sender, EventArgs e)
        {
            mapBox.Image = null;
            redrawMap();
        }

        private void viewForeground_Click(object sender, EventArgs e)
        {
            mapBox.Image = null;
            redrawMap();
        }

        private void viewObjects_Click(object sender, EventArgs e)
        {
            mapBox.Image = null;
            redrawMap();
        }

        private void viewCollision_Click(object sender, EventArgs e)
        {
            mapBox.Image = null;
            redrawMap();
        }

        private void form_Keydown(object sender, KeyEventArgs e)
        {
            if (mapBox.Enabled == true)
            {
                if (e.KeyCode == Keys.Left)
                    mapX += TILE_SIZE;
                else if (e.KeyCode == Keys.Right)
                    mapX -= TILE_SIZE;
                else if (e.KeyCode == Keys.Up)
                    mapY += TILE_SIZE;
                else if (e.KeyCode == Keys.Down)
                    mapY -= TILE_SIZE;
                else return;

                // Clip map to boundaries
                if (mapX > 0)
                    mapX = 0;
                if (mapY > 0)
                    mapY = 0;
                if (mapX < 800 - engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getWidth() * TILE_SIZE)
                    mapX = 800 - engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getWidth() * TILE_SIZE;
                if (mapY < 480 - engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getHeight() * TILE_SIZE)
                    mapY = 480 - engine.getCurrentMap().getLayer(LayerType.BACKGROUND).getHeight() * TILE_SIZE;

                // Move map on screen to new location
                mapBox.Location = new Point(mapX, mapY);
            }
        }

        private void tileBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            erasing = false;

            /// Convert the click coordinates to the tile ID.
            int newTile = e.X / TILE_SIZE + (e.Y / TILE_SIZE * 5);

            ///get current edit layer
            LayerType layer = getEditLayer();

            /// If you selected a non-existent tile, do nothing
            if (newTile >= tileLoader.getNumTiles(layer))
                return;

            /// Update the tile with the new ID and redraw the selected tile
            selectedTile = newTile;
            selectedTileBox.Image = tileLoader.getTile(layer, selectedTile);
            //if on Foreground layer, draw new foreground
            if (layer == LayerType.FOREGROUND)
            {
                drawFore();
            }

        }

        private void mapBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (getEditLayer() != LayerType.FOREGROUND)
            {
                isEditing = true;
                addTile(e.X, e.Y);
            }
        }

        private void mapBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isEditing = false;
        }

        private void mapBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isEditing && e.X >= 0 && e.X < mapBox.Width && e.Y >= 0 && e.Y < mapBox.Height)
            {
                addTile(e.X, e.Y);
            }
        }

        /***********************************************************************
                             About and Info Event Handlers
        ***********************************************************************/

        private void aboutMenuHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("------HELP------\n\n" +
                            "Where do I start?\n" +
                                "To begin just click File (Alt + F) and click New.\n\n" +

                            "What are those weird symbols next to the menu items?\n" +
                                "Those are short cut command to more easily issue commands to the program." +
                                "the Ctrl + [letter] means that you need to hold down the Ctrl button and " +
                                "then press the letter that is next to it.\n\n" +

                            "How do I save?\n" +
                                "To save the progress that you have made on a map click the save button and " +
                                "your work will be saved. You can then continue to work or close out of the " +
                                "program and come back later and load your saved map file.\n\n" +

                            "How do I load a map that I have saved?\n" +
                                "To load an existing map, just click the Load button, or go to File -> Load and " +
                                "select the level file (.lvl) that you wish to load.\n\n" +

                            "How do I change different layers?\n" +
                                "click the down arrow next to the Edit button and choose the layer that you " +
                                "want to start working on. Only one layer can be edited at a time.\n\n" +

                            "How do I stop looking at a layer?\n" +
                                "To change what layer you are viewing, click the down arrow next to the View " +
                                "button and select or deselect what layer you want to look at or what layer " +
                                "you do not want to look at.");
        }

        private void aboutMenuInformation_Click(object sender, EventArgs e)
        {
            MessageBox.Show("------ABOUT------\n\n" +
                            "Map Editor version 1.0.0\n" +
                            "Created by: " +
                            "Zach Pollock, John Keech, and Michael Perkins\n\n" +
                            "For Texas A&M University's CSCE 315\n" +
                            "© 2011 - All rights reserved\n");
        }
    }
}
