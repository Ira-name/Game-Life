using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace житя
{
    public partial class Form1 : Form
    {
        private const int CellSize = 10;
        private const int GridWidth = 60;
        private const int GridHeight = 40;
        private bool[,] grid;
        private Timer timer;
        private bool isAuthenticated;
        
        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeTimer();
        }

        private void InitializeGrid()
        {
            grid = new bool[GridWidth, GridHeight];
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 100; 
            timer.Tick += timer1_Tick;
        }
        private void UpdateGrid()
        {
            bool[,] newGrid = new bool[GridWidth, GridHeight];

            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);

                    if (grid[x, y])
                    {
                        if (liveNeighbors < 2 || liveNeighbors > 3)
                        {
                            newGrid[x, y] = false;
                        }
                        else
                        {
                            newGrid[x, y] = true;
                        }
                    }
                    else
                    {
                        if (liveNeighbors == 3)
                        {
                            newGrid[x, y] = true;
                        }
                        else
                        {
                            newGrid[x, y] = false;
                        }
                    }
                }
            }

            grid = newGrid;
        }
        private int CountLiveNeighbors(int x, int y)
        {
            int count = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < GridWidth && j >= 0 && j < GridHeight && !(i == x && j == y))
                    {
                        if (grid[i, j])
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isAuthenticated) 
            {
                UpdateGrid();
                gridPanel.Refresh();
            }
            else
            {
                MessageBox.Show("Спочатку потрібно авторизуватися!");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            gridPanel.Size = new Size(GridWidth * CellSize, GridHeight * CellSize);
        }
       

        private void gridPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.Black);
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (grid[x, y])
                    {
                        Brush brush = Brushes.Red;
                        graphics.FillRectangle(brush, x * CellSize, y * CellSize, CellSize, CellSize);
                    }
                }
            }
        }

        private void gridPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / CellSize;
            int y = e.Y / CellSize;

            if (x >= 0 && x < GridWidth && y >= 0 && y < GridHeight)
            {
                grid[x, y] = !grid[x, y];
                gridPanel.Refresh();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (isAuthenticated) 
            {
                timer.Start();
            }
            else
            {
                MessageBox.Show("Спочатку потрібно авторизуватися!");
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearGrid();
            gridPanel.Refresh();
        }
        private void ClearGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    grid[x, y] = false;
                }
            }
        }

        private void authenticationButton_Click(object sender, EventArgs e)
        {
            using (AuthenticationForm authForm = new AuthenticationForm())
            {
                DialogResult result = authForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    isAuthenticated = true;
                }
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.SetTextBoxText1("Довідка.txt");
            helpForm.SetTextBoxText2("Користування програмою.txt");
            helpForm.Show();
        }
     }
    
   }
 
   

