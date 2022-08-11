using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score;
    public GameObject scoreTextPrefab;
    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        
    }
}
