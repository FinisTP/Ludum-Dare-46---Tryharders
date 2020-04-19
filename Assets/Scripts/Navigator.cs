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
    public bool bell = false;
    public GameObject neighbor;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inside)
        {
            if(!bell) SceneManager.LoadScene(navigateSceneName);
            else
            {
                if (neighbor != null)
                {
                    StartCoroutine(ring());
                }
            }
        }
    }
    IEnumerator ring()
    {
        //play sound
        if (GameObject.Find("PlayerHint").GetComponent<Text>().text == message) GameObject.Find("PlayerHint").GetComponent<Text>().text = "";

        yield return new WaitForSeconds(1.5f);
        if (neighbor == null) yield return null;
        GameObject n = Instantiate(neighbor);
        n.transform.position = transform.position;
        n.GetComponent<BotController>().neighbor = true;
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        neighbor = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (bell && neighbor == null) return;
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
