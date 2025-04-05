using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoGlaciar : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name);
        Debug.Log("Etiqueta del objeto: " + other.tag);
        if (other.CompareTag("Barco"))
        {
            FindObjectOfType<Vidas>()?.LoseLife();
            Destroy(gameObject);
        }
    }
}
