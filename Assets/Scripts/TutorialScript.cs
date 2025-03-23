using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text narradorText; // UI Text para mostrar los mensajes del narrador
    public GameObject enemyPrefab; // Prefab de los barcos enemigos
    public Transform spawnPoint; // Punto de aparición de enemigos
    public Button menuButton; // Botón para volver al menú (desactivado al inicio)

    private int tutorialStep = 0;
    private int shipsDestroyed = 0;
    private bool hasMovedCannon = false;

    void Start()
    {
     
        narradorText.text = "¿Estás comenzando en la Arctic Armada o buscas recordar lo básico? ¡Pues has venido al lugar correcto! " +
                            "Prueba a mover el cañón con el ratón y a disparar pulsando click izquierdo.";
    }

    void Update()
    {
        // Detectar movimiento del ratón
        if (tutorialStep == 0 && Input.GetAxis("Mouse X") != 0)
        {
            hasMovedCannon = true; // Ha movido el cañón
        }

        // Si el jugador ha movido el cañón y disparado, pasar al siguiente paso
        if (tutorialStep == 0 && hasMovedCannon && Input.GetMouseButtonDown(0)) // M1 = Botón izquierdo del ratón
        {
            narradorText.text = "¡Bien! Ahora vamos a dificultar esto un poco. " +
                                "¿Ves ese glaciar y el barco que se está acercando peligrosamente? " +
                                "Prueba a disparar a dicha embarcación antes de que llegue a su objetivo.";

            SpawnEnemy();
            tutorialStep = 1;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("ERROR: enemyPrefab is NULL! Check if 'barco' exists in the Assets folder.", this);
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("ERROR: spawnPoint is NULL! Check if 'SpawnPoint' is active in the scene.", this);
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemy spawned successfully at: " + spawnPoint.position);
        enemy.GetComponent<EnemyShip>().SetTutorialManager(this); // Asignar el tutorial manager al enemigo
    }
    
    public void EnemyDestroyed()
    {
        if (tutorialStep == 1)
        {
            // Si el jugador derriba el primer barco
            narradorText.text = "¡Perfecto! A lo largo de tus viajes, los glaciares irán apareciendo a tu alrededor " +
                                "y tendrás que estar ojo avizor para evitar que alguna embarcación se les acerque." +
                                " Con esto ya tienes todo lo necesario para ser parte de la Arctic Armada. " +
                                "¡Buena suerte ahí fuera!";

            // Spawnear 3 barcos
            for (int i = 0; i < 5; i++)
            {
                SpawnEnemy();
            }
            tutorialStep = 2;
        }
        else if (tutorialStep == 2)
        {
            shipsDestroyed++;

            if (shipsDestroyed >= 5)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
            }
        }
    }
}