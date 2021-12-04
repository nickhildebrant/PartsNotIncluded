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
    public float health, attackDamage, attackTimer, speed, armor, range;

    public AudioClip roboDeath; // Creates an AudioClip object for the robot death

    [SerializeField] private AudioSource roboHitSound; 
    
    // Name of the enemy
    public string enemyName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyName == "Player" && health <= 0f) {
            Debug.Log("<color=red>Jammo is dead, game over.</color>");
        }
        else if (health <= 0f)
        {
            AudioSource.PlayClipAtPoint(roboDeath, transform.position); // Plays Robot Death noise when dead

            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("<color=red>Player is attacked by enemy " + enemyName + ", dealing " + attackDamage + " damage</color>");
            other.gameObject.GetComponent<HealthDamage>().TakeDamage(attackDamage);
        } else if(other.gameObject.GetComponent<HealthDamage>() != null) {
            TakeDamage(other.gameObject.GetComponent<HealthDamage>().attackDamage);
        }
    }

    /// <summary>
    /// Used when the enemy attacks if the player is in range.
    /// Can be called by enemy or player.
    /// </summary>
    public float AttackPlayer() {
        return attackDamage;
    }

    /// <summary>
    /// Used when the enemy is attacked by the player.
    /// Can be called by enemy or player.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage) {
        Debug.Log("<color=green>" + enemyName + " was attacked by the player, dealing " + damage + " damage</color>");
        health -= damage;
        roboHitSound.Play(); 
    }
}
