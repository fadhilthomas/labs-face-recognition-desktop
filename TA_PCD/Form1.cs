/**
 * Muhammad Thomas Fadhila Yahya 
 * IF15Gx
 * 15312574
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using AForge.Imaging;

namespace TA_PCD
{
    public partial class Form1 : Form
    {
        Bitmap obj1, obj2, obj3, obj4, obj5;

        public Form1()
        {
            InitializeComponent();
        }

        private void btOpenImage_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult ofd = openFileDialog1.ShowDialog();
                if (ofd == DialogResult.OK)
                {
                    obj1 = new Bitmap(openFileDialog1.FileName);
                    obj2 = new Bitmap(openFileDialog1.FileName);
                    for (int x = 0; x < obj1.Width; x++)
                    {
                        for (int y = 0; y < obj1.Height; y++)
                        {
                            Color w = obj1.GetPixel(x, y);
                            int wg = (int)(w.R + w.G + w.B) / 3;
                            Color new_w = Color.FromArgb(wg, wg, wg);
                            obj1.SetPixel(x, y, new_w);
                        }
                    }
                    pbImage.Image = obj2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.ToString());
            }
        }

        private void btOpenTemp_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult ofd = openFileDialog1.ShowDialog();
                if (ofd == DialogResult.OK)
                {
                    obj3 = new Bitmap(openFileDialog1.FileName);
                    obj4 = new Bitmap(openFileDialog1.FileName);
                    for (int x = 0; x < obj3.Width; x++)
                    {
                        for (int y = 0; y < obj3.Height; y++)
                        {
                            Color w = obj3.GetPixel(x, y);
                            int wg = (int)(w.R + w.G + w.B) / 3;
                            Color new_w = Color.FromArgb(wg, wg, wg);
                            obj3.SetPixel(x, y, new_w);
                        }
                    }
                    pbTemp.Image = obj4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.ToString());
            }
        }

        private void btFindMatch_Click(object sender, EventArgs e)
        {
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
            float sim = (float)numSimilarity.Value;
            TemplateMatch[] matchings = tm.ProcessImage(obj1, obj3);
            int X, Y;

            Graphics g = Graphics.FromImage(obj2);
            if (matchings[0].Similarity > sim)
            {
                X = matchings[0].Rectangle.X;
                Y = matchings[0].Rectangle.Y;

                g.DrawRectangle(new Pen(Color.Red, 3), X, Y, matchings[0].Rectangle.Width, matchings[0].Rectangle.Height);
                pbImage.Image = obj2;
                
                obj5 = new Bitmap(matchings[0].Rectangle.Width, matchings[0].Rectangle.Height);
                for (int x = 0; x < matchings[0].Rectangle.Width; x++)
                {
                    for (int y = 0; y < matchings[0].Rectangle.Height; y++)
                    {
                        Color w = obj2.GetPixel(x + X, y + Y);
                        obj5.SetPixel(x, y, w);
                    }
                }
                pbResult.Image = obj5;

                MessageBox.Show("Kecocokan ditemukan.");
            }
            else
            {
                MessageBox.Show("Kecocokan tidak ditemukan.");
            }
        }
    }
}