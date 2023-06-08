using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(PlayerStateMachine PlayerStateMachine) : base(PlayerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.StateReusableData.MovementSpeedModifier = 0f;
        stateMachine.Player.Animator.CrossFade(AnimationData.DieParameterHash, AnimationData.TimeToDie);
    }
}
