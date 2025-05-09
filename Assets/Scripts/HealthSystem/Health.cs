using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool isPlayer;
    [SerializeField] private RespawnManager respawnScript;

    
    private Rigidbody2D rb;


    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void TakeDamage(int damage)
{
    currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

    if (currentHealth > 0)
    {
        if (anim != null)
            anim.SetTrigger("hit");

       
    }
    else
    {
        if (isPlayer)
        {
            // Hráč: okamžité zmizení bez animace
            PlayerMovement movement = GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = false;
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            gameObject.SetActive(false);

            if (respawnScript != null)
                respawnScript.RespawnPlayer(this);
            
        }
        else
        {
            // Nepřítel: animace smrti a deaktivace přes event
            if (anim != null)
                anim.SetTrigger("hit");

            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            // Animace dokončí akci → animation event zavolá DisableObject()
        }
    }
}


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
        
    }

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }


}
