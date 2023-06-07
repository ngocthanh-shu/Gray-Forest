using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInputActions Input { get; private set; }
    private void Awake()
    {
        Input = GetComponent<PlayerInputActions>();
    }

    private void Start()
    {
        Input.PlayerActions.Attack.performed += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        ReadMovementValue();
    }

    private void ReadMovementValue()
    {
        Debug.Log(Input.PlayerActions.Movement.ReadValue<Vector2>());
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("2332");
    }
}
