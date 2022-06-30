using System.Collections;
using System.Collections.Generic;
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

    private static Queue<Dictionary<int, Piece>> boardList = new Queue<Dictionary<int, Piece>>();

    public static readonly int maxDepth = 4;

    public static int GetBestMove(int color)
    {
        // clear queue
        boardList.Clear();

        foreach (KeyValuePair<int, Piece> entry in ChessManager.board)
        {
            if (entry.Value.GetColor() != color)
            {
                continue;
            }
            // the list of legal moves
            List<int> moves = entry.Value.GetLegalMoves(ChessManager.board);
            foreach (int move in moves)
            {
                // create temp dictionary
                Dictionary<int, Piece> temp = ChessManager.board;

                // making the move on the temp board
                temp.Remove(entry.Key);
                temp.Add(move, entry.Value);

                // adding temp board to queue for breadth first search
                boardList.Enqueue(temp);
            }
        }
        // starting point
        boardList.Enqueue(ChessManager.board);

        GetBestMoveRecursion(color, 1);

        // go through the queue, analyzing the position, adding again the possible moves from that

        return 0;
    }

    // recurse through this with a board
    int GetBestMoveRecursion(int color)
    {
        Dictionary<int, Piece> tempBoard = boardList.Dequeue();
/*    static int negaMax(Dictionary<int, Piece> board, int depthLeft, int color)
    {
        if (depthLeft == 0)
        {
            return EvaluateBoard(board, color);
        }
        int max = int.MinValue;
            score = -negaMax(board, depthLeft - 1, color);
            if (score > max)
            {
                max = score;
            }
        return max;
    }*/

    // recurse through this with a board
    static int GetBestMoveRecursion(int color, int depth)
    {
        if (depth > maxDepth)
        {
            return 0;
        }

        // get next board to analyze
        Dictionary<int, Piece> tempBoard = boardList.Dequeue();

        // analyze the board
/*        int value = EvaluateBoard(tempBoard);
*/        //

        // add future moves to queue
        foreach (KeyValuePair<int, Piece> entry in tempBoard)
        {
            if (entry.Value.GetColor() != color)
            {
                continue;
            }
            // the list of legal moves
            List<int> moves = entry.Value.GetLegalMoves(tempBoard);
            foreach (int move in moves)
            {
                // create temp dictionary based on current board
                Dictionary<int, Piece> temp = tempBoard;

                // making the move on the temp board
                temp.Remove(entry.Key);
                temp.Add(move, entry.Value);

                // adding temp board to queue for breadth first search
                boardList.Enqueue(temp);
            }
        }
        return 0;
    }

    public static int EvaluateBoard()

        // go one move deeper. Change color to analyze white's possible responses
        return GetBestMoveRecursion((color * -1), depth + 1);
    }

    // color is the perspective that you are evaluating
    public static int EvaluateBoard(Dictionary<int, Piece> board, int color)
    {
        // material section - starts at 3900 for both
        int whiteMaterial = CountMaterial(board, 1);
        int blackMaterial = CountMaterial(board, -1);
        int totalMaterial = whiteMaterial - blackMaterial;

        // trying to balance down by weighting
        totalMaterial /= 30;


        // positions of the pieces - starts at 95 for both
        int whitePosition = EvaluatePositionWhite(board);
        int blackPosition = EvaluatePositionBlack(board);
        int totalPosition = whitePosition - blackPosition;

        // multiplying by the color makes it so that positive is always good for whichever color you are evaluating for
        return (totalMaterial + totalPosition) * color;
    }

    static int EvaluatePositionWhite(Dictionary<int, Piece> board)
    {
        int value = 0;

        foreach(KeyValuePair<int, Piece> entry in board)
        {
            if (entry.Value.GetColor() != 1)
            {
                continue;
            }

            if (entry.Value is Pawn)
            {
                value += pawnSquareValues[entry.Key];
            }
            else if (entry.Value is Knight)
            {
                value += knightSquareValues[entry.Key];
            }
            else if (entry.Value is Bishop)
            {
                value += bishopSquareValues[entry.Key];
            }
            else if (entry.Value is Rook)
            {
                value += rookSquareValues[entry.Key];
            }
            else if (entry.Value is Queen)
            {
                value += queenSquareValues[entry.Key];
            }
        }
        return value;
    }

    static int EvaluatePositionBlack(Dictionary<int, Piece> board)
    {
        int value = 0;

        foreach(KeyValuePair<int, Piece> entry in board)
        {
            if (entry.Value.GetColor() != -1)
            {
                continue;
            }

            if (entry.Value is Pawn)
            {
                value += pawnSquareValues[63 - entry.Key];
            }
            else if (entry.Value is Knight)
            {
                value += knightSquareValues[63 - entry.Key];
            }
            else if (entry.Value is Bishop)
            {
                value += bishopSquareValues[63 - entry.Key];
            }
            else if (entry.Value is Rook)
            {
                value += rookSquareValues[63 - entry.Key];
            }
            else if (entry.Value is Queen)
            {
                value += queenSquareValues[63 - entry.Key];
            }
        }
        return value;
    }

    // returns value of all the material of the pieces still in play
    static int CountMaterial(Dictionary<int, Piece> board, int color)
    {
        int material = 0;

        foreach(KeyValuePair<int, Piece> entry in board)
        {
            if (entry.Value.GetColor() != color)
            {
                continue;
            }

            if (entry.Value is Pawn) {
                material += pawnValue;
            }
            else if (entry.Value is Knight)
            {
                material += knightValue;
            }
            else if (entry.Value is Bishop)
            {
                material += bishopValue;
            }
            else if (entry.Value is Rook)
            {
                material += rookValue;
            }
            else if (entry.Value is Queen)
            {
                material += queenValue;
            }
        }

        return material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
