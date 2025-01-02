using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPallet : BasePallet
{

    public static event EventHandler OnAnyDuckTrashed;
    public override void Interact(Player player)
    {
        if(player.HasDuckObject())
        {
            player.GetDuckObject().DestroySelf();

            OnAnyDuckTrashed?.Invoke(this, EventArgs.Empty);

        }
    }
}
