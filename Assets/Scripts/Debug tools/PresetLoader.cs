using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetLoader : MonoBehaviour
{
    public ControlPreset activePreset;
    public int activePresetID;
    public ControlPreset[] controlPresets;
    Player player;
    public delegate void PresetEvent();
    public PresetEvent presetChange;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<Player>();

        UpdatePreset(activePreset);
    }

    public void LoadPreset(int i) {
        activePresetID = i;
        UpdatePreset(controlPresets[i - 1]);
    }

    public void UpdatePreset(ControlPreset preset) {

        presetChange?.Invoke();
        player.velocity = preset.groundVelocity;
        player.jumpForce = preset.jumpForce;
        player.rb.drag = preset.drag;
        player.airVelocity = preset.airVelocity;
        player.maxForward = preset.maxAirVelocity;
        player.useForces = preset.useForces;
        Physics.gravity = Vector3.up * preset.gravity;
        
    }
}
