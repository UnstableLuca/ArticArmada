using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int maxLives = 10;
    private int currentLives;

    public TutorialManager tutorialManager; // Para notificar derrota

    void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage()
    {
        currentLives--;
        Debug.Log("Jugador recibi� da�o. Vidas restantes: " + currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador ha muerto");
        if (tutorialManager != null)
        {
            tutorialManager.GameOver();
        }
        // Puedes a�adir aqu� efectos de explosi�n, animaci�n, etc.
        gameObject.SetActive(false);
    }
}

