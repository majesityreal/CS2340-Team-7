using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece
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

    public abstract List<int[]> GetLegalMoves(Piece[,] pieces);
    public virtual List<int[]> GetSpecialMoves(Piece[,] pieces, List<string> moveRecord)
    {
        return new List<int[]>();
    }
}

public enum PieceType
{
    Bishop,
    King,
    Knight,
    Pawn,
    Queen,
    Rook
}