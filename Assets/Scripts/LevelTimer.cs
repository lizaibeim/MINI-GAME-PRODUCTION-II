using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    //code to "trigger" the event: Music_Start.Post(gameObject);
    public AK.Wwise.Event Music_Start;
    public AK.Wwise.Event Music_Stop;



    //WwiseParameter
    public AK.Wwise.RTPC Music_Time;
    public AK.Wwise.State battleState;
    public AK.Wwise.State nonBattleState;

    public float maxIntensityTime;
    float timeSinceLevelLoad;
    [Range(0,1)]
    public float progressionSlider;

    public bool inCombat;
    public static bool playMusicOnce;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.mainMenuEvent += StopAllMusic;

        if (!playMusicOnce)
        {
            playMusicOnce = true;
            Music_Start.Post(gameObject);
        }
        EndBattleMusic();
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLevelLoad = Time.timeSinceLevelLoad;
        progressionSlider = timeSinceLevelLoad / maxIntensityTime;
        progressionSlider = Mathf.Clamp01(progressionSlider);


        //wwise parameter change
        Music_Time.SetGlobalValue(progressionSlider);

        if (!inCombat && AIManager.Instance.inBattle) StartBattleMusic();

        if (inCombat && AIManager.allAI.Count <= 0) EndBattleMusic();
    }

    void StopAllMusic() {
        EndBattleMusic();
        Music_Stop.Post(gameObject);
       
    }

    void StartBattleMusic() {
        battleState.SetValue();
        inCombat = true;
    }
    void EndBattleMusic() {
        nonBattleState.SetValue();
        inCombat = false;
    }


}
