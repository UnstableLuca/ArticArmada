using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public string targetTag = "Glaciar"; // Cambia esto al tag deseado
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // Guarda la posición inicial
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            ResetPosition();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.position = initialPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
