using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public GameObject targetObject; // Reference to the object whose Z rotation will be used

    private Vector3 startRotation; // Store locked rotation
    private Vector3 startPosition;  // Store initial position

    void Start()
    {
        startRotation = transform.eulerAngles; // Store initial rotation as Euler angles
        startPosition = transform.position;     // Store initial position
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Lock rotation to the initial X and Y, and use the Z rotation from the target object
            transform.rotation = Quaternion.Euler(startRotation.x, startRotation.y, targetObject.transform.eulerAngles.z - 90);

            // Lock position to the initial X and Y, and allow Z to change
            transform.position = new Vector3(targetObject.transform.position.x, startPosition.y, targetObject.transform.position.z);
        }
        else
        {
            Debug.LogWarning("Target object is not assigned!", this);
        }
    }
}