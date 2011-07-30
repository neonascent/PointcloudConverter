using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text) == false)
            {
                MessageBox.Show("Input file does not exist");
            }
            else
            {


                //Convert file
                if (ConvertFiles(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
        }

        private bool ConvertFiles(string infile, string outfile)
        {
            
            int counter = 0;
            string line;
            string[] fields;
            string outline;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(infile);
            // create a writer and open the file
            System.IO.TextWriter tw = new StreamWriter(outfile);


            while ((line = file.ReadLine()) != null)
            {
                if (counter == 2)
                {
                    // do something with vertex number line
                    fields = line.Split(' ');
                    outline = fields[2];
                    tw.WriteLine(outline);
                }
                else if (counter > 12) 
                {
                    // process and convert
                    fields = line.Split(' ');
                    // PLY
                    // x, y, z, nx, ny, nz, r, g, b
                    // -3.51649 -1.70558 0.374101 0.27536 0.391808 -0.877874 63 68 41
                    
                    //55.74190000 77.90000000 4.15650000   0.1  246 246 246
                    // PTS
                    // X Y Z Intensity value R G B

                    outline = fields[0] + " " + fields[1] + " " + fields[2] + " 0.1 " + fields[6] + " " + fields[7] + " " + fields[8];
                    tw.WriteLine(outline);
                }
                // write to file
                // write a line of text to the file
                counter++;
            }




            // close the stream
            tw.Close();
            file.Close();

            if (File.Exists(outfile))
            {
                return true;
            }

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Displays a OpenDialog
            openFileDialog1.Filter = "PLY pointcloud|*.ply";
            openFileDialog1.Title = "Select Input PLY file";
            
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
               // Displays a SaveFileDialog so the user can save the Image
               // assigned to Button2.
               SaveFileDialog saveFileDialog1 = new SaveFileDialog();
               saveFileDialog1.Filter = "PTS pointcloud|*.pts";
               saveFileDialog1.Title = "Output PTS filename";
               saveFileDialog1.ShowDialog();

               // If the file name is not an empty string open it for saving.
               if (saveFileDialog1.FileName != "")
               {
                   textBox2.Text = saveFileDialog1.FileName;
               }
        }
    }
}
