using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundTrigger : MonoBehaviour
{
    public void RightWalk()
    {
        SoundManager.Instance.PlaySE(SoundManager.Instance.PlayerRightWalk);
    }

    public void LeftWalk()
    {
        SoundManager.Instance.PlaySE(SoundManager.Instance.PlayerLeftWalk);
    }

    public void PlayJumpSound()
    {
        SoundManager.Instance.PlaySE(SoundManager.Instance.PlayerJumpSound);
    }
}
