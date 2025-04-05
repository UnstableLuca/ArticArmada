using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial2 : MonoBehaviour
{
    public GameObject PantallaAjustes;
    public GameObject MenInicial;
    public bool Pausa = false;

    public void tutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ajustes()
    {
        PantallaAjustes.SetActive(true);
        MenInicial.SetActive(false);
    }
    public void equipo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void salir()
    {
        Debug.Log("Salir..");
        Application.Quit();
    }
}
