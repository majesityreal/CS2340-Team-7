using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public King(int color, int xCoord, int yCoord) : base(PieceType.King, color, xCoord, yCoord)
    {  
    }
    
    public override List<int[]> GetLegalMoves(Piece[,] board, List<string> moveRecord)
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

        List<int[]> specials = GetSpecialMoves(board, moveRecord);
        Piece[,] copyBoard = (Piece[,]) board.Clone();

        /* 
        Check if the King is under check
            If so, cannot do Castling
            If not, check if the square between Castling is not under attack.
                If so, cannot do Castling
                If not, can do Castling 
        */
        if (specials != null)
        {
            foreach(int[] move in specials)
            {
                if (CheckIfSafe(xCoord, yCoord, new int[] {move[0], move[1]}, copyBoard))
                {
                    if (move[0] == 6)
                    {
                        if (CheckIfSafe(xCoord, yCoord, new int[] {move[0] + 1, move[1]}, copyBoard))
                        {
                            possibleMoves.Add(move);   
                        }
                    }
                    else
                    {
                        if (CheckIfSafe(xCoord, yCoord, new int[] {move[0] - 1, move[1]}, copyBoard))
                        {
                            possibleMoves.Add(move); 
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        return possibleMoves;
    }

    private List<int[]> GetSpecialMoves(Piece[,] board, List<string> moveRecord)
    {
        // Cannot do castling for the first 4 moves
        if (moveRecord.Count < 5)
        {
            return null;
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

        if (!leftCastle && !rightCastle)
        {
            return null;
        }

        // Adjust count start for each team (Evens for white, Odds for black)
        int count = 0;
        if (color == -1)
        {
            count = 1;
        }
        
        // Check every single move record to find whether the king or the rook moved
        for (int i = count; (leftCastle || rightCastle) && i < moveRecord.Count; i += 2)
        {
            // King moved? Don't even look anymore
            if (moveRecord[i][0] == 'K')
            {
                return null;
            }

            // Rook moved? Check which one moved
            if (moveRecord[i][0] == 'R')
            {
                if (moveRecord[i][1] == '0' && leftCastle) // Left Rook
                {
                    leftCastle = false;
                }
                else if (moveRecord[i][1] == '7' && rightCastle) // Right Rook
                {
                    rightCastle = false;
                }
            }
        }

        List<int[]> specialMove = new List<int[]>();

        if (leftCastle)
        {
            specialMove.Add(new int[2] {xCoord - 2, yCoord});
        }

        if (rightCastle)
        {
            specialMove.Add(new int[2] {xCoord + 2, yCoord});
        }
        
        if (specialMove.Count == 0)
        {
            return null;
        }
        return specialMove;
    }
}