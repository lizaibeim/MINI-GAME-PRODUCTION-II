using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int i) {
        scoreText.text = "" + i;
    }
}
