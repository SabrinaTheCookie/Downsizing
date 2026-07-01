using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HostileController : MonoBehaviour
{
    [Header("Player")]
    private PlayerController player;

    [Header("Components")]
    private NavMeshAgent agent;

    private HostileManager hostileManager;
    public GameObject hostilePosition;
    public GameObject playerPosition;
    public Animator animator;

    [Header("Room")]
    public Room room;

    [Header("Stats")]
    public float arrestRadius = 0.3f;

    public float playerSlamExplosionForce;
    public float playerSlamExplosionRadius;
    public float explosionUpwardsModifier;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerController>();
        hostileManager = FindFirstObjectByType<HostileManager>();
    }

    // Update is called once per frame
    public void HostileUpdate()
    {
        //if the current room is not active... dont update
        if (!room.isActive) return;

        if (hostilePosition.transform.position.x < player.gameObject.transform.position.x)
        {
            hostilePosition.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            hostilePosition.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Move to player
        agent.SetDestination(player.transform.position);
        //Always face forwards
        transform.LookAt(Camera.main.transform.position, Vector3.up);

        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < arrestRadius)
        {
            animator.SetBool("isIdle", true);
            player.arrest.AddNearbyEnemy(this);
        }
        else
        {
            animator.SetBool("isIdle", false);
            player.arrest.RemoveNearbyEnemy(this);
        }
    }

    public void Deactivate()
    {
        animator.SetTrigger("splat");
        agent.enabled = false;
        player.arrest.RemoveNearbyEnemy(this);

        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            AudioManager.Instance.PlayDialogue($"guarddie{Random.Range(1,4)}");
        }

        //rbody.useGravity = false;
        //rbody.AddExplosionForce(playerSlamExplosionForce, player.transform.position - 0.1f, playerSlamExplosionRadius, explosionUpwardsModifier, ForceMode.Impulse);
    }
}