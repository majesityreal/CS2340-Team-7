using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
    Piece[] Pieces;
    
    // Start is called before the first frame update
    void Start()
    {
        Pieces = new Piece[32];
    }
}

public class Piece
{
    int position;
    int pieceNum;
    Sprite sprite;
    int color; // White : 0, Black : 1
    int[] possibleMoves;
    
    public Piece (int position, int pieceNum, int color) {
        this.position = position;
        this.pieceNum = pieceNum;
        this.color = color;
        setMoves();
    }

    void setMoves() 
    {
        switch(this.pieceNum)
        {
            case 0: // Pawn
                if (color == 0) { // White Pawn
                    possibleMoves = new int[] {8};
                    break;
                }
                // Black Pawn
                possibleMoves = new int[] {-8}; 
                break;
            case 1: // Bishop
                possibleMoves = new int[] {-9, -7, 7, 9};
                break;
            case 2: // Knight
                possibleMoves = new int[] {-17, -15, -10, -6, 6, 10, 15, 17};
                break;
            case 3: // Rook
                possibleMoves = new int[] {-8, -1, 1, 8};
                break;
            case 4: // Queen
                possibleMoves = new int[] {-9, -7, 7, 9, -8, -1, 1, 8};
                break;
            case 5: // King
                possibleMoves = new int[] {-9, -8, -7, -1, 1, 7, 8, 9};
                break;
        }
    }

    void setMoves(int pieceNum) {
        this.pieceNum = pieceNum;
        setMoves();
    }
}