using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]private int healthValue = 1;



private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Player")
    {
        collision.gameObject.GetComponent<Health>().AddHealth(healthValue);
        gameObject.SetActive(false);
    }
}


}
