using UnityEngine;
using UnityEngine.SceneManagement;

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

    private ParticleSystem particle;
    ParticleSystem.EmissionModule em;
    private float timer = 0f;
    
    // Name of the enemy
    public string enemyName;

    private ThirdPersonMovement movement;
    private Animator anim;
    private float deathTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        particle = this.transform.GetComponentInChildren<ParticleSystem>();
        if(particle != null) particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyName == "Player" && health <= 0f) {
            Debug.Log("<color=red>Jammo is dead, game over.</color>");
            movement = this.transform.parent.GetComponent<ThirdPersonMovement>();
            movement.enabled = false;
            anim = movement.getAnimator();
            anim.SetBool("isDead", true);
            deathTimer += Time.deltaTime;

            if(deathTimer > 5.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            
        }
        else if (health <= 0f)
        {
            if(roboDeath != null) AudioSource.PlayClipAtPoint(roboDeath, transform.position); // Plays Robot Death noise when dead

            gameObject.SetActive(false);
        }

        if(timer > 2.0f)
        {
            if(particle != null) particle.Stop();
            timer = 0f;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player" && other.gameObject.GetComponentInChildren<HealthDamage>() != null) {
            other.gameObject.GetComponentInChildren<HealthDamage>().TakeDamage(attackDamage);
            if(particle != null) particle.Play();
            timer += Time.deltaTime;
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
        health -= damage;
        if(roboHitSound != null) roboHitSound.Play();
        if(enemyName == "Player") GameObject.Find("HealthBar").GetComponent<HealthBar>().slider.value = health;
    }
}