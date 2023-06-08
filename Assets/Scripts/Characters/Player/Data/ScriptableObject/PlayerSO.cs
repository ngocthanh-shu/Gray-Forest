using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Data/Character/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] [field: Range(5, 10)] public int BaseHealth { get; private set; }
    [field: SerializeField] [field: Range(1f, 5f)] public float BaseSpeed { get; private set; }
    
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public void Initialize()
    {
        AnimationData.Initialize();
    }

}
