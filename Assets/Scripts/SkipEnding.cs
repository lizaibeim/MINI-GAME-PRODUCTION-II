using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipEnding : MonoBehaviour
{

    private InputMaster controls = null;

    public AK.Wwise.State battleState;
    public AK.Wwise.State nonBattleState;

    public AK.Wwise.Event Music_Start;
    public AK.Wwise.Event Music_Stop;
    public AK.Wwise.RTPC Music_Time;
    bool playMusicOnce = false;

    // Start is called before the first frame update
    void Awake()
    {

        controls = new InputMaster();
        controls.Player.Jump.performed += _ => LoadScene();
    }

    private void Start()
    {
     
        if (!playMusicOnce)
        {
            Music_Start.Post(gameObject);
            Music_Time.SetGlobalValue(1);
            playMusicOnce = true;

            //nonBattleState.SetValue();
       

           StartCoroutine(Skip());
        }
       
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadScene() {
        PauseManager.isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Music_Time.SetGlobalValue(0);
        nonBattleState.SetValue();
        Music_Stop.Post(gameObject);
        SceneManager.LoadScene(1); 
    }

    IEnumerator Skip()
    {
        yield return new WaitForSeconds(0.5F);
        battleState.SetValue();
        yield return new WaitForSeconds(10);
        LoadScene();
    }
}
