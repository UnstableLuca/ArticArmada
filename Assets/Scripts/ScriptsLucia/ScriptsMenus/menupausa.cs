using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menupausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public GameObject PantallaAjustes;
    public GameObject PantallaVidas;
    public GameObject Texto;
    public bool Pausa = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                PantallaVidas.SetActive(false);
                Texto.SetActive(false);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(Pausa == true)
            {
                volver();
            }
        }
    }
    public void volver()
    {
        ObjetoMenuPausa.SetActive(false);
        PantallaVidas.SetActive(true);
        Texto.SetActive(true);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ajustes()
    {
        PantallaAjustes.SetActive(true);
        ObjetoMenuPausa.SetActive(false);
        Pausa = true;
    }
    public void menIn()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
