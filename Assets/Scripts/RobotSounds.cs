using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSounds : MonoBehaviour
{
    public AK.Wwise.Event robot_Shoot;
    public AK.Wwise.Event robot_Footstep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RobotShoot()
    {
        robot_Shoot.Post(gameObject);
    }
    public void Footstep()
    {
        robot_Footstep.Post(gameObject);
    }
}
