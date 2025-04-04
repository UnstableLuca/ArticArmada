using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip : MonoBehaviour
{
    public float speed = 5f;
    private TutorialManager tutorialManager;
    private string tipo;

    public AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
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
            if (explosionSound != null && audioSource != null)
                audioSource.PlayOneShot(explosionSound);

            Destroy(other.gameObject); // Eliminar proyectil
            Destroy(gameObject);       // Eliminar barco
        }
        else if (other.CompareTag("Glaciar"))
        {
            Debug.Log("Enemy hit an iceberg! Restarting tutorial...");
            RestartTutorial();
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
