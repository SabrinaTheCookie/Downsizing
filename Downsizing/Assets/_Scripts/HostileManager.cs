using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileManager : Singleton<HostileManager>
{
    public List<HostileController> hostiles = new List<HostileController>();

    // Update is called once per frame
    public void UpdateHostiles()
    {
        foreach (var hostile in hostiles)
        {
            hostile.HostileUpdate();
        }
    }

}
