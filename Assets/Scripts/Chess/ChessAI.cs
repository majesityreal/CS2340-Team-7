using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
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

    public static int negaMax(int depth, int max, int turn, char[,] board, List<string> record)
    {
        Move.ConvertBoardToBinary(board);

        if (depth < 0)
        {
            Debug.LogError("Depth is under 0!!!!");
        }
        if (depth == 0)
        {
            // this function returns a positive value based on whose turn it is
            int val = EvaluateBoard(turn, board);
            Debug.Log("Reached end of board with eval: " + val);
            return val;
        }
        else
        {
            Debug.Log("Depth: " + depth);
        }

        // go through all the moves!
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (board[x, y] == '-')
                {
                    continue;
                }

                // if not turn skip
                if (turn == 1 && board[x, y] > 'a')
                {
                    continue;
                }
                else if (turn == -1 && board[x, y] < 'a')
                {
                    continue;
                }

                // the list of legal moves
                List<int> moves = Move.GetMovesList(x, y, board[x, y], record);

                foreach (int move in moves)
                {
                    // deep copy of board
                    char[,] temp = new char[8,8];
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            temp[i, j] = board[i, j];
                        }
                    }

                    // deep copy of record
                    List<string> simulatedRecord = record.ToList();

                    char pos1 = temp[x, y];
                    // // before log
                    // Debug.Log("before Piece: " + temp[i % 8, i / 8] + "x: " + (i % 8) + " y:" + (i / 8));
                    ChessManager.MovePosition(x, y, move / 8, move % 8, temp, simulatedRecord);
                    // // after log
                    // Debug.Log("after Piece: " + temp[move[0], move[1]] + "x: " + (move[0]) + " y:" + (move[1]));

                    printBoard(temp);

                    // re does algorithm with opposite turn, with newly moved piece on board
                    int score = -negaMax(depth - 1, max, turn * -1, temp, simulatedRecord);
                    if (score > max)
                    {
                        max = score;
                        // stores the move here
                        // bestMoveBoard = temp;
                        // bestMoveRecord = simulatedRecord;
                    }
                }
            }
        }

        return max;
    }

    public static void negaMaxStarter(int depth, int turn, char[,] board, List<string> copyRecord)
    {
        int max = int.MinValue;
        int amount = negaMax(depth, max, turn, board, copyRecord);
    }

    public static void printBoard(char[,] board)
    {
        string s = "";
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                s+= board[j, i];
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    public static int EvaluateBoard(int turn, char[,] board)
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

    static int EvaluatePositionWhite(char[,] board)
    {
        int value = 0;

        for (int i = 0; i < 64; i++)
        {
            if (board[i % 8, i / 8] == '-' || board[i % 8, i / 8] > 'a')
            {
                continue;
            }

            if (board[i % 8, i / 8] == 'P')
            {
                value += pawnSquareValues[i];
            }
            else if (board[i % 8, i / 8] == 'N')
            {
                value += knightSquareValues[i];
            }
            else if (board[i % 8, i / 8] == 'B')
            {
                value += bishopSquareValues[i];
            }
            else if (board[i % 8, i / 8] == 'R')
            {
                value += rookSquareValues[i];
            }
            else if (board[i % 8, i / 8] == 'Q')
            {
                value += queenSquareValues[i];
            }
        }
        return value;
    }

    static int EvaluatePositionBlack(char[,] board)
    {
        int value = 0;

        for (int i = 0; i < 64; i++)
        {
            if (board[i % 8, i / 8] == '-' || board[i % 8, i / 8] < 'a')
            {
                continue;
            }

            if (board[i % 8, i / 8] == 'p')
            {
                value += pawnSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] == 'n')
            {
                value += knightSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] == 'b')
            {
                value += bishopSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] == 'r')
            {
                value += rookSquareValues[63 - i];
            }
            else if (board[i % 8, i / 8] == 'q')
            {
                value += queenSquareValues[63 - i];
            }
        }
        return value;
    }

    // returns value of all the material of the pieces still in play. Accounts for pawn loss in knight and rook value
    static int CountMaterial(int color, char[,] board)
    {
        int material = 0;
        int pawnCount = 0;

        // get all the tiles from the square
        if (color == 1)
        {
            for (int i = 0; i < 64; i++)
            {
                if (board[i % 8, i / 8] == '-' || board[i % 8, i / 8] > 'a')
                {
                    continue;
                }

                if (board[i % 8, i / 8] == 'P')
                {
                    pawnCount++;
                }
            }
        }
        else
        {
            for (int i = 0; i < 64; i++)
            {
                if (board[i % 8, i / 8] == '-' || board[i % 8, i / 8] < 'a')
                {
                    continue;
                }

                if (board[i % 8, i / 8] == 'p')
                {
                    pawnCount++;
                }
            }
        }

        // subtracting 40 here to keep the same starting value, as the pawn # decrease however, the value decreases
        int newKnightValue = knightValue + (5 * pawnCount) - 40;
        // adding 40 to keep same start, as pawns decrease, the value inreases
        int newRookValue = rookValue - (5 * pawnCount) + 40;

        if (color == 1)
        {
            for (int i = 0; i < 64; i++)
            {
                if (board[i % 8, i / 8] == '-' || board[i % 8, i / 8] > 'a')
                {
                    continue;
                }

                if (board[i % 8, i / 8] == 'P')
                {
                    material += pawnValue;
                }
                else if (board[i % 8, i / 8] == 'N')
                {
                    material += newKnightValue;
                }
                else if (board[i % 8, i / 8] == 'B')
                {
                    material += bishopValue;
                }
                else if (board[i % 8, i / 8] == 'R')
                {
                    material += newRookValue;
                }
                else if (board[i % 8, i / 8] == 'Q')
                {
                    material += queenValue;
                }
            }
        }
        else
        {
            for (int i = 0; i < 64; i++)
            {
                if (board[i % 8, i / 8] == '-' || board[i % 8, i / 8] < 'a')
                {
                    continue;
                }

                if (board[i % 8, i / 8] == 'p')
                {
                    material += pawnValue;
                }
                else if (board[i % 8, i / 8] == 'n')
                {
                    material += newKnightValue;
                }
                else if (board[i % 8, i / 8] == 'b')
                {
                    material += bishopValue;
                }
                else if (board[i % 8, i / 8] == 'r')
                {
                    material += newRookValue;
                }
                else if (board[i % 8, i / 8] == 'q')
                {
                    material += queenValue;
                }
            }
        }

        return material;
    }
}