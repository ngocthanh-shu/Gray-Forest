using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStateReusableData
{
    public Vector2 MovementInput { get; set; }
    public Vector2 CurrentDirection { get; set; }
    public float MovementSpeedModifier { get; set; }
    public bool flipped { get; set; }
}
