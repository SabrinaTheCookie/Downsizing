using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody Rigidbody;

    [Header("Properties")]
    public bool canMove = false;

    public float movementSpeed;
    public Vector3 currentSpeed;
    public AnimationCurve movementCurve;
    public float moveAccellerationDuration;
    public float moveAccelleration;
    public Vector3 moveDirection;
    public Vector2 xConstraints;
    public Vector2 zConstraints;
    public Animator animator;
    public GameObject player;

    public float speedReducedPerNearbyHostile;
    public AnimationCurve attackHoldSlowOverDuration;

    public PlayerController controller;
    public PlayerAttacks playerAttack;

    private void OnEnable()
    {
        InputManager.onMove += UpdateMoveDirection;
    }

    private void OnDisable()
    {
        InputManager.onMove -= UpdateMoveDirection;
    }

    // Update is called once per frame
    public void MovementUpdate()
    {
        //Movement viability checks
        if (!canMove) return;

        if (moveDirection == Vector3.zero)
        {
            //Reset accelleration if player has stopped moving
            if (moveAccelleration > 0) moveAccelleration = 0;
            animator.SetBool("isRunning", false);
            return;
        }

        float attackSlowMultiplier = attackHoldSlowOverDuration.Evaluate(playerAttack.attackDurationNormalized);
        float hostileSlowMultiplier = MathF.Max(1 - (speedReducedPerNearbyHostile * controller.arrest.nearbyHostiles.Count), 0.2f);
        float speed = (movementSpeed * movementCurve.Evaluate(moveAccelleration / moveAccellerationDuration));
        speed *= attackSlowMultiplier * hostileSlowMultiplier;
        Vector3 translatePosition = speed * moveDirection;
        currentSpeed = translatePosition;
        transform.Translate(translatePosition * Time.deltaTime);
        if (moveDirection.x < 0)
        {
            player.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            player.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        animator.SetBool("isRunning", true);

        //Increment moveAccelleration if needed
        if (moveAccelleration < moveAccellerationDuration)
            moveAccelleration = Mathf.Min(moveAccelleration += Time.deltaTime, moveAccellerationDuration);

        //Calculate the next position

        //Constrain Transform
        Vector3 constrainedPosition = transform.position;
        //X Constraints
        if (constrainedPosition.x < xConstraints.x) constrainedPosition.x = xConstraints.x;
        if (constrainedPosition.x > xConstraints.y) constrainedPosition.x = xConstraints.y;
        // Y Constraints
        if (constrainedPosition.z < zConstraints.x) constrainedPosition.z = zConstraints.x;
        if (constrainedPosition.z > zConstraints.y) constrainedPosition.z = zConstraints.y;

        transform.position = constrainedPosition;
    }

    public void UpdateMoveDirection(Vector2 movementDir)
    {
        moveDirection = new Vector3(movementDir.x, 0, movementDir.y);
    }
}