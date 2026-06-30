using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCube : MonoBehaviour
{
    public float despawnDuration;
    public float breakForce;
    public AnimationCurve sizeCurve;

    public void Break()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.down * breakForce);
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        float t = despawnDuration;
        Vector3 localScale = transform.localScale;
        
        //Shrink over time.
        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.localScale = localScale * sizeCurve.Evaluate(t);

            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }


}
