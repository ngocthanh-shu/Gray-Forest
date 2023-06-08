using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [field: Header("Blend Tree Parameter")]
    [field: SerializeField] private string IdlingParameterName = "Idling";
    [field: SerializeField] private string MovingParameterName = "Moving";
    [field: SerializeField] private string AttackingParameterName = "Attacking";
    [field: SerializeField] private string DieParameterName = "Die";

    [field: Header("Float Parameter")]
    [field: SerializeField] private string DirectionXParameterName = "DirectionX";
    [field: SerializeField] private string DirectionYParameterName = "DirectionY";

    [field: Header("Time transitive")]
    [field: SerializeField] public float TimeToIdling { get; private set; } = 0.1f;
    [field: SerializeField] public float TimeToMoving { get; private set; } = 0.1f;
    [field: SerializeField] public float TimeToAttacking { get; private set; } = 0.1f;
    [field: SerializeField] public float TimeToDie { get; private set; } = 0.1f;

    [field: SerializeField] public float TimeToExitAttack { get; private set; } = 0.25f;


    public int IdlingParameterHash { get; private set; }
    public int MovingParameterHash { get; private set; }
    public int AttackingParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }


    public int DirectionXParameterHash { get; private set; }
    public int DirectionYParameterHash { get; private set; }

    public void Initialize()
    {
        IdlingParameterHash = Animator.StringToHash(IdlingParameterName);
        MovingParameterHash = Animator.StringToHash(MovingParameterName);
        AttackingParameterHash = Animator.StringToHash(AttackingParameterName);
        DieParameterHash = Animator.StringToHash(DieParameterName);

        DirectionXParameterHash = Animator.StringToHash(DirectionXParameterName);
        DirectionYParameterHash = Animator.StringToHash(DirectionYParameterName);
    }
}
