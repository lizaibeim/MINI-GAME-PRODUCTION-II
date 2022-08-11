using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    [SerializeField] float smashForce = 100f;
    [SerializeField] float smashTimer = 3.0f;
    [SerializeField] float returnForce = 250f;

    public bool delayInitialSmash = false;
    [SerializeField] float delayInitialSmashTime = 1.5f;
    float delayTimer = 0f;
    public GameObject goDownFX;
    public GameObject goUpFX;
    public GameObject topPlate;
    public GameObject bottomPlate;
    Rigidbody topRB;

    bool goDown = true;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        topRB = topPlate.GetComponent<Rigidbody>();
        if (delayInitialSmash)
        {
            topRB.useGravity = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(delayInitialSmash)
        {
            delayTimer += Time.deltaTime;
            if(delayTimer > delayInitialSmashTime)
            {
                delayInitialSmash = false;
                topRB.useGravity = true;
            }
        } 
        else
        {
            timer += Time.deltaTime;
            if (timer > smashTimer)
            {
                timer = 0;
                if (goDown)
                {
                    Instantiate(goUpFX, topPlate.transform.position, Quaternion.identity);
                    goDown = false;
                } else
                {
                    Instantiate(goDownFX, topPlate.transform.position, Quaternion.identity);
                    goDown = true;
                }
            }
            applyForce();
        }
    }

    void applyForce()
    {
        if (goDown)
        {
            topRB.AddForce(Vector3.down * smashForce);
        } else
        {
            topRB.AddForce(Vector3.up * returnForce);
        }
    }
}
