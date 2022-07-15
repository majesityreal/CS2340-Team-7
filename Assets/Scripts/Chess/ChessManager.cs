using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChessManager : MonoBehaviour
{
    public static char[,] board;
    public static List<string> moveRecord;

    void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChessAI.negaMaxStarter(3, -1, board, moveRecord);

            foreach (string moveAI in ChessAI.bestMoveRecord)
                /*            foreach (string moveAI in ChessAI.bestMoveRecord)
                            {
                                Debug.Log(moveAI);
                            }
                            }*/
                Debug.LogWarning("Finished NO ERERROS");
            //Debug.Log(ChessAI.bestMoveRecord.Count);
        }
    }

    public void InitializeGame()
    {
        moveRecord = new List<string>();

        board = new char[8, 8] 
        {
            {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'},
            {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'}, 
            {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'}
        };
    }

    public static void MovePosition(int oldX, int oldY, int newX, int newY, char[,] board, List<string> moveRecord)
    {       
        if (board[newX, newY].type == 'K' || board[newX, newY].type == 'k')
        {
            Debug.Log("Game End");
            return;
        }
        
        RecordMove(oldX, oldY, newX, newY, board, moveRecord);
        Debug.Log("x:" + oldX + " y:" + oldY);

        PieceType pieceType = board[oldX, oldY].type;

        if (pieceType == PieceType.King) // When King is moved
        {
            board[oldX, oldY].xCoord = newX;
            board[oldX, oldY].yCoord = newY;
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = null;
            
            if (newX - oldX == 2) // Right Castling
            {
                board[7, newY].xCoord = 5;
                board[5, newY] = board[7, newY];
                board[7, newY] = null;
            }
            else if (newX - oldX == -2) // Left Castling
            {
                board[0, newY].xCoord = 3;
                board[3, newY] = board[0, newY];
                board[0, newY] = null;
            }
        } 
        else if (pieceType == PieceType.Pawn) // When Pawn is moved
        {
            // En Passant
            if (newX != oldX && board[newX, newY] == null) // When capturing move, but square is empty
            {
                Debug.LogWarning("This happened");
                board[newX, oldY] = null;
            }

            // Moves the piece to the destination and removes the old pointer index
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY].xCoord = newX;
            board[oldX, oldY].yCoord = newY;
            if (newY == 7)
            {
                Debug.LogWarning("New Y is 7! ");
                ChessAI.printBoard(board);
            }
            board[oldX, oldY] = null;

            // Promotion
            // TODO: Ask player for piece type. Setted it to Queen for right now.
            if ((newY == 0 && board[newX, newY].color == 1) || (newY == 7 && board[newX, newY].color == -1))
            {
                Debug.LogWarning("Pawn is being promoted to a QUEEN");
                board[newX, newY] = new Queen(board[newX, newY].color, newX, newY);
            }
        } 
        else // Any other piece
        {
            board[oldX, oldY].xCoord = newX;
            board[oldX, oldY].yCoord = newY;
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = null;
        }

        CheckInsufficientMaterials(board);
        Check50Move(board);
        CheckRepetition(board);
        CheckCheckmate(board[newX, newY].color, board);
    }

    private static void CheckCheckmate(int color, Piece[,] board)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == null || color == board[i, j].color)
                {
                    continue;
                }

                List<int[]> thisMoves = board[i, j].GetLegalMoves(board, moveRecord);

                if (thisMoves.Count != 0)
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

    private static void CheckInsufficientMaterials(Piece[,] board)
    {
        int countWhite = 0;
        int countBlack = 0;

        foreach (Piece piece in board)
        {
            if (piece == null)
            {
                continue;
            }

            if (piece.type == PieceType.Pawn || piece.type == PieceType.Rook)
            {
                return;
            }

            // Count everything except king
            if (piece.type != PieceType.King)
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
        }

        Draw("Insufficient Materials");
    }

    private static void Check50Move(Piece[,] board)
    {
        if (moveRecord.Count < 50)
        {
            return;
        }
        
        for (int i = 1; i < 51; i++)
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

    private static void CheckRepetition(Piece[,] board)
    {
        if (moveRecord.Count < 5)
        {
            return;
        }

        // Check if there were 3 repeated moves by the player.
        if (moveRecord[moveRecord.Count - 1] == moveRecord[moveRecord.Count - 3] 
        && moveRecord[moveRecord.Count - 1] == moveRecord[moveRecord.Count - 5])
        {
            Draw("Repetition");
        }

        return;
    }

    // Records each move into a string: [Piece Type][oldX][oldY][newX][newY]
    public static void RecordMove(int oldX, int oldY, int newX, int newY, Piece[,] board, List<string> moveRecord)
    {
        string record = "";
        switch (board[oldX, oldY].type)
        {
            case PieceType.Bishop:
                record += "B";
                break;
            case PieceType.King:
                record += "K";
                break;
            case PieceType.Knight:
                record += "N";
                break;
            case PieceType.Pawn:
                break;
            case PieceType.Queen:
                record += "Q";
                break;
            case PieceType.Rook:
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

    private static void Draw(string cause)
    {
        // TODO: Show Draw(Tie) Screen
        Debug.Log("It is a tie \n" + cause);
    }
}