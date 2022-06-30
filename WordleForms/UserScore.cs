using System;
using System.Collections.Generic;

namespace WordleForms
{
    [Serializable]
    public class UserScore
    {
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public Dictionary<int,int> NumberOfGuesses { get; set; }

        public UserScore()
        {
            GamesPlayed = 0;
            Wins = 0;
            NumberOfGuesses = new Dictionary<int,int>();
            for (int i = 1; i <= 6; i++)
            {
                NumberOfGuesses[i] = 0;
            }
        }

        public float GetWinPercentage()
        {
            return Wins / GamesPlayed * 100;
        }

    }
}