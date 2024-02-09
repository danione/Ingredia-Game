using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ghost Potion", menuName = "Scriptable Objects/Potion/Ghost Potion")]
public class GhostPotionData : PotionsData
{
    public float countDownTimer;
    public Transform target;

    public override void UsePotion()
    {
        GameEventHandler.Instance.ActivateGhost(this);
    }
}
