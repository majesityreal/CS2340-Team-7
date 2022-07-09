using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour, IPickupItem
{
    [SerializeField] float healPercent = 0.2f;
    public void OnPickUp(PlayerController player) {
        player.Heal(healPercent*player.getMaxHitpoints());
    }
    

}
