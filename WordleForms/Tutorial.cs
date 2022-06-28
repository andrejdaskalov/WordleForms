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
    public partial class Tutorial : Form
    {
        private static readonly StringFormat StringFormat = new StringFormat();
        private List<LetterBox> correctExample;
        private List<LetterBox> guessedExample;
        private List<LetterBox> incorrectExample;

        static Tutorial()
        {
            StringFormat.Alignment = StringAlignment.Near;
            StringFormat.LineAlignment = StringAlignment.Near;
            
        }
        public Tutorial()
        {
            InitializeComponent();
            correctExample = new List<LetterBox>();
            guessedExample = new List<LetterBox>();
            incorrectExample = new List<LetterBox>();
        }

        private void Tutorial_Load(object sender, EventArgs e)
        {
            // var correctExample = new List<LetterBox>() {new LetterBox(20,180){Letter = "W", State = LetterBoxState.Positioned}, new LetterBox(80, 180) { Letter = "E" }, new LetterBox(80, 180) { Letter = "E" }, }
            
            for (int i = 0; i < 5; i++)
            {
                var letter = new LetterBox(20 + i * 50 + (i + 1) * 10, 180);
                correctExample.Add(letter);
            }

            correctExample.ElementAt(0).Letter = "W";
            correctExample.ElementAt(1).Letter = "E";
            correctExample.ElementAt(2).Letter = "A";
            correctExample.ElementAt(3).Letter = "R";
            correctExample.ElementAt(4).Letter = "Y";
            correctExample.ElementAt(0).State = LetterBoxState.Positioned;

            for (int i = 0; i < 5; i++)
            {
                var letter = new LetterBox(20 + i * 50 + (i + 1) * 10, 305);
                guessedExample.Add(letter);
            }
            guessedExample.ElementAt(0).Letter = "P";
            guessedExample.ElementAt(1).Letter = "I";
            guessedExample.ElementAt(2).Letter = "L";
            guessedExample.ElementAt(3).Letter = "L";
            guessedExample.ElementAt(4).Letter = "S";
            guessedExample.ElementAt(1).State = LetterBoxState.Guessed;

            for (int i = 0; i < 5; i++)
            {
                var letter = new LetterBox(20 + i * 50 + (i + 1) * 10, 430);
                incorrectExample.Add(letter);
            }
            incorrectExample.ElementAt(0).Letter = "V";
            incorrectExample.ElementAt(1).Letter = "A";
            incorrectExample.ElementAt(2).Letter = "G";
            incorrectExample.ElementAt(3).Letter = "U";
            incorrectExample.ElementAt(4).Letter = "E";
            incorrectExample.ElementAt(3).State = LetterBoxState.Incorrect;
        }

        private void Tutorial_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(255, 1, 8, 18));
            g.DrawString("Guess the WORDLE in six tries.", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,25,440,20),StringFormat);
            g.DrawString("Each guess must be a valid five-letter word. Hit the enter button to submit.", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,45,400,40),StringFormat);
            g.DrawString("After each guess, the color of the tiles will change to show how close your guess was to the word.", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,80,400,40),StringFormat);
            g.DrawString("Examples:", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,120,400,20),StringFormat);
            
            g.DrawString("The letter W is in the word and in the correct spot.", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,240,400,20),StringFormat);
            g.DrawString("The letter I is in the word but in the wrong spot.", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,365,400,20),StringFormat);
            g.DrawString("The letter U is not in the word in any spot.", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.Azure), new RectangleF(20,490,400,20),StringFormat);

            foreach (var letterBox in correctExample)
            {
                letterBox.Draw(g);
            }
            foreach (var letterBox in guessedExample)
            {
                letterBox.Draw(g);
            }
            foreach (var letterBox in incorrectExample)
            {
                letterBox.Draw(g);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
