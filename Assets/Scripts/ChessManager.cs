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
        pieces = new ArrayList<Piece> (32);
        isPieceOnSquare = new int[8, 8];
    }
}

interface Piece : Monobehavior
{
    (int X, int Y) position;
    int pieceID;
    Sprite sprite;
    int color;
    (int X, int Y) [] possibleMoves;

    void movePosition((int X, int Y) position)
    {
        this.position = position;
    }
}

public class Pawn : Piece
{
    public Pawn((int X, int Y) position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0) { // White
            // sprite = WHITE PAWN IMAGE;
            possibleMoves = new [] {(0, -1)};
        } 
        else 
        {
            // sprite = BLACK PAWN IMAGE;
            possibleMoves = new [] {(0, 1)};
        }
    }
}

public class Bishop : Piece
{
    public Bishop((int X, int Y) position, int pieceID, int color)
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
        possibleMoves = new [] {(-1, -1), (1, -1), (-1, 1), (1, 1)};
    }
}

public class Knight : Piece
{
    public Knight((int X, int Y) position, int pieceID, int color)
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
        possibleMoves = new [] {(-1, -2), (1, -2), (-2, -1), (2, -1), (-2, 1), (2, 1), (-1, 2), (1, 2)};
    }
}

public class Rook : Piece
{
    public Rook((int X, int Y) position, int pieceID, int color)
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
        possibleMoves = new [] {(0, -1), (-1, 0), (1, 0), (0, 1)};
    }
}

public class Queen : Piece
{
    public Queen((int X, int Y) position, int pieceID, int color)
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
        possibleMoves = new [] {(-1, -1), (1, -1), (-1, 1), (1, 1), (0, -1), (-1, 0), (1, 0), (0, 1)};
    }
}

public class King : Piece
{
    public King((int X, int Y) position, int pieceID, int color)
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
        possibleMoves = new [] {(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)};
    }
}