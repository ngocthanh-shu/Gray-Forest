using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }

    public PlayerIdlingState IdlingState { get; }
    public PlayerMovingState MovingState { get; }
    public PlayerAttackingState AttackingState { get; }
    public PlayerDieState DieState { get; }
    public PlayerStateMachine(Player player)
    {
        Player = player;

        IdlingState = new PlayerIdlingState(this);
        MovingState = new PlayerMovingState(this);
        AttackingState = new PlayerAttackingState(this);
        DieState = new PlayerDieState(this);
    }
}