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

    public AudioSource source;
    public DayStatus type;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inside)
        {
            if(!bell) SceneManager.LoadScene(navigateSceneName);
            else
            {
                if (neighbor != null) StartCoroutine(ring());
            }
        }
    }
    IEnumerator ring()
    {
        //play sound
        if (GameObject.Find("PlayerHint").GetComponent<Text>().text == message) GameObject.Find("PlayerHint").GetComponent<Text>().text = "";
        source.Play();
        yield return new WaitForSeconds(1.5f);
        GameObject n = Instantiate(neighbor);
        n.transform.position = transform.position;
        neighbor = null;
        n.GetComponent<BotController>().neighbor = true;
        gameObject.GetComponent<DialogueTrigger>().dialogue.sentence = GameObject.Find("Main").GetComponent<DayManager>().getS(type).ToArray();
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
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
