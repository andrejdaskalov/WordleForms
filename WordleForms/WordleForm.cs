using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordleForms.Properties;

namespace WordleForms
{
    public partial class WordleForm : Form
    {
 

        private static int Padding = 10;
        private static int XOffset = 80;
        private static int YOffset = 100;
        private readonly Rectangle _rectangleLogo;
        private readonly Rectangle _rectangleLogoOffset;
        private static readonly StringFormat StringFormat = new StringFormat();
        public Board Board;
        public VirtualKeyboard virtualKeyboard;

        static WordleForm()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }

        public WordleForm()
        {
            InitializeComponent();
            Width = 470;
            Height = 700;
            statusStrip1.BackColor = Color.FromArgb(255, 1, 5, 15);
            toolStrip1.BackColor = Color.FromArgb(255, 1, 8, 18);
            toolStrip1.ForeColor = Color.FromArgb(255, 1, 8, 18);
            _rectangleLogo = new Rectangle(0, 0, Width, 100);
            _rectangleLogoOffset = new Rectangle(2, 2, Width, 100);
            Board = new Board(this);
            virtualKeyboard = new VirtualKeyboard();
            DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(255, 1, 8, 18));
            g.DrawString("Wordle Forms", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), new SolidBrush(Color.DarkCyan), _rectangleLogoOffset, StringFormat);
            g.DrawString("Wordle Forms", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), new SolidBrush(Color.Azure), _rectangleLogo, StringFormat);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Board.LetterGrid[i][j].Draw(g);
                }
            }
            virtualKeyboard.Draw(g);
        }

        public void GameWon()
        {
            Invalidate();
            new GameOver(this, Board.UserScore, true).ShowDialog();
        }
        public void GameOver()
        {
            Invalidate();
            new GameOver(this, Board.UserScore, false).ShowDialog();
        }

        public void RestartGame()
        {
            Board = new Board(this);
            virtualKeyboard = new VirtualKeyboard();
            Invalidate();
        }


        private bool KeyIsValid(Keys KeyCode)
        {
            return KeyCode >= Keys.A && KeyCode <= Keys.Z;

        }

        private void DeleteLetter()
        {
            if (Board.CurrentLetter.Previous != null)
            {
                if (Board.CurrentLetter.Next == null && !Board.CurrentLetter.Value.Letter.Equals("_"))
                {
                    Board.CurrentLetter.Value.Letter = "_";
                    Board.CurrentLetter.Value.IsSelected = true;
                }
                else
                {
                    Board.CurrentLetter.Value.IsSelected = false;
                    Board.CurrentLetter = Board.CurrentLetter.Previous;
                    Board.CurrentLetter.Value.Letter = "_";
                    Board.CurrentLetter.Value.IsSelected = true;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            gameMessageLabel.Text = "";
            
            //backspace
            if (e.KeyCode == Keys.Back)
            {
                DeleteLetter();
                Invalidate();
                return;
            }
            //any other key
            if (Board.CurrentLetter.Value.Letter.Equals("_") && KeyIsValid(e.KeyCode))
            {
                string key = "" + (char)e.KeyValue;
                EnterLetter(key);
            }
            //enter key action
            if (e.KeyCode == Keys.Enter && Board.CurrentLetter.Next == null)
            {
                var word = Board.CollectWord();
                if (EnterWord(word)) return;
            }

            Invalidate();
        }

        private bool EnterWord(string word)
        {
            if (word != null && word.Length == 5)
            {
                if (!Board.WordList.Contains(word))
                {
                    gameMessageLabel.Text = "Not a valid word!";
                    return true;
                }

                Board.ProcessWord();

                Board.NumGuesses++;
                if (Board.NumGuesses >= 6)
                {
                    Board.SaveUserScore();
                    GameOver();
                    return true;
                }
            }

            if (Board.CurrentWord.Next != null)
            {
                Board.CurrentLetter.Value.IsSelected = false;
                Board.CurrentWord = Board.CurrentWord.Next;
                Board.CurrentLetter = Board.CurrentWord.Value.First;
                Board.CurrentLetter.Value.IsSelected = true;
            }

            return false;
        }

        private void EnterLetter(string key)
        {
            Board.CurrentLetter.Value.Letter = key;
            Board.CurrentLetter.Value.IsSelected = false;
            if (Board.CurrentLetter.Next != null)
            {
                Board.CurrentLetter = Board.CurrentLetter.Next;
                Board.CurrentLetter.Value.IsSelected = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new Tutorial().ShowDialog();
        }

        private void WordleForm_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (virtualKeyboard.BackKey.Bounds.Contains(p))
            {
                DeleteLetter();
                Invalidate();
                return;
            }

            if (virtualKeyboard.EnterKey.Bounds.Contains(p))
            {
                var word = Board.CollectWord();
                EnterWord(word);
                Invalidate();
                return;
            }
            for (int i = 0; i < virtualKeyboard.KeyMatrix.Length; i++)
            {
                for (int j = 0; j < virtualKeyboard.KeyMatrix[i].Length; j++)
                {
                    if (virtualKeyboard.KeyMatrix[i][j].Bounds.Contains(p))
                    {
                        EnterLetter(virtualKeyboard.KeyMatrix[i][j].Content.ToUpper());
                    }
                }
            }
            Invalidate();
        }

        private void WordleForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.isFirstTime)
            {
                Properties.Settings.Default.isFirstTime = false;
                Properties.Settings.Default.Save();
                new Tutorial().ShowDialog();
            }
        }
    }
}
