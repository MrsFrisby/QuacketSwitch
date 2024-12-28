using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPallet : BasePallet
{
    public override void Interact(Player player)
    {
        if (player.HasDuckObject())
        {
            if (player.GetDuckObject().TryGetAssembled(out AssembledDuckObject assembledDuckObject))
            {//only accepts assembled ducks

                AssemblyManager.Instance.DeliverAssembledProtocol(assembledDuckObject);
                player.GetDuckObject().DestroySelf();
            }
            
        }


    }
}
