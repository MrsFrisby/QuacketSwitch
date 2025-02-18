using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPalletHTTP : MonoBehaviour
{
    public static AssemblyPalletHTTP Instance { get; private set; }

    public event EventHandler<OnHTTPDuckDeliveredEventArgs> OnHTTPDuckDelivered;

    public class OnHTTPDuckDeliveredEventArgs : EventArgs
    {
        public Container containerToDeactivateHTTP;
    }

    public event EventHandler onHTTPDuckAssembled;


    public void PublishHTTPDuckAssembled()
    {
        //notify all protocol container pallets to reactivate 
        onHTTPDuckAssembled?.Invoke(this, EventArgs.Empty);
    }

    public void PublishHTTPDuckDelivered(Container container)
    {
        //publish event to container pallets to deactivate
        OnHTTPDuckDelivered?.Invoke(this, new OnHTTPDuckDeliveredEventArgs
        {
            containerToDeactivateHTTP = container
        });
    }
}
