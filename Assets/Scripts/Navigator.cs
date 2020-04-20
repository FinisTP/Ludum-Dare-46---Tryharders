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
    public bool interactTalk = false;
    public bool nextday = false;
    public GameObject neighbor;

    public AudioSource source;
    public DayStatus type;

    public bool toggleNight = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inside)
        {
            if (nextday)
            {
                if(!GameObject.Find("Player").GetComponent<DayManager>().daytime) GameObject.Find("Player").GetComponent<DayManager>().NextDay();
                SceneManager.LoadScene("Day"+(GameObject.Find("Player").GetComponent<DayManager>().day+1)+"Monologue");
            }
            else if (interactTalk)
            {
                gameObject.GetComponent<DialogueTrigger>().dialogue.sentence = GameObject.Find("Player").GetComponent<DayManager>().getS(type).ToArray();
                gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(true);
            }else if (!bell)
            {
                if(toggleNight) GameObject.Find("Player").GetComponent<DayManager>().ToggleNight();
                SceneManager.LoadScene(navigateSceneName);
            }
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
        if (GameObject.Find("Player").GetComponent<DayManager>().infectedName.Contains(n.name)) n.GetComponent<BotController>().infected = true;
        n.transform.position = transform.position;
        neighbor = null;
        n.GetComponent<BotController>().neighbor = true;
        gameObject.GetComponent<DialogueTrigger>().dialogue.sentence = GameObject.Find("Player").GetComponent<DayManager>().getS(type).ToArray();
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(false);
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
