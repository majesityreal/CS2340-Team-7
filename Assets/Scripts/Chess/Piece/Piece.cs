using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        (Piece[,] copyBoard, int kingX, int kingY) = GetDeepCopy(board);
        
        int count = 0;
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

    protected (Piece[,], int, int) GetDeepCopy(Piece[,] board)
    {
        Piece[,] copyBoard = new Piece[8, 8];
        int kingX = -1;
        int kingY = -1;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y] == null)
                {
                    continue;
                }

                if (color == board[x, y].color)
                {
                    if (board[x, y].type == PieceType.King)
                    {
                        kingX = board[x, y].xCoord;
                        kingY = board[x, y].yCoord;
                    }
                }
                copyBoard[x, y] = board[x, y];
            }
        }

        return (copyBoard, kingX, kingY);
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

        for (int i = 1; kingY - i > -1; i++) // Check Up
        {
            if (board[kingX, kingY - i] != null) // Is not empty square
            {
                if (color == board[kingX, kingY - i].color) // Is ally. Stop.
                {
                    break;
                }
                if (board[kingX, kingY - i].type == PieceType.King 
                || board[kingX, kingY - i].type == PieceType.Queen 
                || board[kingX, kingY - i].type == PieceType.Rook)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingX - i > -1; i++) // Check Left
        {
            if (board[kingX - i, kingY] != null)
            {
                if (color == board[kingX - i, kingY].color)
                {
                    break;
                }
                if (board[kingX - i, kingY].type == PieceType.King 
                || board[kingX - i, kingY].type == PieceType.Queen 
                || board[kingX - i, kingY].type == PieceType.Rook)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingX + i < 8; i++) // Check Right
        {
            if (board[kingX + i, kingY] != null)
            { 
                if (color == board[kingX + i, kingY].color)
                {
                    break;
                }
                if (board[kingX + i, kingY].type == PieceType.King 
                || board[kingX + i, kingY].type == PieceType.Queen 
                || board[kingX + i, kingY].type == PieceType.Rook)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingY + i < 8; i++) // Check Down
        {
            if (board[kingX, kingY + i] != null)
            {
                if (color == board[kingX, kingY + i].color)
                {
                    break;
                }
                if (board[kingX, kingY + i].type == PieceType.King 
                || board[kingX, kingY + i].type == PieceType.Queen 
                || board[kingX, kingY + i].type == PieceType.Rook)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingX - i > -1 && kingY - i > -1; i++) // Check Top Left Diagonal
        {
            if (board[kingX - i, kingY - i] != null)
            {
                if (color == board[kingX - i, kingY - i].color)
                {
                    break;
                }
                if (board[kingX - i, kingY - i].type == PieceType.King 
                || board[kingX - i, kingY - i].type == PieceType.Queen 
                || board[kingX - i, kingY - i].type == PieceType.Bishop
                || board[kingX - i, kingY - i].type == PieceType.Pawn)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingX + i < 8 && kingY - i > -1; i++) // Check Top Right Diagonal
        {
            if (board[kingX + i, kingY - i] != null)
            {
                if (color == board[kingX + i, kingY - i].color)
                {
                    break;
                }
                if (board[kingX + i, kingY - i].type == PieceType.King 
                || board[kingX + i, kingY - i].type == PieceType.Queen 
                || board[kingX + i, kingY - i].type == PieceType.Bishop
                || board[kingX + i, kingY - i].type == PieceType.Pawn)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingX - i > -1 && kingY + i < 8; i++) // Bottom Left Diagonal
        {
            if (board[kingX - i, kingY + i] != null)
            {
                if (color == board[kingX - i, kingY + i].color)
                {
                    break;
                }
                if (board[kingX - i, kingY + i].type == PieceType.King 
                || board[kingX - i, kingY + i].type == PieceType.Queen 
                || board[kingX - i, kingY + i].type == PieceType.Bishop
                || board[kingX - i, kingY + i].type == PieceType.Pawn)
                {
                    return false;
                }
            }
        }

        for (int i = 1; kingX + i < 8 && kingY + i < 8; i++) // Bottom Right Diagonal
        {
            if (board[kingX + i, kingY + i] != null)
            {
                if (color == board[kingX + i, kingY + i].color)
                {
                    break;
                }
                if (board[kingX + i, kingY + i].type == PieceType.King 
                || board[kingX + i, kingY + i].type == PieceType.Queen 
                || board[kingX + i, kingY + i].type == PieceType.Bishop
                || board[kingX + i, kingY + i].type == PieceType.Pawn)
                {
                    return false;
                }
            }
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