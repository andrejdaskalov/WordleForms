using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleForms
{
    public class Key
    {
        public string Content { get; set; }
        public Rectangle Bounds { get; set; }
        public static int Width = 25;
        public static int Height = 35;
        public int fontSize { get; set; }
        private static Brush Brush = new SolidBrush(Color.FromArgb(255,69,68,74));
        private static Brush GreenBrush = new SolidBrush(Color.LawnGreen);
        private static Brush YellowBrush = new SolidBrush(Color.Yellow);
        private static Brush GrayBrush = new SolidBrush(Color.FromArgb(255, 69, 68, 74));

        private static readonly StringFormat StringFormat = new StringFormat();
        private Font _font ;
        private readonly Brush _textColor = new SolidBrush(Color.White);
        private readonly Brush _blackTextColor = new SolidBrush(Color.Black);
        public LetterBoxState State { get; set; }


        static Key()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Constructor for Key with given Content, x and y
        /// </summary>
        /// <param name="content">the text inside the key</param>
        /// <param name="x">x coordinate of position</param>
        /// <param name="y"> y coordinate of position</param>
        

        public Key(string content, int x, int y)
        {
            Content = content;
            Bounds = new Rectangle(x, y, Width, Height);
            State = LetterBoxState.Default;
        }

        public Key(string content, int fontsize = 18)
        {
            Content = content;
            State = LetterBoxState.Default;
            //fontSize = 18;
            this.fontSize = fontsize;
            _font = new Font("Segoe UI", fontSize, FontStyle.Regular);
        }

        public void Draw(Graphics g)
        {
            if (State == LetterBoxState.Default)
            {
                g.FillRectangle(Brush, Bounds);
                g.DrawString(Content, _font, _textColor, Bounds, StringFormat);
            }
            else if (State == LetterBoxState.Positioned)
            {
                g.FillRectangle(GreenBrush,Bounds);
                g.DrawString(Content, _font, _blackTextColor, Bounds, StringFormat);
            } 
            else if (State == LetterBoxState.Guessed)
            {
                g.FillRectangle(YellowBrush,Bounds);
                g.DrawString(Content, _font, _blackTextColor, Bounds, StringFormat);
            }
            else
            {
                g.FillRectangle(GrayBrush,Bounds);
                g.DrawString(Content, _font, _blackTextColor, Bounds, StringFormat);

            }

            
        }


    }
}
