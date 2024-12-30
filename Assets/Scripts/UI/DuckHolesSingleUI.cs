using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckHolesSingleUI : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public void setDuckObjectSO(DucksSO ducksSO)
    {
        image.sprite = ducksSO.sprite;
    }
}
