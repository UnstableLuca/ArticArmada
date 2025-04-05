using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionNivel : MonoBehaviour
{
    public Button[] botonesNivel;

    void Start()
    {
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelDesbloqueado", 1);

        for (int i = 0; i < botonesNivel.Length; i++)
        {
            if (i < nivelDesbloqueado)
                botonesNivel[i].interactable = true;
            else
                botonesNivel[i].interactable = false;
        }
    }

    public void CargarNivel(int nivel)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel" + nivel);
    }
}
