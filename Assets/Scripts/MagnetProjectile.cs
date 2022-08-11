using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetProjectile : MonoBehaviour
{
    public float velocity;
    public bool isPositive;
    public GameObject hitMarkPos;
    public GameObject hitMarkNeg;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            Magnetic magnet = collision.gameObject.GetComponent<Magnetic>();

            if (isPositive)
            {
                magnet.polarity = Magnetic.Polarity.Negative;
            }
            else
            {
                magnet.polarity = Magnetic.Polarity.Positive;

            }
        }
        if (isPositive)
        {

            Instantiate(hitMarkPos, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(hitMarkNeg, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            Magnetic magnet = collider.gameObject.GetComponentInParent<Magnetic>();

            if (isPositive)
            {
                magnet.polarity = Magnetic.Polarity.Negative;
            }
            else
            {
                magnet.polarity = Magnetic.Polarity.Positive;

            }
        }
        if (isPositive)
        {

            Instantiate(hitMarkPos, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(hitMarkNeg, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
