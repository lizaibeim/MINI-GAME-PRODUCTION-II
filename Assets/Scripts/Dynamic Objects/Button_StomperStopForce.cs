using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_StomperStopForce : MonoBehaviour
{
    public GameObject[] Stompers;
    public GameObject button;
    public Material enabledMat;
    public Material disabledMat;
    private RigidbodyConstraints[] stomperData;

    // Start is called before the first frame update
    void Start()
    {
        stomperData = new RigidbodyConstraints[Stompers.Length];
        for(int i = 0; i < Stompers.Length; i++)
        {
            var rbcs = Stompers[i].GetComponentInChildren<Rigidbody>().constraints;
            stomperData[i] = rbcs;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.IsChildOf(transform)) return;

        button.GetComponent<Renderer>().material = enabledMat;
        foreach(GameObject s in Stompers)
        {
            s.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        button.GetComponent<Renderer>().material = disabledMat;
        for (int i = 0; i < Stompers.Length; i++)
        {
            Stompers[i].GetComponentInChildren<Rigidbody>().constraints = stomperData[i];
        }
    }
}
