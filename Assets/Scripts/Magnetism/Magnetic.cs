using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnetic : MonoBehaviour
{
    public enum Polarity { None, Positive, Negative };
    public Polarity polarity = Polarity.None;
    public bool isStatic;
    public bool drawLines;
    public float transformYOffset;
    Vector3 bodyPosition;
    public List<LineRenderer> lines;
    public List<Magnetic> magnetList;
    public List<Magnetic> affectedMagnets;
    public float magnetWeight = 1;
    public int affectedObjects;
    float highestDetectedWeight;
    public bool isBeingAttracted;
    bool sameSize;
    float startMass;
    Renderer rend;
    public Material mat;

    Renderer indicatorRenderer;
    GameObject rangeIndicator;
    public bool willShowRangeIndicator;
    public Rigidbody rb;
    [HideInInspector] public Collider col;
    [HideInInspector] public bool isAffectedByMagnetism;
    [HideInInspector] public bool isPickedUp;
    [HideInInspector] public bool isColliding;
    [HideInInspector] public List<GameObject> collidingObjects;
    [HideInInspector] public bool isStuck;
    public List<GameObject> stuckObjects;

    public bool hasAI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startMass = rb.mass;
        col = GetComponentInChildren<Collider>();
        rend = GetComponentInChildren<Renderer>();
        mat = rend.material;

        MagnetismManager.Instance.resetMagnetism += ResetPolarity;
        collidingObjects = new List<GameObject>();


    }

    private void OnDisable()
    {
        MagnetismManager.Instance.resetMagnetism -= ResetPolarity;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DisplayRangeIndicator();
        ApplyRangeIndicator();
        CheckForCollissions();
        if (polarity == Polarity.None)
        {
            return;
        }
        FindMagnets();
        FindStrongestMagnet();
        ResolveMagnetism();
        DrawMagnetLines();
    }

    private void Update()
    {
        bodyPosition = transform.position + transform.up * transformYOffset;
    }

    void CheckForCollissions()
    {
        if (!isPickedUp) return;
        isColliding = (collidingObjects.Count > 0);
        if (isColliding) rend.material = MagnetismManager.Instance.pickedObjectCollisionMaterial;
        else
        {
            if (MagnetismManager.Instance.changePickedMaterial)
            {
                rend.material = MagnetismManager.Instance.pickedUpMaterial;
            }
            else
                rend.material = mat;
        }
    }

    void DrawMagnetLines()
    {
        if (MagnetismManager.Instance.drawLines)
        {

            if (lines.Count > affectedMagnets.Count)
            {
                //Remove lines
                print("ebola");
                Destroy(lines[0].gameObject);
                lines.RemoveAt(0);
            }
            else if (lines.Count < affectedMagnets.Count)
            {
                //Add new line
                GameObject GO = Instantiate(MagnetismManager.Instance.magnetLineObject, bodyPosition, Quaternion.identity);
                lines.Add(GO.GetComponent<LineRenderer>());
            }
            if (isAffectedByMagnetism) return;
            for (int i = 0; i < affectedMagnets.Count; i++)
            {
                if (lines.Count > i)
                {
                    if (polarity == Polarity.Positive)
                    {
                        lines[i].startColor = Color.red;
                        lines[i].endColor = Color.red;

                    }
                    else
                    {
                        lines[i].startColor = Color.blue;
                        lines[i].endColor = Color.blue;
                    }

                    lines[i].SetPosition(0, bodyPosition);
                    lines[i].SetPosition(1, affectedMagnets[i].bodyPosition);
                }

            }
        }
    }

    void DestroyAllLines()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines[i].gameObject);
        }
        lines.Clear();
    }

    void ApplyRangeIndicator()
    {
        if (polarity != Polarity.None)
        {
          
            if (rangeIndicator == null)
            {
                rangeIndicator = Instantiate(MagnetismManager.Instance.rangeIndicator, bodyPosition, Quaternion.identity, transform);
                indicatorRenderer = rangeIndicator.GetComponent<Renderer>();
            }

            rangeIndicator.transform.localScale =
                new Vector3((MagnetismManager.Instance.magneticFieldRange * magnetWeight * 2) / transform.localScale.x,
                (MagnetismManager.Instance.magneticFieldRange * magnetWeight * 2) / transform.localScale.y,
                (MagnetismManager.Instance.magneticFieldRange * magnetWeight * 2) / transform.localScale.z);

            if (isBeingAttracted && !sameSize && MagnetismManager.Instance.onlyShowDominantMagnet) rangeIndicator.SetActive(false);
            else rangeIndicator.SetActive(true);

            if (polarity == Polarity.Positive)
            {
                mat.SetColor("_RimColor", Color.red);
                indicatorRenderer.material = MagnetismManager.Instance.redMaterial;
            }
            else if (polarity == Polarity.Negative)
            {
                mat.SetColor("_RimColor", Color.blue);
                indicatorRenderer.material = MagnetismManager.Instance.blueMaterial;
            }
        }
        else {
            if (rangeIndicator != null)
                rangeIndicator.SetActive(false);
            mat.SetColor("_RimColor", Color.white);
        }
    }

    void DisplayRangeIndicator()
    {
        if (polarity == Polarity.None)
        {
            willShowRangeIndicator = false;
            if (rangeIndicator != null)
                rangeIndicator.SetActive(false);
            mat.SetColor("_RimColor", Color.white);
        }
        else {
            willShowRangeIndicator = !MagnetismManager.Instance.onlyShowDominantMagnet || !isBeingAttracted && !sameSize && MagnetismManager.Instance.onlyShowDominantMagnet;
        }
    }
    void ResolveMagnetism()
    {
        if (isPickedUp) return;

        affectedObjects = 0;
        affectedMagnets.Clear();

        for (int i = 0; i < magnetList.Count; i++)
        {
            magnetList[i].isAffectedByMagnetism = false;
            if (magnetList[i].magnetWeight <= magnetWeight)
            {
                if (magnetList[i].polarity != Polarity.None)
                {

                    magnetList[i].isAffectedByMagnetism = true;
                    affectedObjects++;
                    affectedMagnets.Add(magnetList[i]);


                    if (magnetList[i].hasAI)
                        magnetList[i].rb.isKinematic = false;
                }


                //REPELL
                if (polarity == magnetList[i].polarity)
                {
                    if (magnetList[i].highestDetectedWeight <= magnetWeight && !magnetList[i].isBeingAttracted)
                    {

                        if (magnetList[i].isStuck)
                        {
                            magnetList[i].EnableMovement();
                            stuckObjects.Remove(magnetList[i].gameObject);
                        }
                        if (magnetList[i].rb != null)
                            magnetList[i].rb.AddForce((magnetList[i].bodyPosition - bodyPosition).normalized * MagnetismManager.Instance.magneticPullVelocity * magnetWeight);
                    }
                }
                //ATTRACT
                else if (polarity != magnetList[i].polarity && magnetList[i].polarity != Polarity.None)
                {
                    if (magnetList[i].rb != null)
                    {
                        if (!stuckObjects.Contains(magnetList[i].gameObject))
                        {
                            magnetList[i].rb.AddForce((bodyPosition - magnetList[i].bodyPosition).normalized * MagnetismManager.Instance.magneticPullVelocity * magnetWeight);
                        }
                        else if (magnetList[i].isPickedUp)
                        {
                            stuckObjects.Remove(magnetList[i].gameObject);
                        }
                    }
                }
            }
        }
    }

    public void DropAllStuckObjects()
    {
        DestroyAllLines();
        foreach (GameObject GO in stuckObjects)
        {
            if (GO != null)
            {
                Magnetic tempMagnet = GO.GetComponent<Magnetic>();
                tempMagnet.EnableMovement();
            }
        }
        stuckObjects.Clear();
    }

    public void DropAllStuckObjectsDelayed()
    {
        DestroyAllLines();
        foreach (GameObject GO in stuckObjects)
        {
            if (GO != null)
            {
                Magnetic tempMagnet = GO.GetComponent<Magnetic>();
                tempMagnet.EnableMovementDelayed();
            }
        }
        stuckObjects.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (polarity == Polarity.None || !MagnetismManager.Instance.setParentWhenAttracting) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            Magnetic other = collision.gameObject.GetComponent<Magnetic>();
            if (other == null) return;
            if (polarity != other.polarity && other.polarity != Polarity.None)
            {
                if (other.magnetWeight < magnetWeight)
                {
                    stuckObjects.Add(other.gameObject);
                    other.transform.parent = transform;
                    other.DisableMovement();
                }
            }
        }
    }

    public void DisableMovement()
    {
        print("FuckThis");
        rb.useGravity = false;
        Destroy(rb);
        isStuck = true;
    }

    public void EnableMovement()
    {
        transform.parent = null;
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = startMass;
        }

        rb.constraints = RigidbodyConstraints.None;
        rend.material = mat;
        isStuck = false;

    }

    public void EnableMovementDelayed() {
        transform.parent = null;
        //Destroy(rb);
        StartCoroutine("ResetRigidbody");
    }

    IEnumerator ResetRigidbody() {
        yield return new WaitForFixedUpdate();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = startMass;
        }

        rb.constraints = RigidbodyConstraints.None;
        rend.material = mat;
        isStuck = false;

    }

    void FindStrongestMagnet()
    {
        sameSize = false;
        isBeingAttracted = false;
        highestDetectedWeight = 0;
        int highestIndex = 0;

        if (magnetList.Count <= 0) return;

        for (int i = 0; i < magnetList.Count; i++)
        {
            if (magnetList[i].polarity != Polarity.None)
                if (magnetList[highestIndex].magnetWeight >= magnetWeight)
                {
                    if (magnetList[i].magnetWeight > highestDetectedWeight)
                    {
                        highestDetectedWeight = magnetList[i].magnetWeight;
                        highestIndex = i;
                    }
                }
        }

        if (magnetList[highestIndex].polarity != polarity && magnetList[highestIndex].polarity != Polarity.None)
        {
            if (MagnetismManager.Instance.attractOverrides)
            {
                if (magnetList[highestIndex].magnetWeight >= magnetWeight)
                    isBeingAttracted = true;
            }
        }

        if (highestDetectedWeight == magnetWeight)
            sameSize = true;
    }
    void FindMagnets()
    {
        magnetList.Clear();

        Collider[] colliders = Physics.OverlapSphere(bodyPosition, MagnetismManager.Instance.magneticFieldRange * magnetWeight, MagnetismManager.Instance.magnetMask);
        foreach (Collider col in colliders)
        {
            Magnetic mag = col.GetComponentInParent<Magnetic>();
            if (mag != null && mag != this)
                magnetList.Add(mag);
        }
    }
    void ResetPolarity()
    {
        if (polarity == Polarity.None) return;
        polarity = Polarity.None;
        DropAllStuckObjects();
        if (!isStatic && isStuck)
            EnableMovement();
        DestroyAllLines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPickedUp) return;
        if (!collidingObjects.Contains(other.gameObject))
            collidingObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPickedUp) return;
        if (collidingObjects.Contains(other.gameObject))
            collidingObjects.Remove(other.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        MagnetismManager manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MagnetismManager>(); ;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(bodyPosition, manager.magneticFieldRange * magnetWeight);
    }
}
