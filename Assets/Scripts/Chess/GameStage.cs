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

    //Show the current data board to the game.
    public void UpdatePieces()
    {
        initChessGameObjects();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece curr = ChessManager.board[i, j];
                if (curr == null)
                {
                    Destroy(SpriteBoard[i, j]);
                    SpriteBoard[i, j] = Instantiate(emptyPiece, new Vector3(i - 4, 4 - j, transform.position.z), Quaternion.identity);
                    SpriteBoard[i, j].transform.parent = EmptyPiece.transform;
                    SpriteBoard[i, j].GetComponent<PieceOnClick>().SetPos(i, j);
                    SpriteBoard[i, j].GetComponent<PieceOnClick>().SetColor(0);
                } 
                else
                {
                    UpdatePieceToGame(ChessManager.board[i, j]);
                }
            }
        }
    }

    public void UpdatePieceToGame(Piece piece)
    {
        if (piece.type == PieceType.Bishop)
        {
            if (piece.color < 0)
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(bBishop, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = BlackPiece.transform;
            }
            else
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(wBishop, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = WhitePiece.transform;
            }
        }

        // King
        if (piece.type == PieceType.King)
        {
            if (piece.color < 0)
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(bKing, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = BlackPiece.transform;
            }
            else
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(wKing, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = WhitePiece.transform;
            }
        }

        // Knight
        if (piece.type == PieceType.Knight)
        {
            if (piece.color < 0)
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(bKnight, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = BlackPiece.transform;
            }
            else
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(wKnight, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = WhitePiece.transform;
            }
        }

        // Pawn
        if (piece.type == PieceType.Pawn)
        {
            if (piece.color < 0)
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(bPown, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = BlackPiece.transform;
            }
            else
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(wPown, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = WhitePiece.transform;
            }
        }

        // Queen
        if (piece.type == PieceType.Queen)
        {
            if (piece.color < 0)
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(bQueen, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = BlackPiece.transform;
            }
            else
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(wQueen, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = WhitePiece.transform;
            }
        }
        // Rook
        if (piece.type == PieceType.Rook)
        {
            if (piece.color < 0)
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(bRook, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = BlackPiece.transform;
            }
            else
            {
                Destroy(SpriteBoard[piece.xCoord, piece.yCoord]);
                SpriteBoard[piece.xCoord, piece.yCoord] = Instantiate(wRook, new Vector3(piece.xCoord - 4, 4 - piece.yCoord, 0), Quaternion.identity);
                SpriteBoard[piece.xCoord, piece.yCoord].transform.parent = WhitePiece.transform;
            }
        }

        SpriteBoard[piece.xCoord, piece.yCoord].GetComponent<PieceOnClick>().SetPos(piece.xCoord, piece.yCoord);
        SpriteBoard[piece.xCoord, piece.yCoord].GetComponent<PieceOnClick>().SetColor(piece.color);
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
