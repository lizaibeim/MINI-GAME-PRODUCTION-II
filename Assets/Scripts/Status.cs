using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public static List<Status> destroyedObjects;

    public bool willExplode;
    public float explosionRadius;
    public float explosionForce;

    public float killRadius;
    public float killThreshold;

    public GameObject destroyParticle;
    public int numberOfSpawnedObjects;
    public GameObject[] spawnObjects;
    public bool useCustomThreshold;
    public float destroyThreshold;
    float currentThreshold;
    public float spawnOffset;
    bool willDebug;
    Magnetic magnetic;
    // Start is called before the first frame update
    void Start()
    {
        if (destroyedObjects == null) destroyedObjects = new List<Status>();
        magnetic = GetComponent<Magnetic>();

        if (useCustomThreshold) currentThreshold = destroyThreshold;
        else currentThreshold = MagnetismManager.Instance.destroyForce;
    }


    private void OnCollisionEnter(Collision collision)
    {
        float collisionImpact = collision.impulse.magnitude;
        if (willDebug) print(collision.gameObject + " " + collisionImpact);
        if (collisionImpact < currentThreshold) return;
        if (willExplode) Explosion();

    }

    public void Explosion()
    {
        if (destroyedObjects.Contains(this)) return;

        destroyedObjects.Add(this);

        foreach (GameObject item in spawnObjects)
        {
            GameObject GO = Instantiate(item, transform.position + Vector3.up * spawnOffset, Quaternion.identity);
          if (MagnetismManager.Instance.spawnedObjectsInheritPolarity)
          {
                Magnetic mag = GO.GetComponent<Magnetic>();
                mag.polarity = magnetic.polarity;
           }
        }

        Instantiate(destroyParticle, transform.position + Vector3.up * spawnOffset, Quaternion.identity);
        magnetic.DropAllStuckObjectsDelayed();

        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up * spawnOffset, explosionRadius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            Status otherStatus = col.GetComponentInParent<Status>();
            if (otherStatus != null)
            {
                float tempRange = Vector3.Distance(col.transform.position, transform.position);
                float rangeFactor =  (1 - tempRange/killRadius);
                //float rangeFactor = 1;

                if (otherStatus.currentThreshold < killThreshold * rangeFactor)
                {
                    otherStatus.Explosion();
                }
            }
        }
      
        Destroy(gameObject);
    }
}
