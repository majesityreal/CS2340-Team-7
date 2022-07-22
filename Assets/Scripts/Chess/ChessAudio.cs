using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Author:         Jing Liu
 *  Last Updated:   2022.07.22
 *  Version:        1.0
 */

public class ChessAudio : MonoBehaviour
{

    [SerializeField] AudioSource Piece;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlayPiece() 
    {
        Piece.Play();
    }
}
