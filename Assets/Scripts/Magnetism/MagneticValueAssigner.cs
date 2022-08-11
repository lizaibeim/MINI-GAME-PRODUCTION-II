using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticValueAssigner : MonoBehaviour
{
    public float massModifier;
    public Magnetic[] magneticScripts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FindMagneticScripts() {
        magneticScripts = GetComponentsInChildren<Magnetic>();
    }

    public void AssignMagnetValues() {


        foreach (Magnetic item in magneticScripts)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null) 
            rb.mass = item.transform.localScale.y * massModifier;
        }
    }

}
