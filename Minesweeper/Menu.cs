using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        void NewGame()
        {
            Controls.Clear();
            Controls.Add(menuStrip1);
            Controls.Add(label1);
            Controls.Add(lblMinesCounter);
            isFirstClick = true;
            minesCount = 0;
            openFieldsCount = 0;
            GenerateGame(width, height, difficulty, maxMines);
            timer1.Stop();
            timer = new DateTime(0, 0);
            label1.Text = "Time:";
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            width = 9;
            height = 9;
            difficulty = 11;
            maxMines = 10;
            NewGame();
            this.Size = new Size(350, 410);
        }
        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            width = 16;
            height = 16;
            difficulty = 15;
            maxMines = 40;
            NewGame();
            this.Size = new Size(595, 650);
        }
        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            width = 30;
            height = 16;
            difficulty = 20;
            maxMines = 99;
            NewGame();
            this.Size = new Size(1080, 650);
        }
        private void autoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGame();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About formAbout = new About();
            formAbout.Show();
        }

    }
}
