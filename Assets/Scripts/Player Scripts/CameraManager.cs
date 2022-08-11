using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public Camera mainCamera;
    public Vector2 camInput;
    public Transform player;
    public static float sensitivity = 0.5F;

    public float x, y;

    [Range(2F, 20F)]
    public float xVelocity;
    [Range(0.0024F, 0.028F)]
    public float yVelocity;

    public float inputSensitivity;

    public float yLimitMin, yLimitMax;
    public float maxY;

    public CinemachineVirtualCamera FPSCam;
    public CinemachineFreeLook TPSCam;


    public bool useFPSCam;
    public bool useTPSCam;

    public float damping;
    public bool resetY;
    public float resetDuration;
    float resetCounter;

    private void Awake()
    {
        Instance = this;

    }



    // Start is called before the first frame update
    void Start()
    {

        yVelocity = Mathf.Lerp(0.0014F, 0.014F, sensitivity);
        xVelocity = Mathf.Lerp(2F, 20F, sensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        x += camInput.x;
        y += -camInput.y * yVelocity;

        y = Mathf.Clamp(y, yLimitMin, yLimitMax);

        if (useFPSCam)
        {
            Quaternion desiredRotation = Quaternion.Euler(y * maxY, FPSCam.transform.transform.localEulerAngles.y + xVelocity * camInput.x, 0);
            FPSCam.transform.localRotation = Quaternion.Slerp(FPSCam.transform.localRotation, desiredRotation, Time.deltaTime * damping);
        }

        if (useTPSCam)
        {
            TPSCam.m_XAxis.m_InputAxisValue = camInput.x;
            TPSCam.m_YAxis.m_InputAxisValue = camInput.y;
        }


        //if (resetY) {
        //    resetCounter -= Time.deltaTime;

        //    y = Mathf.Lerp(y, 0, damping * Time.deltaTime);
        //    if (resetCounter <= 0) {
        //        resetY = false;
        //    }

        //}
    }

    public void SetSensitivity(float sens) {
        print(sens);
        sensitivity = sens;
        yVelocity = Mathf.Lerp(0.0014F, 0.014F, sensitivity);
        xVelocity = Mathf.Lerp(2F, 20F, sensitivity);
    }

    public void ResetY()
    {
        resetY = true;
        resetCounter = resetDuration;
    }
}
