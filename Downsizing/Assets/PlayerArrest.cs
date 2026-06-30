using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerArrest : MonoBehaviour
{
    public Slider arrestSlider;
    public float currentArrestValue;
    public float arrestThreshhold;
    public float arrestPerSecondPerHostile;
    public float arrestReductionSpeedMultiplier;
    public bool isArrested;
    public static event Action onArrest;
    public List<HostileController> nearbyHostiles = new List<HostileController>();

    public void UpdateArrest()
    {
        currentArrestValue += (nearbyHostiles.Count > 0 ? arrestPerSecondPerHostile * nearbyHostiles.Count : -arrestPerSecondPerHostile * arrestReductionSpeedMultiplier) * Time.deltaTime;
        currentArrestValue = Mathf.Clamp(currentArrestValue, 0, arrestThreshhold);

        arrestSlider.value = currentArrestValue / arrestThreshhold;

        if (currentArrestValue >= arrestThreshhold)
        {
            Arrest();
        }
    }

    void Arrest()
    {
        if (isArrested) return;
        isArrested = true;
        onArrest?.Invoke();
        AudioManager.Instance.PlayDialogue($"arrest{Random.Range(1,6)}");
    }

    public void AddNearbyEnemy(HostileController hostile)
    {
        if (!nearbyHostiles.Contains(hostile))
            nearbyHostiles.Add(hostile);
    }

    public void RemoveNearbyEnemy(HostileController hostile)
    {
        if (nearbyHostiles.Contains(hostile))
            nearbyHostiles.Remove(hostile);
    }
}
