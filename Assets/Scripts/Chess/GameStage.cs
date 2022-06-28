using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    public GameObject GameBoard;

    // Prefabs
    public GameObject Board1;
    public GameObject Board2;

    public GameObject bBishop;
    public GameObject bKing;
    public GameObject bKnight;
    public GameObject bPown;
    public GameObject bQueen;
    public GameObject bRock;

    public GameObject wBishop;
    public GameObject wKing;
    public GameObject wKnight;
    public GameObject wQueen;
    public GameObject wPown;
    public GameObject wRock;

    // Start is called before the first frame update
    void Start()
    {
        initChessBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initChessBoard()
    {
        for(int col = 0; col < 8; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                if ((col + row) % 2 == 0)
                {
                    Instantiate(Board1, new Vector3(col - 4,4 - row, 0), Quaternion.identity);
                }else
                {
                    Instantiate(Board2, new Vector3(col - 4, 4 - row, 0), Quaternion.identity);
                }
            }
        }
    }
}
