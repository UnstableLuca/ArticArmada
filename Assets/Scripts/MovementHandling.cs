using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandling : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float maxSpeedMultiplier = 2f;
    public float minSpeedMultiplier = 0.5f;
    public float speedChangeRate = 1.5f;
    public float lateralSpeed = 5f;

    private float speedMultiplier = 1f;

    void Update()
    {
        // Aumentar o disminuir velocidad mientras se mantiene W o S
        if (Input.GetKey(KeyCode.W))
            speedMultiplier += speedChangeRate * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            speedMultiplier -= speedChangeRate * Time.deltaTime;
        else
            speedMultiplier = Mathf.MoveTowards(speedMultiplier, 1f, speedChangeRate * Time.deltaTime); // Volver a base

        speedMultiplier = Mathf.Clamp(speedMultiplier, minSpeedMultiplier, maxSpeedMultiplier);

        // Movimiento hacia adelante (Z)
        Vector3 forwardMovement = Vector3.forward * baseSpeed * speedMultiplier * Time.deltaTime;

        // Movimiento lateral (X)
        float lateralInput = 0f;
        if (Input.GetKey(KeyCode.A)) lateralInput = -1f;
        if (Input.GetKey(KeyCode.D)) lateralInput = 1f;

        Vector3 lateralMovement = Vector3.right * lateralInput * lateralSpeed * Time.deltaTime;

        // Aplicar movimiento combinado
        transform.Translate(forwardMovement + lateralMovement, Space.World);
    }
}
