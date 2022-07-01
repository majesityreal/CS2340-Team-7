using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public PieceType type;
    public int color;
    public int xCoord;
    public int yCoord;

    public Piece(PieceType type, int color, int xCoord, int yCoord)
    {
        this.type = type;
        this.color = color;
        this.xCoord = xCoord;
        this.yCoord = yCoord;
    }

    public abstract List<int[]> GetLegalMoves(ref Piece[,] pieces);
}

public enum PieceType
{
    Empty = 0,
    Bishop = 1,
    King = 2,
    Knight = 3,
    Pawn = 4,
    Queen = 5,
    Rook = 6
}