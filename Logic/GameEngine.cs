using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConnectFour.Logic.Enums;

namespace ConnectFour.Logic
{
    /// <summary>
    /// This class represents a Connect Four game engine class.
    /// </summary>
    class GameEngine
    {
        PlayerType m_Turn;
        private Player m_Winner;
        private Gameboard m_Board;
        private Player m_Player1;
        private Player player2;
        
        private Player winner;

        public GameEngine()
        {
            m_Player1 = new Player(PlayerType.O);
            player2 = new Player(PlayerType.X);
            m_Board = new Gameboard();
            m_Turn = m_Player1.getType();
            m_Winner = new Player(PlayerType.UNDEFINED);
        }

        #region Public Methods
        public Player GetCurrentPlayer()
        {
            return (m_Turn == PlayerType.O) ? m_Player1 : player2;
        }

        internal int WhichPlayer(Player nowPlaying)
        {
            return (m_Turn == PlayerType.O) ? 1 : 2;
        }

        public int GetRowInColumn(int columnChoice)
        {
            return m_Board.GetRowInColumn(columnChoice);
        }

        /// <summary>
        /// This method changes turns between the two players playing.
        /// </summary>
        /// <returns></returns>
        public Player ChangeTurns()
        {
            Player nextToPlay = null;
            if (m_Turn == m_Player1.getType())
            {
                m_Turn = player2.getType();
                nextToPlay = player2;
            }
            else if (m_Turn == player2.getType())
            {
                m_Turn = m_Player1.getType();
                nextToPlay = m_Player1;

            }
            return nextToPlay;
        }

        /// <summary>
        /// This method inserts a player token into the chosen column.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="col"></param>
        /// <returns> row clear for insert in the chosen column </returns>
        public int insertColumn(Player player, int col)
        {
            int rowInserted = m_Board.InsertIntoColumn(player, col);
            if (isWinner(player))
            {
                m_Winner = player;
            }
            return rowInserted;

        }

        /// <summary>
        /// This method checks if there is a winner (game is over).
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool isWinner(Player player)
        {
            bool hasWon = false;

            if (checkRows(player) || checkColumns(player) || checkDiagonal(player))
            {
                hasWon = true;
            }
            return hasWon;
        }

        /// <summary>
        ///     This method takes a ros and a column and checks if they are valid arguments.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool AreValidArgs(int i, int j)
        {
            bool areValid = false;
            if ((i >= 0 && i < m_Board.getRows()) && (j >=0 && j < m_Board.getColumns()))
            {
                areValid = true;
            }
            return areValid;
        }

        /// <summary>
        ///     Prints the board as a string, used for debug.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_Board.ToString();
        }
        #endregion Public Methods

        #region Private Methods
        private bool checkDiagonal(Player player)
        {
            bool hasWon = false;
            int longestConnection = 0;
            int rows = m_Board.getRows();
            int columns = m_Board.getColumns();

            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (m_Board.getTokenType(i, j) == player.getType())
                    {
                        int longestRight = 0;
                        int longestLeft = 0;
                        int nextRowIndex = i;
                        int nextColIndex = j;
                        // Check right diagonal
                        while (AreValidArgs(nextRowIndex, nextColIndex) && m_Board.getTokenType(nextRowIndex, nextColIndex) == player.getType())
                        {
                            longestRight++;
                            nextRowIndex--;
                            nextColIndex++;
                        }

                        // check left diagonal
                        nextRowIndex = i;
                        nextColIndex = j;
                        while (AreValidArgs(nextRowIndex, nextColIndex) && m_Board.getTokenType(nextRowIndex, nextColIndex) == player.getType())
                        {
                            longestLeft++;
                            nextRowIndex--;
                            nextColIndex--;
                        }
                        longestConnection = (longestRight > longestLeft) ? longestRight : longestLeft;
                        if (longestConnection >= 4)
                        {
                            hasWon = true;
                            break;
                        }
                        else
                        {
                            longestConnection = 0;
                        }
                    }
                }
                // End of row
                longestConnection = 0;
                if (hasWon) break;
            }
            return hasWon;
        }

        private bool checkColumns(Player player)
        {
            bool hasWon = false;
            int longestConnection = 0;
            int rows = m_Board.getRows();
            int columns = m_Board.getColumns();

            for (int j = 0; j < columns; j++)
            {
                for (int i = rows - 1; i >= 0; i--)
                {
                    if (m_Board.getTokenType(i, j) == player.getType())
                    {
                        longestConnection++;
                        if (longestConnection >= 4)
                        {
                            hasWon = true;
                            break;
                        }
                    }
                    else
                    {
                        longestConnection = 0;
                    }
                }
                // End of row
                if (hasWon) break;
                longestConnection = 0;

            }
            return hasWon;
        }

        private bool checkRows(Player player)
        {
            bool hasWon = false;
            int longestConnection = 0;
            int rows = m_Board.getRows();
            int columns = m_Board.getColumns();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if(m_Board.getTokenType(i,j) == player.getType())
                    {
                        longestConnection++;
                        if (longestConnection >= 4)
                        {
                            hasWon = true;
                            break;
                        }
                    }
                    else
                    {
                        longestConnection = 0;
                    }
                }
                // End of row
                if (hasWon) break;
                longestConnection = 0;
            }
            return hasWon;
            
        }
        #endregion Private Methods

    }
}