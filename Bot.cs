using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_Ship
{
    public class Bot
    {
        public int[,] myMap = new int[Form1.mapSize, Form1.mapSize];//bot`s map
        public int[,] enemyMap = new int[Form1.mapSize, Form1.mapSize];//player`s map

        public Button[,] myButtons = new Button[Form1.mapSize, Form1.mapSize];
        public Button[,] enemyButtons = new Button[Form1.mapSize, Form1.mapSize];

        public Bot(int[,] myMap, int[,] enemyMap, Button[,] myButtons, Button[,] enemyButtons)
        {
            this.myMap = myMap;
            this.enemyMap = enemyMap;
            this.enemyButtons = enemyButtons;
            this.myButtons = myButtons;
        }

        public bool IsInsideMap(int i, int j)
        {
            if (i < 0 || j < 0 || i >= Form1.mapSize || j >= Form1.mapSize)
            {
                return false;
            }
            return true;
        }

        public bool IsEmpty(int i, int j, int length)
        {
            bool isEmpty = true;

            for (int k = j; k < j + length; k++)
            {
                if (myMap[i, k] != 0)
                {
                    isEmpty = false;
                    break;
                }
            }

            return isEmpty;
        }

        public int[,] ConfigureShips()
        {
            int[,] myMap1 =
             {
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,1,0,0,0,0 },
                {0,1,0,0,0,0,0,0,0,0,0 },
                {0,1,0,0,0,1,1,1,0,0,1 },
                {0,1,0,0,0,0,0,0,0,0,1 },
                {0,1,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,1,0,0,0,1,0,0 },
                {0,0,1,0,1,0,0,0,1,0,0 },
                {0,0,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,0,0 },
                {0,0,1,0,0,0,0,0,1,0,0 }
            };
            int[,] myMap2 =
            {
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,1,0,0 },
                {0,0,0,0,0,1,0,0,0,0,1 },
                {0,0,1,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,1,0,0,0,1 },
                {0,0,0,0,0,0,0,0,0,0,1 },
                {0,0,1,0,0,1,0,0,0,0,1 },
                {0,0,1,0,0,1,0,0,0,0,1 },
                {0,0,0,0,0,1,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,1,0 },
                {0,0,0,1,1,1,0,0,0,1,0 }
            };
            int[,] myMap3 =
{
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,1,1,1,1,0,1,1,1,0,1 },
                {0,0,0,0,0,0,0,0,0,0,1 },
                {0,1,1,0,1,1,0,1,1,0,1 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,1,0 },
                {0,0,1,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 }
            };
            int[,] myMap4 =
{
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,1,0,0,1 },
                {0,0,0,0,0,1,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0 },
                {0,0,1,0,0,1,0,1,1,1,0 },
                {0,0,1,0,0,1,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,1,1,1,0,0,0,1,1,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0,1,1,0 },
                {0,0,0,0,0,1,0,0,0,0,0 }
            };
            Random r = new Random();
            int random = r.Next(1, 20);
            switch (random)
            {
                case 1:
                case 4:
                case 7:
                case 11:
                case 15:
                    myMap = myMap2;
                    break;
                case 2:
                case 5:
                case 8:
                case 14:
                case 18:
                    myMap = myMap1;
                    break;
                case 3:
                case 6: 
                case 9:
                case 13:
                case 17:
                    myMap = myMap3;
                    break;
                case 10:
                case 12:
                case 16:
                case 19:
                case 20:
                    myMap = myMap4;
                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
            return myMap;
        }


        public bool Shoot()
        {
            bool hit = false;
            Random r = new Random();

            int posX = r.Next(1, Form1.mapSize);
            int posY = r.Next(1, Form1.mapSize);

            while (enemyButtons[posX, posY].BackColor == Color.Blue || enemyButtons[posX, posY].BackColor == Color.Black)
            {
                posX = r.Next(1, Form1.mapSize);
                posY = r.Next(1, Form1.mapSize);
            }

            if (enemyMap[posX, posY] != 0)
            {
                hit = true;
                enemyMap[posX, posY] = 0;
                enemyButtons[posX, posY].BackColor = Color.Blue;
                enemyButtons[posX, posY].Text = "X";
            }
            else
            {
                hit = false;
                enemyButtons[posX, posY].BackColor = Color.Black;
            }
            if (hit)
                Shoot();
            return hit;
        }
    }
}
