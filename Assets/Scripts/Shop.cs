using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public GameObject shop, dark;
    public Text text;
    public AudioSource shopMusic, buy, cBuy, streetMusic;
    public void open()
    {
        if (shop.activeSelf)
        {
            shop.SetActive(false);
            dark.SetActive(false);
            text.gameObject.SetActive(false);
            shopMusic.Stop();
            streetMusic.Play();
            return;
        }
        if (GameObject.Find("Player").GetComponent<DayManager>().day >= 3)
        {
            for (int i = 0; i < shop.transform.childCount; i++) Destroy(shop.transform.GetChild(i).gameObject);
        }
        else
            text.gameObject.SetActive(true);
        shop.SetActive(true);
        dark.SetActive(true);
        text.text = GameObject.Find("Player").GetComponent<DayManager>().money + " C";
        shopMusic.Play();
        streetMusic.Stop();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shop.SetActive(false);
            dark.SetActive(false);
            text.gameObject.SetActive(false);
            shopMusic.Stop();
            streetMusic.Play();
        }
    }
    public void buyGun()
    {
        if (!purchaseM(200)) return;
        GameObject.Find("Player").GetComponent<DayManager>().gun = true;
        GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You now can press SPACE to shoot");
    }
    public bool purchaseM(int money)
    {
        if (GameObject.Find("Player").GetComponent<DayManager>().money - money >= 0)
        {
            GameObject.Find("Player").GetComponent<DayManager>().money -= money;
            text.text = GameObject.Find("Player").GetComponent<DayManager>().money + " C";
            //sound
            buy.Play();
            return true;
        }else
        {
            cBuy.Play();
            //sound
            return false;
        }
    }
    public void buyKnife()
    {
        if (!purchaseM(120)) return;
        GameObject.Find("Player").GetComponent<DayManager>().knife = true;
        GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You now can press SPACE to stab people");
    }
    public void buyMask()
    {
        if (!purchaseM(20)) return;
        if (GameObject.Find("Player").GetComponent<DayManager>().mask == 0) GameObject.Find("Player").GetComponent<DayManager>().mask = 2;
        else GameObject.Find("Player").GetComponent<DayManager>().mask++;
        GameObject.Find("Player").transform.GetChild(1).localScale = new Vector3(0.5f,0.5f,0.5f);
        GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "Because of the mask your infection circle narrow down for 1 day");
    }
    public void buyGrocery()
    {
        if (!purchaseM(50)) return;
        GameObject.Find("Player").GetComponent<DayManager>().food++;
        GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "Now you can go home and cook some food in the kitchen");
    }
    public void buyPill()
    {
        if (!purchaseM(100)) return;
        GameObject.Find("Player").GetComponent<PlayerMovement>().infected = false;
        GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "You are cured. But you can still get infected.");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            shop.SetActive(false);
            dark.SetActive(false);
            text.gameObject.SetActive(false);
            shopMusic.Stop();
            streetMusic.Play();
        }
    }
}
