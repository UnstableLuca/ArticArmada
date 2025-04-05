using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject PantallaAjustes;
    public GameObject MenInicial;
    public GameObject dificultad;
    public bool Pausa = false;

    public void tutorial()
    {
        MenInicial.SetActive(false);
        dificultad.SetActive(true);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
