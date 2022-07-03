using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
