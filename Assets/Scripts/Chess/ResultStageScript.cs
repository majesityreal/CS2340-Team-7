using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultStageScript : MonoBehaviour
{
    public static bool IsPlayerWin = false;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI playAgainText;
    public TextMeshProUGUI quitText;
    // Start is called before the first frame update
    void Start()
    {
        HideResult();
        playAgainText.SetText("Play Again");
        quitText.SetText("Back To Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowResult();
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("loaded", 2);
        Application.LoadLevel("Chess");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowResult()
    {
        if (IsPlayerWin)
        {
            resultText.SetText("You Win!");
            Debug.Log("You Win!");
        } 
        else
        {
            resultText.SetText("You Lose...");
            Debug.Log("You Lose!");
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void HideResult()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Debug.Log("Result Stage Hided");
    }
}
