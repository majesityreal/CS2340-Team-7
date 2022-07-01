using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<int[]> GetLegalMoves(Piece[,] pieces) {
        List<int[]> possibleMoves = new List<int[]>();

        // Top Mid
        for (int i = 1; yCoord - i > -1; i++)
        {
            if (pieces[xCoord, yCoord - i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord - i});
            }
            else
            {
                if (color != pieces[xCoord, yCoord - i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord - i});
                }
                break;
            }
        }

        // Mid Left
        for (int i = 1; xCoord - i > -1; i++)
        {
            if (pieces[xCoord - i, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - i, yCoord});
            }
            else
            {
                if (color != pieces[xCoord - i, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - i, yCoord});
                }
                break;
            }
        }

        // Mid Right
        for (int i = 1; xCoord + i < 8 ; i++)
        {
            if (pieces[xCoord + i, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + i, yCoord});
            }
            else
            {
                if (color != pieces[xCoord + i, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + i, yCoord});
                }
                break;
            }
        }

        // Bottom Mid
        for (int i = 1; yCoord + i < 8; i++)
        {
            if (pieces[xCoord, yCoord + i] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord + i});
            }
            else
            {
                if (color != pieces[xCoord, yCoord + i].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord + i});
                }
                break;
            }
        }

        return possibleMoves;
    }
}