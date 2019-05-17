using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;  // Player Health at start of Game
    public int currentHealth;         // Player's health after they have been damaged
    public Slider healthSlider;
    public Image damageImage;         // Damage Image on screen
    public AudioClip deathClip;       // Audio on death
    public float flashSpeed = 5f;     // Speed at which damage image appears
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // Completely red and 10% Opacity


    // References
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;  // Used to stop player when they are dead
    //PlayerShooting playerShooting;

    // Dead or Damaged
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // Transition to clear
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        anim.SetTrigger ("Die"); // Plays Death animation

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "HealPoint")
        {

            currentHealth = currentHealth + 10;
        }
    }

}
