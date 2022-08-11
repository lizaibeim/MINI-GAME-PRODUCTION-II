using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MagnetismManager : MonoBehaviour
{
    public static MagnetismManager Instance { get; private set; }
    
    public bool attractOverrides;
    public bool onlyShowDominantMagnet;
    public bool spawnedObjectsInheritPolarity;
    public bool setParentWhenAttracting;
    public float magneticFieldRange;
    public float magneticPullVelocity;

    public LayerMask magnetMask;



    [Header("Draw lines between magnetic affected objects")]
    public bool drawLines;
    public GameObject magnetLineObject;

    public GameObject rangeIndicator;
    public Material blueMaterial;
    public Material redMaterial;

    public bool changePickedMaterial;
    public Material pickedUpMaterial;
    public Material pickedObjectCollisionMaterial;
    public float destroyForce;


    public delegate void MagnetismEvent();
    public MagnetismEvent resetMagnetism;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetMagnetism() {
        resetMagnetism?.Invoke();
    }

}
