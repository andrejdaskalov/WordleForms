using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordleForms
{
    public partial class Form1 : Form
    {
        private LetterBox letter;
        private LetterBox [][] LetterGrid;
        private static int Padding = 10;
        private static int XOffset = 80;
        private static int YOffset = 100;
        private Rectangle _rectangleLogo;
        private Rectangle _rectangleLogoOffset;
        private static StringFormat stringFormat = new StringFormat();

        static Form1()
        {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        public Form1()
        {
            InitializeComponent();
            //letter = new LetterBox(50,50);
            Width = 470;
            Height = 570;
            _rectangleLogo = new Rectangle(0, 0, Width, 100);
            _rectangleLogoOffset = new Rectangle(2, 2, Width, 100);
            LetterGrid = new LetterBox[6][];
            for (int i = 0; i < 6; i++)
            {
                LetterGrid[i] = new LetterBox[5];
                for (int j = 0; j < 5; j++)
                {
                    LetterGrid[i][j] = new LetterBox(j * LetterBox.LetterBoxSize + (j + 1) * Padding + XOffset, 
                        i*LetterBox.LetterBoxSize + (i + 1) * Padding + YOffset);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(255,1,8,18));
            //letter.Draw(g);
            g.DrawString("Wordle Forms", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), new SolidBrush(Color.DarkCyan), _rectangleLogoOffset, stringFormat);
            g.DrawString("Wordle Forms", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), new SolidBrush(Color.Azure), _rectangleLogo, stringFormat);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    LetterGrid[i][j].Draw(g);
                }
            }
        }
    }
}
