using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField][field: Range(1, 3)] public int BaseDame { get; private set; } = 1;
    [field: SerializeField] [field: Range(0f, 1f)] public float Range { get; private set; } = 0.5f;
    [field: SerializeField] [field: Range(0f, 1f)] public float Radius { get; private set; } = 0.5f;

    [field: SerializeField][field: Range(-1f, 1.5f)] public float YOffset { get; private set; } = 0.5f;
}
