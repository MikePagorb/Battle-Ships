using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Ship
{
    public partial class Form1 : Form
    {
        public const int mapSize = 11;
        public int cellSize = 30;
        public string alphabet = "РЕСПУБЛИКА";
        

        public int[,] myMap = new int[mapSize, mapSize];
        public int[,] enemyMap = new int[mapSize, mapSize];

        public Form1()
        {
            InitializeComponent();
            this.Text = "Battle ship";
            Init();
        }
        public void Init()
        {
            CreateMap();
        }
        public void CreateMap()
        {
            this.Width = (mapSize + 1)* 2 * cellSize + 50;
            this.Height = (mapSize+2) * cellSize + 45;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;
                    

                    Button button = new Button();
                    button.Location = new Point(23+j * cellSize, 23 + i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    if (j == 0 || i == 0)
                    {

                        button.BackColor = Color.Gray;
                        if (i == 0 && j!=0 )
                        {
                            button.Text = alphabet[j-1].ToString();
                        }
                        if(j==0 && i!=0)
                            button.Text = i.ToString();
                    }
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.Text = "Карта игрока";
            map1.Location = new Point(mapSize * cellSize / 2, 0);
            this.Controls.Add(map1);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(400 + j * cellSize, 23+i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    if (j == 0 || i == 0)
                    {

                        button.BackColor = Color.Gray;
                        if (i == 0 && j != 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i != 0)
                            button.Text = i.ToString();
                    }
                    this.Controls.Add(button);
                }
            }
            Label map2 = new Label();
            map2.Text = "Карта противника";
            map2.ForeColor = Color.Red;
            map2.Location = new Point(360 + mapSize*cellSize/2,0);
            this.Controls.Add(map2);

            Button startButton = new Button();
            startButton.Text = "START";
            startButton.Location = new Point(this.Width/2-45, mapSize*cellSize + 30);
            this.Controls.Add(startButton);
        }
    }
}
