using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Cinemachine_FollowPlayerInWorld : MonoBehaviour
{
    public GameObject playerToFollow;

    public Vector3 worldOffset;

    public CinemachineSmoothPath camDolly;
    public CinemachineVirtualCamera vCam;

    private Vector3 offset = Vector3.zero;

    private Vector3 waypointA;
    private Vector3 waypointB;
    void Awake()
    {
        waypointA = camDolly.m_Waypoints[0].position;
        waypointB = camDolly.m_Waypoints[1].position;
    }

    public void Update()
    {
        offset.y = playerToFollow.transform.position.y;
        offset += worldOffset;

        Vector3 newPos = waypointA;
        newPos.y = offset.y;
        camDolly.m_Waypoints[0].position = newPos;

        newPos = waypointB;
        newPos.y = offset.y;
        camDolly.m_Waypoints[1].position = newPos;
        camDolly.InvalidateDistanceCache();
    }
}
