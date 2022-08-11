using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public AK.Wwise.Event wwiseEvent;
    // Start is called before the first frame update
    void Start()
    {
        wwiseEvent.Post(gameObject);
    }

    private void OnDisable()
    {
        //Disable Wwise Event
        wwiseEvent.Stop(gameObject);
    }
}
