using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pantallaaaaa : MonoBehaviour
{
    public GameObject ObjetoMenuPantallaPress;
    public GameObject ObjetoMenuPantallaPress2;
    public bool texto = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (texto == true)
        {
            ObjetoMenuPantallaPress.SetActive(false);
            ObjetoMenuPantallaPress2.SetActive(true);
        }

    }

    public void saltar()
    {
        texto = true;
    }
}
