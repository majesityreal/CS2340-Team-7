using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
TODO:
1. Check & Checkmate
    - Make kill map
    - If king move is in kill map, don't move
    - If in check, only moves that will block kill map will be allowed.
    - If no moves left, checkmate.
    - Implement bool movable to the piece (If that piece moves, it will make a check)
2. Stalemate
    - King is the only piece left
    - No possible moves left for king.
    - Makes it a draw
3. En Passant
    - When pawn comes to the side of enemy pawn by double move.
    - Can en passant immediately after that turn
4. Castling
    - When king is not in check,
    - When both king and rook did not move
    - When there are no pieces between king and rook
    - When any of the squares between king and rook are in opponent's legal moves.
    - King goes 2 squares left or right
    - Rook goes next to the king, at the opposite side where it originally was.
5. Tie
    - No capture has been made and no pawn has been moved inthe last 50 moves.
    - Repetition of 3 moves
    - Agreement
    - Insufficient materials:
        - A long king
        - A king and bishop
        - A king and knight
6. Promotion
    - When pawn reaches the enemies base line(?)
    - Promotes pawn into Queen/Rook/Knight/Bishop
*/


public class ChessManager : MonoBehaviour
{
    public static Dictionary<int, Piece> board;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBoard();
    }

    public void InitializeBoard()
    {
        // Dictionary<piece position, piece object>
        board = new Dictionary<int, Piece>();

        // black pieces
        board.Add(0, new Rook(0, -1));
        board.Add(1, new Knight(1, -1));
        board.Add(2, new Bishop(2, -1));
        board.Add(3, new Queen(3, -1));
        board.Add(4, new King(4, -1));
        board.Add(5, new Bishop(5, -1));
        board.Add(6, new Knight(6, -1));
        board.Add(7, new Rook(7, -1));

        // black pawns
        for (int i = 8; i < 16; i++) 
        {
            board.Add(i, new Pawn(i, -1));
        }

        // white pawns
        for (int j = 48; j < 56; j++) 
        {
            board.Add(j, new Pawn(j, 1));
        }

        // white pieces
        board.Add(56, new Rook(56, 1));
        board.Add(57, new Knight(57, 1));
        board.Add(58, new Bishop(58, 1));
        board.Add(59, new Queen(59, 1));
        board.Add(60, new King(60, 1));
        board.Add(61, new Bishop(61, 1));
        board.Add(62, new Knight(62, 1));
        board.Add(63, new Rook(63, 1));
    }

    private void Update()
    {
        Debug.Log(ChessAI.EvaluateBoard());
    }
}

public abstract class Piece
{
    protected Sprite sprite;
    protected int xCoord;
    protected int yCoord;
    protected int color;
    protected int[,] possibleMoves;
    protected List<int> legalMoves;
    protected int pieceID; // 1: Pawn, 2: Bishop, 3: Knight, 4: Rook, 5: Queen, 6: King
    protected static int posWhiteKing;
    protected static int posBlackKing;
    protected static List<int> whiteKillMap;
    protected static List<int> blackKillMap;
    // Default constructor
    public Piece(int position, int color)
    {
        this.xCoord = position % 8;
        this.yCoord = position / 8;
        this.color = color;
        this.legalMoves = new List<int>();
    }

    public int GetColor()
    {
        return this.color;
    }

    // Returns a list [# of legal move] = position (0-64)
    public List<int> GetLegalMoves(Dictionary<int, Piece> dict)
    {
        UpdateLegalMoves(dict);
        return this.legalMoves;
    }

    public int GetPieceID()
    {
        return this.pieceID;
    }


    // Method that assumes that a king is at a position, then checks if it is check for that color's king.
    protected bool CheckIfCheck(int pos, int color)
    {
        if (color == 1)
        {
            UpdateBlackKillMap();
            return blackKillMap.Contains(pos);
        }
        UpdateWhiteKillMap();
        return whiteKillMap.Contains(pos);
    }

    protected void UpdateWhiteKillMap()
    {
        whiteKillMap = new List<int>();

        foreach(KeyValuePair<int, Piece> entry in ChessManager.board)
        {
            if (entry.Value.GetColor() == -1)
            {
                continue;
            }

            whiteKillMap = whiteKillMap.Union(entry.Value.legalMoves).ToList();
        }
    }

    protected void UpdateBlackKillMap()
    {
        blackKillMap = new List<int>();

        foreach(KeyValuePair<int, Piece> entry in ChessManager.board)
        {
            if (entry.Value.GetColor() == 1)
            {
                continue;
            }

            blackKillMap = blackKillMap.Union(entry.Value.legalMoves).ToList();
        }
    }

