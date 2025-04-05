using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public GameObject PantallaAjustes;
    public GameObject OtraPantalla;

    public void atras()
    {
        PantallaAjustes.SetActive(false);
        OtraPantalla.SetActive(true);
    }
}
