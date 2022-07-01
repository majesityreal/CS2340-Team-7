using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public King(int color, int xCoord, int yCoord) : base(PieceType.King, color, xCoord, yCoord)
    {  
    }
    
    public override List<int[]> GetLegalMoves(Piece[,] pieces)
    {
        List<int[]> possibleMoves = new List<int[]>();

        // Top Moves
        if (yCoord != 0)
        {
            // Left
            if (xCoord != 0)
            {
                if (pieces[xCoord - 1, yCoord - 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 1});
                }
                else
                {
                    if (color != pieces[xCoord - 1, yCoord - 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord - 1, yCoord - 1});
                    }
                }
            }

            // Mid
            if (pieces[xCoord, yCoord - 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord - 1});
            }
            else
            {
                if (color != pieces[xCoord, yCoord - 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord - 1});
                }
            }

            // Right
            if (xCoord != 7)
            {
                if (pieces[xCoord + 1, yCoord - 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 1});
                }
                else
                {
                    if (color != pieces[xCoord + 1, yCoord - 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord + 1, yCoord - 1});
                    }
                }
            }
        }

        // Mid Left
        if (xCoord != 0)
        {
            if (pieces[xCoord - 1, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord});
            }
            else
            {
                if (color != pieces[xCoord - 1, yCoord].color)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord});
                }
            }
        }

        // Mid Right
        if (xCoord != 7)
        {
            if (pieces[xCoord + 1, yCoord] == null)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord});
            }
            else
            {
                if (color != pieces[xCoord + 1, yCoord].color)
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
                if (pieces[xCoord - 1, yCoord + 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 1});
                }
                else
                {
                    if (color != pieces[xCoord - 1, yCoord + 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord - 1, yCoord + 1});
                    }
                }
            }

            // Mid
            if (pieces[xCoord, yCoord + 1] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord + 1});
            }
            else
            {
                if (color != pieces[xCoord, yCoord + 1].color)
                {
                    possibleMoves.Add(new int[2] {xCoord, yCoord + 1});
                }
            }

            // Right
            if (xCoord != 7)
            {
                if (pieces[xCoord + 1, yCoord + 1] == null)
                {
                    possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 1});
                }
                else
                {
                    if (color != pieces[xCoord + 1, yCoord + 1].color)
                    {
                        possibleMoves.Add(new int[2] {xCoord + 1, yCoord + 1});
                    }
                }
            }
        }

        return possibleMoves;
    }
}