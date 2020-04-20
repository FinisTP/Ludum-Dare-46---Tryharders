using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("OpeningMonologue");
    }
    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        Application.Quit();
    }
}
