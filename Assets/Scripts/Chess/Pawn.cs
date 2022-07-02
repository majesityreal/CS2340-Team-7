using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public Pawn(int color, int xCoord, int yCoord) : base(PieceType.Pawn, color, xCoord, yCoord)
    {
    }

    public override List<int[]> GetLegalMoves(Piece[,] board, List<string> moveRecord)
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
        if (board[xCoord, yCoord + move] == null)
        {
            possibleMoves.Add(new int[2] {xCoord, yCoord + move});
        }

        // Double Move
        if ((yCoord == 6 && color == 1) || (yCoord == 1 && color == -1))
        {
            if (board[xCoord, yCoord + move] == null && board[xCoord, yCoord + move * 2] == null)
            {
                possibleMoves.Add(new int[2] {xCoord, yCoord + move * 2});
            }
        }

        // Left Capture
        if (xCoord != 0 && board[xCoord - 1, yCoord + move] == null)
        {
            if (color != board[xCoord - 1, yCoord + move].color)
            {
                possibleMoves.Add(new int[2] {xCoord - 1, yCoord + move});
            }
        }

        // Right Capture
        if (xCoord != 7 && board[xCoord + 1, yCoord + move] == null)
        {
            if (color != board[xCoord + 1, yCoord + move].color)
            {
                possibleMoves.Add(new int[2] {xCoord + 1, yCoord + move});
            }
        }

        List<int[]> specials = GetSpecialMoves(board, moveRecord);
        if (specials != null)
        {
            for (int i = 0; i < specials.Count; i++)
            {
                possibleMoves.Add(specials[i]);
            }
        }

        return possibleMoves;
    }

    private List<int[]> GetSpecialMoves(Piece[,] board, List<string> moveRecord)
    {
        // Don't check En Passant on the first 4 moves
        if (moveRecord.Count < 5)
        {
            return null;
        }
        // Check if it is the pawn's turn.
        if (color == 1 && moveRecord.Count % 2 != 0)
        {
            return null;
        }
        else if (color == -1 && moveRecord.Count % 2 == 0)
        {
            return null;
        }
        
        List<int[]> specialMove = new List<int[]>();
        
        // En Passant
        string lastMove = moveRecord[moveRecord.Count - 1];
        if (lastMove.Length == 4)
        {
            int prevY = lastMove[1] - '0';
            int lastMoveX = lastMove[2] - '0';
            int lastMoveY = lastMove[3] - '0';
            
            if (lastMoveX == xCoord + 1 || lastMoveX == xCoord - 1)
            {
                if (lastMoveY - prevY == 2) // White Pawn double moved
                {
                    specialMove.Add(new int[2] {lastMoveX, lastMoveY - 1});
                }
                else if (prevY - lastMoveY == 2) // Black Pawn double moved
                {
                    specialMove.Add(new int[2] {lastMoveX, lastMoveY + 1});
                }
            }
        }

        if (specialMove.Count == 0)
        {
            return null;
        }
        return specialMove;
    }
}