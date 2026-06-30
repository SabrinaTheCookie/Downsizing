using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Components")]
    public PlayerMovement movement;

    public PlayerRage rage;
    public PlayerAttacks attacks;
    public PlayerArrest arrest;

    public static event Action onWin;
    private bool hasWon;

    public void PlayerUpdate()
    {
        if (arrest.isArrested) return;
        if (hasWon) return;
        movement.MovementUpdate();
        attacks.AttackUpdate();
        arrest.UpdateArrest();
    }

    public void Win()
    {
        StartCoroutine(winEnumerator());
    }

    public IEnumerator winEnumerator()
    {
        hasWon = true;
        yield return new WaitForSeconds(2);
        AudioManager.Instance.PlaySound("promo");
        onWin?.Invoke();
        yield return null;
    }






}
