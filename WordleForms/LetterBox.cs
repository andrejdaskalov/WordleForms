using System;
using System.Drawing;

namespace WordleForms
{

    // An enumerator denoting the state of the letter box. 
    enum LetterBoxState
    {
        Positioned, //  correctly guessed and at the correct spot, green
        Guessed,    //  correctly guessed but NOT in the correct spot, yellow
        Incorrect,  // letter should not be in word, grayed out
        Default     // letterbox has not been used yet

    }
    // Perhaps a bad name but it's a letter in a box => LetterBox
    internal class LetterBox
    {
        public static readonly int LetterBoxSize = 50;
        private readonly Pen pen = new Pen(Color.SlateGray);
        private readonly Brush textColor = new SolidBrush(Color.Coral);
        private readonly Font font = new Font("Segoe UI",24,FontStyle.Bold);
        private Rectangle boundsRectangle;
        private static StringFormat stringFormat = new StringFormat();

        public string Letter { get; set; }
        public bool IsSelected { get; set; }
        public LetterBoxState State { get; set; }

        // public int X { get; set; }
        // public int Y { get; set; }

        static LetterBox()
        {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }
        public LetterBox(int x, int y)
        {
            Letter = "_";
            IsSelected = false;
            State = LetterBoxState.Default;
            boundsRectangle = new Rectangle(x, y, LetterBoxSize, LetterBoxSize);
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(pen, boundsRectangle);
            if (IsSelected || !Letter.Equals("_"))
            {
                g.DrawString(Letter, font, textColor, boundsRectangle, stringFormat);
            }
            
        }
    }
}
