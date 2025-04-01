using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandling : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
