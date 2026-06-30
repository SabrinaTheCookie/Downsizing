using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : Singleton<StatManager>
{
    [Header("Stats")]
    [SerializeField] private int points;
    [SerializeField] private int floors;
    [SerializeField] private float timeTaken;



    protected override void Awake()
    {
        base.Awake();
        points = 0;
        floors = 0;
        timeTaken = 0;
    }


    /// <summary>
    /// Increases points total by parameter value
    /// </summary>
    /// <param name="pointsToAdd"></param>
    public void addPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    /// <summary>
    /// Decreases points total by parameter value
    /// </summary>
    /// <param name="pointsToSubtract"></param>
    public void minusPoints(int pointsToSubtract)
    {
        points -= pointsToSubtract;
    }

    /// <summary>
    /// Increases floor total by one
    /// </summary>
    public void completeFloor()
    {
        floors++;
    }

    /// <summary>
    /// Updates time value
    /// </summary>
    public void updateTime()
    {
        timeTaken = Time.time;
    }
}
