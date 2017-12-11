using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Board
    {

        public List<Piece> P { get; set; }

        public Board()
        {
            P = new List<Piece>();

            
            for (int i = 0; i < 4; i++)
            {
                P.Add((new Piece(1, 2 * i, 0)));
                P.Add(new Piece(1, 2 * i + 1, 1));
                P.Add(new Piece(1, 2 * i, 2));
            }

            
            for (int i = 0; i < 4; i++)
            {
                P.Add(new Piece(2, 2 * i + 1, 7));
                P.Add(new Piece(2, 2 * i, 6));
                P.Add(new Piece(2, 2 * i + 1, 5));
            }
        }

        public int checkPosition(int x, int y)
        {
            int t;
            
            if ((0 <= x && x <= 7) && (0 <= y && y <= 7))
            {
                t =
                    (from piece in P
                     where piece.xPosition == x
                     && piece.yPosition == y
                     select piece.player).SingleOrDefault();
            }
            else
            {
                t = 3;
            }
            return t;
        }

        public void printBoard()
        {

            string returnVal = "";
            returnVal += "   _________________\n";

            for (int i = 0; i < 8; i++)
            { 
                for (int j = 0; j < 8; j++)
                { 


                    if (j == 0)
                        returnVal += i + " | ";

                    if (checkPosition(j, i) == 1)
                        returnVal += "X ";
                    else if (checkPosition(j, i) == 2)
                        returnVal += "O ";
                    else
                        returnVal += "- ";

                    if (j == 7)
                        returnVal += "|\n";
                }
            }

            returnVal += "  |_________________|\n    0 1 2 3 4 5 6 7\n";
            Console.WriteLine(returnVal);
        }

        public string printMoves(List<Move> moves)
        {

            string returnVal = "";
            int i = 1;
            foreach (Move move in moves)
            {
                returnVal += "(" + i + ")  (" + move.right + ", " + move.up + ") to (" + move.left + ", " + move.down + ")\n";
                i++;
            }
            for (int j = 0; j < 10 - moves.Count; j++)
            {
                returnVal += "\n";
            }
            return returnVal;

        }

        public List<Move> checkJumps(int player)
        {

            List<Move> moves = new List<Move>();
            IEnumerable<Piece> p;
            if (player == 1)
            {

                p =
                    (from piece in P
                     where checkPosition(piece.xPosition + 1, piece.yPosition + 1) == 2
                     && checkPosition(piece.xPosition + 2, piece.yPosition + 2) == 0
                     && piece.player == 1
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 2, pi.yPosition + 2, 1));
                }

                
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition + 1) == 2
                     && checkPosition(piece.xPosition - 2, piece.yPosition + 2) == 0
                     && piece.player == 1
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 2, pi.yPosition + 2, 1));
                }

                
                p =
                (from piece in P
                 where checkPosition(piece.xPosition + 1, piece.yPosition - 1) == 2
                 && checkPosition(piece.xPosition + 2, piece.yPosition - 2) == 0
                 && piece.player == 1
                 && piece.isKing
                 select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 2, pi.yPosition - 2, 1));
                }
                
                p =
                (from piece in P
                 where checkPosition(piece.xPosition - 1, piece.yPosition - 1) == 2
                 && checkPosition(piece.xPosition - 2, piece.yPosition - 2) == 0
                 && piece.player == 1
                 && piece.isKing
                 select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 2, pi.yPosition - 2, 1));
                }

            }
            else
            {
                p =
                   (from piece in P
                    where checkPosition(piece.xPosition + 1, piece.yPosition - 1) == 1
                    && checkPosition(piece.xPosition + 2, piece.yPosition - 2) == 0
                    && piece.player == 2
                    select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 2, pi.yPosition - 2, 2));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition - 1) == 1
                     && checkPosition(piece.xPosition - 2, piece.yPosition - 2) == 0
                     && piece.player == 2
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 2, pi.yPosition - 2, 2));
                }
                
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition + 1, piece.yPosition + 1) == 1
                     && checkPosition(piece.xPosition + 2, piece.yPosition + 2) == 0
                     && piece.player == 2
                     && piece.isKing
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 2, pi.yPosition + 2, 2));
                }

                
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition + 1) == 1
                     && checkPosition(piece.xPosition - 2, piece.yPosition + 2) == 0
                     && piece.player == 2
                     && piece.isKing
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 2, pi.yPosition + 2, 2));
                }


            }

            moves = moves.OrderBy(x => x.right).ToList();
            return moves;

        }
        public List<Move> checkValidMoves(int player)
        {

            
            List<Move> moves = new List<Move>();

            IEnumerable<Piece> p;
            if (player == 1)
            {
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition + 1, piece.yPosition + 1) == 0
                     && piece.player == 1
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 1, pi.yPosition + 1, 1));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition + 1) == 0
                     && piece.player == 1
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 1, pi.yPosition + 1, 1));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition + 1, piece.yPosition - 1) == 0
                     && piece.player == 1
                     && piece.isKing
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 1, pi.yPosition - 1, 1));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition - 1) == 0
                     && piece.player == 1
                     && piece.isKing
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 1, pi.yPosition - 1, 1));
                }

            }
            else
            {
                p =
                   (from piece in P
                    where checkPosition(piece.xPosition + 1, piece.yPosition - 1) == 0
                    && piece.player == 2
                    select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 1, pi.yPosition - 1, 2));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition - 1) == 0
                     && piece.player == 2
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 1, pi.yPosition - 1, 2));
                }

               
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition + 1, piece.yPosition + 1) == 0
                     && piece.player == 2
                     && piece.isKing
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition + 1, pi.yPosition + 1, 2));
                }
                p =
                    (from piece in P
                     where checkPosition(piece.xPosition - 1, piece.yPosition + 1) == 0
                     && piece.player == 2
                     && piece.isKing
                     select piece);
                if (p != null)
                {
                    foreach (Piece pi in p)
                        moves.Add(new Move(pi.xPosition, pi.yPosition, pi.xPosition - 1, pi.yPosition + 1, 2));
                }
            }
            moves = moves.OrderBy(x => x.right).ToList();
            return moves;
        }

        public void move(Move m)
        {
            var p =
                (from piece in P
                 where piece.xPosition == m.right
                 && piece.yPosition == m.up
                 select piece).Single();

            p.xPosition = m.left;
            p.yPosition = m.down;

        }
        public void removePiece(int x, int y)
        {
            Piece p = new Piece(0, -1, -1); 

            
            if ((0 <= x && x <= 7) && (0 <= y && y <= 7))
            {
                p =
                    (from piece in P
                     where piece.xPosition == x
                     && piece.yPosition == y
                     select piece).SingleOrDefault();
            }
            this.P.Remove(p);
        }
    }
}