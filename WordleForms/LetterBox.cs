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

        public char Letter { get; set; }
        public bool IsSelected { get; set; }
        public LetterBoxState State { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public LetterBox(int x, int y)
        {
            Letter = '_';
            IsSelected = false;
            State = LetterBoxState.Default;
            X = x;
            Y = y;
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(pen, X, Y,LetterBoxSize,LetterBoxSize);
        }
    }
}
