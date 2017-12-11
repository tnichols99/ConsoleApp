using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Piece
    {

        internal int player { get; set; }
        internal int xPosition { get; set; }
        internal int yPosition { get; set; }
        internal bool isKing { get; set; }

        public Piece(int p, int x, int y)
        {
            player = p;
            xPosition = x;
            yPosition = y;
        }

    }

}