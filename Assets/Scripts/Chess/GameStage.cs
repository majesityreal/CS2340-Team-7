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
    public GameObject PossibleMoves;

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

    public GameObject possibleBlock;

    public static GameObject[,] SpriteBoard = new GameObject[8, 8];
    public static List<int> HighLightIndex = null;
    public static GameObject[,] HighLightBlock = new GameObject[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        initChessBoard();
        initChessGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // Initalize the Board Pieces.
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdatePieces();
        }

        // If Player selected a piece, update it's possible move.
        if (HighLightIndex != null)
        {
            HidePossibleMoves();
            ShowPossibleMoves();
        }

        if (PlayerInput.CurrSelected == null)
        {
            HidePossibleMoves();
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
                    Instantiate(Board1, new Vector3(col - 4, 4 - row, transform.position.z), Quaternion.identity).transform.parent = GameBoard.transform;
                }
                else
                {
                    Instantiate(Board2, new Vector3(col - 4, 4 - row, transform.position.z), Quaternion.identity).transform.parent = GameBoard.transform;
                }
                SpriteBoard[col, row] = Instantiate(emptyPiece, new Vector3(col - 4, 4 - row, transform.position.z), Quaternion.identity);
                SpriteBoard[col, row].transform.parent = EmptyPiece.transform;
                SpriteBoard[col, row].GetComponent<PieceOnClick>().SetPos(col,row);
                SpriteBoard[col, row].GetComponent<PieceOnClick>().SetColor(0);
            }
        }
    }

    public void initChessGameObjects()
    {
        for (int col = 0; col < 8; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                // Initialize the GameObjects.
                Destroy(SpriteBoard[col, row]);
                SpriteBoard[col, row] = Instantiate(emptyPiece, new Vector3(col - 4, 4 - row, transform.position.z), Quaternion.identity);
                SpriteBoard[col, row].transform.parent = EmptyPiece.transform;
                SpriteBoard[col, row].GetComponent<PieceOnClick>().SetPos(col, row);
                SpriteBoard[col, row].GetComponent<PieceOnClick>().SetColor(0);
            }
        }
    }

    public void UpdatePieces()
    {
        initChessGameObjects();
        foreach (KeyValuePair<int, Piece> entry in ChessManager.board)
        {
            if (entry.Value.GetType() == typeof(Bishop))
            {
                if (entry.Value.GetColor() < 0)
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(bBishop, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = BlackPiece.transform;
                }
                else
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(wBishop, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = WhitePiece.transform;
                }
            }

            // King
            if (entry.Value.GetType() == typeof(King))
            {
                if (entry.Value.GetColor() < 0)
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(bKing, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = BlackPiece.transform;
                }
                else
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(wKing, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = WhitePiece.transform;
                }
            }

            // Knight
            if (entry.Value.GetType() == typeof(Knight))
            {
                if (entry.Value.GetColor() < 0)
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(bKnight, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = BlackPiece.transform;
                }
                else
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(wKnight, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = WhitePiece.transform;
                }
            }

            // Pawn
            if (entry.Value.GetType() == typeof(Pawn))
            {
                if (entry.Value.GetColor() < 0)
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(bPown, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = BlackPiece.transform;
                }
                else
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(wPown, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = WhitePiece.transform;
                }
            }

            // Queen
            if (entry.Value.GetType() == typeof(Queen))
            {
                if (entry.Value.GetColor() < 0)
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(bQueen, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = BlackPiece.transform;
                }
                else
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(wQueen, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = WhitePiece.transform;
                }
            }
            // Rook
            if (entry.Value.GetType() == typeof(Rook))
            {
                if (entry.Value.GetColor() < 0)
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(bRook, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = BlackPiece.transform;
                }
                else
                {
                    Destroy(SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()]);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()] = Instantiate(wRook, new Vector3(entry.Value.GetXPos() - 4, 4 - entry.Value.GetYPos(), transform.position.z), Quaternion.identity);
                    SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].transform.parent = WhitePiece.transform;
                }
            }

            SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].GetComponent<PieceOnClick>().SetPos(entry.Value.GetXPos(), entry.Value.GetYPos());
            SpriteBoard[entry.Value.GetXPos(), entry.Value.GetYPos()].GetComponent<PieceOnClick>().SetColor(entry.Value.GetColor());
        }
    }

    public void ShowPossibleMoves()
    {
        foreach (int index in HighLightIndex)
        {
            int x = index % 8;
            int y = index / 8;
            Debug.Log("You Can Move To " + x.ToString() + y.ToString());
            HighLightBlock[x, y] = Instantiate(possibleBlock, new Vector3(x - 4, 4 - y, transform.position.z), Quaternion.identity);
            HighLightBlock[x, y].transform.parent = PossibleMoves.transform;
        }
        HighLightIndex = null;
    }

    public void HidePossibleMoves()
    {
        foreach (Transform child in PossibleMoves.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
