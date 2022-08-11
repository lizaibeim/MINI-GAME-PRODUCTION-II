using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
        Status status = other.GetComponent<Status>();
        AI ai = other.GetComponent<AI>();

        if (ai != null) {
            status.Explosion();
        }

        if (playerStatus != null)
        {
            playerStatus.TakeDamage(100);
        }


    }
}
