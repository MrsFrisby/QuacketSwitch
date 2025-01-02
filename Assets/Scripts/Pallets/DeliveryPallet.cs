using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPallet : BasePallet
{
    public static DeliveryPallet Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.HasDuckObject())
        {
            if (player.GetDuckObject().TryGetAssembled(out AssembledDuckObject assembledDuckObject))
            {//only accepts assembled ducks

                //pass delivered duck into assembly manager for checking
                AssemblyManager.Instance.DeliverAssembledProtocol(assembledDuckObject);
                player.GetDuckObject().DestroySelf();
            }
            
        }


    }
}
