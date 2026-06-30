using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour
{
    public float rageMax;
    public float currentRage;
    public Slider rageMeter;
    public GameObject rageText;
    public bool isEnraged;

    public static event Action OnEnrageStart;
    public static event Action OnEnrageEnd;
    public static event Action<float> OnRageGained;

    private void OnEnable()
    {
        Smashable.OnSmash += AddRage;
    }
    private void OnDisable()
    {
        Smashable.OnSmash -= AddRage;
    }

    public void AddRage(float rageToAdd)
    {
        if (isEnraged) return;

        currentRage += rageToAdd;
        
        float rageNormalized = currentRage / rageMax;
        OnRageGained?.Invoke(rageNormalized);
        rageMeter.value = rageNormalized;
        
        if(currentRage >= rageMax)
        {
            //Rage has just reached max
            currentRage = rageMax;
            Enrage();
        }
    }

    void Enrage()
    {
        isEnraged = true;
        rageText.gameObject.SetActive(true);
        OnEnrageStart?.Invoke();
    }

    public void ClearRage()
    {
        isEnraged = false;
        rageText.gameObject.SetActive(false);
        currentRage = 0;
        rageMeter.value = 0;
        OnEnrageEnd?.Invoke();

    }
}
