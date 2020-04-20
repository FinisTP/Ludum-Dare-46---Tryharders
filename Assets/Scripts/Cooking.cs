using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooking : MonoBehaviour
{
    public Image barfill;
    public GameObject pointer;
    public Vector2 loc;
    public float i = 0;
    public float point = 0;
    public int score = 0;
    public Animator anim;
    public AudioSource win, lose;
    public Text text;
    void Start()
    {
        loc = pointer.GetComponent<RectTransform>().anchoredPosition;
    }
    
    void Update()
    {
        text.text = score + "/5";
        pointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(pointer.GetComponent<RectTransform>().anchoredPosition.x,Mathf.Lerp(pointer.GetComponent<RectTransform>().anchoredPosition.y,- 100+point,Time.deltaTime));
        if (i > 15)
        {
            anim.SetTrigger("flipfail");
            pointer.GetComponent<RectTransform>().anchoredPosition = loc;
            point = 0;
            i = 0;
            lose.Play();
            return;
        }
        if (pointer.GetComponent<RectTransform>().anchoredPosition.y > 100)
        {
            pointer.GetComponent<RectTransform>().anchoredPosition = loc;
            point = 0;
            anim.SetTrigger("flip");
            win.Play();
            score++;
            if (score >= 5)
            {
                GameObject.Find("Player").GetComponent<DayManager>().actualFood++;
                GameObject.FindObjectOfType<DialogueManager>().sendMessage("", "Your wife and son thankyou for the meal");
                transform.parent.gameObject.SetActive(false);
            }
            i = 0;
        }
        i += Time.deltaTime;
        barfill.fillAmount = Mathf.Cos(2*i)/2+0.5f;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(pointer.GetComponent<RectTransform>().anchoredPosition.y);
            if (Mathf.Abs((barfill.fillAmount - 0.5f) * 200 - (pointer.GetComponent<RectTransform>().anchoredPosition.y)) < 40) point += 50;
            else if (point - 20 >= 0) point -= 20;
        }
    }
}
