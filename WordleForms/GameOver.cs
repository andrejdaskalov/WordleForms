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
    public partial class GameOver : Form
    {
        private WordleForm parent;
        public GameOver(WordleForm parent, UserScore UserScore, bool IsWin)
        {
            InitializeComponent();
            this.parent = parent;
            lblWinTimes.Text = UserScore.Wins.ToString();
            lblTotal.Text = UserScore.GamesPlayed.ToString();
            labelOver.Text = IsWin ? "You Win!" : $"You Lose, the correct word was {parent.board.CorrectWord}";
            Text = IsWin ? "You Win!" : "You Lose";
            var total = UserScore.GamesPlayed;
            
            progressBar1.Value = UserScore.NumberOfGuesses[1];
            progressBar1.Maximum = total;

            progressBar2.Value = UserScore.NumberOfGuesses[2];
            progressBar2.Maximum = total;

            progressBar3.Value = UserScore.NumberOfGuesses[3];
            progressBar3.Maximum = total;

            progressBar4.Value = UserScore.NumberOfGuesses[4];
            progressBar4.Maximum = total;

            progressBar5.Value = UserScore.NumberOfGuesses[5];
            progressBar5.Maximum = total;

            progressBar6.Value = UserScore.NumberOfGuesses[6];
            progressBar6.Maximum = total;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            parent.RestartGame();
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            parent.Close();
            Close();
        }
    }
}
