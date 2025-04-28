using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hit");

            
        }
        else
        {
            anim.SetTrigger("hit");
            GetComponent<PlayerMovement>().enabled = false;
            
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
        
    }

}
