using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class HealingItem : MonoBehaviour, IPickupItem
{
    [SerializeField] float healPercent = 0.2f;
    public void OnPickUp(PlayerController player) {
        player.Heal(healPercent*player.getMaxHitpoints());
    }
    

}
