using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    // Coded by Kevin Kwan
    // 6/3/2022
    // This script is used to fade the screen in and out.
    // https://docs.unity3d.com/ScriptReference/CanvasGroup.html
    private CanvasGroup UIGroup;
    private float fadeTime = 0.35f; // seconds of duration of fade effect
    private bool fadeIn = false;
    private bool fadeOut = false;

    public void showElement()
    {
        gameObject.SetActive(true);
        fadeIn = true;
    }

    public void hideElement()
    {
        fadeOut = true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        UIGroup = gameObject.GetComponent<CanvasGroup>();
        // if (gameObject.name == "Landing" && gameObject.layer == 5)
        // {
        //     UIGroup.alpha = 0;
        //     fadeIn = true;
        // }
        if (gameObject.name == "Black" && gameObject.layer == 5)
        {
            UIGroup.alpha = 1;
            fadeOut = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // this can be optimized by using a coroutine, but I'm not super comfortable with coroutines
        // todo: OPTIMIZE THIS
        if (fadeIn) {
            if (UIGroup.alpha >= 0) // if alpha of the group/element is hidden or not fully opaque
            {
                UIGroup.alpha += Time.deltaTime / fadeTime;
                if (UIGroup.alpha >= 1.0f) //if the group/element is finally shown
                {
                    fadeIn = false;
                }
            }
        } else if (fadeOut) {
            if (UIGroup.alpha <= 1) // if alpha of the group/element is opaque or not fully opaque
            {
                UIGroup.alpha -= Time.deltaTime / fadeTime;
                if (UIGroup.alpha <= 0.0f) //if the group/element is finally hidden
                {
                    fadeOut = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
