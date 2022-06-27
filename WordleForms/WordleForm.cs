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
        private Board board;
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
            board = new Board(this);
            virtualKeyboard = new VirtualKeyboard();
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
                    board.LetterGrid[i][j].Draw(g);
                }
            }
            virtualKeyboard.Draw(g);
        }

        public void GameWon()
        {
            Invalidate();
            DialogResult message = MessageBox.Show("You won. Nice work! Would you like to try again?", "You win!", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
            if (message == DialogResult.Yes)
            {
                RestartGame();
            }
            else
                Close();
        }
        public void GameOver()
        {
            Invalidate();
            DialogResult message = MessageBox.Show($"You ran out of tries before guessing correctly. The correct word was {board.CorrectWord}. Would you like to try again?", "Game Over", MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);
            if (message == DialogResult.Yes)
            {
                RestartGame();
            }
            else
                Close();
        }

        private void RestartGame()
        {
            board = new Board(this);
            Invalidate();
        }


        private bool KeyIsValid(Keys KeyCode)
        {
            return KeyCode >= Keys.A && KeyCode <= Keys.Z;

        }

        private void DeleteLetter()
        {
            if (board.CurrentLetter.Previous != null)
            {
                if (board.CurrentLetter.Next == null && !board.CurrentLetter.Value.Letter.Equals("_"))
                {
                    board.CurrentLetter.Value.Letter = "_";
                    board.CurrentLetter.Value.IsSelected = true;
                }
                else
                {
                    board.CurrentLetter.Value.IsSelected = false;
                    board.CurrentLetter = board.CurrentLetter.Previous;
                    board.CurrentLetter.Value.Letter = "_";
                    board.CurrentLetter.Value.IsSelected = true;
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
            if (board.CurrentLetter.Value.Letter.Equals("_") && KeyIsValid(e.KeyCode))
            {
                string key = "" + (char)e.KeyValue;
                EnterLetter(key);
            }
            //enter key action
            if (e.KeyCode == Keys.Enter && board.CurrentLetter.Next == null)
            {
                var word = board.CollectWord();
                if (EnterWord(word)) return;
            }

            Invalidate();
        }

        private bool EnterWord(string word)
        {
            if (word != null && word.Length == 5)
            {
                if (!board.WordList.Contains(word))
                {
                    gameMessageLabel.Text = "Not a valid word!";
                    return true;
                }

                board.ProcessWord();

                board.NumGuesses++;
                if (board.NumGuesses >= 6)
                {
                    GameOver();
                    return true;
                }
            }

            if (board.CurrentWord.Next != null)
            {
                board.CurrentLetter.Value.IsSelected = false;
                board.CurrentWord = board.CurrentWord.Next;
                board.CurrentLetter = board.CurrentWord.Value.First;
                board.CurrentLetter.Value.IsSelected = true;
            }

            return false;
        }

        private void EnterLetter(string key)
        {
            board.CurrentLetter.Value.Letter = key;
            board.CurrentLetter.Value.IsSelected = false;
            if (board.CurrentLetter.Next != null)
            {
                board.CurrentLetter = board.CurrentLetter.Next;
                board.CurrentLetter.Value.IsSelected = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
