using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_Manager : MonoBehaviour
{
    private InputMaster controls = null;
    public GameObject soundPlayer;
    public AK.Wwise.Event introEvent;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += _ => NextScene();
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    void NextScene()
    {
        introEvent.Post(soundPlayer);
        SceneManager.LoadScene(1);
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(53);
        NextScene();
    }
}
