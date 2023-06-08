using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : IState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimationData AnimationData;
    public PlayerState(PlayerStateMachine PlayerStateMachine)
    {
        stateMachine = PlayerStateMachine;
        AnimationData = stateMachine.Player.PlayerData.AnimationData;
    }

    #region State Methods
    public virtual void Enter()
    {
        Debug.Log("Player current state: " + GetType().Name);
        AddInputActionCallbacks();
    }


    public virtual void Exit()
    {
        RemoveInputActionCallbacks();
    }

   
    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void Update()
    {
    }

    public virtual void OnAnimationEnterEvent()
    {
    }

    public virtual void OnAnimationExitEvent()
    {
    }

    public virtual void OnDrawGizmos()
    {
    }
    #endregion

    #region Main Methods
    private void ReadMovementInput()
    {
        stateMachine.Player.StateReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        SetDirecition();
        Flipped();

    }

    private void SetDirecition()
    {
        if (stateMachine.Player.StateReusableData.MovementInput == Vector2.zero) return;
        stateMachine.Player.StateReusableData.CurrentDirection = stateMachine.Player.StateReusableData.MovementInput;

        stateMachine.Player.Animator.SetFloat(AnimationData.DirectionXParameterHash, stateMachine.Player.StateReusableData.CurrentDirection.x);
        stateMachine.Player.Animator.SetFloat(AnimationData.DirectionYParameterHash, stateMachine.Player.StateReusableData.CurrentDirection.y);
    }

    private void Flipped()
    {
        stateMachine.Player.transform.rotation = Quaternion.Euler(new Vector3(0f, (stateMachine.Player.StateReusableData.CurrentDirection.x < 0) ? 180f : 0f, 0f));
    }

    private void Move()
    {
        if (stateMachine.Player.StateReusableData.MovementInput == Vector2.zero || stateMachine.Player.StateReusableData.MovementSpeedModifier == 0f) return;


        Vector2 movementDirection = GetMovementDirection();
        float movementSpeed = GetMovementSpeed();

        Vector2 currentPlayerHorizontalVelocity = GetPlayerVelocity();

        stateMachine.Player.Rigidbody.AddForce(movementDirection * movementSpeed - currentPlayerHorizontalVelocity, (ForceMode2D) ForceMode.VelocityChange);
    }


    protected float GetMovementSpeed()
    {
        Debug.Log(stateMachine.Player.PlayerData.BaseSpeed);
        return stateMachine.Player.StateReusableData.MovementSpeedModifier * stateMachine.Player.PlayerData.BaseSpeed;
    }

    protected Vector2 GetMovementDirection()
    {
        return stateMachine.Player.StateReusableData.MovementInput.normalized;
    }

    protected Vector2 GetPlayerVelocity()
    {
        return stateMachine.Player.Rigidbody.velocity;
    }

    protected bool IsMoving(float minimumMagnitude = 0.1f)
    {
        return GetPlayerVelocity().magnitude > minimumMagnitude;
    }
    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector2.zero;
    }

    protected virtual void AddInputActionCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.started += OnAttackStarted;
        /*stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;*/
    }

    protected virtual void RemoveInputActionCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;
        /*stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;*/
    }
    #endregion

    #region Callback Methods
    /* protected virtual void OnMovementPerformed(InputAction.CallbackContext context)
     {
     }
     protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
     {
     }*/
    protected virtual void OnAttackStarted(InputAction.CallbackContext obj)
    {
        stateMachine.ChangeState(stateMachine.AttackingState);
    }
    #endregion
}
