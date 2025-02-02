using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyPalletWarningAudio : MonoBehaviour
{
    [SerializeField]
    private AssemblyPalletDuckHoles assemblyPalletDuckHoles;

    private AudioSource audioSource;

    private float warningSoundTimer;

    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        assemblyPalletDuckHoles.OnProgressChanged += AssemblyPalletDuckHoles_OnProgressChanged;
    }

    private void AssemblyPalletDuckHoles_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float corruptShowProgressAmount = .5f;
        playWarningSound = assemblyPalletDuckHoles.IsAssembled() && e.progressNormalized >= corruptShowProgressAmount;

        //if (playWarningSound)
        //{
        //    audioSource.Play();
        //}
        //else
        //{
        //    audioSource.Pause();
        //}
    }

    

    
    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .5f;
                warningSoundTimer = warningSoundTimerMax;

                AudioManager.Instance.PlayWarningSound(assemblyPalletDuckHoles.transform.position);

            }
        }
    }
}
