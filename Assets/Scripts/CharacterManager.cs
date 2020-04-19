using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject main;
    void Start()
    {
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
