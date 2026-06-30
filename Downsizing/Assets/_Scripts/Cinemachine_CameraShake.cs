using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Cinemachine_CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    public float enrageShakeStart;

    public AnimationCurve shakeCurve;
    private CinemachineBasicMultiChannelPerlin noiseModule;

    void OnEnable()
    {
        PlayerRage.OnEnrageStart += StartEnrageShake;
        PlayerRage.OnEnrageEnd += EndEnrageShake;
        PlayerRage.OnRageGained += UpdateShake;

    }

    void OnDisable()
    {
        PlayerRage.OnEnrageStart -= StartEnrageShake;
        PlayerRage.OnEnrageEnd -= EndEnrageShake;
        PlayerRage.OnRageGained -= UpdateShake;

    }

    void Start()
    {
        noiseModule = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void StartEnrageShake()
    {
        noiseModule.m_AmplitudeGain = enrageShakeStart;
    }

    void EndEnrageShake()
    {
        noiseModule.m_AmplitudeGain = 0;
    }

    void UpdateShake(float rageNormalized)
    {
        noiseModule.m_AmplitudeGain = shakeCurve.Evaluate(rageNormalized);
    }
}
