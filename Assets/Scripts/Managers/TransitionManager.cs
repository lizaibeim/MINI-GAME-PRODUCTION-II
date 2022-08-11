using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetTrigger("Neutral");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death() {
        anim.SetTrigger("Death");
    }

    public void FadeToBlack() {
        anim.SetTrigger("Black");
    }
}
