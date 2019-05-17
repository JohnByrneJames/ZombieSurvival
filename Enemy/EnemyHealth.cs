using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 2.5f; // Sink into floor
    public int scoreValue = 10;    // Score value
    public AudioClip deathClip;    


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); // Sinking per second instead of per frame
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint) // Hitpoint indicates location particles come out
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play(); // Plays particle system

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true; // Allows player to walk through dead Enemies

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }



    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false; // Stop enemy being affected by NavAgent
        GetComponent <Rigidbody> ().isKinematic = true; // ignores recalculating rigidbody in Unity (save memory)
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f); // Destroy this game object after 2 seconds
    }
}
