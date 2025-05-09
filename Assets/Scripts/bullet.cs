using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Debug.Log(hitInfo.name);
        
        Health enemyHealth = hitInfo.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(1);
        }
        Destroy(gameObject);

        
    }

}
