using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class HoldToSkipCutscene : MonoBehaviour
{

    public float holdDuration;
    private float timer;
    private bool isHeld;

    public GameObject progressSliderGO;
    public Image progressSlider;
    public Image fadeCover;
    public Color fadeColour;
    public GameObject loading;
    void OnEnable()
    {
        InputManager.onPrimaryDown += StartHold;
        InputManager.onPrimaryUp += EndHold;
    }

    void OnDisable()
    {
        InputManager.onPrimaryDown -= StartHold;
        InputManager.onPrimaryUp -= EndHold;
    }

    void Start()
    {
        progressSliderGO.SetActive(false);

    }

    void StartHold()
    {
        isHeld = true;
        progressSliderGO.SetActive(true);
    }

    void EndHold()
    {
        isHeld = false;
        timer = 0;
        progressSliderGO.SetActive(false);
        fadeCover.color = Color.clear;

    }

    void SkipCutscene()
    {
        loading.SetActive(true);
        SceneManager.Instance.LoadScene(SceneManager.Scenes.Game2);
    }

    void Update()
    {
        if (isHeld)
        {
            timer += Time.deltaTime;
            float timerNormalized = timer / holdDuration;
            if (timerNormalized > 0.6f)
            {
                //Start fade
                float fadePercent = (timerNormalized - 0.6f) / 0.4f;
                fadeColour.a = fadePercent;
                fadeCover.color = fadeColour;
            }
            progressSlider.fillAmount = timerNormalized;
            if (timer >= holdDuration)
            {
                SkipCutscene();
            }
        }
    }
}
