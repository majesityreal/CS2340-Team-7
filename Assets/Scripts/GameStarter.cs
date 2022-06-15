using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBlackjack()
    {
        PlayerPrefs.SetInt("loaded", 1); // player has loaded the first game
        Application.LoadLevel("Blackjack");
    }
    public void StartChess()
    {
        PlayerPrefs.SetInt("loaded", 1);
        Application.LoadLevel("Chess");
    }
}
