using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public Pawn(int color, int xCoord, int yCoord) : base(PieceType.Pawn, color, xCoord, yCoord)
    {
    }

    public override List<int[]> GetLegalMoves(Piece[,] pieces)
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

    public override List<int[]> GetSpecialMoves(Piece[,] pieces, List<string> moveRecord)
    {
        List<int[]> specialMove = new List<int[]>();
        
        // Check if it is the pawn's turn.
        if (color == 1 && moveRecord.Count % 2 != 0)
        {
            return specialMove;
        }
        else if (color == 0 && moveRecord.Count % 2 == 0)
        {
            return specialMove;
        }
        
        // En Passant
        string lastMove = moveRecord[moveRecord.Count - 1];
        if (lastMove.Length == 2)
        {
            int lastMoveX = lastMove[0] - '0';
            int lastMoveY = lastMove[1] - '0';
            
            if (lastMoveX == xCoord + 1 || lastMoveX == xCoord - 1)
            {
                if (color == 1 && lastMoveY == 3)
                {
                    specialMove.Add(new int[2] {lastMoveX, lastMoveY - 1});
                }
                else if (color == -1 && lastMoveY == 4)
                {
                    specialMove.Add(new int[2] {lastMoveX, lastMoveY + 1});
                }
            }
        }

        return specialMove;
    }
}