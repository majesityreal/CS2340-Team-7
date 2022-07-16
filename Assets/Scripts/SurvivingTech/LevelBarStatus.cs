using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class LevelBarStatus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    public void XPIndicator(float currentXP, float maxXP) {
        GetComponent<Slider>().value = currentXP;
        GetComponent<Slider>().maxValue = maxXP;
    }

    public void setLevel(int level, int xpNeeded) {
        levelText.text = "Level: "
            + level.ToString() + " | XP Needed: " + xpNeeded.ToString();
    }
}
