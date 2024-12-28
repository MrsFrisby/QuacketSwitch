using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPallet : BasePallet
{
    public override void Interact(Player player)
    {
        if (player.HasDuckObject())
        {
            player.GetDuckObject().DestroySelf();
        }


    }
}
