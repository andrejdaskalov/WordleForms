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
        private LinkedList<LinkedList<LetterBox>> _wordsOnTable;
        private LinkedListNode<LetterBox> _currentLetter;
        private LinkedListNode<LinkedList<LetterBox>> _currentWord;
        private string _correctWord;
        private List<string> _wordList;
        private int _numGuesses;

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
            _currentWord = _wordsOnTable.First;
            _currentLetter = _currentWord.Value.First;
            _currentLetter.Value.IsSelected = true;
            if (GetWordList())
            {
                _correctWord = PickWord();
            }
            _numGuesses = 0;

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


        /// <summary>
        /// This method deserializes the wordlist stored in the wordlist.bin, created by the WordListProcessor script. The .bin contains a List<string>.
        ///
        /// </summary>
        private bool GetWordList()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream;
            using (stream = File.OpenRead(@"C:\Users\Andrej\Documents\FINKI\vizuelno programiranje\WordleForms\WordListProcessor\wordlist.bin"))
            {
                _wordList = (List<string>)formatter.Deserialize(stream);
            }
            return _wordList != null;
        }

        private string PickWord()
        {
            Random random = new Random();
            var index = random.Next(0, _wordList.Count);
            return _wordList.ElementAt(index);
        }

        public string CollectWord()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var letter in _currentWord.Value)
            {
                sb.Append(letter.Letter);
            }

            return sb.ToString().ToLower();
        }

        public void ProcessWord()
        {
            int i = 0;
            int correctLetters = 0;
            StringBuilder sb = new StringBuilder(_correctWord);
            foreach (var letterBox in _currentWord.Value)
            {
                if (_correctWord.Contains(letterBox.Letter.ToLower()))
                {
                    letterBox.State = LetterBoxState.Guessed;
                    if (sb.ToString().IndexOf(letterBox.Letter.ToLower()) == i)
                    {
                        letterBox.State = LetterBoxState.Positioned;
                        correctLetters++;
                        if (correctLetters == 5)
                        {
                            GameWon();
                        }
                    }

                    sb.Replace(letterBox.Letter, "-", i, 1);
                }
                else
                {
                    letterBox.State = LetterBoxState.Incorrect;
                }
                i++;
            }
        }

        private void GameWon()
        {
            DialogResult message = MessageBox.Show("You won. Nice work! Would you like to try again?", "You win!", MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);
            if (message == DialogResult.Yes)
            {
                new Form1().Show();
                //TODO:this doesn't work

            }
            Close();
        }
        private void GameOver()
        {
            DialogResult message = MessageBox.Show($"You ran out of tries before guessing correctly. The correct word was {_correctWord}. Would you like to try again?", "Game Over", MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);
            if (message == DialogResult.Yes)
            {
                new Form1().Show();
                //TODO:this doesn't work

            }
            Close();
        }

        // private bool WordInList(string word)
        // {
        //     return _wordList.Contains(word);
        // }

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
            _wordsOnTable = new LinkedList<LinkedList<LetterBox>>();
            for (int i = 0; i < 6; i++)
            {
                LinkedList<LetterBox> letterList = new LinkedList<LetterBox>();
                for (int j = 0; j < 5; j++)
                {
                    letterList.AddLast(LetterGrid[i][j]);
                }
                _wordsOnTable.AddLast(letterList);
            }
        }

        private bool KeyIsValid(Keys KeyCode)
        {
            return KeyCode >= Keys.A && KeyCode <= Keys.Z;

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
            if (e.KeyCode == Keys.Enter && _currentLetter.Next == null)
            {

                var word = CollectWord();
                if (word != null && word.Length == 5)
                {
                    if (! _wordList.Contains(word))
                    {
                        DialogResult message = MessageBox.Show("Not a valid word", "Invalid word", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);//TODO: make it so that enter is ignored while dismissing dialog
                        return;
                    }

                    ProcessWord();

                    _numGuesses++;
                    if (_numGuesses >= 6)
                    {
                        GameOver();
                    }
                }
                
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
