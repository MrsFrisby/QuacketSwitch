using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }


    [SerializeField]
    private Button soundEffectsButton;

    [SerializeField]
    private Button musicButton;

    [SerializeField]
    private Button closeButton;

    //rebind buttons

    [SerializeField]
    private Button moveUpButton;

    [SerializeField]
    private Button moveDownButton;

    [SerializeField]
    private Button moveLeftButton;

    [SerializeField]
    private Button moveRightButton;

    [SerializeField]
    private Button jumpButton;

    [SerializeField]
    private Button interactButton;

    [SerializeField]
    private Button altInteractButton;

    [SerializeField]
    private Button reviveButton;

    [SerializeField]
    private Button grabButton;

    [SerializeField]
    private Button fireButton;

    [SerializeField]
    private Button pauseButton;

    //Audio Controls

    [SerializeField]
    private TextMeshProUGUI soundEffectsText;

    [SerializeField]
    private TextMeshProUGUI musicText;

    //Rebind button Text

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


    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => {
            AudioManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: "+ Mathf.Round(AudioManager.Instance.GetVolume() * 10f);

        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
