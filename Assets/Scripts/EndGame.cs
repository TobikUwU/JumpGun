using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    public GameObject endScreenUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            endScreenUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }
}
