using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGun : MonoBehaviour
{

    public Camera cam;
    public float gunRange;
    public bool shootOnce;
    public LayerMask mask;
    public GameObject hitMarkL;
    public GameObject hitMarkR;
    public bool shootProjectile;
    public GameObject projectile;

    public Transform gunShootingPoint;
    public LineRenderer lineRenderer;
    public float lineDuration;
    float lineCounter;

    public GameObject blueMuzzleFlash;
    public GameObject redMuzzleFlash;

    public delegate void GunEvent();
    public GunEvent shootAnimationEvent;
    public GunEvent shotLEvent;
    public GunEvent shotREvent;
    public GunEvent gravityPickupEvent;
    public GunEvent gravityShootEvent;
    public GunEvent failedShootEvent;
    public GunEvent magnetFailEvent;

    public float maxPickupWeight;
    public float pickupRange;
    public float shotVelocity;
    public Transform pickupTransform;
    public Transform pickedObject;

    public Material pickedMaterial;
    Material oldMaterial;

    public float cooldown;
    float cooldownCounterR;
    float cooldownCounterL;

    Player player;
    CharacterSound cs;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        cs = GetComponentInParent<CharacterSound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownCounterR > 0)
        {
            cooldownCounterR -= Time.deltaTime * Time.timeScale;
        }

        if (cooldownCounterL > 0)
        {
            cooldownCounterL -= Time.deltaTime * Time.timeScale;
        }

        if (lineCounter > 0)
        {
            lineCounter -= Time.deltaTime * Time.timeScale;

            if (lineCounter <= 0)
            {
                lineRenderer.enabled = false;
            }
        }
    }

    public void Pickup()
    {
        if (pickedObject != null)
        {
            DropPickedObject();
        }
        else
        {
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

            if (Physics.Raycast(ray, out hit, pickupRange, mask))
            {
                if (hit.collider)
                {
                    print(hit.collider);

                    Magnetic targetMagnet = hit.collider.GetComponentInParent<Magnetic>();
                    if (targetMagnet == null) return;
                    if (targetMagnet.isStatic) return;
                    if (targetMagnet.magnetWeight > maxPickupWeight) return;
                    if (targetMagnet.rb == null) targetMagnet.EnableMovement();

                    pickedObject = hit.transform;
                    pickedObject.position = pickupTransform.position;
                    pickedObject.parent = pickupTransform;
                    pickedObject.gameObject.layer = LayerMask.NameToLayer("PickedObject");

                    gravityPickupEvent?.Invoke();

                    targetMagnet.isPickedUp = true;
                    targetMagnet.DropAllStuckObjects();
                    Rigidbody tempRB = pickedObject.GetComponent<Rigidbody>();

                    tempRB.constraints = RigidbodyConstraints.FreezeAll;
                    targetMagnet.col.isTrigger = true;
                }
            }
        }
    }



    void DropPickedObject()
    {
        Magnetic targetMagnet = pickedObject.GetComponent<Magnetic>();
        if (targetMagnet.isColliding)
        {
            failedShootEvent?.Invoke();
            return;
        }

        Rigidbody tempRB = pickedObject.GetComponent<Rigidbody>();
        tempRB.constraints = RigidbodyConstraints.None;
        pickedObject.parent = null;
        pickedObject.gameObject.layer = LayerMask.NameToLayer("Metal");
        targetMagnet.col.isTrigger = false;
        targetMagnet.isPickedUp = false;
        targetMagnet.EnableMovement();

        pickedObject = null;
    }
    void ShootPickedObject()
    {
        Magnetic targetMagnet = pickedObject.GetComponent<Magnetic>();
        if (targetMagnet.isColliding)
        {
            failedShootEvent?.Invoke();
            return;
        }

        Rigidbody tempRB = pickedObject.GetComponent<Rigidbody>();
        tempRB.constraints = RigidbodyConstraints.None;
        pickedObject.parent = null;
        tempRB.velocity = cam.transform.forward * shotVelocity;
        targetMagnet.EnableMovement();
        pickedObject.gameObject.layer = LayerMask.NameToLayer("Metal");
        targetMagnet.col.isTrigger = false;
        targetMagnet.isPickedUp = false;

        gravityShootEvent?.Invoke();
        pickedObject = null;
    }

    public void ToggleProjectile()
    {
        shootProjectile = !shootProjectile;
    }

    public void Shoot(bool isPositive)
    {
        if (pickedObject != null)
        {
            ShootPickedObject();

        }
        else
        {
            //if (isPositive) {
            
                if (cooldownCounterR > 0) return;
                cooldownCounterR = cooldown;
            //}
            //else { 
            //    if (cooldownCounterL > 0) return;
            //    cooldownCounterL = cooldown;
            //}
            
            player.RotateForward();
            shootAnimationEvent?.Invoke();

            if (shootProjectile)
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                Quaternion shootDirection;
                if (Physics.Raycast(ray, out hit, gunRange, mask))
                {
                    shootDirection = Quaternion.LookRotation(hit.point - gunShootingPoint.position);

                }
                else { shootDirection = cam.transform.rotation; }

                shootAnimationEvent?.Invoke();
                GameObject GO = Instantiate(projectile, gunShootingPoint.position, shootDirection);
                if (isPositive) Instantiate(blueMuzzleFlash, gunShootingPoint.position, gunShootingPoint.transform.rotation);
                else
                    Instantiate(redMuzzleFlash, gunShootingPoint.position, gunShootingPoint.transform.rotation);

                MagnetProjectile tempProjectile = GO.GetComponent<MagnetProjectile>();
                tempProjectile.isPositive = isPositive;

                if (isPositive) shotREvent?.Invoke();
                else shotLEvent?.Invoke();
            }
            else
            {

                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

                if (Physics.Raycast(ray, out hit, gunRange, mask))
                {
                    if (hit.collider)
                    {
                        Magnetic targetMagnet;
                        Rigidbody rb = hit.transform.GetComponent<Rigidbody>();




                        if (rb == null)
                        {
                            targetMagnet = hit.transform.GetComponentInParent<Magnetic>();
                        }
                        else
                        {
                            targetMagnet = hit.transform.GetComponent<Magnetic>();
                        }

                        if (targetMagnet == null) return;
                        if (isPositive)
                        {
                            targetMagnet.polarity = Magnetic.Polarity.Negative;
                            Instantiate(hitMarkL, hit.point, Quaternion.LookRotation(cam.transform.forward, hit.normal));
                            shotREvent?.Invoke();
                        }
                        else
                        {
                            Instantiate(hitMarkR, hit.point, Quaternion.LookRotation(cam.transform.forward, hit.normal));
                            targetMagnet.polarity = Magnetic.Polarity.Positive;
                            shotLEvent?.Invoke();
                        }
                        Vector3 startPosition = gunShootingPoint.position;
                        lineRenderer.SetPosition(0, startPosition);
                        lineRenderer.SetPosition(1, hit.point);
                        lineRenderer.enabled = true;
                        lineCounter = lineDuration;
                        
                    }
                    else magnetFailEvent();
                }
                else magnetFailEvent();
            }
        }
    }
}
