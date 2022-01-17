using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {

    public delegate void AnimationEvent();
    public static event AnimationEvent OnSlashAnimationHit;
    public static event AnimationEvent OnJumpAnimationJump;
    public event AnimationEvent OnAnimationAttackEvent;

    void SlashAnimationHitEvents()
    {
        OnSlashAnimationHit();
    }

    void JumpAnimationJumpEvent()
    {
        OnJumpAnimationJump();
    }

    void AnimationAttackEvent()
    {
        OnAnimationAttackEvent();
    }

}
