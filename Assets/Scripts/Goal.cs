using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    bool triggerOnce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce) return;

        if (other.CompareTag("Player")) {
            triggerOnce = true;
            TransitionManager.Instance.FadeToBlack();
            LevelManager.Instance.NextLevel();
        }
    }
}
