using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ww_occlusion_raycast : MonoBehaviour
{
    public GameObject Audio_listener;
    private float MaxDistanceOcclusion;
    public bool UseOcclusion = false;
    public string RTPC_loPass = "RTPC_Occlusion_LoPass";
    public string RTPC_Volume = "RTPC_Occlusion_Volume";
    public float LoPass_Max = 1f;
    public float Volume_Max = 1f;
    public bool UseDebug = false;
    public string NameOfListener = "Main Camera";
    public string IgnoreTypeOccluder = "Insert Name of object to ignore";

    // Start is called before the first frame update
    void Start()
    {
        MaxDistanceOcclusion = GetComponent<SphereCollider>().radius;
        AkSoundEngine.RegisterGameObj(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!UseOcclusion || Audio_listener == null) { return; }
        var direction = Audio_listener.transform.position - this.transform.position;
        RaycastHit outInfo;
        bool hit = Physics.Raycast(this.transform.position, direction, out outInfo, MaxDistanceOcclusion);
        if (hit)
        {
            if(UseDebug) { Debug.Log(outInfo.collider.gameObject.name); }
            if(outInfo.collider.gameObject.name != NameOfListener && outInfo.collider.gameObject.name != IgnoreTypeOccluder)
            {
                Debug.Log("occlude");
                AkSoundEngine.SetRTPCValue(RTPC_loPass, LoPass_Max, gameObject);
                AkSoundEngine.SetRTPCValue(RTPC_Volume, Volume_Max, gameObject);
            } else
            {
                AkSoundEngine.SetRTPCValue(RTPC_loPass, 0, gameObject);
                AkSoundEngine.SetRTPCValue(RTPC_Volume, 0, gameObject);
            }
        }
    }
}
