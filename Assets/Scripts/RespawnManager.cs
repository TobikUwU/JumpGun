using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    public void RespawnPlayer(Health playerHealth)
    {
        StartCoroutine(RespawnCoroutine(playerHealth));
    }

    private IEnumerator RespawnCoroutine(Health playerHealth)
    {
        yield return new WaitForSeconds(respawnDelay);

        playerHealth.transform.position = respawnPoint.position;
        playerHealth.gameObject.SetActive(true);
        playerHealth.AddHealth(3); // nebo maxHealth
        PlayerMovement movement = playerHealth.GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = true;

        

        Debug.Log("Player respawned.");
    }
}
