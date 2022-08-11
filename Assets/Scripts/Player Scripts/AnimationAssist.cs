using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAssist : MonoBehaviour
{
    CharacterSound cs;


    // Start is called before the first frame update
    void Start()
    {
        cs = GetComponentInParent<CharacterSound>();
    }

    void FootL() {
        if(cs!= null)
        cs.FootL();
    }
    
    void FootR() {
        if (cs != null)
            cs.FootR();
    }
}
