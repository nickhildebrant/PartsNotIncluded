using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Handles all health and damage related functions for the enemies. 
/// Attach to each enemy in the inspector.
/// </summary>
public class HealthDamage : MonoBehaviour
{

    /// <summary>
    /// Set these values in the inspector according to each enemy type
    /// </summary>
    public float health, attackDamage, attackTimer, speed, armor;

    // Name of the enemy
    public string enemyName;

    // The transform the enemy will navigate to
    private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        // Disable when level design is ready
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.destination = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        // For movement, AI pathing, and attacking
        // Potentially switch to something more efficient than Update()
        switch (enemyName) {
            case "Alpha":
                // attackDamage
                // attackTimer
                // speed
                break;

            case "Bravo":
                // attackDamage
                // attackTimer
                // speed
                break;

            case "Echo":
                // attackDamage
                // attackTimer
                // speed
                break;

            case "Foxtrot":
                // attackDamage
                // attackTimer
                // speed
                break;

            case "Kilo":
                // attackDamage
                // attackTimer
                // speed
                break;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("<color=red>Player is attacked by a" + enemyName + "</color>");
        }
    }

    /// <summary>
    /// Used when the enemy attacks if the player is in range.
    /// Can be called by enemy or player.
    /// </summary>
    /// <param name="damage"></param>
    public float AttackPlayer() {
        return attackDamage;
    }

    /// <summary>
    /// Used when the enemy is attacked by the player.
    /// Can be called by enemy or player.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage) {
        Debug.Log("<color=green>" + enemyName + " was attacked by the player</color>");
        health -= damage;
    }
}