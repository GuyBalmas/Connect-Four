using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConnectFour.Logic.Enums;

namespace ConnectFour.Logic
{
    /*
     * This class represents a Connect-Four gameboard object.
     */
    class Gameboard
    {
        private static readonly int ROWS = 6;
        private static readonly int COLUMNS = 7;
        private static readonly string EMPTY = "";
        private string[,] m_Board;
        
        public Gameboard()
        {
            m_Board = new string[ROWS, COLUMNS];
        }

        #region Public Methods
        public int getRows()
        {
            return ROWS;
        }

        public int getColumns()
        {
            return COLUMNS;
        }

        /// <summary>
        /// 
        /// This method takes a row and a column and checks the coresponding cell on the board.
        /// 
        /// </summary>
        /// <param name="row"> row number </param>
        /// <param name="col"> column number </param>
        /// <returns> 
        ///         O - player 1 token exists in the coresponding cell on the board.
        ///         X - player 2 token exists in the coresponding cell on the board.\
        ///         "" - returns an empty string if the cell is clear with no tokens.
        /// </returns>
        public string getToken(int row, int col)
        {
            string token = "";
            if (row >= ROWS || col >= COLUMNS || row < 0 || col < 0)
            {
                return "-1"; // Invalid arguments
            }

            if (m_Board[row, col] == "X")
            {
                token = "X";
            }
            else if(m_Board[row, col] == "O")
            {
                token = "O";
            }
            return token;
        }

        /// <summary>
        /// 
        /// This method takes a column number and returns the correct row to insert to, starting from buttom to upper.
        /// 
        /// </summary>
        /// <param name="col"> column to check </param>
        /// <returns> 
        ///         rowInserted - row number in the column, open to insert a token to.
        ///         -1          - invalid argument or no rows open in that column.
        /// </returns>
        public int GetRowInColumn(int col)
        {
            int rowInserted = -1;
            for (int row = ROWS - 1; row >= 0; row--)
            {
                if (getToken(row, col) == "")
                {
                    rowInserted = row;
                    break;
                }
            }
            return rowInserted;
        }

        /// <summary>
        ///     This method takes a row and a column and returns the coresponding token on the borad.
        /// </summary>
        /// <param name="row"> row index </param>
        /// <param name="col"> column index </param>
        /// <returns> 
        ///             PlayerType.O - player 1 has a token in the coresponding slot.
        ///             PlayerType.X - player 2 has a token in the coresponding slot.
        ///             PlayerType.UNDEFINED - invalid arguments, or no token found. 
        /// </returns>
        public PlayerType getTokenType(int row, int col)
        {
            PlayerType type = PlayerType.UNDEFINED;
            if (row >= ROWS || col >= COLUMNS || row < 0 || col < 0)
            {
                return type; // Invalid arguments
            }

            string token = this.m_Board[row, col];
            if(token == "O")
            {
                type = PlayerType.O;
            }
            else if(token == "X")
            {
                type = PlayerType.X;
            }
            return type;
        }
        
        /// <summary>
        ///     This method takes a player, a row and a column
        ///     and inserts the player token to the coresponding slot on the board.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void insertToken(Player player, int row, int column)
        {
            if (row >= ROWS || column >= COLUMNS || row < 0 || column < 0)
            {
                return; // Invalid arguments
            }

            if (player.getType() == Enums.PlayerType.O)
            {
                m_Board[row, column] = "O";
            }
            else if (player.getType() == Enums.PlayerType.X)
            {
                m_Board[row, column] = "X";
            }
        }
        
        /// <summary>
        /// This methos prints the board as a string, used for debug;
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            StringBuilder board = new StringBuilder();

            board.AppendLine("------------------------------------");
            for (int i = 0; i < ROWS; i++)
            {
                board.Append("|");
                for (int j = 0; j < COLUMNS; j++)
                {
                    board.Append("  ");
                    string slot = m_Board[i, j];
                    string mark = "";

                    if(slot == EMPTY)
                    {
                        mark = " ";
                    }
                    else
                    {
                        mark = slot;
                    }
                    board.Append(mark + "  |");
                }
                board.AppendLine();
                board.AppendLine("------------------------------------");
            }
            return board.ToString();
        }
        #endregion Public Methods

        #region Internal Methods
        /// <summary>
        ///     This method takes a player and a column, and inserts that player's token into the chosen column.
        /// </summary>
        /// <param name="player"> The player of which his token should be inserted. </param>
        /// <param name="col"> The desired column to insert to.</param>
        /// <returns></returns>
        internal int InsertIntoColumn(Player player, int col)
        {
            int rowInserted = -1;
            for (int row = ROWS - 1; row >= 0; row--)
            {
                if (getToken(row, col) == "")
                {
                    insertToken(player, row, col);
                    rowInserted = row;
                    break;
                }
            }
            return rowInserted;
        }
        #endregion Internal Methods
    }
}
