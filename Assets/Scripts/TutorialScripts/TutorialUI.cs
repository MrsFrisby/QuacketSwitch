using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI upButtonText;

    [SerializeField]
    private TextMeshProUGUI downButtonText;

    [SerializeField]
    private TextMeshProUGUI leftButtonText;

    [SerializeField]
    private TextMeshProUGUI rightButtonText;

    [SerializeField]
    private TextMeshProUGUI jumpButtonText;

    [SerializeField]
    private TextMeshProUGUI interactButtonText;

    [SerializeField]
    private TextMeshProUGUI reviveButtonText;

    [SerializeField]
    private TextMeshProUGUI turboBoostButtonText;


    [SerializeField]
    private TextMeshProUGUI pauseButtonText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManger_OnStateChanged;

        UpdateVisual();
        Show();
    }

    private void GameManger_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        upButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        downButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        leftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        rightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        jumpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Jump);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        reviveButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Revive);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        turboBoostButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Turbo);
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