    // Updates legal moves and handles edge cases
    public abstract void UpdateLegalMoves(Dictionary<int, Piece> dict);

    // Removes piece from dictionary, then reassigns location.
    public virtual void MovePosition(int startPosition, int endPosition)
    {
        // Assume that the system already updated the legal moves
        if (legalMoves.Contains(endPosition))
        {
            ChessManager.board.Remove(startPosition);
            this.xCoord = endPosition % 8;
            this.yCoord = endPosition / 8;
            ChessManager.board.Add(endPosition, this);
        }
    }

    protected bool IsSquareOccupied(int pos, Dictionary<int, Piece> dict)
    {
        return dict.ContainsKey(pos);
    }

    protected bool IsSameColor(int pos, int color, Dictionary<int, Piece> dict)
    {
        if (IsSquareOccupied(pos, dict))
        {
            return color == ChessManager.board[pos].GetColor();
        }
        return false;
    }
}

class Pawn : Piece
{
    // Used for double move of a Pawn
    bool isFirstMove;
    bool didEnPassant;
    bool didDoubleMove;

    public Pawn(int position, int color) : base(position, color)
    { 
        isFirstMove = true;
        didDoubleMove = false;
        pieceID = 1;

        if (color == 0)
        {
            // TODO: sprite = WHITE PAWN IMAGE;
            possibleMoves = new int[1, 2]
            {
                {0, -1}
            };
        }
        else
        {
            // TODO: sprite = BLACK PAWN IMAGE;
            possibleMoves = new int[1, 2]
            {
                {0, 1}
            };
        }
    }

    public override void UpdateLegalMoves(Dictionary<int, Piece> dict)
    {
        // Clears list
        legalMoves = new List<int>();

        // Add double move to the legal moves list
        if (isFirstMove)
        {
            int doubleMove = yCoord * 8 + possibleMoves[0, 1] * 16 + xCoord;

            // Check if both 2 squares are empty before moving.
            if (!IsSquareOccupied(doubleMove, dict) && !IsSquareOccupied(yCoord * 8 + possibleMoves[0, 1] * 8 + xCoord, dict))
            {
                legalMoves.Add(doubleMove);
            }
        }

        // Add single move to the legal moves list
        int singleMove = yCoord * 8 + possibleMoves[0, 1] * 8 + xCoord;
        if (!IsSquareOccupied(singleMove, dict))
        {
            legalMoves.Add(singleMove);
        }

        // Add capturing the left to the legal moves list
        int captureLeft = yCoord * 8 + possibleMoves[0, 1] * 8 + xCoord - 1;
        if (!IsSameColor(captureLeft, color, dict))
        {
            // TODO: Capture the unit on the left
            legalMoves.Add(captureLeft);
        }

        // Add capturing the right to the legal moves list
        int captureRight = yCoord * 8 + possibleMoves[0, 1] * 8 + xCoord + 1;
        if (!IsSameColor(captureRight, color, dict))
        {
            // TODO: Capture the unit on the right
            legalMoves.Add(captureRight);
        }

        // Add En Passant (left) to the legal moves list
        int enPassantLeft = yCoord * 8 + xCoord - 1;
        if (CheckEnPassant(enPassantLeft, color, dict))
        {
            // TODO: Capture the pawn on the left
            if (!legalMoves.Contains(captureLeft)) {
                legalMoves.Add(captureLeft);
            }
        }

        // Add En Passant (right) to the legal moves list
        int enPassantRight = yCoord * 8 + xCoord + 1;
        if (CheckEnPassant(enPassantRight, color, dict))
        {
            // TODO: Capture the pawn on the right
            if (!legalMoves.Contains(captureRight)) {
                legalMoves.Add(captureRight);
            }
        }
    }

    private bool CheckEnPassant(int pos, int color, Dictionary<int, Piece> dict)
    {
        if (IsSquareOccupied(pos, dict))
        {
            return color != ChessManager.board[pos].GetColor() && ChessManager.board[pos].GetPieceID() == 1;
        }
        return false;
    }

    public bool GetDidDoubleMove()
    {
        return didDoubleMove;
    }

    public override void MovePosition(int startPosition, int endPosition)
    {
        if (isFirstMove) 
        {
            isFirstMove = false;
        }
        if (didDoubleMove)
        {
            didDoubleMove = false;
        }
        if (Math.Abs(endPosition - startPosition) == 16)
        {
            didDoubleMove = true;
        }
        base.MovePosition(startPosition, endPosition);
    }
}

