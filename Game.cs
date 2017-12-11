using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Game
    {

        private bool isLive;
        private int turn;
        private bool isJump;
        private int otherTurn;

        public Game()
        {
            this.isLive = true;
            this.turn = 1;
        }

        public void startGame()
        {

            string input;
            int x;
            Board board = new Board();
            List<Move> moves = new List<Move>();

            while (this.isLive)
            {
                Console.WriteLine("Player " + this.turn + "'s turn.");
                board.printBoard();

                if (this.turn == 1)
                {
                    this.otherTurn = 2;
                }
                else
                {
                    this.otherTurn = 1;
                }

                moves = board.checkJumps(this.turn);

                if (this.isJump && board.checkJumps(this.otherTurn).Count > 0)
                {
                    moves = board.checkJumps(this.otherTurn);
                    this.turn = this.otherTurn;
                    this.isJump = true;
                }
                else if (moves.Count == 0)
                {
                    moves = board.checkValidMoves(this.turn);
                    this.isJump = false;
                }
                else
                {
                    this.isJump = true;
                }

                if (moves.Count == 0)
                {
                    this.isLive = false;
                    continue;
                }
                Console.WriteLine("Available moves:\n");
                Console.WriteLine(board.printMoves(moves));
                Console.WriteLine("Player " + this.turn + "'s move:");
                input = Console.ReadLine();
                while (true)
                {
                    if (!int.TryParse(input, out x))
                    {
                        Console.WriteLine("Invalid move");
                        input = Console.ReadLine();
                    }
                    if (x <= moves.Count && x > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move");
                        input = Console.ReadLine();
                    }
                }
                Move move = moves[x - 1];
                board.move(move);

                Piece p;
                if ((move.down == 0 && this.turn == 2) || move.down == 7 && this.turn == 1)
                {

                    p =
                        (from piece in board.P
                         where piece.xPosition == move.left
                         && piece.yPosition == move.down
                         select piece).SingleOrDefault();
                    p.isKing = true;
                }

                if (Math.Abs(move.left - move.right) > 1)
                { 
                    board.removePiece((move.left + move.right) / 2, (move.down + move.up) / 2);
                }
                
                if (this.turn == 1)
                {
                    this.turn = 2;
                }
                else
                {
                    this.turn = 1;
                }
                

            }
            Console.WriteLine("Player " + this.turn + " wins!");
            Console.Read();
        }
    }
}