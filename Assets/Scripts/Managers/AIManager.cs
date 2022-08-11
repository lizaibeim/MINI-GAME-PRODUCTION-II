using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    public static List<AI> allAI;
    public static List<AI> slowBots;
    public static List<AI> swarmBots;
    public static List<AI> alertedBots;

    public bool inBattle;

    private void Awake()
    {
        Instance = this;

        allAI = new List<AI>();
        alertedBots = new List<AI>();
        swarmBots = new List<AI>();
        slowBots = new List<AI>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inBattle = alertedBots.Count > 2;
        
    }
}
