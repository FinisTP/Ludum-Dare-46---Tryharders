using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    Animator anim;
    public Vector3 target, direction;
    public float speed = 0.5f;
    float timeStop = 0;
    float timeSneeze = 0;

    public bool neighbor = false; //Cho outdoor

    public bool pedestrian = false; //Cho  puzzle 1
    public Puzzle1 puzzle1;
    public bool infected = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        timeStop = Random.Range(3.0f, 8.0f); 
    }
    void setDirection()
    {
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
        anim.SetFloat("OldHorizontal", direction.x);
        anim.SetFloat("OldVertical", direction.y);
    }
    void Update()
    {
        timeStop -= Time.deltaTime;
        if (infected)
        {
            timeSneeze += Time.deltaTime;
            if(timeSneeze > 3)
            {
                timeSneeze = 0;
                anim.SetTrigger("punch"); 
                GameObject player = GameObject.Find("Player");
                if(gameObject.GetComponent<EdgeCollider2D>().bounds.Contains(player.transform.position)) player.GetComponent<PlayerMovement>().infected = true;
            }
        }
        if ((pedestrian && timeStop < 0) || neighbor)
        {
            if(timeStop < -3) timeStop = Random.Range(8.0f, 16.0f);
            GameObject look=null;
            float min = 100;
            GameObject[] list = GameObject.FindGameObjectsWithTag("Entity");
            for(int i = 0; i < list.Length; i++) if(Vector2.Distance(list[i].transform.position,transform.position) < min && list[i] != gameObject)
                {
                    min = Vector2.Distance(list[i].transform.position, transform.position);
                    look = list[i];
                }
            anim.SetBool("playerWalk", false);
            direction = -transform.position + look.transform.position;
            setDirection();
            return;
        }
        direction = -transform.position + target;
        setDirection();
        if (Vector2.Distance(target, transform.position) > 0.1) anim.SetBool("playerWalk", true);
        else
        {
            anim.SetBool("playerWalk", false);
            if (pedestrian)
            {
                target = puzzle1.transform.position + new Vector3(Random.Range(-puzzle1.rangeX, puzzle1.rangeX), Random.Range(-puzzle1.rangeY, puzzle1.rangeY), 0);
            }
        }
        this.gameObject.transform.position += direction.normalized * Time.deltaTime *speed;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 10 + 100);
        
    }
}
