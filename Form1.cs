using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Ship
{
    public partial class Form1 : Form
    {
        public const int mapSize = 11;
        public const int cellSize = 30;
        public string alphabet = "РЕСПУБЛИКА";

        private int[,] _myMap = new int[mapSize, mapSize];
        private int[,] _enemyMap = new int[mapSize, mapSize];

        private Button[,] _myButtons = new Button[mapSize, mapSize];
        private Button[,] _enemyButtons = new Button[mapSize, mapSize];

        public bool isPlaying = false;

        public Bot bot;

        public Form1()
        {       
            this.BackgroundImage = Image.FromFile(@"..\..\images\images_3.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            InitializeComponent();
            this.Text = "Battle ship";
            Init();
        }
        public void Init()
        {
            isPlaying = false;
            CreateMap();
            bot = new Bot(_enemyMap, _myMap, _enemyButtons, _myButtons);
            _enemyMap = bot.ConfigureShips();
        }
        public void InitRestart()
        {
            isPlaying = false;
            ReStart(ref _myButtons, ref _enemyButtons, ref _myMap, ref _enemyMap);
            bot = new Bot(_enemyMap, _myMap, _enemyButtons, _myButtons);
            _enemyMap = bot.ConfigureShips();
        }
        public void CreateMap()
        {
            this.Width = (mapSize+1) * 2 * cellSize + 50;
            this.Height = (mapSize+2) * cellSize+45;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    _myMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(23 + j * cellSize, 25 + i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    button.Font = new Font("Ravie", 8);
                    button.ForeColor = Color.Azure;


                    if (j==0 || i==0)
                    {
                        button.BackColor = Color.DarkGreen;
                        if(i==0 && j!=0)
                        {
                            button.Text = alphabet[j-1].ToString(); 
                        }
                        if (j == 0 && i!=0)
                            {
                                button.Text = i.ToString();
                            }
                    }
                    else
                    { 
                        button.Click += new EventHandler(ConfigureShips);
                    }
                    _myButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    _myMap[i, j] = 0;
                    _enemyMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(400+j * cellSize, 25+i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    button.Font = new Font("Ravie", 8);
                    button.ForeColor = Color.Azure;

                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.DarkRed;
                        if (i == 0 && j != 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i != 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler(PlayerShoot);
                    }
                    _enemyButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.BackColor = Color.Transparent;
            map1.Text = "Player Map";
            map1.Size = new Size(5 * cellSize, 30);
            map1.ForeColor = Color.ForestGreen;
            map1.Font = new Font("Ravie", 13);
            map1.Location = new Point(mapSize * cellSize / 2 - 40, 0);
            this.Controls.Add(map1);

            Label map2 = new Label();
            map2.BackColor = Color.Transparent;
            map2.Text = "Enemy Map";
            map2.ForeColor = Color.Red;
            map2.Font = new Font("Ravie", 13);
            map2.Size = new Size(5 * cellSize, 30);
            map2.Location = new Point(340+mapSize * cellSize / 2, 0);
            this.Controls.Add(map2);
            
            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += new EventHandler(Start);
            startButton.Location = new Point((this.Width/2)- 80,mapSize*cellSize+30);
            startButton.Font = new Font("Ravie", 13);
            startButton.Size = new Size(5 * cellSize, 30);
            this.Controls.Add(startButton);
        }
        public void Start(object sender,EventArgs e)
        {
            if (CheckIfMyMapIsNotEmpty())
                isPlaying = true;
            else
                MessageBox.Show("Please, place your ships");
        }

        public void ReStart (ref Button[,] myButton, ref Button[,] enemyButton, ref int[,] myMap,ref int [,] enemyMap)
        {
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++) 
                {

                    myButton[i, j].BackColor = Color.White;
                    myButton[i, j].Text = "";
                    enemyButton[i, j].BackColor = Color.White;
                    enemyButton[i, j].Text = "";
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;
                }
            }
        }

        public bool CheckIfMyMapIsNotEmpty()
        {
            bool isEmpty = true;
            for (int i = 1; i < mapSize; i++)
            {
                for (int j = 1; j < mapSize; j++)
                {
                    if (_myMap[i, j] != 0)
                        isEmpty = false;

                }
            }
            if (isEmpty)
                return false;
            else return true;
        }
        public bool CheckIfEnemyMapIsNotEmpty()
        {
            bool isEmpty = true;
            for (int i = 1; i < mapSize; i++)
            {
                for (int j = 1; j < mapSize; j++)
                {
                    if (_enemyMap[i, j] != 0)
                        isEmpty = false;

                }
            }
            if (isEmpty)
                return false;
            else return true;
        }
        //public bool CheckIfMapIsNotEmpty()
        //{
        //    if (CheckIfMyMapIsNotEmpty() || CheckIfEnemyMapIsNotEmpty())
        //        return false;
        //    else return true;
        //}

        public void ConfigureShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (!isPlaying)
            {
                if (_myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
                {
                    pressedButton.BackColor = Color.Green;
                    _myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
                }
                else
                {
                    pressedButton.BackColor = Color.White;
                    _myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
                }
            }
        }
        public void PlayerShoot(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            bool playerTurn = Shoot(ref _enemyMap, pressedButton);
            if (!playerTurn)
                bot.Shoot();

            if (!CheckIfMyMapIsNotEmpty())
            {
                MessageBox.Show("You lose!");
                //this.Controls.Clear();
                InitRestart();
            }
            else if (!CheckIfEnemyMapIsNotEmpty())
            {
                MessageBox.Show("You Win!");
                //this.Controls.Clear();
                InitRestart();
            }
        }

        public bool Shoot(ref int[,] map, Button pressedButton)
        {
            bool hit = false;
            if (isPlaying)
            {
                int delta = 0;
                if (pressedButton.Location.X > 400)
                    delta = 400;
                if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X-delta) / cellSize] != 0)
                {
                    hit = true;

                    pressedButton.BackColor = Color.Red;
                    pressedButton.Text = "X";
                    pressedButton.ForeColor = Color.Black;
                    map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = 0;
                }
                else if(pressedButton.BackColor != Color.Red)
                {

                    pressedButton.BackColor = Color.DarkGray;
                }
            }
            return hit;
        }
    } 
}
