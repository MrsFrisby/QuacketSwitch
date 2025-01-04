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
    private TextMeshProUGUI altInteractButtonText;

    [SerializeField]
    private TextMeshProUGUI reviveButtonText;

    [SerializeField]
    private TextMeshProUGUI grabButtonText;

    [SerializeField]
    private TextMeshProUGUI fireButtonText;

    [SerializeField]
    private TextMeshProUGUI pauseButtonText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        UpdateVisual();
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void UpdateVisual()
    {
        upButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        downButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        leftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        rightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);

        jumpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Jump);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        altInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.AltInteract);
        reviveButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Revive);
        grabButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Grab);
        fireButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Fire);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }
}
