using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the game

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with an obstacle
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("💥 Game Over! Player hit an obstacle.");
            GameOver();
        }
    }

    void GameOver()
    {
        // Restart the scene (or show a game over screen)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
