using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
