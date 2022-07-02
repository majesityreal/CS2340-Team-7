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

    public abstract List<int[]> GetLegalMoves(Piece[,] board, List<string> moveRecord);

    protected List<int[]> ReturnValidMoves(List<int[]> moves, Piece[,] board)
    {
        Piece[,] copyBoard = new Piece[8, 8];
        int kingX = -1;
        int kingY = -1;

        // Copy over all pieces
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == null)
                {
                    continue;
                }

                if (color == board[i, j].color)
                {
                    if ((int) board[i, j].type == 1)
                    {
                        kingX = board[i, j].xCoord;
                        kingY = board[i, j].yCoord;
                    }
                }
                copyBoard[i, j] = board[i, j];
            }
        }
        
        int count = 0;
        while (count < moves.Count)
        {
            if (!CheckIfSafe(kingX, kingY, moves[count], copyBoard))
            {
                moves.RemoveAt(count);
                continue;
            }
            count++;
        }

        return moves;
    }

    private bool CheckIfSafe(int kingX, int kingY, int[] move, Piece[,] board)
    {
        board[move[0], move[1]] = board[xCoord, yCoord];
        board[xCoord, yCoord] = null;

        // Check Top
        for (int i = 1; kingY - i > -1; i++)
        {
            if (board[kingX, kingY - i] == null)
            {
                continue;
            }

            // If rook, check if it is enemy's queen or rook.
            if ((int) board[kingX, kingY - i].type == 4 || (int) board[kingX, kingY - i].type == 5)
            {
                if (color != board[kingX, kingY - i].color)
                {
                    return false;
                }
            }
            break;
        }

        // Check Left
        for (int i = 1; kingX - i > -1; i++)
        {
            if (board[kingX - i, kingY] == null)
            {
                continue;
            }

            // If rook, check if it is enemy's queen or rook.
            if ((int) board[kingX - i, kingY].type == 4 || (int) board[kingX - i, kingY].type == 5)
            {
                if (color != board[kingX - i, kingY].color)
                {
                    return false;
                }
            }
            break;
        }

        // Check Right
        for (int i = 1; kingX + i < 8; i++)
        {
            if (board[kingX + i, kingY] == null)
            {
                continue;
            }

            // If rook, check if it is enemy's queen or rook.
            if ((int) board[kingX + i, kingY].type == 4 || (int) board[kingX + i, kingY].type == 5)
            {
                if (color != board[kingX + i, kingY].color)
                {
                    return false;
                }
            }
            break;
        }

        // Check Down
        for (int i = 1; kingY + i < 8; i++)
        {
            if (board[kingX, kingY + i] == null)
            {
                continue;
            }

            // If queen or rook, check if it's an enemy.
            if ((int) board[kingX, kingY + i].type == 4 || (int) board[kingX, kingY + i].type == 5)
            {
                if (color != board[kingX, kingY + i].color)
                {
                    return false;
                }
            }
            break;
        }

        // Check Top Left Diagonal
        for (int i = 1; kingX - i > -1 && kingY - i > -1; i++)
        {
            if (board[kingX - i, kingY - i] == null)
            {
                continue;
            }

            // If bishop or queen, check if it's an enemy
            if ((int) board[kingX - i, kingY - i].type == 1 || (int) board[kingX - i, kingY + i].type == 4)
            {
                if (color != board[kingX, kingY + i].color)
                {
                    return false;
                }
            }
            break;
        }

        // Check Top Right Diagonal
        for (int i = 1; kingX + i < 8 && kingY - i > -1; i++)
        {
            if (board[kingX + i, kingY - i] == null)
            {
                continue;
            }

            // If bishop or queen, check if it's an enemy
            if ((int) board[kingX + i, kingY - i].type == 1 || (int) board[kingX + i, kingY + i].type == 4)
            {
                if (color != board[kingX + i, kingY + i].color)
                {
                    return false;
                }
            }
            break;
        }

        // Bottom Left Diagonal
        for (int i = 1; kingX - i > -1 && kingY + i < 8; i++)
        {
            if (board[kingX - i, kingY + i] == null)
            {
                continue;
            }

            // If bishop or queen, check if it's an enemy
            if ((int) board[kingX - i, kingY + i].type == 1 || (int) board[kingX - i, kingY + i].type == 4)
            {
                if (color != board[kingX - i, kingY + i].color)
                {
                    return false;
                }
            }
            break;
        }

        // Bottom Right Diagonal
        for (int i = 1; kingX + i < 8 && kingY + i < 8; i++)
        {
            if (board[kingX + i, kingY + i] == null)
            {
                continue;
            }

            // If bishop or queen, check if it's an enemy
            if ((int) board[kingX + i, kingY + i].type == 1 || (int) board[kingX + i, kingY + i].type == 4)
            {
                if (color != board[kingX + i, kingY + i].color)
                {
                    return false;
                }
            }
            break;
        }

        // 8 Knight Attack Positions
        // Up Up Left
        if (kingX != 0 && kingY > 1)
        {
            if (board[kingX - 1, kingY - 2] != null)
            {
                if ((int) board[kingX - 1, kingY - 2].type == 2)
                {
                    if (color != board[kingX - 1, kingY - 2].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Up Up Right
        if (kingX != 7 && kingY > 1)
        {
            if (board[kingX + 1, kingY - 2] != null)
            {
                if ((int) board[kingX + 1, kingY - 2].type == 2)
                {
                    if (color != board[kingX + 1, kingY - 2].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Up Left Left
        if (kingX > 1 && kingY != 0)
        {
            if (board[kingX - 2, kingY - 1] != null)
            {
                if ((int) board[kingX - 2, kingY - 1].type == 2)
                {
                    if (color != board[kingX - 2, kingY - 1].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Up Right Right
        if (kingX < 6 && kingY != 0)
        {
            if (board[kingX + 2, kingY - 1] != null)
            {
                if ((int) board[kingX + 2, kingY - 1].type == 2)
                {
                    if (color != board[kingX + 2, kingY - 1].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Down Left Left
        if (kingX > 1 && kingY != 7)
        {
            if (board[kingX - 2, kingY + 1] != null)
            {
                if ((int) board[kingX - 2, kingY + 1].type == 2)
                {
                    if (color != board[kingX - 2, kingY + 1].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Down Right Right
        if (kingX < 6 && kingY != 7)
        {
            if (board[kingX + 2, kingY + 1] != null)
            {
                if ((int) board[kingX + 2, kingY + 1].type == 2)
                {
                    if (color != board[kingX + 2, kingY + 1].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Down Down Left
        if (kingX != 0 && kingY < 6)
        {
            if (board[kingX - 1, kingY + 2] != null)
            {
                if ((int) board[kingX - 1, kingY + 2].type == 2)
                {
                    if (color != board[kingX - 1, kingY + 2].color)
                    {
                        return false;
                    }
                }
            }
        }

        // Down Down Right
        if (kingX != 7 && kingY < 6)
        {
            if (board[kingX + 1, kingY + 2] != null)
            {
                if ((int) board[kingX + 1, kingY + 2].type == 2)
                {
                    if (color != board[kingX + 1, kingY + 2].color)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
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