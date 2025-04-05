using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Vidas : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxLives = 10;
    private int currentLives;
    public Canvas gameOverCanvas;

    public Image[] hearts; // Asigna los corazones en el Inspector

    void Start()
    {
        currentLives = maxLives;
        UpdateHeartsUI();

        if (gameOverCanvas != null)
            gameOverCanvas.enabled = false; // Oculta al inicio
    }

    void Update()
    {
        //LoseLife(); // Quita una vida al pulsar espacio
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateHeartsUI();

            if (currentLives == 0)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                HideAllCanvasesExceptGameOver();
            }
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives;
        }
    }

    void HideAllCanvasesExceptGameOver()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in canvases)
        {
            if (canvas != gameOverCanvas)
                canvas.enabled = false;
        }

        if (gameOverCanvas != null)
        {
            gameOverCanvas.enabled = true;
        }

        Debug.Log("Game Over - Mostrando menú de Game Over");
    }
}
