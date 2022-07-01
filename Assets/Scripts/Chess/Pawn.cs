using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<int[]> GetLegalMoves(ref Piece[,] pieces)
    {
        List<int[]> possibleMoves = new List<int[]>();
        int move = 0;

        if (color == 1)
        {
            move = -1;
        }
        else
        {
            move = 1;
        }
        
        // Single Move
        if (pieces[xCoord, yCoord + move] == null)
        {
            possibleMoves.Add(new int[2] {xCoord, yCoord + move});
        }

        // Double Move
        if ((yCoord == 6 && color == 1) || (yCoord == 1 && color == -1))
        {
            if (pieces[xCoord, yCoord + move] == null && pieces[xCoord, yCoord + move * 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord + move * 2});
            }
        }

        // Left Capture
        if (xCoord != 0 && pieces[xCoord - 1, yCoord + move] == null)
        {
            if (color != pieces[xCoord - 1, yCoord + move].color)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord + move});
            }
        }

        // Right Capture
        if (xCoord != 7 && pieces[xCoord + 1, yCoord + move] == null)
        {
            if (color != pieces[xCoord + 1, yCoord + move].color)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord + move});
            }
        }

        return possibleMoves;
    }
}