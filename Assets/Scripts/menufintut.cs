using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menufintut : MonoBehaviour
{
    public void menIn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void Sair()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
