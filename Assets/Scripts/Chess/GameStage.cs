using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Author:         Zheng Yuan
 *  Date:           2022.06.28
 *  Version:        1.0
 *  
 *  Last update:    
 *                  2022.06.28  Fix Sprite Scale. Impelement Initalize Chess Board.
 *  
 *  Script for Player Input in Chess Board Game.
 */

public class GameStage : MonoBehaviour
{
    // GameObjects
    public GameObject GameBoard;
    public GameObject EmptyPiece;
    public GameObject BlackPiece;
    public GameObject WhitePiece;

    // Prefabs
    public GameObject Board1;
    public GameObject Board2;

    public GameObject emptyPiece;

    public GameObject bBishop;
    public GameObject bKing;
    public GameObject bKnight;
    public GameObject bPown;
    public GameObject bQueen;
    public GameObject bRook;

    public GameObject wBishop;
    public GameObject wKing;
    public GameObject wKnight;
    public GameObject wPown;
    public GameObject wQueen;
    public GameObject wRook;

    public static GameObject[,] SpriteBoard = new GameObject[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        initChessBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdatePieces();
        }
    }

    public void initChessBoard()
    {
        for(int col = 0; col < 8; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                // SetUp GameBoard
                if ((col + row) % 2 == 0)
                {
                    Instantiate(Board1, new Vector3(col - 4, 4 - row, 0), Quaternion.identity).transform.parent = GameBoard.transform;
                }
                else
                {
                    Instantiate(Board2, new Vector3(col - 4, 4 - row, 0), Quaternion.identity).transform.parent = GameBoard.transform;
                }
                SpriteBoard[col, row] = Instantiate(emptyPiece, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                SpriteBoard[col, row].transform.parent = EmptyPiece.transform;
            }
        }
    }

    public void UpdatePieces()
    {
        for (int col = 0; col < 8; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                // Empty Piece.
                if (ChessManager.board[col, row] == null)
                {
                    Destroy(SpriteBoard[col, row]);
                    SpriteBoard[col, row] = Instantiate(emptyPiece, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                    SpriteBoard[col, row].transform.parent = EmptyPiece.transform;
                }
                // Piece Exist
                else
                {
                    // Bishop
                    if (ChessManager.board[col, row].GetType() == typeof(Bishop))
                    {
                        if (ChessManager.board[col, row].color < 0)
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(bBishop, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = BlackPiece.transform;
                        } else
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(wBishop, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = WhitePiece.transform;
                        }
                    }

                    // King
                    if (ChessManager.board[col, row].GetType() == typeof(King))
                    {
                        if (ChessManager.board[col, row].color < 0)
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(bKing, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = BlackPiece.transform;
                        }
                        else
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(wKing, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = WhitePiece.transform;
                        }
                    }

                    // Knight
                    if (ChessManager.board[col, row].GetType() == typeof(Knight))
                    {
                        if (ChessManager.board[col, row].color < 0)
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(bKnight, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = BlackPiece.transform;
                        }
                        else
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(wKnight, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = WhitePiece.transform;
                        }
                    }

                    // Pawn
                    if (ChessManager.board[col, row].GetType() == typeof(Pawn))
                    {
                        if (ChessManager.board[col, row].color < 0)
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(bPown, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = BlackPiece.transform;
                        }
                        else
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(wPown, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = WhitePiece.transform;
                        }
                    }

                    // Queen
                    if (ChessManager.board[col, row].GetType() == typeof(Queen))
                    {
                        if (ChessManager.board[col, row].color < 0)
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(bQueen, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = BlackPiece.transform;
                        }
                        else
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(wQueen, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = WhitePiece.transform;
                        }
                    }

                    // Rook
                    if (ChessManager.board[col, row].GetType() == typeof(Rook))
                    {
                        if (ChessManager.board[col, row].color < 0)
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(bRook, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = BlackPiece.transform;
                        }
                        else
                        {
                            Destroy(SpriteBoard[col, row]);
                            SpriteBoard[col, row] = Instantiate(wRook, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                            SpriteBoard[col, row].transform.parent = WhitePiece.transform;
                        }
                    }
                }
            }
        }
    }

}
