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
        // black pieces
        board[0, 0] = new Rook(-1);
        board[1, 0] = new Knight(-1);
        board[2, 0] = new Bishop(-1);
        board[3, 0] = new Queen(-1);
        board[4, 0] = new King(-1);
        board[5, 0] = new Bishop(-1);
        board[6, 0] = new Knight(-1);
        board[7, 0] = new Rook(-1);

        // black pawns
        board[0, 1] = new Pawn(-1);
        board[1, 1] = new Pawn(-1);
        board[2, 1] = new Pawn(-1);
        board[3, 1] = new Pawn(-1);
        board[4, 1] = new Pawn(-1);
        board[5, 1] = new Pawn(-1);
        board[6, 1] = new Pawn(-1);
        board[7, 1] = new Pawn(-1);

        // white pieces
        board[0, 7] = new Rook(1);
        board[1, 7] = new Knight(1);
        board[2, 7] = new Bishop(1);
        board[3, 7] = new King(1);
        board[4, 7] = new Queen(1);
        board[5, 7] = new Bishop(1);
        board[6, 7] = new Knight(1);
        board[7, 7] = new Rook(1);

        // white pawns
        board[0, 6] = new Pawn(1);
        board[1, 6] = new Pawn(1);
        board[2, 6] = new Pawn(1);
        board[3, 6] = new Pawn(1);
        board[4, 6] = new Pawn(1);
        board[5, 6] = new Pawn(1);
        board[6, 6] = new Pawn(1);
        board[7, 6] = new Pawn(1);

    }

    private void Update()
    {
        Debug.Log(ChessAI.EvaluateBoard());
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
    protected Sprite sprite;
    public int color;
    public int[,] possibleMoves;

    public void movePosition(int position)
    {
        ChessManager.board[position % 8, position / 8] = this;
    }
}

class Pawn : Piece
{
    public Pawn(int color)
    {
        this.color = color;

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
    public Bishop(int color)
    {
        this.color = color;

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
    public Knight(int color)
    {
        this.color = color;

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
    public Rook(int color)
    {
        this.color = color;
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
    public Queen(int color)
    {
        this.color = color;

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
    public King(int color)
    {
        this.color = color;

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