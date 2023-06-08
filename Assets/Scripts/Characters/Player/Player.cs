using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(PlayerInputActions))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: Header("References:")]
    [field: SerializeField] public PlayerSO PlayerData { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public UnityEvent InitializeEvent { get; private set; }
    
    [field: Header("Settings:")]
    public PlayerInputActions Input { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayStateReusableData StateReusableData { get; private set; }

    [field: Header("Values:")]
    public int Health { get; private set; }
    public int Dame { get; private set; }

    private PlayerStateMachine PlayerStateMachine;
   
    private void Awake()
    {
        Input = GetComponent<PlayerInputActions>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Initialize();

        StateReusableData = new PlayStateReusableData();
        PlayerStateMachine = new PlayerStateMachine(this);
    }

    private void Initialize()
    {
        PlayerData.Initialize();

        Health = PlayerData.BaseHealth;
        Dame = PlayerData.AttackData.BaseDame;

        InitializeEvent?.Invoke();
    }

    private void Start()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdlingState);
    }

    private void Update()
    {
        PlayerStateMachine?.Update();
        PlayerStateMachine?.HandleInput();
    }

    private void FixedUpdate()
    {
        PlayerStateMachine?.PhysicsUpdate();
    }

    public void OnTriggerAnimationExit()
    {
        PlayerStateMachine?.OnAnimationExitEvent();
    }

    public void OnTriggerAnimationEnter()
    {
        PlayerStateMachine?.OnAnimationEnterEvent();
    }

    public void GetDame(int value)
    {
        Health -= value;

        UIManager.Instance.ChangeHealthBar.Invoke(Health);

        if(Health <= 0)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.DieState);

            GameManager.Instance.EndGameEvent?.Invoke();
        }

    }

    public void OnDrawGizmos()
    {
        PlayerStateMachine?.OnDrawGizmos();
    }
}
