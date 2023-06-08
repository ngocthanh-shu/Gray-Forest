using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    protected PlayerAttackData AttackData;
    private Vector2 position;
    public PlayerAttackingState(PlayerStateMachine PlayerStateMachine) : base(PlayerStateMachine)
    {
        AttackData = stateMachine.Player.PlayerData.AttackData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.StateReusableData.MovementSpeedModifier = 0f;
        stateMachine.Player.Animator.CrossFade(AnimationData.AttackingParameterHash, AnimationData.TimeToAttacking);

        stateMachine.Player.Invoke("OnTriggerAnimationExit", AnimationData.TimeToExitAttack);
    }

    public override void OnAnimationExitEvent()
    {
        Attack();
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        position = GetDirectionAttack(position); 
       
        Handles.DrawWireDisc(position, Vector3.forward, AttackData.Radius);
    }
#endif

    public void Attack()
    {
        position = GetDirectionAttack(position);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, AttackData.Radius);
        if (colliders.Length > 0)
        {               
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().getHit(stateMachine.Player.Dame);
                }
            }
        }

        if (stateMachine.Player.StateReusableData.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.MovingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    private Vector2 GetDirectionAttack(Vector2 transform, float minimum = 0.1f)
    {
        transform = stateMachine.Player.transform.position;
        transform.y += AttackData.YOffset;

        if (stateMachine.Player.StateReusableData.CurrentDirection.x < -minimum)
        {
            transform.x -= AttackData.Range;
        }
        else if (stateMachine.Player.StateReusableData.CurrentDirection.x > minimum)
        {
            transform.x += AttackData.Range;
        }
        else if (stateMachine.Player.StateReusableData.CurrentDirection.y > minimum)
        {
            transform.y += AttackData.Range;
        }
        else
        {
            transform.y -= AttackData.Range;
        }
        return transform;
    }

    protected override void AddInputActionCallbacks()
    {
    }

    protected override void RemoveInputActionCallbacks()
    {
    }
}
