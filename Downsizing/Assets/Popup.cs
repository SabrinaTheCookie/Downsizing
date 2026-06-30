using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public float despawnTime;
    public float speed;
    public AnimationCurve speedOverLifetime;
    public Vector2 direction;
    public AnimationCurve sizeOverLifetime;
    public TextMeshProUGUI textMeshPro;
    public RectTransform rTransform;
    
    public void InitPopup(string text, Vector3 worldPos)
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(worldPos);
        pos.x *= 1920f;
        pos.y *= 1080f;
        rTransform.anchoredPosition = pos; 
        textMeshPro.text = text;
        StartCoroutine(PopupBehaviour());
    }

    IEnumerator PopupBehaviour()
    {
        float t = despawnTime;
        Vector3 scale = rTransform.localScale;
        while (t > 0)
        {
            t -= Time.deltaTime;
            rTransform.localScale = scale * sizeOverLifetime.Evaluate(t / despawnTime);
            Vector2 pos = rTransform.anchoredPosition;
            pos += (speed * speedOverLifetime.Evaluate(t / despawnTime)) * Time.deltaTime * direction;
            rTransform.anchoredPosition = pos;
            yield return null;
        }
        Destroy(gameObject, despawnTime);
        yield return null;
    }

}
