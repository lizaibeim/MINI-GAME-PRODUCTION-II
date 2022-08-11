using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public CinemachineVirtualCamera[] cameras;
    CinemachineBasicMultiChannelPerlin[] noises;
    [SerializeField] private float shakeTimer;
    private float startTimer;
    private float startIntensity;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        noises = new CinemachineBasicMultiChannelPerlin[cameras.Length];
        for (int i = 0; i < noises.Length; i++)
        {
            noises[i] = cameras[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
      

        ShakeCamera(0, 0.1F);
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            // if (shakeTimer <= 0F)
            {
                for (int i = 0; i < noises.Length; i++)
                {
                    //  CinemachineBasicMultiChannelPerlin perlin = cameras[i].GetComponent<CinemachineBasicMultiChannelPerlin>();
                    //  perlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, (1 - (shakeTimer / startTimer)));

                    noises[i].m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, (1 - (shakeTimer / startTimer)));
                }



            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        startIntensity = intensity;
        shakeTimer = time;
        startTimer = time;

        for (int i = 0; i < noises.Length; i++)
        {
           
            noises[i].m_AmplitudeGain = startIntensity;
        }
    }
}
