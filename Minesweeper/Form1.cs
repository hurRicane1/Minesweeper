using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        int width = 9, height = 9;
        int minesCount = 0;
        int maxMines = 10;
        int openFieldsCount = 0;
        int difficulty = 11;
        int padding = 35;
        bool isFirstClick = true;
        ButtonExtend[,] allFields;
        Random random = new Random();
        DateTime timer = new DateTime(0,  0);
        MouseEventArgs mouseArgs;
        public Form1()
        {
            InitializeComponent();
        }
        void GenerateGame(int width, int height, int diff, int maxMine)
        {
            
            allFields = new ButtonExtend[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ButtonExtend button = new ButtonExtend();
                    button.Location = new Point(x*padding+10, y*padding+45);
                    button.Size = new Size(30, 30);
                    button.isClickable = true;
                    button.isAdded = false;
                    button.FlatStyle = FlatStyle.Flat;
                    button.BackColor = Color.Silver;
                    button.value = 9;
                    button.toCheck = true;
                    if (random.Next(0, 100) <= diff && minesCount < maxMine)
                    {
                        button.isBomb = true;
                        minesCount++;
                        //button.BackgroundImage = Resource1.bomb;
                        //button.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    button.xPosition = x;
                    button.yPosition = y;
                    allFields[x, y] = button;
                    Controls.Add(button);
                    button.MouseUp += new MouseEventHandler(FieldClick);
                    lblMinesCounter.Text = $"Mines: {minesCount}";
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateGame(width, height, difficulty, maxMines);            
        }
        void FieldClick(object sender, MouseEventArgs e)
        {
            ButtonExtend button = (ButtonExtend)sender;
            if (e.Button == MouseButtons.Left && button.isClickable)
            {
                if (button.isBomb)
                {
                    if (isFirstClick)
                    {
                        button.isBomb = false;
                        minesCount--;
                        isFirstClick = false;
                        OpenRegion(button);
                    }
                    else
                    {
                        button.BackColor = Color.Red;
                        Explode();
                    }
                }
                else
                {
                    OpenRegion(button);
                }
                isFirstClick = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                button.isClickable = !button.isClickable;
                if(!button.isClickable)
                {
                    button.BackgroundImage = Resource1.flag;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.BackColor = Color.White;
                    button.value = -1;
                }
                else
                {
                    button.BackgroundImage = null;
                    button.BackgroundImageLayout = ImageLayout.None;
                    button.BackColor = Color.Silver;
                    button.value = 9;
                }
            }
            timer1.Start();
            IsTheGameWon();
        }
        bool Explode()
        {
            foreach(ButtonExtend button in allFields)
            {
                if (button.isBomb)
                {
                    button.BackgroundImage = Resource1.bomb;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            timer1.Stop();
            MessageBox.Show("You lost!");
            foreach(ButtonExtend field in allFields)
            {
                field.Enabled = false;
                field.isClickable = false;
            }
            return true;
        }

        int FindBombsAround(int xEmpty, int yEmpty)
        {
            int bombCounter = 0;
            for (int y = yEmpty-1; y <= yEmpty+1; y++)
            {
                for (int x = xEmpty-1; x <= xEmpty+1; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allFields[x, y].isBomb)
                        {
                            bombCounter++;
                        }
                    }
                }
            }
            return bombCounter;
        }
        void OpenRegion(ButtonExtend button)
        {
            Queue<ButtonExtend> emptyFields = new Queue<ButtonExtend>();
            emptyFields.Enqueue(button);
            button.isAdded = true;
            while(emptyFields.Count > 0)
            {
                ButtonExtend currentField = emptyFields.Dequeue();
                OpenField(currentField);
                openFieldsCount++;
                if (FindBombsAround(currentField.xPosition, currentField.yPosition) == 0)
                {
                    for (int y = currentField.yPosition - 1; y <= currentField.yPosition + 1; y++)
                    {
                        for (int x = currentField.xPosition - 1; x <= currentField.xPosition + 1; x++)
                        {
                            if (x >= 0 && x < width && y < height &&y>=0)
                            {
                                if (!allFields[x, y].isAdded)
                                {
                                    emptyFields.Enqueue(allFields[x, y]);
                                    allFields[x, y].isAdded = true;
                                }
                            }
                        }
                    }
                }
            } 
        }
        void OpenField(ButtonExtend field)
        {
            int bombsAround = FindBombsAround(field.xPosition, field.yPosition);
            if (bombsAround != 0)
            {
                field.Text = bombsAround.ToString();
                field.value = bombsAround;
            }
            else
            {
                field.value = 0;
            }
            field.BackColor = Color.White;
            field.Enabled = false;
            field.BackgroundImage = null;
        }
        bool IsTheGameWon()
        {
            bool isGameWon = false;
            int fields = height * width;
            int emptyFields = fields - minesCount;
            if (openFieldsCount == emptyFields)
            {
                isGameWon = true;
                timer1.Stop();

                MessageBox.Show("Congratulations! You won the game...");
                foreach (ButtonExtend button in allFields)
                {
                    if (button.isBomb)
                    {
                        if (button.value == -1)
                        {
                            button.BackColor = Color.Green;
                        }
                        button.BackgroundImage = Resource1.bomb;
                        button.BackgroundImageLayout = ImageLayout.Stretch;
                        
                    }
                }
                foreach (ButtonExtend field in allFields)
                {
                    field.Enabled = false;
                    field.isClickable = false;
                }
            }
            return isGameWon;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer = timer.AddSeconds(1);
            label1.Text = $"Time: {timer.ToString("mm:ss")}";
        }

    }
}