class Bishop : Piece
{
    public Bishop(int position, int color) : base(position, color)
    {
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

    public override void UpdateLegalMoves(Dictionary<int, Piece> dict)
    {
        // Clears list
        legalMoves = new List<int>();
        
        for (int i = 0; i < 4; i++)
        {
            int newXCoord = xCoord + possibleMoves[i, 0];
            int newYCoord = yCoord + possibleMoves[i, 1];

            while (newXCoord > -1 && newXCoord < 8 && newYCoord > -1 && newYCoord < 8)
            {
                int newPosition = newXCoord + newYCoord * 8;

                if (!IsSquareOccupied(newPosition, dict))
                {
                    legalMoves.Add(newPosition);
                }
                else if (!IsSameColor(newPosition, color, dict))
                {
                    legalMoves.Add(newPosition);
                    break;
                }

                newXCoord += possibleMoves[i, 0];
                newYCoord += possibleMoves[i, 1];
            }
        }
    }
}

class Knight : Piece
{
    public Knight(int position, int color) : base(position, color)
    {
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

    public override void UpdateLegalMoves(Dictionary<int, Piece> dict)
    {
        // Clears list
        legalMoves = new List<int>();

        for (int i = 0; i < 8; i++)
        {
            int newXCoord = xCoord + possibleMoves[i, 0];
            int newYCoord = yCoord + possibleMoves[i, 1];
            if (newXCoord > -1 && newXCoord < 8 && newYCoord > -1 && newYCoord < 8)
            {
                int newPosition = newXCoord + newYCoord * 8;

                if (!IsSquareOccupied(newPosition, dict) || !IsSameColor(newPosition, color, dict))
                {
                    legalMoves.Add(newPosition);
                }
            }
        }
    }
}

class Rook : Piece
{
    public Rook(int position, int color) : base(position, color)
    {
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

    public override void UpdateLegalMoves(Dictionary<int, Piece> dict)
    {
        // Clears list
        legalMoves = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int newXCoord = xCoord + possibleMoves[i, 0];
            int newYCoord = yCoord + possibleMoves[i, 1];

            while (newXCoord > -1 && newXCoord < 8 && newYCoord > -1 && newYCoord < 8)
            {
                int newPosition = newXCoord + newYCoord * 8;

                if (!IsSquareOccupied(newPosition, dict))
                {
                    legalMoves.Add(newPosition);
                }
                else if (!IsSameColor(newPosition, color, dict))
                {
                    legalMoves.Add(newPosition);
                    break;
                }

                newXCoord += possibleMoves[i, 0];
                newYCoord += possibleMoves[i, 1];
            }
        }
    }
}

class Queen : Piece
{
    public Queen(int position, int color) : base(position, color)
    {
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

    public override void UpdateLegalMoves(Dictionary<int, Piece> dict)
    {
        // Clears list
        legalMoves = new List<int>();

        for (int i = 0; i < 8; i++)
        {
            int newXCoord = xCoord + possibleMoves[i, 0];
            int newYCoord = yCoord + possibleMoves[i, 1];

            while (newXCoord > -1 && newXCoord < 8 && newYCoord > -1 && newYCoord < 8)
            {
                int newPosition = newXCoord + newYCoord * 8;

                if (!IsSquareOccupied(newPosition, dict))
                {
                    legalMoves.Add(newPosition);
                }
                else if (!IsSameColor(newPosition, color, dict))
                {
                    legalMoves.Add(newPosition);
                    break;
                }

                newXCoord += possibleMoves[i, 0];
                newYCoord += possibleMoves[i, 1];
            }
        }
    }
}

class King : Piece
{
    public King(int position, int color) : base(position, color)
    {
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

        if (color == 1)
        {
            posWhiteKing = position;
        }
        else
        {
            posWhiteKing = position;
        }
    }

    public override void UpdateLegalMoves(Dictionary<int, Piece> dict)
    {
        // Clears list
        legalMoves = new List<int>();

        for (int i = 0; i < 8; i++)
        {
            int newXCoord = xCoord + possibleMoves[i, 0];
            int newYCoord = yCoord + possibleMoves[i, 1];

            if (newXCoord > -1 && newXCoord < 8 && newYCoord > -1 && newYCoord < 8)
            {
                int newPosition = newXCoord + newYCoord * 8;

                if (!IsSquareOccupied(newPosition, dict))
                {
                    legalMoves.Add(newPosition);
                }
                else if (!IsSameColor(newPosition, color, dict))
                {
                    legalMoves.Add(newPosition);
                    break;
                }
            }
        }
    }

    private bool CheckUnderAttack(int pos, int color) 
    {

    }
}