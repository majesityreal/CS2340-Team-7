using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{

    public static Piece[,] bestMoveBoard; // board that has the best move

    const int pawnValue = 100;
    const int knightValue = 300;
    const int bishopValue = 300;
    const int rookValue = 500;
    const int queenValue = 900;

    // square values table, default from white's perspective
    readonly static int[] pawnSquareValues = new int[] {
         0, 0, 0, 0, 0, 0, 0, 0,
         50, 50, 50, 50, 50, 50, 50, 50,
         10, 10, 20, 30, 30, 20, 10, 10,
         5, 5, 10, 25, 25, 10, 5, 5,
         0, 0, 0, 20, 20, 0, 0, 0,
         5, -5, -10, 0, 0, -10, -5, 5,
         5, 10, 10, -20, -20, 10, 10, 5,
         0, 0, 0, 0, 0, 0, 0, 0 };

    readonly static int[] knightSquareValues = new int[] {
        -50,-40,-30,-30,-30,-30,-40,-50,
        -40,-20,  0,  0,  0,  0,-20,-40,
        -30,  0, 10, 15, 15, 10,  0,-30,
        -30,  5, 15, 20, 20, 15,  5,-30,
        -30,  0, 15, 20, 20, 15,  0,-30,
        -30,  5, 10, 15, 15, 10,  5,-30,
        -40,-20,  0,  5,  5,  0,-20,-40,
        -50,-40,-30,-30,-30,-30,-40,-50 };

    readonly static int[] bishopSquareValues = new int[] {
        -20,-10,-10,-10,-10,-10,-10,-20,
        -10,  0,  0,  0,  0,  0,  0,-10,
        -10,  0,  5, 10, 10,  5,  0,-10,
        -10,  5,  5, 10, 10,  5,  5,-10,
        -10,  0, 10, 10, 10, 10,  0,-10,
        -10, 10, 10, 10, 10, 10, 10,-10,
        -10,  5,  0,  0,  0,  0,  5,-10,
        -20,-10,-10,-10,-10,-10,-10,-20 };

    readonly static int[] rookSquareValues = new int[] {
          0,  0,  0,  0,  0,  0,  0,  0,
          5, 10, 10, 10, 10, 10, 10,  5,
         -5,  0,  0,  0,  0,  0,  0, -5,
         -5,  0,  0,  0,  0,  0,  0, -5,
         -5,  0,  0,  0,  0,  0,  0, -5,
         -5,  0,  0,  0,  0,  0,  0, -5,
         -5,  0,  0,  0,  0,  0,  0, -5,
          0,  0,  0,  5,  5,  0,  0,  0 };

    readonly static int[] queenSquareValues = new int[] {
        -20,-10,-10, -5, -5,-10,-10,-20,
        -10,  0,  0,  0,  0,  0,  0,-10,
        -10,  0,  5,  5,  5,  5,  0,-10,
         -5,  0,  5,  5,  5,  5,  0, -5,
          0,  0,  5,  5,  5,  5,  0, -5,
        -10,  5,  5,  5,  5,  5,  0,-10,
        -10,  0,  5,  0,  0,  0,  0,-10,
        -20,-10,-10, -5, -5,-10,-10,-20 };

    // these are for the middle game!
    readonly static int[] kingSquareValues = new int[] {
        -30,-40,-40,-50,-50,-40,-40,-30,
        -30,-40,-40,-50,-50,-40,-40,-30,
        -30,-40,-40,-50,-50,-40,-40,-30,
        -30,-40,-40,-50,-50,-40,-40,-30,
        -20,-30,-30,-40,-40,-30,-30,-20,
        -10,-20,-20,-20,-20,-20,-20,-10,
         20, 20,  0,  0,  0,  0, 20, 20,
         20, 30, 10,  0,  0, 10, 30, 20 };

    readonly static int[] kingSquareValuesEndgame = new int[] {
        -50,-40,-30,-20,-20,-30,-40,-50,
        -30,-20,-10,  0,  0,-10,-20,-30,
        -30,-10, 20, 30, 30, 20,-10,-30,
        -30,-10, 30, 40, 40, 30,-10,-30,
        -30,-10, 30, 40, 40, 30,-10,-30,
        -30,-10, 20, 30, 30, 20,-10,-30,
        -30,-30,  0,  0,  0,  0,-30,-30,
        -50,-30,-30,-30,-30,-30,-30,-50 };

    // go through all the pieces in the board for a color (whose turn is it?)
    // go through all the moves for each piece

    // BREADTH FIRST SEARCH

    public static int negaMax(int depth, int turn, Piece[,] board)
    {
        Debug.Log("Depth: " + depth);
        if (depth == 0)
        {
            // this function returns a positive value based on whose turn it is
            int val = EvaluateBoard(turn, board);
            Debug.Log("Reached end of board with eval: " + val);
            return val;
        }

        int max = int.MinValue;

        // go through all the moves!
        for (int i = 0; i < 64; i++)
        {
            if (board[i % 8, i / 8] == null)
            {
                continue;
            }
            Debug.Log("Continuing");
            // if not turn skip
            if (board[i % 8, i / 8].color != turn)
            {
                continue;
            }

            // the list of legal moves
            List<int[]> moves = board[i % 8, i / 8].GetLegalMoves(board, ChessManager.moveRecord);
            foreach (int[] move in moves)
            {
                // create temp array - THIS NEEDS TO CREATE A DEEP COPY
                Piece[,] temp = new Piece[8,8];
                temp = board.Clone() as Piece[,];
                /*                for (int i = 0; i < 64; i++)
                                {
                                    temp[i % 8, i / 8] = board[i % 8, i / 8];
                                }*/

                // making the move on the temp board
                // TODO ---- ALSO UPDATE THE PIECE ITSELFFFF!!! YES
                Piece pos1 = temp[i % 8, i / 8];
                ChessManager.MovePosition(pos1.xCoord, pos1.yCoord, move[0], move[1], temp);

                // re does algorithm with opposite turn, with newly moved piece on board
                int score = -negaMax(depth - 1, turn * -1, temp);
                if (score > max)
                {
                    max = score;
                    // stores the move here
                    bestMoveBoard = temp;
                }
            }
        }

        return max;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int EvaluateBoard(int turn, Piece[,] board)
    {
        // material section - starts at 3900 for both
        int whiteMaterial = CountMaterial(1, board);
        int blackMaterial = CountMaterial(-1, board);
        int totalMaterial = whiteMaterial - blackMaterial;

        // trying to balance down by weighting
        totalMaterial /= 10;


        // positions of the pieces - starts at 95 for both
        int whitePosition = EvaluatePositionWhite(board);
        int blackPosition = EvaluatePositionBlack(board);
        int totalPosition = whitePosition - blackPosition;

        // TODO - check the perspective, change the totalEval based on this

        return (totalMaterial + totalPosition) * turn;
    }

    static int EvaluatePositionWhite(Piece[,] board)
    {
        int value = 0;

        for (int i = 0; i < 64; i++)
        {
            if (board[i % 8, i / 8] == null)
            {
                continue;
            }
            if (board[i % 8, i / 8].color != 1)
            {
                continue;
            }

            if (board[i % 8, i / 8] is Pawn)
            {
                value += pawnSquareValues[i];
            }
            else if (board[i % 8, i / 8] is Knight)
            {
                value += knightSquareValues[i];
            }
            else if (board[i % 8, i / 8] is Bishop)
            {
                value += bishopSquareValues[i];
            }
            else if (board[i % 8, i / 8] is Rook)
            {
                value += rookSquareValues[i];
            }
            else if (board[i % 8, i / 8] is Queen)
            {
                value += queenSquareValues[i];
            }
        }
        return value;
    }

    static int EvaluatePositionBlack(Piece[,] board)
    {
        int value = 0;

        for (int i = 0; i < 64; i++)
        {
            if (board[i % 8, i / 8] == null)
            {
                continue;
            }
            if (board[i % 8, i / 8].color != -1)
            {
                continue;
            }

            if (board[i % 8, i / 8] is Pawn)
            {
                value += pawnSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] is Knight)
            {
                value += knightSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] is Bishop)
            {
                value += bishopSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] is Rook)
            {
                value += rookSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] is Queen)
            {
                value += queenSquareValues[63 - i];
            }
        }
        return value;
    }

    // returns value of all the material of the pieces still in play. Accounts for pawn loss in knight and rook value
    static int CountMaterial(int color, Piece[,] board)
    {
        int material = 0;
        int pawnCount = 0;

        // get all the tiles from the square
        for (int i = 0; i < 64; i++)
        {
            // if nothing there, go to next piece
            if (board[i % 8, i / 8] == null)
            {
                continue;
            }
            // if they are not the color, go to next piece
            if (board[i % 8, i / 8].color != color)
            {
                continue;
            }
            if (board[i % 8, i / 8] is Pawn)
            {
                pawnCount++;
            }
        }

        // subtracting 40 here to keep the same starting value, as the pawn # decrease however, the value decreases
        int newKnightValue = knightValue + (5 * pawnCount) - 40;
        // adding 40 to keep same start, as pawns decrease, the value inreases
        int newRookValue = rookValue - (5 * pawnCount) + 40;

        for (int i = 0; i < 64; i++)
        {
            if (board[i % 8, i / 8] is Pawn)
            {
                material += pawnValue;
            }
            else if (board[i % 8, i / 8] is Knight)
            {
                material += newKnightValue;
            }
            else if (board[i % 8, i / 8] is Bishop)
            {
                material += bishopValue;
            }
            else if (board[i % 8, i / 8] is Rook)
            {
                material += newRookValue;
            }
            else if (board[i % 8, i / 8] is Queen)
            {
                material += queenValue;
            }
        }
        return material;
    }
}