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
        Debug.Log("Jugador recibió daño. Vidas restantes: " + currentLives);

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
        // Puedes añadir aquí efectos de explosión, animación, etc.
        gameObject.SetActive(false);
    }
}

