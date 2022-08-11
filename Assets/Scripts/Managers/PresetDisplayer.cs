using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PresetDisplayer : MonoBehaviour
{
    public PresetLoader presetLoader;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        presetLoader.presetChange += UpdateDisplayer;
    }

    private void OnDisable()
    {
        presetLoader.presetChange -= UpdateDisplayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateDisplayer() {
        text.text = "Preset: " + presetLoader.activePresetID;
    }

}
