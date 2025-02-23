using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialReviveUI : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.OnImpact += Player_OnImpact;
        Player.Instance.OnRevive += Player_OnRevive;
        Hide();
    }

    private void Player_OnRevive(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Player_OnImpact(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
