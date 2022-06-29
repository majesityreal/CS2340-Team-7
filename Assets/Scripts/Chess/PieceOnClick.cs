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
    private void OnMouseDown()
    {
        if (PlayerInput.CurrSelected == null)
        {
            PlayerInput.CurrSelected = gameObject;
            Debug.Log("You Select " + gameObject.name.ToString());
        }
        else
        {
            Debug.Log("You Already Selected " + PlayerInput.CurrSelected.name.ToString());
        }
    }
}
