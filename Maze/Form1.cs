using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maze
{
    public partial class Form1 : Form
    {
        private Labirint l;
        private int playerX;
        private int playerY;
        private int playerMedals;
        private int playerHealth = 100;
        private Random random;
        public Form1()
        {
            random = new Random();
            InitializeComponent();
            Options();
            StartGame();
        }

        public void Options()
        {
            //Text = "Maze";
            Text = "Медалі: " + playerMedals.ToString() + " HP: " + playerHealth.ToString();
            BackColor = Color.FromArgb(255, 92, 118, 137);

            int sizeX = 40;
            int sizeY = 20;

            Width = sizeX * 16 + 16;
            Height = sizeY * 16 + 40;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame() 
        {
            l = new Labirint(this, 40, 20);
            playerX = 0;
            playerY = 2;
            l.Show();
        }

        public void RefreshStats()
        {
            Text = "Медалі: " + playerMedals.ToString() + " HP: " + playerHealth.ToString();
        }

        private void Win(string winMsg)
        {
            MessageBox.Show("Перемога!\nПоки що тільки в грі, але скоро буде і у дійсності!", winMsg);
            Close();
        }

        private void MovePlayer(int newPosX, int newPosY)
        { 
            if (l.maze[newPosY, newPosX].type == MazeObject.MazeObjectType.WALL)
                return;

            if(l.maze[newPosY, newPosX].type == MazeObject.MazeObjectType.HALL)
            {
                if(l.maze[playerY, playerX].type != MazeObject.MazeObjectType.AIDKIT)
                    l.maze[playerY, playerX] = new MazeObject(MazeObject.MazeObjectType.HALL);

                l.maze[newPosY, newPosX] = new MazeObject(MazeObject.MazeObjectType.CHAR);
                l.images[playerY, playerX].BackgroundImage = l.maze[playerY, playerX].texture;
                l.images[newPosY, newPosX].BackgroundImage = l.maze[newPosY, newPosX].texture;
                playerX = newPosX;
                playerY = newPosY;
            }

            if (l.maze[newPosY, newPosX].type == MazeObject.MazeObjectType.FINISH)
                Win(" Ви пройшли лабіринт!");

            if(l.maze[newPosY, newPosX].type == MazeObject.MazeObjectType.MEDAL)
            {
                playerMedals++;
                RefreshStats();
                if (playerMedals == l.medalCount)
                    Win(" Ви зібрали всі медалі!");

                if (l.maze[playerY, playerX].type != MazeObject.MazeObjectType.AIDKIT)
                    l.maze[playerY, playerX] = new MazeObject(MazeObject.MazeObjectType.HALL);

                l.maze[newPosY, newPosX] = new MazeObject(MazeObject.MazeObjectType.CHAR);
                l.images[playerY, playerX].BackgroundImage = l.maze[playerY, playerX].texture;
                l.images[newPosY, newPosX].BackgroundImage = l.maze[newPosY, newPosX].texture;
                playerX = newPosX;
                playerY = newPosY;
            }

            if(l.maze[newPosY, newPosX].type == MazeObject.MazeObjectType.ENEMY)
            {
                int damage = random.Next(20, 25);
                if (damage > playerHealth)
                {
                    MessageBox.Show("Вас вбили кляті орки(\nАле нащастя це тільки в грі!");
                    Close();
                }
                else
                {
                    playerHealth -= damage;
                    RefreshStats();
                }

                if (l.maze[playerY, playerX].type != MazeObject.MazeObjectType.AIDKIT)
                    l.maze[playerY, playerX] = new MazeObject(MazeObject.MazeObjectType.HALL);

                l.maze[newPosY, newPosX] = new MazeObject(MazeObject.MazeObjectType.CHAR);
                l.images[playerY, playerX].BackgroundImage = l.maze[playerY, playerX].texture;
                l.images[newPosY, newPosX].BackgroundImage = l.maze[newPosY, newPosX].texture;
                playerX = newPosX;
                playerY = newPosY;
            }

            if(l.maze[newPosY, newPosX].type == MazeObject.MazeObjectType.AIDKIT)
            {
                if(playerHealth == 100)
                {
                    l.images[newPosY, newPosX].BackgroundImage = l.maze[playerY, playerX].texture;

                    l.maze[playerY, playerX] = new MazeObject(MazeObject.MazeObjectType.HALL);
                    l.images[playerY, playerX].BackgroundImage = l.maze[playerY, playerX].texture;

                    playerX = newPosX;
                    playerY = newPosY;
                }
                else if (playerHealth + 5 > 100)
                {
                    playerHealth = 100;
                    RefreshStats();
                    l.maze[playerY, playerX] = new MazeObject(MazeObject.MazeObjectType.HALL);
                    l.maze[newPosY, newPosX] = new MazeObject(MazeObject.MazeObjectType.CHAR);
                    l.images[playerY, playerX].BackgroundImage = l.maze[playerY, playerX].texture;
                    l.images[newPosY, newPosX].BackgroundImage = l.maze[newPosY, newPosX].texture;
                    playerX = newPosX;
                    playerY = newPosY;
                }
                else
                {
                    playerHealth += 5;
                    RefreshStats();
                    l.maze[playerY, playerX] = new MazeObject(MazeObject.MazeObjectType.HALL);
                    l.maze[newPosY, newPosX] = new MazeObject(MazeObject.MazeObjectType.CHAR);
                    l.images[playerY, playerX].BackgroundImage = l.maze[playerY, playerX].texture;
                    l.images[newPosY, newPosX].BackgroundImage = l.maze[newPosY, newPosX].texture;
                    playerX = newPosX;
                    playerY = newPosY;
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // нові координати гравця
            int newPosX = playerX;
            int newPosY = playerY;

            // встановлюємо нові значення
            switch (e.KeyData)
            {
                case Keys.Up:
                    newPosX = playerX;
                    newPosY = playerY - 1;
                    break;
                case Keys.Down:
                    newPosX = playerX;
                    newPosY = playerY + 1;
                    break;
                case Keys.Right:
                    newPosX = playerX + 1;
                    newPosY = playerY;
                    break;
                case Keys.Left:
                    // встановлюємо нові значення тільки якщо гравець не виходить за вікно ліворуч
                    if (playerX - 1 >= 0)
                    {
                        newPosX = playerX - 1;
                        newPosY = playerY;
                    }
                    break;
            }

            MovePlayer(newPosX, newPosY);
        }
    }
}
