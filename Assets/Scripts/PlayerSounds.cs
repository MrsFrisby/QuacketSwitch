using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;

    [SerializeField]
    [Range(0.1f,1f)]
    private float footstepTimerMax = .5f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer <0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsMoving)
            {
                float volume = 1f;
                AudioManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
            
        }
    }
}
