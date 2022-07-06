using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public Rook(int color, int xCoord, int yCoord) : base(PieceType.Rook, color, xCoord, yCoord)
    {
    }

    public override List<int[]> GetLegalMoves(Piece[,] board, List<string> moveRecord) {
        List<int[]> possibleMoves = new List<int[]>();

        // Top Mid
        for (int i = 1; yCoord - i > -1; i++)
        {
            if (board[xCoord, yCoord - i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord - i});
            }
            else
            {
                if (color != board[xCoord, yCoord - i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord - i});
                }
                break;
            }
        }

        // Mid Left
        for (int i = 1; xCoord - i > -1; i++)
        {
            if (board[xCoord - i, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - i, yCoord});
            }
            else
            {
                if (color != board[xCoord - i, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - i, yCoord});
                }
                break;
            }
        }

        // Mid Right
        for (int i = 1; xCoord + i < 8 ; i++)
        {
            if (board[xCoord + i, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + i, yCoord});
            }
            else
            {
                if (color != board[xCoord + i, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + i, yCoord});
                }
                break;
            }
        }

        // Bottom Mid
        for (int i = 1; yCoord + i < 8; i++)
        {
            if (board[xCoord, yCoord + i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord + i});
            }
            else
            {
                if (color != board[xCoord, yCoord + i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord + i});
                }
                break;
            }
        }

        return ReturnValidMoves(possibleMoves, board);
    }
}