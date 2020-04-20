using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterManager : MonoBehaviour
{
    public GameObject main;
    public float[] lightDay;
    public float[] lightNight;
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
