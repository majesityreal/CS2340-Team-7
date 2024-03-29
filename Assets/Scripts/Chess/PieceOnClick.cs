using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Zheng Yuan
 *  Date:           2022.06.28
 *  Version:        1.0
 *  
 *  Last update:    
 *                  2022.06.28  Add OnMouseDown() Function for Piece selection.
 *  
 *  Script for Piece OnMouseClick.
 */
public class PieceOnClick : MonoBehaviour
{
    private int xPos;
    private int yPos;
    private int color;
    private GameObject chessManager;
    private ChessAudio sound;

    void Start()
    {
        chessManager = GameObject.Find("ChessManager");
        sound = FindObjectOfType<ChessAudio>();
    }
    private void OnMouseDown()
    {
        if (PlayerInput.IsPlayerTurn && !PlayerInput.IsGamePaused)
        {
            if (PlayerInput.CurrSelected == null)
            {
                if (GetColor() == PlayerInput.PlayerColor)
                {
                    PlayerInput.CurrSelected = gameObject;

                    //Replace when Game Move Check Fixed.
                    PieceOnClick curr = PlayerInput.CurrSelected.GetComponent<PieceOnClick>();
                    List<int> temp = Move.GetMovesList(curr.GetXPos(), curr.GetYPos(), ChessManager.board[curr.GetXPos(), curr.GetYPos()], ChessManager.moveRecord);
                    GameStage.HighLightIndex = new List<int>();
                    foreach (int index in temp)
                    {
                        GameStage.HighLightIndex.Add(index);
                    }
                    GameStage.CurrPossibleMove = GameStage.HighLightIndex;
                    PlayerInput.CurrSelected.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                }
                else
                {
                    Debug.Log("You Can't Select it!");
                }
            }
            else
            {
                if (GetColor() == PlayerInput.PlayerColor)// Switch Piece
                {
                    PlayerInput.CurrSelected.transform.GetChild(0).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    PlayerInput.CurrSelected = gameObject;
                    PlayerInput.CurrSelected.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

                    //Replace when Game Move Check Fixed.
                    PieceOnClick curr = PlayerInput.CurrSelected.GetComponent<PieceOnClick>();
                    List<int> temp = Move.GetMovesList(curr.GetXPos(), curr.GetYPos(), ChessManager.board[curr.GetXPos(), curr.GetYPos()], ChessManager.moveRecord);
                    GameStage.HighLightIndex = new List<int>();
                    foreach (int index in temp)
                    {
                        GameStage.HighLightIndex.Add(index);
                    }
                    GameStage.CurrPossibleMove = GameStage.HighLightIndex;
                    //Debug.Log("You Switch to " + gameObject.name.ToString());
                }
                else
                {
                    if (IsPossibleMove())
                    {
                        ChessManager.MovePosition(PlayerInput.CurrSelected.GetComponent<PieceOnClick>().GetXPos(), PlayerInput.CurrSelected.GetComponent<PieceOnClick>().GetYPos(), GetXPos(), GetYPos(), ChessManager.board, ChessManager.moveRecord);
                        //PlayerInput.IsPlayerTurn = false;
                        PlayerInput.CurrSelected.GetComponent<PieceOnClick>().SetPos(GetXPos(), GetYPos());
                        PlayerInput.CurrSelected.transform.position = new Vector3(GetXPos() - 4, 4 - GetYPos());
                        Debug.Log("Move Success!");
                        sound.PlayPiece();                
                        PlayerInput.PlayerColor *= -1;
                    }
                    else
                    {
                        // Invalid Move
                        Debug.Log("You Can't Move There!");
                    }
                    PlayerInput.CurrSelected.transform.GetChild(0).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    PlayerInput.CurrSelected = null;
                    GameStage.CurrPossibleMove = null;
                }
            }
        }
    }

    public bool IsPossibleMove()
    {
        foreach (int pos in GameStage.CurrPossibleMove)
        {
           if (pos % 8 == GetXPos() && pos / 8 == GetYPos())
           {
               return true;
           }
        }
        return false;
    }

    // Getter && Setter
    public void SetPos(int x, int y)
    {
        SetXPos(x);
        SetYPos(y);
    }

    public void SetXPos(int x) {
        xPos = x;
    }

    public void SetYPos(int y)
    {
        yPos = y;
    }

    public int GetXPos()
    {
        return xPos;
    }
    public int GetYPos()
    {
        return yPos;
    }

    public int GetIndex()
    {
        return xPos + yPos * 8;
    }
    
    public void SetColor(int color)
    {
        this.color = color;
    }

    public int GetColor()
    {
        return color;
    }
}
