using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControlPreset", menuName = "ControlPreset/Preset", order = 1)]
public class ControlPreset : ScriptableObject
{

    public float groundVelocity = 400;
    public float gravity = -20;
    public float jumpForce = 10;
    public float drag = 1;
    public float airVelocity = 20;
    public float maxAirVelocity = 5;
    public bool useForces = true;
}
