using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_LaserStopAnimation : MonoBehaviour
{
    public GameObject[] Lasers;
    public GameObject button;
    public Material enabledMat;
    public Material disabledMat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.IsChildOf(transform)) return;

        button.GetComponent<Renderer>().material = enabledMat;
        foreach (GameObject laser in Lasers)
        {
            laser.GetComponent<Animator>().speed = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        button.GetComponent<Renderer>().material = disabledMat;
        foreach (GameObject laser in Lasers)
        {
            laser.GetComponent<Animator>().speed = 1.0f;
        }
    }
}
