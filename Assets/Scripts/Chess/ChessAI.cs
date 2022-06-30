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


    public static int negaMax(int depth, int turn, Dictionary<int, Piece> board)
    {
        Debug.Log("Depth: " + depth);
        if (depth == 0)
        {
            // this function returns a positive value based on whose turn it is
            int val = EvaluateBoard(turn);
            Debug.Log("Reached end of board with eval: " + val);
            return val;
        }

        int max = int.MinValue;

        // go through all the moves!
        foreach (KeyValuePair<int, Piece> entry in board)
        {
            if (entry.Value.GetColor() != turn)
            {
                continue;
            }
            // the list of legal moves
            List<int> moves = entry.Value.GetLegalMoves(board);
            foreach (int move in moves)
            {
                Debug.Log("The piece " + entry.Value.GetColor() + " " + entry.Value + " at position: " + (entry.Value.GetXPos() + (entry.Value.GetYPos() * 8)) + " can move to " + move);
            }
            foreach (int move in moves)
            {
                // create temp dictionary - THIS NEEDS TO CREATE A DEEP COPY
                // the other thing to try here is: (I think that does the same thing)
                // Dictionary<int, Piece> temp = new Dictionary<int, Piece>(board);
                Dictionary<int, Piece> temp = new Dictionary<int, Piece>();
                foreach(KeyValuePair<int, Piece> entry2 in board)
                {
                    temp.Add(entry2.Key, entry2.Value);
                }

                // making the move on the temp board
                // TODO ---- ALSO UPDATE THE PIECE ITSELFFFF!!! YES
                temp.Remove(entry.Key);
                if (temp.ContainsKey(move))
                {
                    temp.Remove(move);
                }
                temp.Add(move, entry.Value);

                Debug.Log("The board: ================================= ");
                foreach (KeyValuePair<int, Piece> entry2 in temp)
                {
                    Debug.Log(entry2.Key + " " + entry2.Value);
                }

                // re does algorithm with opposite turn, with newly moved piece on board
                int score = -negaMax(depth - 1, turn * -1, temp);
                if (score > max)
                {
                    max = score;
                    // TODO - STORE THE MOVE HERE STORE THE MOVE HERE STORE THE MOVE HERE
                }
            }
        }

        return max;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int EvaluateBoard(int turn)
    {
        // material section - starts at 3900 for both
        int whiteMaterial = CountMaterial(1);
        int blackMaterial = CountMaterial(-1);
        int totalMaterial = whiteMaterial - blackMaterial;

        // trying to balance down by weighting
        totalMaterial /= 10;


        // positions of the pieces - starts at 95 for both
        int whitePosition = EvaluatePositionWhite();
        int blackPosition = EvaluatePositionBlack();
        int totalPosition = whitePosition - blackPosition;

        // TODO - check the perspective, change the totalEval based on this

        return (totalMaterial + totalPosition) * turn;
    }

    static int EvaluatePositionWhite()
    {
        int value = 0;

        foreach(KeyValuePair<int, Piece> entry in ChessManager.board)
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

    static int EvaluatePositionBlack()
    {
        int value = 0;

        foreach(KeyValuePair<int, Piece> entry in ChessManager.board)
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
    static int CountMaterial(int color)
    {
        int material = 0;

        foreach(KeyValuePair<int, Piece> entry in ChessManager.board)
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


    /*    public static int GetBestMove(int color)
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

        // go through the queue, analyzing the position, adding again the possible moves from that

        return 0;
    }*/

    // recurse through this with a board
    /*    int GetBestMoveRecursion(int color)
        {
            Dictionary<int, Piece> tempBoard = boardList.Dequeue();
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
                    // create temp dictionary
                    Dictionary<int, Piece> temp = ChessManager.board;

                    // making the move on the temp board
                    temp.Remove(entry.Key);
                    temp.Add(move, entry.Value);

                    // adding temp board to queue for breadth first search
                    boardList.Enqueue(temp);
                }
            }
            return 0;
        }*/
}
