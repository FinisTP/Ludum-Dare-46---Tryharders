using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    Animator anim;

    // Thong tin nguoi choi
    public bool infected = false;
    float timeSneeze = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movementCheck();
        interactCheck();
        if (infected)
        {
            timeSneeze += Time.deltaTime;
            if (timeSneeze > 3)
            {
                timeSneeze = 0;
                anim.SetTrigger("punch");
                GameObject[] list = GameObject.FindGameObjectsWithTag("Entity");
                for (int i = 0; i < list.Length; i++) if (list[i] != gameObject && gameObject.GetComponent<EdgeCollider2D>().bounds.Contains(list[i].transform.position)) list[i].GetComponent<BotController>().infected = true;
            }
        }
    }
    void interactCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("punch");
        }
    }
    void movementCheck()
    {
        float moveHoriontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHoriontal != 0 || moveVertical != 0)
        {
            anim.SetBool("playerWalk", true);
            int x = 0, y = 0;
            if (moveHoriontal > 0) x = 1; else if (moveHoriontal < 0) x = -1;
            if (moveVertical > 0) y = 1; else if (moveVertical < 0) y = -1;
            anim.SetFloat("OldHorizontal", x);
            anim.SetFloat("OldVertical", y);
            //Sorting order
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 10 + 100);
        }
        else anim.SetBool("playerWalk", false);
        transform.position += new Vector3(moveHoriontal * speed * Time.deltaTime, moveVertical * speed * Time.deltaTime, 0);
        anim.SetFloat("Horizontal", moveHoriontal);
        anim.SetFloat("Vertical", moveVertical);
    }
}
