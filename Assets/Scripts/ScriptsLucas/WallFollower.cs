using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollower : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 0f, 0f);

    void Update()
    {
        if (player != null)
        {
            Vector3 newPos = transform.position;
            newPos.z = player.position.z + offset.z;
            transform.position = newPos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name);
        Debug.Log("Etiqueta del objeto: " + other.tag);
        if (other.CompareTag("Barco"))
        {
                FindObjectOfType<Vidas>()?.LoseLife();

                // Reposicionar X a 0 (mantener Y y Z)
                Vector3 fixedPosition = other.transform.position;
                fixedPosition.x = 0f;
                other.transform.position = fixedPosition;
        }
    }
}