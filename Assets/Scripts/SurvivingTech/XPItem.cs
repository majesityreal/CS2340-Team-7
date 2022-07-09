using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPItem : MonoBehaviour, IPickupItem
{
    [SerializeField] int xpAmount = 10;
    public void OnPickUp(PlayerController player) {
        player.addXP(xpAmount);
    }
    

}
