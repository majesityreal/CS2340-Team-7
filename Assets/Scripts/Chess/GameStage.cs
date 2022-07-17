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
    public GameObject bPawn;
    public GameObject bQueen;
    public GameObject bRook;

    public GameObject wBishop;
    public GameObject wKing;
    public GameObject wKnight;
    public GameObject wPawn;
    public GameObject wQueen;
    public GameObject wRook;

    public GameObject possibleBlock;

    public static GameObject[,] SpriteBoard = new GameObject[8, 8];
    public static List<int[]> HighLightIndex = null;
    public static List<int[]> CurrPossibleMove = null;
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
        // If Player selected a piece, update it's possible move.
        if (HighLightIndex != null)
        {
            HidePossibleMoves();
            ShowPossibleMoves();
        }

        if (PlayerInput.CurrSelected == null)
        {
            HidePossibleMoves();
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
                char curr = ChessManager.board[i, j];
                if (curr == '-')
                {
                    Destroy(SpriteBoard[i, j]);
                    SpriteBoard[i, j] = Instantiate(emptyPiece, new Vector3(i - 4, 4 - j, transform.position.z), Quaternion.identity);
                    SpriteBoard[i, j].transform.parent = EmptyPiece.transform;
                    SpriteBoard[i, j].GetComponent<PieceOnClick>().SetPos(i, j);
                    SpriteBoard[i, j].GetComponent<PieceOnClick>().SetColor(0);
                } 
                else
                {
                    UpdatePieceToGame(ChessManager.board[i, j], i, j);
                }
            }
        }
    }

    public void UpdatePieceToGame(char piece, int x, int y)
    {
        if (piece == 'B')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(wBishop, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = WhitePiece.transform;
        }
        else if (piece == 'b')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(bBishop, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = BlackPiece.transform;
        }
        else if (piece == 'K')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(wKing, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = WhitePiece.transform;
        }
        else if (piece == 'k')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(bKing, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = BlackPiece.transform;
        }
        else if (piece == 'N')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(wKnight, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = WhitePiece.transform;
        }
        else if (piece == 'n')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(bKnight, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = BlackPiece.transform;
        }
        else if (piece == 'P')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(wPawn, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = WhitePiece.transform;
        }
        else if (piece == 'p')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(bPawn, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = BlackPiece.transform;
        }
        else if (piece == 'Q')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(wQueen, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = WhitePiece.transform;
        }
        else if (piece == 'q')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(bQueen, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = BlackPiece.transform;
        }
        else if (piece == 'R')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(wRook, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = WhitePiece.transform;
        }
        else if (piece == 'r')
        {
            Destroy(SpriteBoard[x, y]);
            SpriteBoard[x, y] = Instantiate(bRook, new Vector3(x - 4, 4 - y, 0), Quaternion.identity);
            SpriteBoard[x, y].transform.parent = BlackPiece.transform;
        }

        SpriteBoard[x, y].GetComponent<PieceOnClick>().SetPos(x, y);
        SpriteBoard[x, y].GetComponent<PieceOnClick>().SetColor(piece < 'a' ? 1 : -1);
    }


    public void ShowPossibleMoves()
    {
        foreach (int[] index in HighLightIndex)
        {
            //Debug.Log("You Can Move To " + index[0].ToString() + index[1].ToString());
            HighLightBlock[index[0], index[1]] = Instantiate(possibleBlock, new Vector3(index[0] - 4, 4 - index[1], transform.position.z), Quaternion.identity);
            HighLightBlock[index[0], index[1]].transform.parent = PossibleMoves.transform;

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
