using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideCover : MonoBehaviour
{
    public float timeToHide;
    public Color imageColour;
    public Image image;

    public GameObject loadingGO;
    // Start is called before the first frame update
    void OnEnable()
    {
        RoomManager.OnRoomsGenerated += StartHide;
    }
    void OnDisable()
    {
        RoomManager.OnRoomsGenerated -= StartHide;
    }

    void Start()
    {
        image.color = imageColour;
    }

    // Update is called once per frame
    void StartHide()
    {
        loadingGO.SetActive(false);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        float t = timeToHide;
        while (t > 0)
        {
            t -= Time.deltaTime;
            imageColour.a = t / timeToHide;
            image.color = imageColour;
            yield return null;
        }

        imageColour.a = 0;
        image.color = imageColour;
        gameObject.SetActive(false);
        yield return null;
    }
}
