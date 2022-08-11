using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UISounds : MonoBehaviour
{
    public GameObject hoverSound;
    public GameObject selectSound;
    public GameObject clickSound;
    public GameObject startSound;
    public GameObject backSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void HoverSound() {
        Instantiate(hoverSound);
    }

    public void SelectSound()
    {
        Instantiate(selectSound);
    }
    public void ClickSound()
    {
        Instantiate(clickSound);
    }

    public void StartSound()
    {
        Instantiate(startSound);
    }

    public void BackSound()
    {
        Instantiate(backSound);
    }

}
