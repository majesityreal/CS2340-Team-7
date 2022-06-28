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
        board[0, 0] = new Pawn(1);
        board[0, 1] = new Pawn(-1);
        board[0, 0] = new Pawn(-1);

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
    Pawn(int color)
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
    Bishop(int color)
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
    Knight(int color)
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
    Rook(int color)
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
    Queen(int color)
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
    King(int color)
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