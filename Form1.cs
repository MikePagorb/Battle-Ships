﻿using System;
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
        public int[,] myMap = new int[mapSize, mapSize];
        public int[,] enemyMap = new int[mapSize, mapSize];
        public bool isPlaying = false;

        public Button[,] myButtons = new Button[mapSize, mapSize];
        public Button[,] enemyButtons = new Button[mapSize, mapSize];


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
        }
        public void CreateMap()
        {
            this.Width = (mapSize+1) * 2 * cellSize + 50;
            this.Height = (mapSize+2) * cellSize+45;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(23+j * cellSize, 25+i * cellSize);
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
                    myButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }

            Label map1 = new Label();
            map1.BackColor = Color.Transparent;
            map1.Text = "Player Map";
            map1.Size = new Size(5*cellSize, 30);
            map1.ForeColor = Color.ForestGreen;
            map1.Font = new Font("Ravie", 13);
            map1.Location=new Point(mapSize*cellSize/2-40,0);
            this.Controls.Add(map1);

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

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
                    enemyButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
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
            isPlaying = true;
        }
        public void ConfigureShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (!isPlaying)
            {
                if (myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
                {
                    pressedButton.BackColor = Color.Green;
                    myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
                }
                else
                {
                    pressedButton.BackColor = Color.White;
                    myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
                }
            }
        }
        public void PlayerShoot(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            Shoot(enemyMap, pressedButton);
        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            bool hit = false;
            if (isPlaying)
            {
                int delta = 0;
                if (pressedButton.Location.X > 400)
                    delta = 400;
                if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X-400) / cellSize] != 0)
                {
                    hit = true;

                    pressedButton.BackColor = Color.Red;
                    pressedButton.Text = "X";
                    pressedButton.ForeColor = Color.Black;
                }
                else 
                {
                    pressedButton.BackColor = Color.DarkGray;
                }
            }
            return hit;
        }
    } 
}
