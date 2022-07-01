using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChessManager : MonoBehaviour
{
    // [xCoord, yCoord]
    public static Piece[,] board;
    public List<string> moveRecord;
    public bool isWhiteTurn;

    void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        isWhiteTurn = true;
        moveRecord = new List<string>();

        // Clears 2D array
        board = new Piece[8, 8];

        // Black pieces [new Piece(Color, X, Y)]
        board[0, 0] = new Rook(-1, 0, 0);
        board[1, 0] = new Knight(-1, 1, 0);
        board[2, 0] = new Bishop(-1, 2, 0);
        board[3, 0] = new Queen(-1, 3, 0);
        board[4, 0] = new King(-1, 4, 0);
        board[5, 0] = new Bishop(-1, 5, 0);
        board[6, 0] = new Knight(-1, 6, 0);
        board[7, 0] = new Rook(-1, 7, 0);
        for (int i = 0; i < 8; i++)


        // White pieces [new Piece(Color, X, Y)]
        for (int j = 0; j < 8; j++)
        {
            board[j, 6] = new Pawn(1, j, 6);
        }
        board[0, 7] = new Rook(1, 0, 7);
        board[1, 7] = new Knight(1, 1, 7);
        board[2, 7] = new Bishop(1, 2, 7);
        board[3, 7] = new King(1, 3, 7);
        board[4, 7] = new Queen(1, 4, 7);
        board[5, 7] = new Bishop(1, 5, 7);
        board[6, 7] = new Knight(1, 6, 7);
        board[7, 7] = new Rook(1, 7, 7);
    }

    public void MovePosition(int oldX, int oldY, int newX, int newY)
    {
        RecordMove(oldX, oldY, newX, newY);
        
        board[oldX, oldY].xCoord = newX;
        board[oldX, oldY].yCoord = newY;
        board[newX, newY] = board[oldX, oldY];
        board[oldX, oldY] = null;

        // Promotion
        // TODO: Ask player for piece type 
        if ((int) board[newX, newY].type == 3 && newY == 0 || newY == 7)
        {
            board[newX, newY] = new Queen(board[newX, newY].color, newX, newY);
        }

        isWhiteTurn = !isWhiteTurn;
    }

    public void SpecialMovePosition(int oldX, int oldY, int newX, int newY)
    {
        RecordMove(oldX, oldY, newX, newY);
        int pieceType = (int) board[oldX, oldY].type;

        // If Pawn, En Passant. If King, Castling
        if (pieceType == 3)
        {
            board[oldX, oldY].xCoord = newX;
            board[oldX, oldY].yCoord = newY;
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = null;
            // Removes the pawn that double moved
            board[newX, oldY] = null;
        }
        else if (pieceType == 1)
        {
            board[oldX, oldY].xCoord = newX;
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = null;

            if (newX == 1)
            {
                board[0, oldY].xCoord = newX + 1;
                board[newX + 1, newY] = board[0, oldY];
                board[0, oldY] = null;
            }
            else
            {
                board[7, oldY].xCoord = newX - 1;
                board[newX - 1, newY] = board[7, oldY];
                board[7, oldY] = null;
            }
        }

        isWhiteTurn = !isWhiteTurn;
    }

    // Records each move into a string: [Piece Type][oldX][oldY][newX][newY]
    private void RecordMove(int oldX, int oldY, int newX, int newY)
    {
        string record = "";
        switch ((int) board[oldX, oldY].type)
        {
            case 0:
                record += "";
                break;
            case 1:
                record += "K";
                break;
            case 2:
                record += "N";
                break;
            case 3:
                break;
            case 4:
                record += "Q";
                break;
            case 5:
                record += "R";
                break;
        };
        
        if (board[newX, newY] != null)
        {
            record += "x";
        }
        record += "" + oldX + oldY + newX + newY;
        moveRecord.Add(record);
    }

    public void CheckMate(int color)
    {
        if (color == 1)
        {
            // TODO: White Win
        }
        else
        {
            // TODO: Black Win
        }
    }
}