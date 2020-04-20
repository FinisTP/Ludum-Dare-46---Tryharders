using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Navigator : MonoBehaviour
{
    public string message = "Press F to go home";
    public string nightMessage = "";
    public string navigateSceneName = "OutdoorScene";
    bool inside = false;
    public bool bell = false;
    public bool interactTalk = false;
    public bool nextday = false;
    public int money = 0;
    public int food = 0;
    public GameObject cooking;
    public GameObject neighbor;
    public GameObject typoCheck;

    public AudioSource source;
    public DayStatus type;

    public bool toggleNight = false;
    private void Start()
    {

    }
    void Update()
    {
        if (typoCheck != null && typoCheck.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.F) && inside)
        {
            if(money > 0)
            {
                GameObject.Find("Player").GetComponent<DayManager>().money += money;
                GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You take 20 C from the drawer");
            }else if (food > 0)
            {
                GameObject.Find("Player").GetComponent<DayManager>().food += food;
                GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You take enough food for your family for one day");
            }else if (cooking != null)
            {
                if (GameObject.Find("Player").GetComponent<DayManager>().food > 0)
                {
                    cooking.SetActive(true);
                    GameObject.Find("Player").GetComponent<DayManager>().food--;
                }
                else
                {
                    GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You don't have any food to cook");
                }
            }else if (nextday)
            {
                if (!GameObject.Find("Player").GetComponent<DayManager>().daytime)
                {
                    GameObject.Find("Player").GetComponent<DayManager>().NextDay();
                    SceneManager.LoadScene("Day" + (GameObject.Find("Player").GetComponent<DayManager>().day + 1) + "Monologue");
                }
                else
                    GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "It's too soon to go to sleep");
            }
            else if (interactTalk)
            {
                gameObject.GetComponent<DialogueTrigger>().dialogue.sentence = GameObject.Find("Player").GetComponent<DayManager>().getS(type).ToArray();
                gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(true);
            }else if (!bell)
            {
                if(toggleNight) GameObject.Find("Player").GetComponent<DayManager>().ToggleNight();
                if(SceneManager.GetActiveScene().name == "DowntownScene" && GameObject.Find("Player").GetComponent<DayManager>().day == 1)
                {
                    GameObject.Find("Player").GetComponent<DayManager>().choose1 = true;
                    GameObject.Find("Player").GetComponent<DayManager>().NextDay();
                    GameObject.Find("Player").GetComponent<DayManager>().NextDay();
                }
                SceneManager.LoadScene(navigateSceneName);
            }
            else
            {
                if (neighbor != null)
                {
                    if (GameObject.Find("Player").GetComponent<DayManager>().daytime) StartCoroutine(ring());
                    else
                    {
                        if(type == DayStatus.n1speech) GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "The door is locked shut.");
                        else
                        {
                            SceneManager.LoadScene("ThiefScene");
                        }
                    }
                }
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
            if(!GameObject.Find("Player").GetComponent<DayManager>().daytime && nightMessage.Length > 0) GameObject.Find("PlayerHint").GetComponent<Text>().text = nightMessage;
            else GameObject.Find("PlayerHint").GetComponent<Text>().text = message;
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
