using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefBehaviour : MonoBehaviour
{
    public GameObject movingPositionsHolder;

    private List<Transform> movingPositions;
    private Transform[] temp;
    public float waitTime = 4f;
    void Start()
    {
        movingPositions = new List<Transform>();
        temp = movingPositionsHolder.GetComponentsInChildren<Transform>();
        foreach (Transform tf in temp)
        {
            movingPositions.Add(tf);
        }

        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        for (int i = 0; i < movingPositions.Count; ++i)
        {
            Transform mp = movingPositions[i];
            gameObject.GetComponent<BotController>().target = new Vector3(mp.transform.position.x, mp.transform.position.y, 0);
            while (Vector2.Distance(mp.transform.position, gameObject.transform.position) < 0.1f)
            {
                yield return null;
                
            }
            yield return new WaitForSeconds(waitTime);
            Debug.Log("asfjaosg");
        }
        
    }


}
