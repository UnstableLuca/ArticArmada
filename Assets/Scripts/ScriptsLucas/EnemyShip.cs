using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip : MonoBehaviour
{
    public float maxHealth = 1f;
    private float currentHealth;

    public float speed = 5f;
    private TutorialManager tutorialManager;
    private string tipo;

    public AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        // Rotación correcta (ya apuntando hacia Z+ visualmente)
        transform.rotation = Quaternion.Euler(270, 90, 0);

        // Configurar sonido de explosión
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Movimiento constante hacia adelante en el eje Z
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    public void SetTutorialManager(TutorialManager manager, string tipo)
    {
        this.tutorialManager = manager;
        this.tipo = tipo;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);

            float damage = 1f;
            string dificultad = PlayerPrefs.GetString("Dificultad", "Facil");
            if (dificultad == "Dificil")
            {
                damage *= 0.5f;
            }

            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                if (explosionSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(explosionSound);
                }

                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Glaciar"))
        {
            Debug.Log("Colisión con glaciar detectada");
            FindObjectOfType<Vidas>()?.LoseLife();
            Destroy(gameObject);
            Destroy(other.gameObject);
        } else if (other.CompareTag("Barco"))
        {
            Debug.Log("Colisión con barco detectada");
            FindObjectOfType<Vidas>()?.LoseLife();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (tutorialManager != null)
            tutorialManager.OnEnemyDestroyed(tipo);
    }

    void RestartTutorial()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
