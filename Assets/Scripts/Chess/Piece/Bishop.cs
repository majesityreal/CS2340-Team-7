using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public Bishop(int color, int xCoord, int yCoord) : base(PieceType.Bishop, color, xCoord, yCoord)
    {
    }

    public override List<int[]> GetLegalMoves(Piece[,] board, List<string> moveRecord)
    {
        List<int[]> possibleMoves = new List<int[]>();

        // Top Left
        for (int i = 1; xCoord - i > -1 && yCoord - i > -1; i++)
        {
            if (board[xCoord - i, yCoord - i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - i, yCoord - i});
            }
            else
            {
                if (color != board[xCoord - i, yCoord - i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - i, yCoord - i});
                }
                break;
            }
        }

        // Top Right
        for (int i = 1; xCoord + i < 8 && yCoord - i > -1; i++)
        {
            if (board[xCoord + i, yCoord - i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + i, yCoord - i});
            }
            else
            {
                if (color != board[xCoord + i, yCoord - i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + i, yCoord - i});
                }
                break;
            }
        }

        // Bottom Left
        for (int i = 1; xCoord - i > -1 && yCoord + i < 8; i++)
        {
            if (board[xCoord - i, yCoord + i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - i, yCoord + i});
            }
            else
            {
                if (color != board[xCoord - i, yCoord + i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - i, yCoord + i});
                }
                break;
            }
        }

        // Bottom Right
        for (int i = 1; xCoord + i < 8 && yCoord + i < 8; i++)
        {
            if (board[xCoord + i, yCoord + i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + i, yCoord + i});
            }
            else
            {
                if (color != board[xCoord + i, yCoord + i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + i, yCoord + i});
                }
                break;
            }
        }

        return ReturnValidMoves(possibleMoves, board);
    }
}