using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConnectFour.Logic.Enums;

namespace ConnectFour.Logic
{
    class Player
    {
        private PlayerType m_PlayerType;
        private int m_Score;                // extansion option for future development, counting minigames scores

        // Constructor
        public Player(PlayerType type)
        {
            m_PlayerType = type;
            m_Score = 0;
        }
       
        // Player variables get
        public PlayerType getType()
        {
            return m_PlayerType;
        }

        public int getScore()
        {
            return m_Score;
        }

    }
}
