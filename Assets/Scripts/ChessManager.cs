using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
/*    public static List<Piece> pieces;
*/    
    public static Piece[,] board;
    int[,] positions;

    // Start is called before the first frame update
    void Start()
    {
/*        pieces = new List<Piece>(32);
*/        board = new Piece[8, 8];
    }

/*    public int[,] getPositions()
    {
        int[,] temp = new int[32, 2];
        int count = 0;
        foreach (Piece piece in pieces)
        {
            if (piece != null)
            {
                int[] temp2 = piece.getPosition();
                temp[count, 0] = temp2[0];
                temp[count, 1] = temp2[1];
            }
        }
        return temp;
    }*/
}

public abstract class Piece
{
    protected int[] position;
    protected int pieceID;
    protected Sprite sprite;
    public int color;
    protected int[,] possibleMoves;

    public void movePosition(int[] position)
    {
        this.position = position;
    }

    public int[] getPosition()
    {
        return this.position;
    }
}

class Pawn : Piece
{
    Pawn(int[] position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0)
        { // White
            // sprite = WHITE PAWN IMAGE;
            possibleMoves = new int[1, 2] { { 0, -1 } };
        }
        else
        {
            // sprite = BLACK PAWN IMAGE;
            possibleMoves = new int[1, 2] { { 0, 1 } };
        }
    }
}

class Bishop : Piece
{
    Bishop(int[] position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0)
        {
            // sprite = WHITE BISHOP IMAGE;
        }
        else
        {
            // sprite = BLACK BISHOP IMAGE;
        }

        possibleMoves = new int[4, 2]
        {
            {-1, -1},
            {1, -1},
            {-1, 1},
            {1, 1}
        };
    }
}

class Knight : Piece
{
    Knight(int[] position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0)
        {
            // sprite = WHITE KNIGHT IMAGE;
        }
        else
        {
            // sprite = BLACK KNIGHT IMAGE;
        }

        possibleMoves = new int[8, 2]
        {
            {-1, -2},
            {1, -2},
            {-2, -1},
            {2, -1},
            {-2, 1},
            {2, 1},
            {-1, 2},
            {1, 2}
        };
    }
}

class Rook : Piece
{
    Rook(int[] position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;
        if (color == 0)
        {
            // sprite = WHITE ROOK IMAGE;
        }
        else
        {
            // sprite = BLACK ROOK IMAGE;
        }
        possibleMoves = new int[4, 2]
        {
            {0, -1},
            {-1, 0},
            {1, 0},
            {0, 1}
        };
    }
}

class Queen : Piece
{
    Queen(int[] position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0)
        {
            // sprite = WHITE QUEEN IMAGE;
        }
        else
        {
            // sprite = BLACK QUEEN IMAGE;
        }

        possibleMoves = new int[8, 2]
        {
            {-1, -1},
            {1, -1},
            {-1, 1},
            {1, 1},
            {0, -1},
            {-1, 0},
            {1, 0},
            {0, 1}
        };
    }
}

class King : Piece
{
    King(int[] position, int pieceID, int color)
    {
        position = this.position;
        pieceID = this.pieceID;
        color = this.color;

        if (color == 0)
        {
            // sprite = WHITE KING IMAGE;
        }
        else
        {
            // sprite = BLACK KING IMAGE;
        }

        possibleMoves = new int[8, 2]
        {
            {-1, -1},
            {0, -1},
            {1, -1},
            {-1, 0},
            {1, 0},
            {-1, 1},
            {0, 1},
            {1, 1}
        };
    }
}