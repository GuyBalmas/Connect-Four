using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectFour.Logic;

namespace ConnectFour.UI
{
    public partial class GameForm : Form
    {
        // Points array to hold the location of each token slot on the Game Form.
        private Point[,] m_Board;
        private GameEngine Connect4Engine;
        private Player nowPlaying;

        private readonly Color player1Color = Color.Red;
        private readonly Color player2Color = Color.Blue;
        private readonly Color defaultColor = Color.AntiqueWhite;
        private readonly Point nowPlayingIndicator;

        public GameForm()
        {
            InitializeComponent();
            this.m_Board = new Point[6, 7];
            Connect4Engine = new GameEngine();
            nowPlaying = Connect4Engine.GetCurrentPlayer();
            nowPlayingIndicator = new Point(8, 80);
        }

        /*
         * This Method takes a column choice by the user, 
         * and inserts the token in the chosen column if possible. 
         */
        private void playMove(int columnChoice)
        {
            if (!Connect4Engine.isWinner(nowPlaying))
            {
                int row = Connect4Engine.GetRowInColumn(columnChoice);
                Color playerColor = Color.Black;
                Color indicator = Color.Black;
                if (Connect4Engine.WhichPlayer(nowPlaying) == 1)
                {
                    playerColor = player1Color;
                    indicator = player2Color;
                }
                else if (Connect4Engine.WhichPlayer(nowPlaying) == 2)
                {
                    playerColor = player2Color;
                    indicator = player1Color;

                }

                if (Connect4Engine.AreValidArgs(row, columnChoice))
                {
                    Connect4Engine.insertColumn(nowPlaying, columnChoice);
                    fill(m_Board[row, columnChoice], playerColor);                    
                    if (Connect4Engine.isWinner(nowPlaying))
                    {
                        gameOver(playerColor);
                    }
                    nowPlaying = Connect4Engine.ChangeTurns();
                    changeColorIndicator(indicator);
                }
            }
        }

        /*
         * This method popps a Message box asking the user if he wants to play a new game.
         */
        private void gameOver(Color playerColor)
        {
            fillAll(playerColor);
            DialogResult result = MessageBox.Show("Start a new game?", "New Game Menu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Connect4Engine = new GameEngine();
                fillAll(defaultColor);
            }
            else {
                Application.Exit();
            }
        }

        /*
         * This method changes the 'Turn' token idicator displayed on the game form.
         * The indicator shows which player is currently playing.
         */
        private void changeColorIndicator(Color color)
        {
            Graphics gp = this.CreateGraphics();
            SolidBrush sb = new SolidBrush(color);
            gp.FillEllipse(sb, nowPlayingIndicator.X, nowPlayingIndicator.Y, 50, 50);
        }
        private void fillAll(Color playerColor)
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    fill(m_Board[x, y], playerColor);
                }
            }
        }

        /*
         * This method takes a point and a color, and fills a circle on panel1 with that color.
         * The method is used to draw the tokens on screen.
         */
        private void fill(Point point, Color playerColor)
        {
            Graphics g = panel1.CreateGraphics();
            SolidBrush sb = new SolidBrush(playerColor);
            g.FillEllipse(sb, point.X, point.Y, 50, 50);
        }

        private void GameForm_Load(object sender, EventArgs e)
        { 
        }

        /*
         * This Method draws all the token slots on the board previous to game start.
         * It also draws the 'Turn' indicator.
         */
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black);
            SolidBrush sb = new SolidBrush(defaultColor);
            int radius = 50;
            int spacing = 0;
            for (int y = 0; y < 7; y++)
            {
                for(int x = 0; x < 6; x++)
                {
                    Point newPoint = new Point(y * radius + spacing, x * radius);
                    m_Board[x, y] = newPoint;
                    g.DrawEllipse(p, newPoint.X, newPoint.Y, radius, radius);
                    g.FillEllipse(sb, newPoint.X, newPoint.Y, radius, radius);

                }
                spacing += 12;
            }
            SolidBrush brush = new SolidBrush(player1Color);
            Graphics gp = this.CreateGraphics();
            gp.DrawEllipse(p, nowPlayingIndicator.X, nowPlayingIndicator.Y, 50, 50);
            gp.FillEllipse(brush, nowPlayingIndicator.X, nowPlayingIndicator.Y, 50, 50);
        }

        // Button no.2
        private void button2_Click(object sender, EventArgs e)
        {
            playMove(1);
        }

        // Button no.1
        private void button1_Click_1(object sender, EventArgs e)
        {
            playMove(0);
        }

        // Button no.3
        private void button3_Click_1(object sender, EventArgs e)
        {
            playMove(2);
        }

        // Button no.4
        private void button4_Click(object sender, EventArgs e)
        {
            playMove(3);
        }

        // Button no.6
        private void button6_Click(object sender, EventArgs e)
        {
            playMove(5);
        }

        // Button no.7
        private void button1_Click(object sender, EventArgs e)
        {
            playMove(6);
        }

        // Button no.5
        private void button3_Click(object sender, EventArgs e)
        {
            playMove(4);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
