using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text narradorText;

    public GameObject barcoNormalPrefab;
    public GameObject lanchaPrefab;
    public GameObject barcoPesadoPrefab;
    public GameObject glaciarPrefab;

    public Transform playerTransform; // Player reference
    public float distanceInFrontOfPlayer = 50f; // How far in front of player the spawn should occur
    public float glaciarOffsetZ = 25f;
    public float[] xSpawnPool = new float[] { -30f, -20f, -10f, 0f, 10f, 20f, 30f }; // pool of z positions

    private Queue<float> xPositions = new();
    private int tutorialStep = 0;
    private bool moved = false;
    private bool shot = false;
    private List<GameObject> currentEnemies = new();

    void Start()
    {
        Time.timeScale = 1f;
        narradorText.text = "¿Estás comenzando en la Arctic Armada o buscas recordar lo básico? ¡Pues has venido al lugar correcto! Prueba a mover el cañón con el ratón y a disparar pulsando click izquierdo.";

        FillXQueueRandomly();
    }

    void Update()
    {
        if (tutorialStep == 0)
        {
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
            if (currentEnemies.Count == 0) return;

            currentEnemies.RemoveAll(e => e == null);

            if (currentEnemies.Count == 0)
                StartCoroutine(Fase3_Jefe());
        }
    }

    IEnumerator Fase2()
    {
        tutorialStep = 2;

        narradorText.text = "¡Perfecto! A lo largo de tus viajes, los glaciares irán apareciendo a tu alrededor y tendrás que estar ojo avizor...";
        yield return new WaitForSeconds(4f);

        narradorText.text = "Ten cuidado porque estas embarcaciones no tienen porqué ser iguales...";
        yield return new WaitForSeconds(5f);

        narradorText.text = "A continuación, se probará tu habilidad con diversos enemigos que aparecerán a tu alrededor...";
        yield return new WaitForSeconds(3f);

        GameObject pesado = SpawnBarcoConGlaciar("Pesado");
        yield return new WaitForSeconds(2f);

        GameObject lancha = SpawnBarcoConGlaciar("Lancha");
        yield return new WaitForSeconds(3f);

        GameObject normal = SpawnBarcoConGlaciar("Normal");

        currentEnemies.Clear();
        currentEnemies.Add(pesado);
        currentEnemies.Add(lancha);
        currentEnemies.Add(normal);
    }

    IEnumerator Fase3_Jefe()
    {
        narradorText.text = "Pues con esto ya sabrías todo lo...";
        yield return new WaitForSeconds(2f);
        narradorText.text = "<color=red>*ALARMA SONANDO*</color>";
        // Aquí empieza la lógica del jefe...
    }

    GameObject SpawnBarcoConGlaciar(string tipo)
    {
        if (xPositions.Count == 0)
        {
            FillXQueueRandomly();
        }

        float x = xPositions.Dequeue();
        float y = -20f;
        float z = playerTransform.position.z + distanceInFrontOfPlayer;

        Vector3 barcoPos = new Vector3(x, y, z);
        Vector3 glaciarPos = new Vector3(x, y, z + glaciarOffsetZ);

        GameObject barco = Instantiate(GetPrefabFromTipo(tipo), barcoPos, Quaternion.Euler(270, 90, 0));
        barco.GetComponent<EnemyShip>().SetTutorialManager(this, tipo);

        Instantiate(glaciarPrefab, glaciarPos, Quaternion.identity);
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
        {
            xPositions.Enqueue(x);
        }
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
}
