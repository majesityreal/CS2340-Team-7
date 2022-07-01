using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public King(int color, int xCoord, int yCoord) : base(PieceType.King, color, xCoord, yCoord)
    {  
    }
    
    public override List<int[]> GetLegalMoves(Piece[,] board)
    {
        List<int[]> possibleMoves = new List<int[]>();

        // Top Moves
        if (yCoord != 0)
        {
            // Left
            if (xCoord != 0)
            {
                if (board[xCoord - 1, yCoord - 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 1});
                }
                else
                {
                    if (color != board[xCoord - 1, yCoord - 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 1});
                    }
                }
            }

            // Mid
            if (board[xCoord, yCoord - 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord - 1});
            }
            else
            {
                if (color != board[xCoord, yCoord - 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord - 1});
                }
            }

            // Right
            if (xCoord != 7)
            {
                if (board[xCoord + 1, yCoord - 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 1});
                }
                else
                {
                    if (color != board[xCoord + 1, yCoord - 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 1});
                    }
                }
            }
        }

        // Mid Left
        if (xCoord != 0)
        {
            if (board[xCoord - 1, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord});
            }
            else
            {
                if (color != board[xCoord - 1, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord});
                }
            }
        }

        // Mid Right
        if (xCoord != 7)
        {
            if (board[xCoord + 1, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord});
            }
            else
            {
                if (color != board[xCoord + 1, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord});
                }
            }
        }

        // Bottom Moves
        if (yCoord != 7)
        {
            // Left
            if (xCoord != 0)
            {
                if (board[xCoord - 1, yCoord + 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 1});
                }
                else
                {
                    if (color != board[xCoord - 1, yCoord + 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 1});
                    }
                }
            }

            // Mid
            if (board[xCoord, yCoord + 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord + 1});
            }
            else
            {
                if (color != board[xCoord, yCoord + 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord + 1});
                }
            }

            // Right
            if (xCoord != 7)
            {
                if (board[xCoord + 1, yCoord + 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 1});
                }
                else
                {
                    if (color != board[xCoord + 1, yCoord + 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 1});
                    }
                }
            }
        }

        return possibleMoves;
    }

    public override List<int[]> GetSpecialMoves(Piece[,] board, List<string> moveRecord)
    {
        List<int[]> specialMove = new List<int[]>();

        // Cannot do castling for the first 4 moves
        if (moveRecord.Count < 5)
        {
            return specialMove;
        }

        bool leftCastle = true;
        bool rightCastle = true;
        // Check if any of the squares are occupied (left)
        if (board[1, yCoord] != null || board[2, yCoord] != null || board[3, yCoord] != null)
        {
            leftCastle = false;
        }
        
        // Check if any of the squares are occupied (right)
        if (board[6, yCoord] != null || board[5, yCoord] != null)
        {
            rightCastle = false;
        }

        
        int count = 0;

        if (color == -1)
        {
            count = 1;
        }

        for (int i = count; (leftCastle || rightCastle) && i < moveRecord.Count; i += 2)
        {
            if (moveRecord[i][0] == 'K')
            {
                return specialMove;
            }
            if (moveRecord[i][0] == 'R')
            {
                if (moveRecord[i][1] == '0' && leftCastle)
                {
                    leftCastle = false;
                }
                else if (moveRecord[i][1] == '7' && rightCastle)
                {
                    rightCastle = false;
                }
            }
        }

        if (leftCastle)
        {
            specialMove.Add(new int[2] {xCoord - 2, yCoord});
        }
        if (rightCastle)
        {
            specialMove.Add(new int[2] {xCoord + 2, yCoord});
        }

        return specialMove;
    }
}