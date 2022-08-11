using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSpawn : MonoBehaviour
{
    public GameObject SFXPrefab;

    private void OnEnable()
    {
        Instantiate(SFXPrefab, transform.position, transform.rotation);
    }
}
