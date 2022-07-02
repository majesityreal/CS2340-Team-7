using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChessManager : MonoBehaviour
{
    // [xCoord, yCoord]
    public static Piece[,] board;
    public static List<string> moveRecord;
    public static bool isWhiteTurn;

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
        {
            board[i, 1] = new Pawn(-1, i, 1);
        }

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

    public void MovePosition(int oldX, int oldY, int newX, int newY, Piece[,] board)
    {
        RecordMove(oldX, oldY, newX, newY);

        int pieceType = (int) board[oldX, oldY].type;
        
        if (pieceType == 3) // Check if En Passant
        {
            board[oldX, oldY].xCoord = newX;
            board[oldX, oldY].yCoord = newY;
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = null;
            board[newX, oldY] = null;
        }
        else if (pieceType == 1) // Check if Castling
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
        else // Any other move
        {
            board[oldX, oldY].xCoord = newX;
            board[oldX, oldY].yCoord = newY;
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = null;

            // Promotion
            // TODO: Ask player for piece type. Setted it to Queen for right now.
            if ((int) board[newX, newY].type == 3 && newY == 0 || newY == 7)
            {
                board[newX, newY] = new Queen(board[newX, newY].color, newX, newY);
            }
        }

        isWhiteTurn = !isWhiteTurn;
        CheckCheckmate(board[newX, newY].color);
        CheckInsufficientMaterials();
        Check50Move();
        CheckRepetition();
    }

    private void CheckCheckmate(int color)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (color == board[i, j].color)
                {
                    continue;
                }

                if (board[i, j].GetLegalMoves(board, moveRecord).Count != 0)
                {
                    return;
                }
            }
        }

        if (color == 1)
        {
            // TODO: White Win
            Debug.Log("White Win");
        }
        else
        {
            // TODO: Black Win
            Debug.Log("Black Win");
        }
    }

    private void CheckInsufficientMaterials()
    {
        int countWhite = 0;
        int countBlack = 0;
        int[] whiteKingPos = new int[2];
        int[] blackKingPos = new int[2];

        foreach (Piece piece in board)
        {
            if (piece == null)
            {
                continue;
            }

            int ty = (int) piece.type;

            // If pawn or rook are alive, don't end game.
            if (ty == 3 || ty == 5)
            {
                return;
            }

            // Count everything except king
            if (ty != 1)
            {
                if (piece.color == 1)
                {
                    countWhite++;
                    if (countWhite > 1)
                    {
                        return;
                    }
                }
                else if (piece.color == -1)
                {
                    countBlack++;
                    if (countBlack > 1)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (piece.color == 1)
                {
                    whiteKingPos[0] = piece.xCoord;
                    whiteKingPos[1] = piece.yCoord;
                }
                else
                {
                    blackKingPos[0] = piece.xCoord;
                    blackKingPos[1] = piece.yCoord;
                }
            }
        }

        if (countWhite == 0)
        {
            if (board[whiteKingPos[0], whiteKingPos[1]].GetLegalMoves(board, moveRecord).Count == 0)
            {
                Draw("Stalemate");
            }
        }
        else if (countBlack == 0)
        {
            if (board[blackKingPos[0], blackKingPos[1]].GetLegalMoves(board, moveRecord).Count == 0)
            {
                Draw("Stalemate");
            }
        }

        Draw("Insufficient Materials");
    }

    private void Check50Move()
    {
        if (moveRecord.Count < 50)
        {
            return;
        }
        
        for (int i = 0; i < 50; i++)
        {
            if (moveRecord[moveRecord.Count - i].Length == 4)
            {
                return;
            }
            else if (moveRecord[moveRecord.Count - i][0] == 'x' || moveRecord[moveRecord.Count - i][1] == 'x')
            {
                return;
            }
        }
        
        Draw("50 move rule");
    }

    private void CheckRepetition()
    {
        if (moveRecord.Count < 5)
        {
            return;
        }

        // Check if last and the 5th last are the same.
        // If so, check if the starting position of the last and ending position of the 3rd are the same.
        if (moveRecord[moveRecord.Count - 1] == moveRecord[moveRecord.Count - 5])
        {
            if (moveRecord[moveRecord.Count - 1].Substring(moveRecord[moveRecord.Count - 1].Length - 4, moveRecord[moveRecord.Count - 1].Length - 2) 
            == moveRecord[moveRecord.Count - 3].Substring(moveRecord[moveRecord.Count - 3].Length - 2))
            {
                Draw("Repetition");
            }
        }

        return;
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

    private void Draw(string cause)
    {
        // TODO: Show Draw(Tie) Screen
        Debug.Log("It is a tie \n" + cause);
    }
}