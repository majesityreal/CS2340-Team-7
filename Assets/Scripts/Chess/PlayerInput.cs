using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Zheng Yuan
 *  Date:           2022.06.28
 *  Version:        1.0
 *  
 *  Last update:    
 *                  2022.06.28  Add Cancel current selection function.
 *  
 *  Script for Piece OnMouseClick.
 */
public class PlayerInput : MonoBehaviour
{
    public static GameObject CurrSelected;
    public static bool isPlayerTurn;
    // Start is called before the first frame update
    void Start()
    {
        CurrSelected = null;
        isPlayerTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CurrSelected = null;
            Debug.Log("Cancel Selection");
        }
            
    }

    //public void SelectPiece(GameObject piece)
    //{
        
    //}
    public void MoveCurrentPiece()
    {

    }
    private void showPossibleMove()
    {

    }

    private void hidePossibleMove()
    {

    }
}
