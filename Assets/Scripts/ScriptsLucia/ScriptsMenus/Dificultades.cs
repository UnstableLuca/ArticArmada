using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dificultades : MonoBehaviour
{
    // Start is called before the first frame update
   public void Facil()
   {
        PlayerPrefs.SetString("Dificultad", "Facil");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   public void Dificil()
   {
        PlayerPrefs.SetString("Dificultad", "Dificil");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
