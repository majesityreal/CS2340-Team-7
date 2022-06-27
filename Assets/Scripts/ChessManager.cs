using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
    List<Piece> pieces;
    int[,] isPieceOnSquare;
    
    // Start is called before the first frame update
    void Start()
    {
        pieces = new List<Piece>(32);
        isPieceOnSquare = new int[8, 8];
    }
}

abstract class Piece
{
    protected (int, int) position;
    protected int pieceID;
    protected Sprite sprite;
    protected int color;
    protected (int, int)[] possibleMoves;

    public void movePosition((int, int) position)
    {
        this.position = position;
    }
}

class Pawn : Piece
{
    Pawn((int, int) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0) { // White
            // sprite = WHITE PAWN IMAGE;
            possibleMoves = new (int, int)[] {(0, -1)};
        } 
        else 
        {
            // sprite = BLACK PAWN IMAGE;
            possibleMoves = new (int, int)[] {(0, 1)};
        }
    }
}

class Bishop : Piece
{
    Bishop((int, int) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;
        if (color == 0) {
            // sprite = WHITE BISHOP IMAGE;
        }
        else
        {
            // sprite = BLACK BISHOP IMAGE;
        }
        possibleMoves = new (int, int)[] {(-1, -1), (1, -1), (-1, 1), (1, 1)};
    }
}

class Knight : Piece
{
    Knight((int, int) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;
        if (color == 0) {
            // sprite = WHITE KNIGHT IMAGE;
        }
        else
        {
            // sprite = BLACK KNIGHT IMAGE;
        }
        possibleMoves = new (int, int)[] {(-1, -2), (1, -2), (-2, -1), (2, -1), (-2, 1), (2, 1), (-1, 2), (1, 2)};
    }
}

class Rook : Piece
{
    Rook((int, int) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;
        if (color == 0) {
            // sprite = WHITE ROOK IMAGE;
        }
        else
        {
            // sprite = BLACK ROOK IMAGE;
        }
        possibleMoves = new (int, int)[] {(0, -1), (-1, 0), (1, 0), (0, 1)};
    }
}

class Queen : Piece
{
    Queen((int, int) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;
        if (color == 0) {
            // sprite = WHITE QUEEN IMAGE;
        }
        else
        {
            // sprite = BLACK QUEEN IMAGE;
        }
        possibleMoves = new (int, int)[] {(-1, -1), (1, -1), (-1, 1), (1, 1), (0, -1), (-1, 0), (1, 0), (0, 1)};
    }
}

class King : Piece
{
    King((int, int) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;
        if (color == 0) {
            // sprite = WHITE KING IMAGE;
        }
        else
        {
            // sprite = BLACK KING IMAGE;
        }
        possibleMoves = new (int, int)[] {(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)};
    }
}