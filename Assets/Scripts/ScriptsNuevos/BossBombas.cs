using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBombas : MonoBehaviour
{
    public float speed = 7f;
    public int maxHealth = 12;
    public GameObject bombaPrefab;
    public float bombDropInterval = 2f;

    private int currentHealth;
    private TutorialManager tutorialManager;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(DropBombsRoutine());
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    IEnumerator DropBombsRoutine()
    {
        while (!isDead)
        {
            Instantiate(bombaPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(bombDropInterval);
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        StopAllCoroutines();
        Destroy(gameObject);

        if (tutorialManager != null)
        {
            tutorialManager.OnBossDefeated();
        }
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
            TakeDamage();
        }
    }
}
