using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_LaserStopShoot : MonoBehaviour
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
            laser.GetComponent<LaserBehavior>().shootingRay = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        button.GetComponent<Renderer>().material = disabledMat;
        foreach (GameObject laser in Lasers)
        {
            laser.GetComponent<LaserBehavior>().shootingRay = true;
        }
    }
}
