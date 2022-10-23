using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GantiScene : MonoBehaviour
{
    public void PindahGameplay()
    {
        SceneManager.LoadScene("Gameplay");
        Debug.Log("Pindah ke gameplay");
    }

    public void PindahKeSetting()
    {
        SceneManager.LoadScene("Setting");
        Debug.Log("Pindah ke scene Setting");
    }

    public void PindahKeMenu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Pindah ke scene Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
