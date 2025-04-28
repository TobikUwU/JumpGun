using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;

    private float cooldownTime = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Update(){

        cooldownTime += Time.deltaTime;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (cooldownTime >= attackCooldown)
        {
            cooldownTime = 0;
            anim.SetTrigger("attack");
        } 
        
    }

}
