using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBrain : MonoBehaviour
{
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

}
