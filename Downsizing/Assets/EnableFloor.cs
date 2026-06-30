using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFloor : MonoBehaviour
{
    public MeshRenderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    public void EnableFloorRenderers()
    {
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }

    public void DisableFloorRenderers()
    {
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
