﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordleForms.Properties;

namespace WordleForms
{
    internal class Board
    {
        private static int Padding = 10;
        private static int XOffset = 80;
        private static int YOffset = 100;

        private WordleForm form;

        public LetterBox[][] LetterGrid;
        public LinkedList<LinkedList<LetterBox>> WordsOnTable;
        public LinkedListNode<LetterBox> CurrentLetter;
        public LinkedListNode<LinkedList<LetterBox>> CurrentWord;
        public string CorrectWord;
        public List<string> WordList;
        public List<string> AnswerList;
        public int NumGuesses;

        public Board(WordleForm form)
        {
            this.form = form;

            LetterGrid = ConstructGrid();
            CreateLists();
            CurrentWord = WordsOnTable.First;
            CurrentLetter = CurrentWord.Value.First;
            CurrentLetter.Value.IsSelected = true;
            if (GetAnswerList())
            {
                CorrectWord = PickWord();
            }

            GetWordList();
            NumGuesses = 0;
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
            WordsOnTable = new LinkedList<LinkedList<LetterBox>>();
            for (int i = 0; i < 6; i++)
            {
                LinkedList<LetterBox> letterList = new LinkedList<LetterBox>();
                for (int j = 0; j < 5; j++)
                {
                    letterList.AddLast(LetterGrid[i][j]);
                }
                WordsOnTable.AddLast(letterList);
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
            using (stream = new MemoryStream(Resources.wordlist))
            {
                WordList = (List<string>)formatter.Deserialize(stream);
            }

            return WordList != null;
        }

        /// <summary>
        /// This method deserializes the answerlist stored in the wordlist.bin, created by the WordListProcessor script. The .bin contains a List<string>.
        ///
        /// </summary>
        private bool GetAnswerList()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream;
            using (stream = new MemoryStream(Resources.answerlist))
            {
                AnswerList = (List<string>)formatter.Deserialize(stream);
            }
            return AnswerList != null;
        }

        private string PickWord()
        {
            Random random = new Random();
            var index = random.Next(0, AnswerList.Count);
            return AnswerList.ElementAt(index);
        }

        public void ProcessWord()
        {
            int i = 0;
            int correctLetters = 0;
            StringBuilder sb = new StringBuilder(CorrectWord);
            foreach (var letterBox in CurrentWord.Value)
            {
                if (CorrectWord.Contains(letterBox.Letter.ToLower()))
                {
                    letterBox.State = LetterBoxState.Guessed;
                    if (sb.ToString().IndexOf(letterBox.Letter.ToLower()) == i)
                    {
                        letterBox.State = LetterBoxState.Positioned;
                        correctLetters++;
                        if (correctLetters == 5)
                        {
                            form.GameWon();
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

        public string CollectWord()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var letter in CurrentWord.Value)
            {
                sb.Append(letter.Letter);
            }

            return sb.ToString().ToLower();
        }


    }
}