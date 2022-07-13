using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class HPBarStatus : MonoBehaviour
{
    [SerializeField] Transform HPBar;
    
    public void HPIndicator(float currentHP, float maxHP) {
        float HPPercent = currentHP / maxHP;
        if (HPPercent < 0f) { // in case character took too much damage
            HPPercent = 0f;
        }
        HPBar.localScale = new Vector3(HPPercent, 1f, 1f);
    }
}
