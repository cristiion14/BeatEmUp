using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShakeCam : MonoBehaviour
{
    //static instance
    public static CinemachineShakeCam Instance { get; private set; }

    public CinemachineVirtualCamera cinemachineVirtualCam;
    float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCam = GetComponent<CinemachineVirtualCamera>();
    }


    private void Update()
    {

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                //Time over
                //reset camera
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

   public void ShakeCamera(float intensity, float time)
    {
        //reference to the noise of the camera
        CinemachineBasicMultiChannelPerlin noise = cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

}
