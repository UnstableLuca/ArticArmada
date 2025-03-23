using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to restart the tutorial

public class EnemyShip : MonoBehaviour
{
    public float speed = 5f; // Speed of the enemy ship
    private TutorialManager tutorialManager; // Reference to the tutorial manager

    public AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
        // Ensure the ship is correctly rotated
        Vector3 rotation = transform.eulerAngles; // Get current rotation
        rotation.x = -90; // Modify the X-axis rotation
        transform.eulerAngles = rotation; // Apply back to transform

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Move left constantly without changing height
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public void SetTutorialManager(TutorialManager manager)
    {
        tutorialManager = manager;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name); // Debug log

        if (other.CompareTag("Projectile")) // If hit by a projectile
        {
            if (explosionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(explosionSound);
            }

            Debug.Log("Projectile hit detected! Destroying enemy...");
            Destroy(other.gameObject); // Destroy the projectile
            Destroy(gameObject); // Destroy this enemy ship
        }
        else if (other.CompareTag("Glaciar")) // If hit by an iceberg
        {
            Debug.Log("Enemy hit an iceberg! Restarting tutorial...");
            RestartTutorial();
        }
    }

    void OnDestroy()
    {
        if (tutorialManager != null)
        {
            tutorialManager.EnemyDestroyed(); // Notify the tutorial manager
        }
    }

    void RestartTutorial()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Reloads the current scene to restart the tutorial
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}