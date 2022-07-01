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
    private void OnMouseDown()
    {
        if (PlayerInput.IsPlayerTurn)
        {
            if (PlayerInput.CurrSelected == null)
            {
                if (GetColor() == PlayerInput.PlayerColor)
                {
                    PlayerInput.CurrSelected = gameObject;

                    //Replace when Game Move Check Fixed.
                    //GameStage.HighLightIndex = ChessManager.board[PlayerInput.CurrSelected.GetComponent<PieceOnClick>().GetIndex()].GetLegalMoves();
                    GameStage.HighLightIndex = new List<int>();
                    GameStage.HighLightIndex.Add(GetIndex() - 8);

                    PlayerInput.CurrSelected.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    Debug.Log("You Select " + gameObject.name.ToString());
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
                    //GameStage.HighLightIndex = ChessManager.board[PlayerInput.CurrSelected.GetComponent<PieceOnClick>().GetIndex()].GetLegalMoves();
                    GameStage.HighLightIndex = new List<int>();
                    GameStage.HighLightIndex.Add(GetIndex() - 8);
                    GameStage.HighLightIndex.Add(GetIndex() - 16);
                    GameStage.HighLightIndex.Add(GetIndex() - 24);

                    Debug.Log("You Switch to " + gameObject.name.ToString());
                }
                else
                {
                    // TODO - fix this!
                    if (1 == 1)
                    {
                        ChessManager.MovePosition(PlayerInput.CurrSelected.GetComponent<PieceOnClick>().GetXPos(), PlayerInput.CurrSelected.GetComponent<PieceOnClick>().GetYPos(), GetXPos(), GetYPos());
                        //PlayerInput.IsPlayerTurn = false;
                        PlayerInput.CurrSelected.transform.position = new Vector3(GetXPos(), GetYPos());
                        Debug.Log("Move Success!");
                    } else
                    {
                        // Invalid Move
                        Debug.Log("You Can't Move There!");
                    }
                    PlayerInput.CurrSelected.transform.GetChild(0).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    PlayerInput.CurrSelected = null;
                }
            }
        }
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
