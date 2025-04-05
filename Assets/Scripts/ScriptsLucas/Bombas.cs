using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombas : MonoBehaviour
{
    public float floatSpeed = 1f;      // velocidad de oscilación
    public float floatAmplitude = 0.5f; // amplitud de oscilación

    private float originalY;

    void Start()
    {
        originalY = transform.position.y;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {
        float newY = originalY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        Vector3 newPos = new Vector3(transform.position.x, newY, transform.position.z);
        transform.position = newPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barco"))
        {
            FindObjectOfType<Vidas>()?.LoseLife();
            Destroy(gameObject);
        }
    }
}
