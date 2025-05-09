using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float bulletDelay = 0.2f; // Delay before bullet is instantiated
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerMovement playerMovement;

    private Animator anim;
    private float cooldownTime = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTime += Time.deltaTime;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (cooldownTime >= attackCooldown && playerMovement.canAttack())
        {
            cooldownTime = 0;
            anim.SetTrigger("attack");
            StartCoroutine(DelayedShoot());
        }
    }

    private System.Collections.IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(bulletDelay);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
