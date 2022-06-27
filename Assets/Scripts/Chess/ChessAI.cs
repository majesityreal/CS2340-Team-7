using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{

    const int pawnValue = 100;
    const int knightValue = 300;
    const int bishopValue = 300;
    const int rookValue = 500;
    const int queenValue = 900;

    public static int Evaluate()
    {
        // material section
        int whiteMaterial = CountMaterial(0);
        int blackMaterial = CountMaterial(1);
        int totalMaterial = whiteMaterial - blackMaterial;

        // positions of the pieces

        // check the perspective, change the totalEval based on this


        return totalMaterial;
    }

    static int CountMaterial(int color)
    {
        int material = 0;
        // get all the values from chess manager
        for (int i = 0; i < ChessManager.pieces.Count; i++)
        {
            // if they are not the color, go to next piece
            if (ChessManager.pieces[i].color != color)
            {
                continue;
            }
            // sum up the total

            if (ChessManager.pieces[i] is Pawn) {
                material += pawnValue;
            }
            else if (ChessManager.pieces[i] is Knight)
            {
                material += knightValue;
            }
            else if (ChessManager.pieces[i] is Bishop)
            {
                material += bishopValue;
            }
            else if (ChessManager.pieces[i] is Rook)
            {
                material += rookValue;
            }
            else if (ChessManager.pieces[i] is Queen)
            {
                material += queenValue;
            }
        }

        return material;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
