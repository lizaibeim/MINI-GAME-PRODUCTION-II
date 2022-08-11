using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canMove;
    [HideInInspector] public Rigidbody rb;
    public float velocity;
    [HideInInspector] public Vector2 inputDirection;
    [HideInInspector] public Vector3 movementDirection;
    [SerializeField] Vector3 inheritedVelocity;
    public Transform cam;
    public float damping = 1;

    private Vector3 gravityDirection;
    private Vector3 forwardVector;
    private Vector3 rightVector;
    public float jumpForce;

    [Header("Ground detection properties")]
    public bool grounded;
    public float groundRayDistance;
    public float groundRayOffset;
    public SurfaceMaterial currentSurfaceMaterial;
    [SerializeField] private LayerMask groundLayers;
    public PhysicMaterial groundMaterial;
    public PhysicMaterial airMaterial;
    private Collider col;
    private RaycastHit groundRayHit;
    private bool checkGround = true;

    [Header("Airborne properties")]
    public bool useForces;
    public float airVelocity;
    public float maxForward;
    public float maxFallSpeed;
    private Vector3 currentFallVelocity;
    [HideInInspector] public float fallMagnitude;

    [Header("Landing properties")]

    [SerializeField] private float shakeMultiplier;
    [SerializeField] private float landingShakeDuration;

    public delegate void AnimatorEvent();
    public AnimatorEvent jumpEvent;
    public AnimatorEvent landEvent;
    public AnimatorEvent hardLandEvent;


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        movementDirection = transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        //Calculate forward and right
        forwardVector = Vector3.Cross(-Vector3.up, cam.transform.right).normalized;
        //rightVector = Vector3.Cross(RemoveFallVelocity(forwardVector), GravityManager.Instance.GravityDirection.normalized).normalized;
        rightVector = cam.transform.right;

        //Update direction only while performing an input

        movementDirection = ((rightVector * inputDirection.x) + (forwardVector * inputDirection.y)).normalized;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        gravityDirection = Vector3.down;


        //Simple rotation of gameplay aspect of 3rd person player
        if (inputDirection.x != 0 || inputDirection.y != 0)
        {
            if (movementDirection != -gravityDirection)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, -gravityDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * damping);
            }
        }

        GroundDetection();
        //Save falling velocity before setting movement velocity
        Vector3 fallVelocity = FallVelocity();

        if (IsFalling())
        {
            if (fallVelocity.magnitude > currentFallVelocity.magnitude) currentFallVelocity = fallVelocity;
            fallVelocity = Vector3.ClampMagnitude(fallVelocity, maxFallSpeed);
            fallMagnitude = fallVelocity.magnitude / maxFallSpeed;
        }

        if (grounded)
        {
            rb.velocity = new Vector3(movementDirection.x * velocity * Time.deltaTime, rb.velocity.y, movementDirection.z * velocity * Time.deltaTime);
            //rb.velocity += fallVelocity;

            //if (onSlope)
            //{
            //    if (onlyForward) rb.velocity = new Vector3(transform.forward.x * direction.magnitude * currentVel, rb.velocity.y, transform.forward.z * direction.magnitude * currentVel);
            //    else rb.velocity = Vector3.Cross(new Vector3(direction.z, 0, -direction.x), groundRayHit.normal) * currentVel;

            //}
            // rb.velocity += inheritedVelocity;
        }
        else
        {
            if (!useForces)
            {
                rb.velocity = RemoveFallVelocity(movementDirection).normalized * airVelocity * Time.deltaTime;

                rb.velocity += fallVelocity;
            }
            else
            {
                rb.AddForce(RemoveFallVelocity(movementDirection).normalized * airVelocity, ForceMode.Acceleration);
                Vector3 forwardVelocity = RemoveYAxis(rb.velocity);
                forwardVelocity = Vector3.ClampMagnitude(forwardVelocity, maxForward);
                rb.velocity = forwardVelocity + fallVelocity;
            }
        }
    }

    void GroundDetection()
    {
        if (!checkGround) return;

        Debug.DrawRay(transform.position + transform.up * 0.1F, -transform.up * groundRayDistance, Color.green);
        Debug.DrawRay(transform.position + transform.up * 0.1F + transform.right * groundRayOffset, -transform.up * groundRayDistance, Color.green);
        Debug.DrawRay(transform.position + transform.up * 0.1F - transform.right * groundRayOffset, -transform.up * groundRayDistance, Color.green);
        bool tempGround =
            Physics.Raycast(transform.position + transform.up * 0.1F, -transform.up, out groundRayHit, groundRayDistance, groundLayers)
            || Physics.Raycast(transform.position + transform.up * 0.1F + transform.right * groundRayOffset, -transform.up, out groundRayHit, groundRayDistance, groundLayers)
            || Physics.Raycast(transform.position + transform.up * 0.1F - transform.right * groundRayOffset, -transform.up, out groundRayHit, groundRayDistance, groundLayers)
            ;

        if (tempGround)
        {
            switch (groundRayHit.collider.tag)
            {
                case "Metal":
                    currentSurfaceMaterial = SurfaceMaterial.Metal;
                    break;

                default:
                    currentSurfaceMaterial = SurfaceMaterial.Default;
                    break;
            }

            Rigidbody tempRB = groundRayHit.collider.GetComponentInParent<Rigidbody>();
            if (tempRB != null)
            {
                inheritedVelocity = tempRB.velocity;
                //print(tempRB);
            }
            else inheritedVelocity = Vector3.zero;
        }


        if (!grounded && tempGround)
        {
            Land();
            col.material = groundMaterial;
            grounded = true;
        }
        else if (!tempGround)
        {
            col.material = airMaterial;
            grounded = false;
        }
    }

    public void RotateForward()
    {
        //Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, -gravityDirection);
        transform.rotation = Quaternion.LookRotation(forwardVector, -gravityDirection);
    }

    bool IsFalling()
    {
        Vector3 n = gravityDirection;
        Vector3 v = rb.velocity;
        float d = Vector3.Dot(v, n);

        return (d > 0f);

    }

    public Vector3 FallVelocity()
    {
        Vector3 n = gravityDirection;
        Vector3 v = rb.velocity;
        float d = Vector3.Dot(v, n);
        return n * d;
    }

    public Vector3 RemoveYAxis(Vector3 vec)
    {
        Vector3 n = gravityDirection;

        Vector3 dir = vec;
        float d = Vector3.Dot(dir, n);
        dir -= n * d;
        return dir;
    }

    Vector3 RemoveFallVelocity(Vector3 vec)
    {
        Vector3 n = gravityDirection;

        Vector3 dir = vec;
        float d = Vector3.Dot(dir, n);

        if (d > 0f)
        {
            dir -= n * d;

        }
        return dir;
    }


    public void Jump()
    {
        if (!grounded) return;
        jumpEvent?.Invoke();
        StartCoroutine("JumpStartup");
        grounded = false;
        col.material = airMaterial;
        rb.velocity = RemoveFallVelocity(rb.velocity);

        rb.velocity += jumpForce * Vector3.up;
    }

    IEnumerator JumpStartup()
    {
        checkGround = false;
        yield return new WaitForSeconds(0.5F);
        checkGround = true;
    }

    void Land()
    {

        float multiplier = currentFallVelocity.magnitude * shakeMultiplier;
        // print(currentFallVelocity.magnitude + " " + multiplier);
        //StartCoroutine("ScreenShakeDelay");

        //CameraShake.Instance.ShakeCamera(landingShakeIntensity * multiplier, Mathf.Clamp(landingShakeDuration * multiplier, 0, maxShakeDuration));
        if (fallMagnitude > 0.5F)
        {
            hardLandEvent?.Invoke();
        }
        else { landEvent?.Invoke(); }
        fallMagnitude = 0;
        currentFallVelocity = Vector3.zero;
    }
}
