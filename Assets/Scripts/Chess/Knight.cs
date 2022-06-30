using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<int[]> GetLegalMoves(ref Piece[,] pieces)
    {
        List<int[]> possibleMoves = new List<int[]>();

        // Up Up Left
        if (xCoord != 0 && yCoord > 1)
        {
            if (pieces[xCoord - 1, yCoord - 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 2});
            }
            else
            {
                if (color != pieces[xCoord - 1, yCoord - 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 2});
                }
            }
        }

        // Up Up Right
        if (xCoord != 7 && yCoord > 1)
        {
            if (pieces[xCoord + 1, yCoord - 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 2});
            }
            else
            {
                if (color != pieces[xCoord + 1, yCoord - 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 2});
                }
            }
        }

        // Up Left Left
        if (xCoord > 1 && yCoord != 0)
        {
            if (pieces[xCoord - 2, yCoord - 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 2, yCoord - 1});
            }
            else
            {
                if (color != pieces[xCoord - 2, yCoord - 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 2, yCoord - 1});
                }
            }
        }

        // Up Right Right
        if (xCoord < 6 && yCoord != 0)
        {
            if (pieces[xCoord + 2, yCoord - 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 2, yCoord - 1});
            }
            else
            {
                if (color != pieces[xCoord + 2, yCoord - 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 2, yCoord - 1});
                }
            }
        }

        // Down Left Left
        if (xCoord > 1 && yCoord != 7)
        {
            if (pieces[xCoord - 2, yCoord + 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 2, yCoord + 1});
            }
            else
            {
                if (color != pieces[xCoord - 2, yCoord + 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 2, yCoord + 1});
                }
            }
        }

        // Down Right Right
        if (xCoord < 6 && yCoord != 7)
        {
            if (pieces[xCoord + 2, yCoord + 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 2, yCoord + 1});
            }
            else
            {
                if (color != pieces[xCoord + 2, yCoord + 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 2, yCoord + 1});
                }
            }
        }

        // Down Down Left
        if (xCoord != 0 && yCoord < 6)
        {
            if (pieces[xCoord - 1, yCoord + 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 2});
            }
            else
            {
                if (color != pieces[xCoord - 1, yCoord + 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 2});
                }
            }
        }

        // Down Down Right
        if (xCoord != 7 && yCoord < 6)
        {
            if (pieces[xCoord + 1, yCoord + 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 2});
            }
            else
            {
                if (color != pieces[xCoord + 1, yCoord + 2].color)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 2});
                }
            }
        }

        return possibleMoves;
    }
}