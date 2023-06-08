using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovingState : PlayerState
{
    public PlayerMovingState(PlayerStateMachine PlayerStateMachine) : base(PlayerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.StateReusableData.MovementSpeedModifier = 1f;
        stateMachine.Player.Animator.CrossFade(AnimationData.MovingParameterHash, AnimationData.TimeToMoving);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.StateReusableData.MovementInput != Vector2.zero) return;
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
}
