using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdlingState : PlayerState
{
    public PlayerIdlingState(PlayerStateMachine PlayerStateMachine) : base(PlayerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.StateReusableData.MovementSpeedModifier = 0f;
        stateMachine.Player.Animator.CrossFade(AnimationData.IdlingParameterHash, AnimationData.TimeToIdling);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.StateReusableData.MovementInput == Vector2.zero) return;
        stateMachine.ChangeState(stateMachine.MovingState);
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsMoving()) return;
        ResetVelocity();
    }
}
