using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }
    [SerializeField]
    private AudioClipRefsSO audioClipRefsSO;
    private float volume = 1f;
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        AssemblyManager.Instance.OnProtocolSuccess += AssemblyManager_OnProtocolSuccess;
        AssemblyManager.Instance.OnProtocolFailed += AssemblyManager_OnProtocolFailed;
        Player.Instance.OnPickUp += Player_OnPickUp;
        BasePallet.OnAnyDuckDropHere += BasePallet_OnDuckDropHere;
        AssemblyPalletDuckHoles.OnAnyIdle += AssemblyPalletDuckHoles_OnAnyIdle;
        AssemblyPalletDuckHoles.OnAnyAssembling += AssemblyPalletDuckHoles_OnAnyAssembling;
        AssemblyPalletDuckHoles.OnAnyCorrupting += AssemblyPalletDuckHoles_OnAnyCorrupting;
        AssemblyPalletDuckHoles.OnAnyCorrupt += AssemblyPalletDuckHoles_OnAnyCorrupt;
        TrashPallet.OnAnyDuckTrashed += TrashPallet_OnAnyDuckTrashed;
    }

    private void TrashPallet_OnAnyDuckTrashed(object sender, System.EventArgs e)
    {
        TrashPallet trashPallet = sender as TrashPallet;
        PlaySound(audioClipRefsSO.trash, trashPallet.transform.position);
    }

    private void AssemblyPalletDuckHoles_OnAnyCorrupt(object sender, System.EventArgs e)
    {
        AssemblyPalletDuckHoles assemblyPalletDuckHoles = sender as AssemblyPalletDuckHoles;
        PlaySound(audioClipRefsSO.corrupt, assemblyPalletDuckHoles.transform.position);
    }

    private void AssemblyPalletDuckHoles_OnAnyCorrupting(object sender, System.EventArgs e)
    {
        AssemblyPalletDuckHoles assemblyPalletDuckHoles = sender as AssemblyPalletDuckHoles;
        PlaySound(audioClipRefsSO.corrupting, assemblyPalletDuckHoles.transform.position);
    }

    private void AssemblyPalletDuckHoles_OnAnyAssembling(object sender, System.EventArgs e)
    {
        AssemblyPalletDuckHoles assemblyPalletDuckHoles = sender as AssemblyPalletDuckHoles;
        PlaySound(audioClipRefsSO.assembling, assemblyPalletDuckHoles.transform.position);



    }

    private void AssemblyPalletDuckHoles_OnAnyIdle(object sender, System.EventArgs e)
    {
        AssemblyPalletDuckHoles assemblyPalletDuckHoles = sender as AssemblyPalletDuckHoles;
        PlaySound(audioClipRefsSO.idleBuzz, assemblyPalletDuckHoles.transform.position);
    }

    private void BasePallet_OnDuckDropHere(object sender, System.EventArgs e)
    {
        BasePallet basePallet = sender as BasePallet;
        PlaySound(audioClipRefsSO.duckDrop, basePallet.transform.position);
    }

    private void Player_OnPickUp(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.duckPickUp, Player.Instance.transform.position);
    }

    private void AssemblyManager_OnProtocolFailed(object sender, System.EventArgs e)
    {
        DeliveryPallet deliveryPallet = DeliveryPallet.Instance;
        PlaySound(audioClipRefsSO.assemblyDeliveryFail, deliveryPallet.transform.position);
    }

    private void AssemblyManager_OnProtocolSuccess(object sender, System.EventArgs e)
    {
        DeliveryPallet deliveryPallet = DeliveryPallet.Instance;
        PlaySound(audioClipRefsSO.assemblyDeliverySuccess, deliveryPallet.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound (Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
