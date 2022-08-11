using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIK : MonoBehaviour
{
    Animator anim;
    RobotAnimator roboAnim;
    public float handWeight;
    //public Transform handTarget;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        roboAnim = GetComponent<RobotAnimator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        handWeight = anim.GetFloat("HandIK");
        if (roboAnim.handIKTarget == null) return;
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
        //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, handWeight);
        //anim.SetIKPosition(AvatarIKGoal.RightHand, handTarget.position);
        //anim.SetIKRotation(AvatarIKGoal.RightHand, handTarget.rotation);
        anim.SetIKPosition(AvatarIKGoal.RightHand, roboAnim.handIKTarget.position);
        //anim.SetIKRotation(AvatarIKGoal.RightHand, roboAnim.handIKTarget.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
