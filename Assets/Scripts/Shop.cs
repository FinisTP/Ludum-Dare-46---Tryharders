using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public GameObject shop, dark;
    public Text text;
    public AudioSource shopMusic, buy, cBuy, streetMusic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            shop.SetActive(true);
            dark.SetActive(true);
            text.gameObject.SetActive(true);
            text.text = collision.gameObject.GetComponent<DayManager>().money + " C";
            shopMusic.Play();
            streetMusic.Stop();
        }
    }
    public void buyGun()
    {
        if (!purchaseM(200)) return;
        GameObject.Find("Player").GetComponent<DayManager>().gun = true;
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
    }
    public void buyMask()
    {
        if (!purchaseM(20)) return;
        if (GameObject.Find("Player").GetComponent<DayManager>().mask == 0) GameObject.Find("Player").GetComponent<DayManager>().mask = 2;
        else GameObject.Find("Player").GetComponent<DayManager>().mask++;
        GameObject.Find("Player").transform.GetChild(1).localScale = new Vector3(0.5f,0.5f,0.5f);
    }
    public void buyGrocery()
    {
        if (!purchaseM(50)) return;
        GameObject.Find("Player").GetComponent<DayManager>().food++;
    }
    public void buyPill()
    {
        if (!purchaseM(100)) return;
        GameObject.Find("Player").GetComponent<PlayerMovement>().infected = false;
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
