using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBombas : MonoBehaviour
{
    public float speed = 7f;
    public float dropInterval = 2f; // tiempo entre bombas
    public GameObject bombaPrefab;
    public Transform bombaSpawnPoint;
    public int maxHealth = 12;

    private float currentHealth;
    private float dropTimer = 0f;
    private TutorialManager tutorialManager;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Movimiento hacia adelante
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Controlar temporizador
        dropTimer += Time.deltaTime;
        if (dropTimer >= dropInterval)
        {
            DropBomb();
            dropTimer = 0f; // reiniciar
        }
    }

    void DropBomb()
    {
        if (bombaPrefab != null && bombaSpawnPoint != null)
        {
            float randomX = Random.Range(-75f, 75f);
            Vector3 spawnPos = new Vector3(randomX, bombaSpawnPoint.position.y, bombaSpawnPoint.position.z);

            GameObject bomba = Instantiate(bombaPrefab, spawnPos, Quaternion.identity);
            bomba.transform.parent = null; // Asegura que no siga al jefe
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (tutorialManager != null)
        {
            tutorialManager.OnBossDefeated();
        }
        Destroy(gameObject);
    }

    public void SetTutorialManager(TutorialManager manager)
    {
        tutorialManager = manager;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);

            float damage = 1f;

            string dificultad = PlayerPrefs.GetString("Dificultad", "Facil");
            if (dificultad == "Dificil")
            {
                damage *= 0.5f; // hace la mitad de daño
            }

            TakeDamage(damage);
        }
    }
}
