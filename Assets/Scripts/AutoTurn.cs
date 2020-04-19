using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurn : MonoBehaviour
{
    public GameObject[] list;
    void Start()
    {
        for (int i = 0; i < list.Length; i++) list[i].SetActive(true);
    }
}
