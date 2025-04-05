using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text narradorText;

    public GameObject barcoNormalPrefab;
    public GameObject lanchaPrefab;
    public GameObject barcoPesadoPrefab;
    public GameObject glaciarPrefab;
    public GameObject jefePrefab;

    public Transform playerTransform;
    public float distanceInFrontOfPlayer = 50f;
    public float glaciarOffsetZ = 25f;
    public float[] xSpawnPool = new float[] { -30f, -20f, -10f, 0f, 10f, 20f, 30f };

    private Queue<float> xPositions = new();
    private int tutorialStep = 0;
    private bool moved = false;
    private bool shot = false;
    private List<GameObject> currentEnemies = new();

    void Start()
    {
        Time.timeScale = 0f;
        FillXQueueRandomly();
    }

    void Update()
    {
        if (tutorialStep == 0)
        {
            narradorText.text = "¿Estás comenzando en la Arctic Armada o buscas recordar lo básico? ¡Pues has venido al lugar correcto! Prueba a mover el cañón con el ratón y a disparar pulsando click izquierdo.";
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                moved = true;

            if (Input.GetMouseButtonDown(0))
                shot = true;

            if (moved && shot)
                StartCoroutine(Fase1());
        }
    }

    IEnumerator Fase1()
    {
        tutorialStep = 1;
        narradorText.text = "¡Bien! Ahora vamos a dificultar esto un poco.";
        yield return new WaitForSeconds(2f);

        narradorText.text = "¿Ves ese glaciar y el barco que se está acercando peligrosamente? Prueba a disparar a dicha embarcación antes de que llegue a su objetivo.";
        yield return new WaitForSeconds(3f);

        SpawnBarcoConGlaciar("Normal");
    }

    public void OnEnemyDestroyed(string type)
    {
        if (tutorialStep == 1 && type == "Normal")
        {
            StartCoroutine(Fase2());
        }
        else if (tutorialStep == 2)
        {
            // Eliminar nulls primero
            currentEnemies.RemoveAll(e => e == null);
            Debug.Log("Enemigos restantes: " + currentEnemies.Count);
            // Verificar si todos han sido destruidos
            if (currentEnemies.Count == 1)
            {
                StartCoroutine(Fase3_Jefe());
            }
        }
    }

    IEnumerator Fase2()
    {
        tutorialStep = 2;

        EmptyXQueue(); // Limpiar la cola de posiciones X

        narradorText.text = "¡Perfecto! A lo largo de tus viajes, los glaciares irán apareciendo a tu alrededor y tendrás que estar ojo avizor...";
        yield return new WaitForSeconds(4f);

        narradorText.text = "Ten cuidado porque estas embarcaciones no tienen porqué ser iguales...";
        yield return new WaitForSeconds(5f);

        narradorText.text = "A continuación, se probará tu habilidad con diversos enemigos que aparecerán a tu alrededor...";
        yield return new WaitForSeconds(3f);

        currentEnemies.Clear(); // Limpiar antes de añadir nuevos enemigos

        SpawnBarcoConGlaciar("Pesado");
        yield return new WaitForSeconds(5f);

        SpawnBarcoConGlaciar("Lancha");
        yield return new WaitForSeconds(7f);

        SpawnBarcoConGlaciar("Normal");
    }

    IEnumerator Fase3_Jefe()
    {
        tutorialStep = 3;

        narradorText.text = "Pues con esto ya sabrías todo lo...";
        yield return new WaitForSeconds(2f);

        narradorText.text = "<color=red>*ALARMA SONANDO*</color>";
        yield return new WaitForSeconds(2f);

        narradorText.text = "Oh, cierto, sabía que se me había olvidado algo...";
        yield return new WaitForSeconds(2.5f);

        narradorText.text = "Es bastante probable que de vez en cuando te topes con alguna escoria criminal que haya modificado su embarcación...";
        yield return new WaitForSeconds(4f);

        narradorText.text = "Las de este tipo suelen aguantar bastantes zambombazos y esta en concreto parece que cuenta con una gran cantidad de explosivos.";
        yield return new WaitForSeconds(4f);

        narradorText.text = "Creo que tienes el instinto de supervivencia necesario para darte cuenta, pero por si acaso, intenta que no te den.";
        yield return new WaitForSeconds(3.5f);

        // Spawnea el jefe frente al jugador en X = 0
        float x = 0f;
        float y = -18f;
        float z = playerTransform.position.z + distanceInFrontOfPlayer;
        Vector3 jefePos = new Vector3(x, y, z);

        GameObject jefe = Instantiate(jefePrefab, jefePos, Quaternion.Euler(270, 0, 0));
        jefe.GetComponent<BossBombas>().SetTutorialManager(this);
    }

    public void OnBossDefeated()
    {
        narradorText.text = "¡Bien hecho! Con esto ya tienes todo lo necesario para ser parte de la Arctic Armada. ¡Buena suerte ahí fuera!";
        StartCoroutine(FinalizarTutorial());
    }

    IEnumerator FinalizarTutorial()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("SampleScene"); // Cambia el nombre si tu escena de menú se llama diferente Lucia
    }

    GameObject SpawnBarcoConGlaciar(string tipo)
    {
        if (xPositions.Count == 0)
            FillXQueueRandomly();

        float x = xPositions.Dequeue();
        float y = -20f;
        float z = playerTransform.position.z + distanceInFrontOfPlayer;
        Debug.Log(z);

        Vector3 glaciarPos = new Vector3(x, y, z);
        GameObject glaciar = Instantiate(glaciarPrefab, glaciarPos, Quaternion.Euler(270, 90, 0));

        float barcoZ = glaciar.transform.position.z - glaciarOffsetZ - 5f;
        Vector3 barcoFinalPos = new Vector3(x, y, barcoZ);

        GameObject barco = Instantiate(GetPrefabFromTipo(tipo), barcoFinalPos, Quaternion.Euler(270, 90, 0));
        barco.GetComponent<EnemyShip>().SetTutorialManager(this, tipo);

        //Añadir al conteo de enemigos si estamos en Fase 2
        if (tutorialStep == 2)
            currentEnemies.Add(barco);

        return barco;
    }

    void FillXQueueRandomly()
    {
        List<float> shuffled = new List<float>(xSpawnPool);
        for (int i = 0; i < shuffled.Count; i++)
        {
            float temp = shuffled[i];
            int randIndex = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[randIndex];
            shuffled[randIndex] = temp;
        }

        foreach (float x in shuffled)
            xPositions.Enqueue(x);
    }

    void EmptyXQueue()
    {
        xPositions.Clear();
    }

    GameObject GetPrefabFromTipo(string tipo)
    {
        switch (tipo)
        {
            case "Normal": return barcoNormalPrefab;
            case "Lancha": return lanchaPrefab;
            case "Pesado": return barcoPesadoPrefab;
            default: return barcoNormalPrefab;
        }
    }

    public void GameOver()
    {
        narradorText.text = "¡Casi, vamos a intentarlo de nuevo!";
        Debug.Log("GAME OVER - Reiniciando tutorial...");

        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(3f); // tiempo de espera opcional
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
