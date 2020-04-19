using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject main;
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
    }

    void Update()
    {
        
    }
}
