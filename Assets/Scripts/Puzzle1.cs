using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    float time = 0;
    public float spawnTime = 2.5f;
    public float rangeX = 20, rangeY = 3;
    public GameObject bot;
    public int defaultSpawn = 10;
    public int defaultSpawnInfected = 5;
    public RuntimeAnimatorController a1, a2;
    void Start()
    {
        spawn(); //Spawn truoc mot so nguoi di duong
    }
    void spawn()
    {
        for (int i = 0; i < defaultSpawn+defaultSpawnInfected; i++)
        {
            GameObject b = Instantiate(bot, transform);
            b.GetComponent<Animator>().runtimeAnimatorController = (Random.Range(0, 10) > 5 ? a1 : a2);
            b.transform.position = transform.position + new Vector3(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0);
            b.GetComponent<BotController>().target = transform.position + new Vector3(Random.Range(0, 10) > 5 ? -rangeX : rangeX, Random.Range(-rangeY, rangeY));
            b.GetComponent<BotController>().pedestrian = true;
            b.GetComponent<BotController>().puzzle1 = this;
            if (i >= defaultSpawn) b.GetComponent<BotController>().infected = true;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(rangeX * 2, rangeY * 2, 0));
    }
}
