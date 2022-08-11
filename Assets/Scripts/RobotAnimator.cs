using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RobotAnimator : MonoBehaviour
{
    Animator anim;
    AI ai;
    NavMeshAgent agent;
    RobotSounds sounds;
    public bool pickedSwarmbot;
    public Transform handIKTarget;
    public GameObject chargeParticle;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ai = GetComponentInParent<AI>();
        agent = ai.GetComponent<NavMeshAgent>();
        sounds = GetComponentInParent<RobotSounds>();


        ai.attackEvent += ShootAnimation;
        ai.pickUpSwarmbotEvent += PickUpSwarmbot;
        ai.pickUpMetalEvent += PickUpMetal;
    }

    // Update is called once per frame
    void Update()
    {
      anim.SetBool("Walking", (agent.velocity.magnitude > 2));
    }

    void ShootAnimation() {
        anim.SetTrigger("Shoot");
    }

    void Shoot() {
        ai.ShootBullet();
        sounds.RobotShoot();
    }

    void Throw() {
        ai.ThrowSwarmBot();
        sounds.RobotShoot();
    }

    void PickUpSwarmbot() {
        pickedSwarmbot = true;
        PickUpAnimation();
    }

    void Charge() {
        Instantiate(chargeParticle, ai.BulletSpawn.position, ai.BulletSpawn.rotation , ai.BulletSpawn);
    }
    void PickUpMetal()
    {
        pickedSwarmbot = false;
        PickUpAnimation();
    }

    void PickUpAnimation() {
        anim.SetTrigger("PickUp");
    }

    void PickUp() {
        if (pickedSwarmbot) { ai.PickUpSwarmBot();
            handIKTarget = ai.GetClosestSwarmBot();
        }
        else { ai.PickUpMetalPiece();
            handIKTarget = ai.GetClosestMetalPiece();
        }
    }



    void FootL()
    {
        sounds.Footstep();
    }

    void FootR()
    {
        sounds.Footstep();
    }
}
