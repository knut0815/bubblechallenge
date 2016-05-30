using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace BubbleApp
{
    public partial class Form1 : Form
    {

        private CirclePacker cp;

        public Form1()
        {
            InitializeComponent();
            cp = new CirclePacker(canvas);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            // only render if we have something to show
            if (cp.AllCircles != null && cp.AllCircles.Count > 0)
            {
                // TODO - If this were a production app I'd consider
                // a means to determine when to stop iterating prior
                // to rendering.  
                for (int i = 1; i <= 3000; i++) 
                {
                    cp.Iterate(30);
                    // During debugging, it's cool to render the 
                    // the circles on the canvas after each iteration.
                    // This allows one to see how circles converge over time.  
                    // cp.Render(e.Graphics);
                }
                cp.Render(e.Graphics);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO - improve exception handling
            var FD = new OpenFileDialog();
            FD.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            FD.InitialDirectory = Application.StartupPath;
            FD.Title = "Please select a bubble definition file to draw.";
            if (FD.ShowDialog() == DialogResult.OK)
            {
                doClear();
                string fileToOpen = FD.FileName;
                try
                {
                    using (TextFieldParser parser = new TextFieldParser(fileToOpen))
                    {
                        parser.SetDelimiters(",");
                        while (!parser.EndOfData)
                        {
                            //Processing row
                            // fields[0] = radius
                            // fields[1] = circle name
                            string[] fields = parser.ReadFields();
                            cp.AddCircle(Convert.ToInt32(fields[0]), fields[1]);
                        }
                    }

                    canvas.Refresh();
                }
                catch (Exception Ex)
                {
                    String errMsg = string.Format("Error reading selected file. {0} ", Ex.Message);
                    MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void doClear()
        {
            if (cp.AllCircles != null)
            {
                cp.AllCircles.Clear();
            }

            canvas.Refresh();
        }

        private void canvas_Resize(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doClear();
        }
    }
}
