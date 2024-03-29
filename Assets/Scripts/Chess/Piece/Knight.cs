using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public Knight(int color, int xCoord, int yCoord) : base(PieceType.Knight, color, xCoord, yCoord)
    {
    }
    
    public override List<int[]> GetLegalMoves(Piece[,] board, List<string> moveRecord)
    {
        List<int[]> possibleMoves = new List<int[]>();

        // Up Up Left
        if (xCoord != 0 && yCoord > 1)
        {
            if (board[xCoord - 1, yCoord - 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 2});
            }
            else
            {
                if (color != board[xCoord - 1, yCoord - 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 2});
                }
            }
        }

        // Up Up Right
        if (xCoord != 7 && yCoord > 1)
        {
            if (board[xCoord + 1, yCoord - 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 2});
            }
            else
            {
                if (color != board[xCoord + 1, yCoord - 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 2});
                }
            }
        }

        // Up Left Left
        if (xCoord > 1 && yCoord != 0)
        {
            if (board[xCoord - 2, yCoord - 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 2, yCoord - 1});
            }
            else
            {
                if (color != board[xCoord - 2, yCoord - 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 2, yCoord - 1});
                }
            }
        }

        // Up Right Right
        if (xCoord < 6 && yCoord != 0)
        {
            if (board[xCoord + 2, yCoord - 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 2, yCoord - 1});
            }
            else
            {
                if (color != board[xCoord + 2, yCoord - 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 2, yCoord - 1});
                }
            }
        }

        // Down Left Left
        if (xCoord > 1 && yCoord != 7)
        {
            if (board[xCoord - 2, yCoord + 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 2, yCoord + 1});
            }
            else
            {
                if (color != board[xCoord - 2, yCoord + 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 2, yCoord + 1});
                }
            }
        }

        // Down Right Right
        if (xCoord < 6 && yCoord != 7)
        {
            if (board[xCoord + 2, yCoord + 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 2, yCoord + 1});
            }
            else
            {
                if (color != board[xCoord + 2, yCoord + 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 2, yCoord + 1});
                }
            }
        }

        // Down Down Left
        if (xCoord != 0 && yCoord < 6)
        {
            if (board[xCoord - 1, yCoord + 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 2});
            }
            else
            {
                if (color != board[xCoord - 1, yCoord + 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 2});
                }
            }
        }

        // Down Down Right
        if (xCoord != 7 && yCoord < 6)
        {
            if (board[xCoord + 1, yCoord + 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 2});
            }
            else
            {
                if (color != board[xCoord + 1, yCoord + 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 2});
                }
            }
        }

        return ReturnValidMoves(possibleMoves, board);
    }
}