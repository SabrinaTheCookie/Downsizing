using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Smashable : MonoBehaviour
{
    public float rageOnSmash;

    public static event Action<float> OnSmash;

    public float despawnTime;
    public AnimationCurve despawnCurve;

    public float costValue;

    public string soundOnSmash;

    public GameObject moneyPopupPrefab;

    public Transform hud;

    private bool isSmashed;

    private void Start()
    {
        hud = GameObject.Find("HUD").transform;
    }

    public void Smash()
    {
        if (isSmashed) return;
        isSmashed = true;
        OnSmash.Invoke(rageOnSmash);
        StartCoroutine(Despawn());
        GameStateManager.Instance.UpdateGameState(1, 0, costValue, 0);
        AudioManager.Instance.PlaySound(soundOnSmash);
        Popup moneyPopup = Instantiate(moneyPopupPrefab, hud).GetComponent<Popup>();
        moneyPopup.InitPopup($"${costValue.ToString("00.00")}", transform.position);
    }

    IEnumerator Despawn()
    {
        float t = despawnTime;
        Vector3 localScale = transform.localScale;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float multiplier = despawnCurve.Evaluate(t / despawnTime);
            Vector3 scale = localScale * multiplier;
            transform.localScale = scale;
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }

}