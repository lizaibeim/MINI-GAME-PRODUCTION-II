using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        
        if(!File.Exists(Application.persistentDataPath + "/mgp2save" + ".dat"))
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
