using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public int damage;
    [SerializeField] float LaserRange = 10f;
    private LineRenderer lineRenderer;
    public bool shootingRay = true;
    private float cachedLaserRange;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cachedLaserRange = LaserRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shootingRay)
        {
            LaserRange = -0.1f;
        } else
        {
            LaserRange = cachedLaserRange;
        }

        lineRenderer.SetPosition(0, Vector3.zero);
        
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, transform.forward, out hitInfo, LaserRange);

        if (hit)
        {
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
            if(hitInfo.collider.GetComponentInParent<Status>() != null) {
                hitInfo.collider.GetComponentInParent<Status>().Explosion();
            }

            if (hitInfo.collider.GetComponentInParent<PlayerStatus>() != null)
            {
                hitInfo.collider.GetComponentInParent<PlayerStatus>().TakeDamage(damage);
            }

        } else
        {
            
            lineRenderer.SetPosition(1, Vector3.zero + transform.worldToLocalMatrix.MultiplyVector(transform.forward) * LaserRange);
        }
    }
}
