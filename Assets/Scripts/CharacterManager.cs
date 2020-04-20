using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public GameObject main;
    public float[] lightDay;
    public float[] lightNight;
    public Sprite castil, irana;


    public bool warnFood = false;
    public bool warnOutside = false;
    public bool warnInside = false;

    void Start()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Object");
        for(int i = 0; i < list.Length;i++) list[i].GetComponent<SpriteRenderer>().sortingOrder = (int)(-list[i].transform.position.y * 10 + 100);
        if (GameObject.Find("Player") == null)
        {
            GameObject p = Instantiate(main);
            p.name = "Player";
        }
        GameObject player = GameObject.Find("Player");
        player.transform.position = transform.position;
        reupdateScene(player);
        if (!player.GetComponent<DayManager>().daytime)
        {
            GameObject.FindObjectOfType<TransitionStart>().dayStatus++;
            GameObject.FindObjectOfType<TransitionStart>().reload();
        }
        if (SceneManager.GetActiveScene().name=="ThiefScene")
        {
            GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "The door seems a bit loose, you can easily sneak inside with some workaround...");
        }else if (SceneManager.GetActiveScene().name == "OutdoorScene")
        {
            if (GameObject.Find("Player").GetComponent<DayManager>().actualFood == 0 && !GameObject.Find("Player").GetComponent<DayManager>().daytime && !warnFood)
            {
                GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "Your family is hungry. Go buy some food and cook it in the kitchen");
                warnFood = true;
            }
                
            if(GameObject.Find("Player").GetComponent<DayManager>().choose1 && GameObject.Find("Player").GetComponent<DayManager>().day == 3 && !warnOutside)
            {
                GameObject.Find("Player").GetComponent<DayManager>().choose1 = false;
                warnOutside = true;
                GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You had so much fun with Athena... But she kept you f*cking on for two days straight...");
            }
        }
        else if (SceneManager.GetActiveScene().name == "IndoorScene")
        {
            if (!GameObject.Find("Player").GetComponent<DayManager>().wifeAlive || !GameObject.Find("Player").GetComponent<DayManager>().childAlive && !warnInside)
            {
                warnInside = true;
                if (GameObject.Find("Player").GetComponent<DayManager>().day == 2 && !GameObject.Find("Player").GetComponent<DayManager>().daytime && GameObject.Find("Player").GetComponent<DayManager>().choose2)
                {
                    GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "Your wife and your child have been murdered.");
                }
                else
                {
                    GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "Your wife and your child starved to death.");
                }
                GameObject.Find("Castil").GetComponent<Animator>().runtimeAnimatorController = null;
                GameObject.Find("Irana").GetComponent<Animator>().runtimeAnimatorController = null;
                GameObject.Find("Irana").GetComponent<SpriteRenderer>().sprite = irana;
                GameObject.Find("Castil").GetComponent<SpriteRenderer>().sprite = castil;
                Destroy(GameObject.Find("Castil").GetComponent<Navigator>());
                Destroy(GameObject.Find("Irana").GetComponent<Navigator>());
            }
        }
        else if (SceneManager.GetActiveScene().name == "DowntownScene")
        {
            if (GameObject.Find("Player").GetComponent<DayManager>().day == 2 && GameObject.Find("Player").GetComponent<DayManager>().daytime)
            {
                GameObject.Find("Player").GetComponent<DayManager>().wifeAlive = false;
                GameObject.Find("Player").GetComponent<DayManager>().childAlive = false;
                GameObject.Find("Player").GetComponent<DayManager>().choose2 = true;
            }
        }
    }

    public void reupdateScene(GameObject player)
    {
        if (player.GetComponent<DayManager>().mask > 0) player.transform.GetChild(1).localScale = new Vector3(0.5f, 0.5f, 0.5f);
        GameObject[] list = GameObject.FindGameObjectsWithTag("Entity");
        for (int i = 0; i < list.Length; i++)
        {
            if (player.GetComponent<DayManager>().infectedName.Contains(list[i].name)) list[i].GetComponent<BotController>().infected = true;
            list[i].GetComponent<SpriteRenderer>().sortingOrder = (int)(-list[i].transform.position.y * 10 + 100);
        }
        GameObject light = GameObject.Find("LightManager");
        if (light != null)
        {
            for (int i = 0; i < light.transform.childCount; i++)
            {
                if (player.GetComponent<DayManager>().daytime) light.transform.GetChild(i).GetComponent<Light2D>().intensity = lightDay[i];
                else light.transform.GetChild(i).GetComponent<Light2D>().intensity = lightNight[i];
            }
        }

    }
}
