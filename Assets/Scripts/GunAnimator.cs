using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    Animator anim;
    public MagnetGun gun;
    public PlayerStatus playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        gun.shotREvent += Shoot;
        gun.shotLEvent += Shoot;
        gun.magnetFailEvent += Shoot;
        gun.gravityShootEvent += Shoot;

        playerStatus.deathEvent += Dead;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Dead() {
        anim.SetBool("Dead", true);
    }

    void Shoot() {
        anim.SetTrigger("Shoot");
    }
}
