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



    public static int EvaluateBoard()
    {
        // material section
        int whiteMaterial = CountMaterial(1);
        int blackMaterial = CountMaterial(-1);
        int totalMaterial = whiteMaterial - blackMaterial;

        // positions of the pieces

        // check the perspective, change the totalEval based on this


        return totalMaterial;
    }

    // returns value of all the material of the pieces still in play
    static int CountMaterial(int color)
    {
        int material = 0;
        // get all the tiles from the square
        for (int i = 0; i < 64; i++)
        {
            // if nothing there, go to next piece
            if (ChessManager.board[i % 8, i / 8] == null)
            {
                continue;
            }
            // if they are not the color, go to next piece
            if (ChessManager.board[i % 8, i / 8].color != color)
            {
                continue;
            }

            if (ChessManager.board[i % 8, i / 8] is Pawn) {
                material += pawnValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Knight)
            {
                material += knightValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Bishop)
            {
                material += bishopValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Rook)
            {
                material += rookValue;
            }
            else if (ChessManager.board[i % 8, i / 8] is Queen)
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
