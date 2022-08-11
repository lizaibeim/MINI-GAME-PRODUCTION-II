using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenryDies : MonoBehaviour
{
    public GameObject henryDeathSound;
    public GameObject henry;
    public AK.Wwise.Event soundOne;
    public GameObject soundPlayer;
    bool playOnce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (henry == null && !playOnce ) {
            playOnce = true;
            soundPlayer = GameObject.FindGameObjectWithTag("Monologue");
            soundOne.Stop(soundPlayer);

            StartCoroutine("HenryDelay");
        }
    }

    IEnumerator HenryDelay() {
        yield return new WaitForSeconds(1F);
        Instantiate(henryDeathSound);
    }

}

