using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingChanger : MonoBehaviour
{
    public VolumeProfile standardProfile;
    public VolumeProfile enragedProfile;
    public Volume volume;

    private void OnEnable()
    {
        PlayerRage.OnEnrageStart += UseEnragedPostProcessing;
        PlayerRage.OnEnrageEnd += UseStandardPostProcessing;
    }
    private void OnDisable()
    {
        PlayerRage.OnEnrageStart -= UseEnragedPostProcessing;
        PlayerRage.OnEnrageEnd -= UseStandardPostProcessing;
    }

    void UseEnragedPostProcessing()
    {
        volume.profile = enragedProfile;
    }

    void UseStandardPostProcessing()
    {
        volume.profile = standardProfile;

    }
    
}
