using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThiefBehaviour : MonoBehaviour
{
    public GameObject movingPositionsHolder;

    public List<Transform> movingPositions;
    public float waitTime = 4f;
    public float time = 0;
    public int i = 0;
    GameObject collider;
    bool shoot = false;
    public Sprite die;
    void Start()
    {
        collider = transform.GetChild(0).gameObject;
        movingPositions = new List<Transform>(movingPositionsHolder.GetComponentsInChildren<Transform>());
        movingPositions.Remove(movingPositionsHolder.transform);
    }

    private void Update()
    {
        if (collider.GetComponent<PolygonCollider2D>().bounds.Contains(GameObject.Find("Player").transform.position))
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, GameObject.Find("Player").transform.position);
            if (hit.collider == null || (hit.collider.tag != "Object" && hit.collider.gameObject.name != "circle"))
            {
                if (shoot) return;
                else shoot = true;
                gameObject.GetComponent<Animator>().SetTrigger("picking");
                gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(died());
            }
        }
        Transform mp = movingPositions[i];
        Vector2 dir =- movingPositions[i].transform.position + gameObject.transform.position;
        dir.Normalize();
        collider.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(dir.y,dir.x)*180/Mathf.PI-90);
        if(Vector2.Distance(movingPositions[i].transform.position, gameObject.transform.position) < 0.1f)
        {
            if (time > waitTime)
            {
                if (i < movingPositions.Count) i++;
                else i = 0;
            }
            time += Time.deltaTime;
            return;
        }
        gameObject.GetComponent<BotController>().target = new Vector3(mp.transform.position.x, mp.transform.position.y, 0);
    }
    IEnumerator died()
    {
        GameObject.Find("Player").GetComponent<Animator>().runtimeAnimatorController = null;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = die;
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(3f);
        Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene("Ending01");
    }
}
