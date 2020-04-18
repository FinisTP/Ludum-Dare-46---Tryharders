using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Navigator : MonoBehaviour
{
    public string message = "Press F to go home";
    public string navigateSceneName = "OutdoorScene";
    bool inside = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inside)
        {
            SceneManager.LoadScene(navigateSceneName);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("PlayerHint").GetComponent<Text>().text = message;
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (GameObject.Find("PlayerHint").GetComponent<Text>().text == message) GameObject.Find("PlayerHint").GetComponent<Text>().text = "";
            inside = false;
        }
    }
}
