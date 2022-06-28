using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBrain : MonoBehaviour
{
    public GameObject PlayAgainButton;

    void Start()
    {
        if (PlayAgainButton == null)
        {
            PlayAgainButton = GameObject.Find("PlayAgain");
        }
    }

    void Update()
    {
        if (PlayAgainButton != null)
        {
            updatePlayAgain();
        }
    }
    public void Hit()
    {
        GameManager.Instance.Hit();
    }

    public void Stand()
    {
        GameManager.Instance.Stand();
    }

    public void Double ()
    {
        GameManager.Instance.Double();
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetGame();
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    public void updatePlayAgain()
    {
        if (GameManager.PlayerMoney == 0)
        {
            PlayAgainButton.SetActive(false);
        }
    }
}
