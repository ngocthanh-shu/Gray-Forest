using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;

    [SerializeField] public float attackSpeed = 0.06f;
    [SerializeField] public float attackDelay = 2f;

    [SerializeField] public int attack = 1;
    
    [SerializeField] public float attackRange = 1f;

    [SerializeField] public int hp = 1;

    [SerializeField] private int score;

    private Animator _anim;

    private bool attacking;

    private int _currentState;

    private CircleCollider2D collider;

    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Die = Animator.StringToHash("die");
    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int Run = Animator.StringToHash("run");

    private bool isCollidingPlayer;

    private float timeDelay;

    void Start()
    {
        timeDelay = Time.time + attackDelay;
    }
    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_currentState == Die) return;
        

        float distance = calculateDistance();


        if (distance <= attackRange && Time.time > timeDelay)
        {
            _currentState = Attack;
            DoAnimationAttack();
            timeDelay = Time.time + attackDelay;
            return;
        }
        
    }

    private void DoAnimationAttack()
    {
        _anim.CrossFade(_currentState, 0.1f);
        Invoke("DoAttack", attackSpeed);
    }

    private void DoAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("test");
                    collider.GetComponent<Player>().GetDame(attack);
                }
            }
        }
        _currentState = Run;
        _anim.CrossFade(_currentState, 0, 0);
    }
    
    public void getHit(int dmg){
        Debug.Log("Player Dame:" + dmg);
        hp-=dmg;
        if (hp<=0){
            _currentState = Die;

            _anim.CrossFade(_currentState, 0, 0);
            collider.enabled = false;

            GameManager.Instance.AddScoreEvent.Invoke(score);

            Destroy(gameObject, 0.5f);
            
        }
    }

    private float calculateDistance(){
        return Vector3.Distance(player.transform.position, gameObject.transform.position);
    }

    
}
