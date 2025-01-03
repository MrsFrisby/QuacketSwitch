using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        AssemblyPalletDuckHoles.ResetStaticData();
        BasePallet.ResetStaticData();
        TrashPallet.ResetStaticData();

        //reset all static events/ eliminate all previous listeners
    }
}
