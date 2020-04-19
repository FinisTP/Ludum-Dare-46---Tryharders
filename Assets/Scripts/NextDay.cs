using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextDay : MonoBehaviour
{
    public string navigateNextScene = "OutdoorScene";
    public string message = "Press F to go to sleep.";
    private bool touchedBed = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && touchedBed)
        {
            GameObject.FindObjectOfType<DayManager>().NextDay();
            SceneManager.LoadScene(navigateNextScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("PlayerHint").GetComponent<Text>().text = message;
            touchedBed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (GameObject.Find("PlayerHint").GetComponent<Text>().text == message) GameObject.Find("PlayerHint").GetComponent<Text>().text = "";
            touchedBed = false;
        }
    }
}
