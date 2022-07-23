using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChessManager : MonoBehaviour
{
    public static char[,] board;
    public static List<string> moveRecord;
    private static GameObject resultStage;

    void Start()
    {
        InitializeGame();
        if (resultStage == null)
        {
            resultStage = GameObject.Find("ResultStage");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("MoveRecord Count: " + moveRecord.Count);
            string aiMove = ChessAI.negaMaxStarter(3, -1, board, moveRecord);
            
            if (aiMove.Length == 5)
            {
                Debug.Log("x: " + aiMove[1] + ", y: " + aiMove[2] + ", nx: " + aiMove[3] + ", ny: " + aiMove[4]);
                MovePosition(aiMove[1] - '0', aiMove[2] - '0', aiMove[3] - '0', aiMove[4] - '0', board, moveRecord);
            }
            else
            {
                Debug.Log("x: " + aiMove[2] + ", y: " + aiMove[3] + ", nx: " + aiMove[4] + ", ny: " + aiMove[5]);
                MovePosition(aiMove[2] - '0', aiMove[3] - '0', aiMove[4] - '0', aiMove[5] - '0', board, moveRecord);
            }

            PlayerInput.PlayerColor *= -1;
            

            // foreach (string moveAI in ChessAI.bestMoveRecord)
            //     /*            foreach (string moveAI in ChessAI.bestMoveRecord)
            //                 {
            //                     Debug.Log(moveAI);
            //                 }
            //                 }*/
            //     Debug.LogWarning("Finished NO ERERROS");
            //Debug.Log(ChessAI.bestMoveRecord.Count);
        }
    }

    public void InitializeGame()
    {
        moveRecord = new List<string>();

        board = new char[8, 8] 
        {
            {'r', 'p', '-', '-', '-', '-', 'P', 'R'},
            {'n', 'p', '-', '-', '-', '-', 'P', 'N'}, 
            {'b', 'p', '-', '-', '-', '-', 'P', 'B'}, 
            {'q', 'p', '-', '-', '-', '-', 'P', 'Q'}, 
            {'k', 'p', '-', '-', '-', '-', 'P', 'K'}, 
            {'b', 'p', '-', '-', '-', '-', 'P', 'B'}, 
            {'n', 'p', '-', '-', '-', '-', 'P', 'N'}, 
            {'r', 'p', '-', '-', '-', '-', 'P', 'R'}
        };

        Move.ConvertBoardToBinary(board);
    }

    /*
    {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'},
            {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'-', '-', '-', '-', '-', '-', '-', '-'}, 
            {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'}, 
            {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'}

    */

    public static void MovePosition(int oldX, int oldY, int newX, int newY, char[,] board, List<string> moveRecord)
    {
        Debug.Log(board[oldX, oldY]);
        RecordMove(oldX, oldY, newX, newY, board[oldX, oldY], moveRecord);
        Debug.Log("x:" + oldX + " y:" + oldY);

        if (board[oldX, oldY] == 'K' || board[oldX, oldY] == 'k') // When King is moved
        {
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = '-';
            
            if (newX - oldX == 2) // Right Castling
            {
                board[5, newY] = board[7, newY];
                board[7, newY] = '-';
            }
            else if (newX - oldX == -2) // Left Castling
            {
                board[3, newY] = board[0, newY];
                board[0, newY] = '-';
            }
        } 
        else if (board[oldX, oldY] == 'P' || board[oldX, oldY] == 'p') // When Pawn is moved
        {
            // En Passant
            if (newX != oldX && board[newX, newY] == '-') // When capturing move, but square is empty
            {
                Debug.LogWarning("En Passant'd");
                board[newX, oldY] = '-';
            }

            board[newX, newY] = board[oldX, oldY];

            if (newY == 7)
            {
                Debug.LogWarning("New Y is 7! ");
                ChessAI.printBoard(board);
            }
            board[oldX, oldY] = '-';

            // Promotion
            if (newY == 0 && board[newX, newY] == 'P')
            {
                Debug.LogWarning("White Pawn is being promoted to a QUEEN");
                board[newX, newY] = 'Q';
            }
            else if (newY == 7 && board[newX, newY] == 'p')
            {
                Debug.LogWarning("Black Pawn is being promoted to a QUEEN");
                board[newX, newY] = 'q';
            }
        } 
        else // Any other piece
        {
            board[newX, newY] = board[oldX, oldY];
            board[oldX, oldY] = '-';
        }

        Move.ConvertBoardToBinary(board);

        // string temp = "";
        // for (int y = 0; y < 8; y++)
        // {
        //     for (int x = 0; x < 8; x++)
        //     {
        //         temp += board[x, y];
        //     }
        //     temp += "\n";
        // }
        // Debug.Log(temp);

        // string temp2 = "";
        // for (int y = 0; y < 8; y++)
        // {
        //     for (int x = 0; x < 8; x++)
        //     {
        //         if (((Move.wq >> (- 8 * y - x + 63)) & 1UL) == 1UL)
        //         {
        //             temp2 += "1";
        //         }
        //         else
        //         {
        //             temp2 += "0";
        //         }
        //     }
        //     temp2 += "\n";
        // }
        // Debug.Log(temp2);

        CheckInsufficientMaterials(board);
        Check50Move(moveRecord);
        CheckRepetition(moveRecord);
        CheckCheckmate(board[newX, newY], board, moveRecord);
    }

    private static void CheckCheckmate(char type, char[,] board, List<string> moveRecord)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == '-')
                {
                    continue;
                }
                else if (type < 'a' && board[i, j] > 'a')
                {
                    continue;
                }
                else if (type > 'a' && board[i, j] < 'a')
                {
                    continue;
                }

                if (Move.GetMoves(i, j, board[i, j], moveRecord) != 0UL)
                {
                    return;
                }
            }
        }

        if (type < 'a')
        {
            if (Move.KingMoveCheck(new List<ulong> {Move.wk}, 'K') != 0UL)
            {
                Draw("Stalemate");
            }
            ResultStageScript.IsWhiteWin = true;
            resultStage.GetComponent<ResultStageScript>().ShowResult();
            Debug.Log("White Win");
        }
        else
        {
            if (Move.KingMoveCheck(new List<ulong> {Move.bk}, 'k') != 0UL)
            {
                Draw("Stalemate");
            }
            ResultStageScript.IsWhiteWin = false;
            resultStage.GetComponent<ResultStageScript>().ShowResult();
            Debug.Log("Black Win");
        }
    }

    private static void CheckInsufficientMaterials(char[,] board)
    {
        if (Move.wp != 0UL || Move.bp != 0UL) // No pawns?
        {
            return;
        }

        if (Move.wq != 0UL || Move.bq != 0UL) // No queens?
        {
            return;
        }

        if (Move.wr != 0UL || Move.br != 0UL) // No rooks?
        {
            return;
        }

        if (Move.wnCount < 2 && Move.bnCount < 2 && Move.wbCount == 0 && Move.bbCount == 0)
        {
            Draw("Insufficient Materials");
        }

        if (Move.wnCount == 0 && Move.bnCount == 0 && Move.wbCount < 2 && Move.bbCount < 2)
        {
            Draw("Insufficient Materials");
        }
    }

    private static void Check50Move(List<string> moveRecord)
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

    private static void CheckRepetition(List<string> moveRecord)
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
    public static void RecordMove(int oldX, int oldY, int newX, int newY, char type, List<string> moveRecord)
    {
        string record = "" + type;
        
        if (board[newX, newY] != '-')
        {
            record += "x";
        }

        record += "" + oldX + "" + oldY + "" + newX + "" + newY;
        moveRecord.Add(record);
    }

    private static void Draw(string cause)
    {
        // TODO: Show Draw(Tie) Screen
        Debug.Log("It is a tie \n" + cause);
    }
}