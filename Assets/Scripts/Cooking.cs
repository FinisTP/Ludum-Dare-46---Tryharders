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
    void Start()
    {
        loc = pointer.GetComponent<RectTransform>().anchoredPosition;
    }
    
    void Update()
    {
        pointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(pointer.GetComponent<RectTransform>().anchoredPosition.x,-100+point);
        if(pointer.GetComponent<RectTransform>().anchoredPosition.y > 100)
        {
            pointer.GetComponent<RectTransform>().anchoredPosition = loc;
            point = 0;
        }
        i += Time.deltaTime;
        barfill.fillAmount = Mathf.Cos(2*i)/2+0.5f;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(pointer.GetComponent<RectTransform>().anchoredPosition.y);
            if (Mathf.Abs((barfill.fillAmount - 0.5f) * 200 - (pointer.GetComponent<RectTransform>().anchoredPosition.y)) < 40) point += 35;
            else if (point - 20 >= 0) point -= 20;
        }
    }
}
