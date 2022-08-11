using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    Player player;
    Animator anim;
    MagnetGun gun;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        gun = GetComponentInParent<MagnetGun>();

        player.jumpEvent += AnimatorJump;
        gun.shotLEvent += ShotL;
        gun.shotREvent += ShotR;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Walking", player.inputDirection.magnitude > 0);
        anim.SetBool("Grounded", player.grounded);
        anim.SetFloat("FallVelocity", player.rb.velocity.y);
    }

    void AnimatorJump() {
        anim.SetTrigger("Jump");
    }

    void ShotL() { anim.SetTrigger("Attack"); }
    void ShotR() { anim.SetTrigger("Attack"); }
}
