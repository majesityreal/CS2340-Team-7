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

    public static int EvaluateBoard()
    {
        // material section - starts at 3900 for both
        int whiteMaterial = CountMaterial(1);
        int blackMaterial = CountMaterial(-1);
        int totalMaterial = whiteMaterial - blackMaterial;

        // trying to balance down by weighting
        totalMaterial /= 30;


        // positions of the pieces - starts at 95 for both
        int whitePosition = EvaluatePositionWhite();
        int blackPosition = EvaluatePositionBlack();
        int totalPosition = whitePosition - blackPosition;

        // TODO - check the perspective, change the totalEval based on this

        return totalMaterial + totalPosition;
    }

    static int EvaluatePositionWhite()
    {
        int value = 0;
        for (int i = 0; i < 64; i++)
        {
            if (ChessManager.board[i % 8, i / 8] == null)
            {
                continue;
            }
            if (ChessManager.board[i % 8, i / 8].color != 1)
            {
                continue;
            }

            if (ChessManager.board[i % 8, i / 8] is Pawn)
            {
                value += pawnSquareValues[i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Knight)
            {
                value += knightSquareValues[i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Bishop)
            {
                value += bishopSquareValues[i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Rook)
            {
                value += rookSquareValues[i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Queen)
            {
                value += queenSquareValues[i];
            }
        }
        return value;
    }

    static int EvaluatePositionBlack()
    {
        int value = 0;
        for (int i = 0; i < 64; i++)
        {
            if (ChessManager.board[i % 8, i / 8] == null)
            {
                continue;
            }
            if (ChessManager.board[i % 8, i / 8].color != -1)
            {
                continue;
            }

            if (ChessManager.board[i % 8, i / 8] is Pawn)
            {
                value += pawnSquareValues[63 - i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Knight)
            {
                value += knightSquareValues[63 - i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Bishop)
            {
                value += bishopSquareValues[63 - i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Rook)
            {
                value += rookSquareValues[63 - i];
            }
            else if (ChessManager.board[i % 8, i / 8] is Queen)
            {
                value += queenSquareValues[63 - i];
            }
        }
        return value;
    }

    // returns value of all the material of the pieces still in play
    static int CountMaterial(int color)
    {
        int material = 0;
        // get all the tiles from the square
        for (int i = 0; i < 64; i++)
        {
            // if nothing there, go to next piece
            if (ChessManager.board[i % 8, i / 8] == null)
            {
                continue;
            }
            // if they are not the color, go to next piece
            if (ChessManager.board[i % 8, i / 8].color != color)
            {
                continue;
            }


            if (ChessManager.board[i % 8, i / 8] is Pawn) {
                material += pawnValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Knight)
            {
                material += knightValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Bishop)
            {
                material += bishopValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Rook)
            {
                material += rookValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Queen)
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
