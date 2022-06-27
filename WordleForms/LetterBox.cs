using System;
using System.Drawing;

namespace WordleForms
{

    // An enumerator denoting the state of the letter box. 
    public enum LetterBoxState
    {
        Positioned, //  correctly guessed and at the correct spot, green
        Guessed,    //  correctly guessed but NOT in the correct spot, yellow
        Incorrect,  // letter should not be in word, grayed out
        Default     // letterbox has not been used yet

    }
    // Perhaps a bad name but it's a letter in a box => LetterBox
    public class LetterBox
    {

        //UI stuff
        public static readonly int LetterBoxSize = 50;
        private const float PEN_WIDTH = 2;
        private readonly Pen _defaultPen = new Pen(Color.SlateGray, PEN_WIDTH); //DarkCyan ?
        private readonly Pen _greenPen = new Pen(Color.LawnGreen, PEN_WIDTH); 
        private readonly Pen _yellowPen = new Pen(Color.Yellow, PEN_WIDTH); 
        private readonly Pen _darkPen = new Pen(Color.DarkSlateGray, PEN_WIDTH); 
        private readonly Brush _textColor = new SolidBrush(Color.Azure);
        private readonly Brush _wrongTextColor = new SolidBrush(Color.SlateGray);
        private readonly Font _font = new Font("Segoe UI",24,FontStyle.Bold);
        private readonly Rectangle _boundsRectangle;
        private static readonly StringFormat StringFormat = new StringFormat();

        //public fields
        public string Letter { get; set; }
        public bool IsSelected { get; set; }
        public LetterBoxState State { get; set; }


        static LetterBox()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }
        public LetterBox(int x, int y)
        {
            Letter = "_";
            IsSelected = false;
            State = LetterBoxState.Default;
            _boundsRectangle = new Rectangle(x, y, LetterBoxSize, LetterBoxSize);
        }

        public void Draw(Graphics g)
        {
            if (State == LetterBoxState.Positioned)
            {
                g.DrawRectangle(_greenPen, _boundsRectangle);
                if (IsSelected || !Letter.Equals("_"))
                {
                    g.DrawString(Letter, _font, _textColor, _boundsRectangle, StringFormat);
                }
            } else if (State == LetterBoxState.Guessed)
            {
                g.DrawRectangle(_yellowPen, _boundsRectangle);
                if (IsSelected || !Letter.Equals("_"))
                {
                    g.DrawString(Letter, _font, _textColor, _boundsRectangle, StringFormat);
                }
            } else if (State == LetterBoxState.Incorrect)
            {
                g.DrawRectangle(_darkPen, _boundsRectangle);
                if (IsSelected || !Letter.Equals("_"))
                {
                    g.DrawString(Letter, _font, _wrongTextColor, _boundsRectangle, StringFormat);
                }
            }
            else
            {
                g.DrawRectangle(_defaultPen, _boundsRectangle);
                if (IsSelected || !Letter.Equals("_"))
                {
                    g.DrawString(Letter, _font, _textColor, _boundsRectangle, StringFormat);
                }
            }

        }
    }
}
