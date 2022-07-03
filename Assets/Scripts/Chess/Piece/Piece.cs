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
                    if (board[i, j].type == PieceType.King)
                    {
                        kingX = board[i, j].xCoord;
                        kingY = board[i, j].yCoord;
                    }
                }
                copyBoard[i, j] = board[i, j];
            }
        }
        
        int count = 0;
        Debug.Log("King Position: x:" + kingX + " y:" + kingY);
        while (count < moves.Count)
        {
            if (!CheckIfSafe(kingX, kingY, moves[count], copyBoard))
            {
                if (board[xCoord, yCoord] != null)
                {
                    Debug.Log(board[xCoord, yCoord].type + "x:" + moves[count][0] + " y:" + moves[count][1] + " is not safe.");
                }
                moves.RemoveAt(count);
                continue;
            }
            count++;
        }

        return moves;
    }

    protected bool CheckIfSafe(int kingX, int kingY, int[] move, Piece[,] board)
    {
        Piece temp = board[xCoord, yCoord];
        board[xCoord, yCoord] = null;
        board[move[0], move[1]] = temp;

        // If this piece is the king, then update the kingX and kingY.
        if (type == PieceType.King)
        {
            kingX = move[0];
            kingY = move[1];
            Debug.Log("This piece is King changing to x:" + kingX + " y:" + kingY);
        }

        PieceType thisType = PieceType.Empty;
        for (int i = 1; i < 8; i++) // Check Up
        {
            if (kingY - i < 0) // Stop when out of bound
            {
                break;
            }

            if (board[kingX, kingY - i] != null) // When its not an empty square
            {
                if (color == board[kingX, kingY - i].color) // If it's an ally, don't look anymore
                {
                    break;
                }
                thisType = board[kingX, kingY - i].type; // If enemy, get it's PieceType
            }
            
            if (thisType == PieceType.King || thisType == PieceType.Queen || thisType == PieceType.Rook) // If one of these, move is not safe.
            {
                return false;
            }
        }

        thisType = PieceType.Empty;
        for (int i = 1; i < 8; i++) // Check Left
        {
            if (kingX - i < 0)
            {
                break;
            }

            if (board[kingX - i, kingY] != null)
            {
                if (color == board[kingX - i, kingY].color)
                {
                    break;
                }
                thisType = board[kingX - i, kingY].type;
            }

            if (thisType == PieceType.King || thisType == PieceType.Queen || thisType == PieceType.Rook)
            {
                return false;
            }
        }

        thisType = PieceType.Empty;
        for (int i = 1; i < 8; i++) // Check Right
        {
            if (kingX + i > 7)
            {
                break;
            }

            if (board[kingX + i, kingY] != null)
            { 
                if (color == board[kingX + i, kingY].color)
                {
                    break;
                }
                thisType = board[kingX + i, kingY].type;
            }

            if (thisType == PieceType.King || thisType == PieceType.Queen || thisType == PieceType.Rook)
            {
                return false;
            }
        }

        thisType = PieceType.Empty;
        for (int i = 1; i < 8; i++)
        {
            if (kingY + i > 7)
            {
                break;
            }

            if (board[kingX, kingY + i] != null) // Down
            {
                if (color == board[kingX, kingY + i].color)
                {
                    break;
                }
                thisType = board[kingX, kingY + i].type;
            }

            if (thisType == PieceType.King || thisType == PieceType.Queen || thisType == PieceType.Rook)
            {
                return false;
            }
        }

        // thisType = PieceType.Empty;
        // for (int i = i; i < 8; i++) // Top Left
        // {
        //     if (kingX )
        // }

        // thisType = PieceType.Empty;


        // thisType = PieceType.Empty;


        // thisType = PieceType.Empty;

        // Check Top Left Diagonal
        for (int i = 1; kingX - i > -1 && kingY - i > -1; i++)
        {
            if (board[kingX - i, kingY - i] == null)
            {
                continue;
            }

            // If bishop, king or queen, check if it's an enemy
            if (board[kingX - i, kingY - i].type == PieceType.Bishop || board[kingX - i, kingY - i].type == PieceType.King || board[kingX - i, kingY - i].type == PieceType.Queen)
            {
                if (color != board[kingX - i, kingY - i].color)
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

            // If bishop, king or queen, check if it's an enemy
            if (board[kingX + i, kingY - i].type == PieceType.Bishop || board[kingX + i, kingY - i].type == PieceType.King || board[kingX + i, kingY - i].type == PieceType.Queen)
            {
                if (color != board[kingX + i, kingY - i].color)
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

            // If bishop, king or queen, check if it's an enemy
            if (board[kingX - i, kingY + i].type == PieceType.Bishop || board[kingX - i, kingY + i].type == PieceType.King || board[kingX - i, kingY + i].type == PieceType.Queen)
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

            // If bishop, king or queen, check if it's an enemy
            if (board[kingX + i, kingY + i].type == PieceType.Bishop || board[kingX + i, kingY + i].type == PieceType.King || board[kingX + i, kingY + i].type == PieceType.Queen)
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
                if (board[kingX - 1, kingY - 2].type == PieceType.Knight)
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
                if (board[kingX + 1, kingY - 2].type == PieceType.Knight)
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
                if (board[kingX - 2, kingY - 1].type == PieceType.Knight)
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
                if (board[kingX + 2, kingY - 1].type == PieceType.Knight)
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
                if (board[kingX - 2, kingY + 1].type == PieceType.Knight)
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
                if (board[kingX + 2, kingY + 1].type == PieceType.Knight)
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
                if (board[kingX - 1, kingY + 2].type == PieceType.Knight)
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
                if (board[kingX + 1, kingY + 2].type == PieceType.Knight)
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
    Rook,
    Empty
}