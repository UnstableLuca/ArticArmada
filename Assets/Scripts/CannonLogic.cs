using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonLogic : MonoBehaviour
{
    public GameObject projectilePrefab; // The cannonball prefab
    public Transform firePoint; // The point from which the projectile is fired
    public float baseFireForce = 10f; // Minimum force
    public float maxFireForce = 50f; // Maximum force when fully charged
    public float chargeTime = 2f; // Time to reach max charge
    public float fireRate = 1f; // Cooldown time between shots
    public float projectileLifetime = 5f; // Time before projectile is destroyed
    public float sensitivity = 2f; // Speed for adjusting rotation

    public Slider cooldownSlider; // UI Slider for cooldown tracking
    public Slider chargeSlider; // UI Slider for charge power tracking
    public GameObject beaconPrefab; // Prefab for impact point prediction

    private float nextFireTime = 0f;
    private float currentChargeTime = 0f;
    private GameObject beaconInstance;

    private float rotationZ = 0f; // Tracks horizontal rotation (left/right)
    private float rotationY = 0f; // Tracks vertical rotation (up/down)

    public AudioClip fireSound;
    private AudioSource audioSource;

    void Start()
    {
        if(beaconPrefab != null)
        {
            beaconInstance = Instantiate(beaconPrefab);
            beaconInstance.SetActive(false);
        }
        else
        {
            Debug.LogError("Beacon Prefab is not assigned in the Inspector!", this);
        }

        cooldownSlider.maxValue = fireRate;
        cooldownSlider.value = 0;
        chargeSlider.maxValue = chargeTime;
        chargeSlider.value = 0;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        cooldownSlider.value = Mathf.Clamp(nextFireTime - Time.time, 0, fireRate);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Acumular rotación en los ejes
        rotationZ += mouseX;
        rotationY -= mouseY; // Se invierte para la rotación vertical

        rotationY = Mathf.Clamp(rotationY, -110f, -80f);
        rotationZ = Mathf.Clamp(rotationZ, -130f, -50f);

        transform.rotation = Quaternion.Euler(rotationY, 0f, rotationZ);

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            currentChargeTime += Time.deltaTime;
            chargeSlider.value = Mathf.Clamp(currentChargeTime, 0, chargeTime);
        }

        if (Input.GetButtonUp("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
            currentChargeTime = 0f;
            chargeSlider.value = 0f;
        }

        PredictImpactPoint();
    }

    void Shoot()
    {
        audioSource.PlayOneShot(fireSound);
        float chargePercent = Mathf.Clamp01(currentChargeTime / chargeTime);
        float fireForce = Mathf.Lerp(baseFireForce, maxFireForce, chargePercent);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }

        Destroy(projectile, projectileLifetime);
    }

    void PredictImpactPoint()
    {
        if (beaconInstance == null)
        {
            Debug.LogError("beaconInstance is null! Make sure it is instantiated in Start().", this);
            return;
        }

        // Always show the beacon while charging
        beaconInstance.SetActive(true);

        // Get actual charge level for accurate force prediction
        float chargePercent = Mathf.Clamp01(currentChargeTime / chargeTime);
        float predictedForce = Mathf.Lerp(baseFireForce, maxFireForce, chargePercent);

        // Calculate the trajectory
        Vector3 initialVelocity = firePoint.forward * predictedForce;
        Vector3 gravity = Physics.gravity;

        // Simulate the projectile's path
        Vector3 position = firePoint.position;
        float time = 0f;
        float deltaTime = 0.1f; // Time step for simulation

        // Reset the beacon position
        Vector3 lastBeaconPosition = beaconInstance.transform.position;

        while (time < 5f) // Simulate for a maximum of 5 seconds
        {
            position += initialVelocity * deltaTime;
            initialVelocity += gravity * deltaTime;

            // Check for ground hit using raycast
            if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 1f))
            {
                beaconInstance.transform.position = hit.point;
                return; // Exit if we found a hit
            }

            time += deltaTime;
        }

        // If no hit was detected, keep the beacon at the last known position
        beaconInstance.transform.position = position; // Update to the last position if no hit
    }

}

