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
        private LetterBox [][] LetterGrid;
        private static int Padding = 10;
        private static int XOffset = 80;
        private static int YOffset = 100;
        private readonly Rectangle _rectangleLogo;
        private readonly Rectangle _rectangleLogoOffset;
        private static readonly StringFormat StringFormat = new StringFormat();

        //private PlayBoard _board;
        private LinkedList<LinkedList<LetterBox>> wordList;
        private LinkedListNode<LetterBox> _currentLetter;
        private LinkedListNode<LinkedList<LetterBox>> _currentWord;

        static Form1()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }

        public Form1()
        {
            InitializeComponent();
            Width = 470;
            Height = 570;
            _rectangleLogo = new Rectangle(0, 0, Width, 100);
            _rectangleLogoOffset = new Rectangle(2, 2, Width, 100);
            LetterGrid = ConstructGrid();
            CreateLists();
            _currentWord = wordList.First;
            _currentLetter = _currentWord.Value.First;
            _currentLetter.Value.IsSelected = true;
            DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(255,1,8,18));
            g.DrawString("Wordle Forms", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), new SolidBrush(Color.DarkCyan), _rectangleLogoOffset, StringFormat);
            g.DrawString("Wordle Forms", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), new SolidBrush(Color.Azure), _rectangleLogo, StringFormat);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    LetterGrid[i][j].Draw(g);
                }
            }
        }

        private LetterBox[][] ConstructGrid()
        {
            var LetterGrid = new LetterBox[6][];
            for (int i = 0; i < 6; i++)
            {
                LetterGrid[i] = new LetterBox[5];

                for (int j = 0; j < 5; j++)
                {
                    LetterGrid[i][j] = new LetterBox(j * LetterBox.LetterBoxSize + (j + 1) * Padding + XOffset,
                        i * LetterBox.LetterBoxSize + (i + 1) * Padding + YOffset);
                }
            }
            return LetterGrid;
        }

        /// <summary>
        /// Takes the existing LetterBox grid and connects the appropriate Next and Previous connections, as well as creates the Word and Letter structure
        /// </summary>
        private void CreateLists()
        {
            wordList = new LinkedList<LinkedList<LetterBox>>();
            for (int i = 0; i < 6; i++)
            {
                LinkedList<LetterBox> letterList = new LinkedList<LetterBox>();
                for (int j = 0; j < 5; j++)
                {
                    letterList.AddLast(LetterGrid[i][j]);
                }
                wordList.AddLast(letterList);
            }
        }

        private bool KeyIsValid(Keys KeyCode)
        {
            return KeyCode == Keys.A || KeyCode == Keys.B || KeyCode == Keys.C || KeyCode == Keys.D ||
                   KeyCode == Keys.E ||
                   KeyCode == Keys.F || KeyCode == Keys.G || KeyCode == Keys.H || KeyCode == Keys.I ||
                   KeyCode == Keys.J ||
                   KeyCode == Keys.K || KeyCode == Keys.L || KeyCode == Keys.M || KeyCode == Keys.N ||
                   KeyCode == Keys.O ||
                   KeyCode == Keys.P || KeyCode == Keys.Q || KeyCode == Keys.R || KeyCode == Keys.S ||
                   KeyCode == Keys.T ||
                   KeyCode == Keys.U || KeyCode == Keys.V || KeyCode == Keys.W || KeyCode == Keys.X ||
                   KeyCode == Keys.Y ||
                   KeyCode == Keys.Z;

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            //backspace
            if (e.KeyCode == Keys.Back)
            {
                
                if (_currentLetter.Previous != null)
                {
                    if (_currentLetter.Next == null && !_currentLetter.Value.Letter.Equals("_"))
                    {
                        _currentLetter.Value.Letter = "_";
                        _currentLetter.Value.IsSelected = true;
                    }
                    else
                    {
                        _currentLetter.Value.IsSelected = false;
                        _currentLetter = _currentLetter.Previous;
                        _currentLetter.Value.Letter = "_";
                        _currentLetter.Value.IsSelected = true;
                    }
                }
                Invalidate();
                return;
            }
            //any other key
            if (_currentLetter.Value.Letter.Equals("_") && KeyIsValid(e.KeyCode))
            {
                string key = "" + (char)e.KeyValue;
                _currentLetter.Value.Letter = key;
                _currentLetter.Value.IsSelected = false;
                if (_currentLetter.Next != null)
                {
                    _currentLetter = _currentLetter.Next;
                    _currentLetter.Value.IsSelected = true;
                }
            }
            //enter key action
            if (e.KeyCode == Keys.Enter)
            {
                //add code here to check word
                if (_currentWord.Next != null)
                {
                    _currentLetter.Value.IsSelected = false;
                    _currentWord = _currentWord.Next;
                    _currentLetter = _currentWord.Value.First;
                    _currentLetter.Value.IsSelected = true;
                }
            }

            Invalidate();
        }
    }
}
