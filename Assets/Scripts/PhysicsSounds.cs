using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSounds : MonoBehaviour
{
    public GameObject collisionSound;
    public AK.Wwise.Event SlidingEvent;
    public AK.Wwise.Event SlidingSTOP;

    public float collisionImpactThreshold;

    public float slideVelocityThreshold;
    Magnetic magnet;
    public float currentVelocity;
    public bool isSliding;
    // Start is called before the first frame update
    void Start()
    {
        magnet = GetComponent<Magnetic>();
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad < 1F) return;

        if (magnet.rb == null) {
            return;
        }
        Vector3 forwardVelocity = magnet.rb.velocity;

        forwardVelocity.y = 0;
        currentVelocity = forwardVelocity.magnitude;
        isSliding = (currentVelocity > slideVelocityThreshold) && Mathf.Abs(magnet.rb.velocity.y) < 0.1F;
        if (isSliding == true)
        {
            SlidingEvent.Post(gameObject);
        }
        else
        {
            SlidingSTOP.Post(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.timeSinceLevelLoad < 1F) return;

        float collisionImpact = collision.impulse.magnitude;
        if (collisionImpactThreshold < collisionImpact) {
            Instantiate(collisionSound,transform.position,Quaternion.identity);
        }
    }
}
