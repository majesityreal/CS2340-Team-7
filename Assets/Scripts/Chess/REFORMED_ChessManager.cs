using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REFORMED_ChessManager : MonoBehaviour
{
    // [xCoord, yCoord]
    public Piece[,] pieces;
    public List<int[]> legalMoves;
    public List<string> moveRecord;
    public bool isWhiteTurn;

    void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        isWhiteTurn = true;
        moveRecord = new List<string>();

        // Clears 2D array
        pieces = new Piece[8, 8];

        // Black Pieces [new Piece(Type, Color, X, Y)]
        pieces[0, 0] = new Piece(PieceType.Rook, -1, 0, 0);
        pieces[1, 0] = new Piece(PieceType.Knight, -1, 1, 0);
        pieces[2, 0] = new Piece(PieceType.Bishop, -1, 2, 0);
        pieces[3, 0] = new Piece(PieceType.Queen, -1, 3, 0);
        pieces[4, 0] = new Piece(PieceType.King, -1, 4, 0);
        pieces[5, 0] = new Piece(PieceType.Bishop, -1, 5, 0);
        pieces[6, 0] = new Piece(PieceType.Knight, -1, 6, 0);
        pieces[7, 0] = new Piece(PieceType.Rook, -1, 7, 0);
        for (int i = 0; i < 8; i++)
        {
            pieces[i, 1] = new Piece(PieceType.Pawn, -1, i, 1);
        }

        // White Pieces [new Piece(Type, Color, X, Y)]
        for (int j = 0; j < 8; j++)
        {
            pieces[j, 6] = new Piece(PieceType.Pawn, 1, i, 6);
        }
        pieces[0, 7] = new Piece(PieceType.Rook, 1, 0, 7);
        pieces[1, 7] = new Piece(PieceType.Knight, 1, 1, 7);
        pieces[2, 7] = new Piece(PieceType.Bishop, 1, 2, 7);
        pieces[3, 7] = new Piece(PieceType.King, 1, 3, 7);
        pieces[4, 7] = new Piece(PieceType.Queen, 1, 4, 7);
        pieces[5, 7] = new Piece(PieceType.Bishop, 1, 5, 7);
        pieces[6, 7] = new Piece(PieceType.Knight, 1, 6, 7);
        pieces[7, 7] = new Piece(PieceType.Rook, 1, 7, 7);
    }

    public void MovePosition(int oldX, int oldY, int newX, int newY)
    {
        string record = "";
        switch ((int) pieces[oldX, oldY].PieceType)
        {
            case 1:
                record += "B";
                break;
            case 2:
                record += "K";
                break;
            case 3:
                record += "N";
                break;
            case 4:
                break;
            case 5:
                record += "Q";
                break;
            case 6:
                record += "R";
                break;
        };
        
        if (pieces[newX, newY] != null)
        {
            record += "x";
        }
        record += "" + newX + newY;
        moveRecord.Add(record);
        
        pieces[oldX, oldY].xCoord = newX;
        pieces[oldX, oldY].yCoord = newY;
        pieces[newX, newY] = Piece[oldX, oldY];
        pieces[oldX, oldY] = null;
        isWhiteTurn = !isWhiteTurn;
    }

    public void CheckMate(int color)
    {
        if (color == 1)
        {
            // TODO: White Win
        }
        else
        {
            // TODO: Black Win
        }
    }
}