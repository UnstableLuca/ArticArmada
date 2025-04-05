using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textoescrito : MonoBehaviour
{
    string parrafo = "Saludos. Si estás viendo esto es que has decidido unirte a la A.A (Arctic Armada), una organización surgida para detener las recientes actividades ilícitas de ciertas embarcaciones que, al buscar destruir los glaciares, están poniendo en peligro a la fauna local.";
    string parrafo2 = "No nos importa si buscas unirte a nuestra causa por justicia o por posibles arranques de violencia, al igual que esperamos que no te sea de importancia la posible o no legalidad de nuestra institución.";
    string parrafo3 = "Solo importa si cuentas con lo necesario para repartir cañonazos a diestro y siniestro.";
    string parrafo4 = "Si tu respuesta es afirmativa, ¡te damos la bienvenida a ARCTIC ARMADA!";
    public Text texto;
    public Text texto2;
    public Text texto3;
    public Text texto4;

    public GameObject ObjetoMenuPantallaPress;
    public GameObject ObjetoMenuPantallaPress2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reloj());
    }

    // Update is called once per frame
   IEnumerator Reloj()
    {

        
        foreach (char caracter in parrafo)
        {
            texto.text += caracter;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitUntil(() => Input.anyKeyDown || Input.GetMouseButtonDown(0));

        texto.text = ""; // Limpiar antes de escribir
        foreach (char caracter in parrafo2)
        {
            texto2.text += caracter;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitUntil(() => Input.anyKeyDown || Input.GetMouseButtonDown(0));

        texto2.text = ""; // Limpiar antes de escribir
        foreach (char caracter in parrafo3)
        {
            texto3.text += caracter;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitUntil(() => Input.anyKeyDown || Input.GetMouseButtonDown(0));

        texto3.text = ""; // Limpiar antes de escribir
        foreach (char caracter in parrafo4)
        {
            texto4.text += caracter;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitUntil(() => Input.anyKeyDown || Input.GetMouseButtonDown(0));

        ObjetoMenuPantallaPress.SetActive(false);
        ObjetoMenuPantallaPress2.SetActive(true);

    }
}
