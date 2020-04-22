using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    Animator anim;

    // Thong tin nguoi choi
    public bool infected = false;
    float timeSneeze = 0;

    public bool canMove = true;
    public Sprite dickson;
    public Sprite dummy1;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movementCheck();
        interactCheck();
        if(infected)
            GameObject.Find("circle").GetComponent<SpriteRenderer>().color = Color.red;
        else
            GameObject.Find("circle").GetComponent<SpriteRenderer>().color = Color.green;
        if (infected)
        {
            
            timeSneeze += Time.deltaTime;
            if (timeSneeze > 3)
            {
                timeSneeze = 0;
                anim.SetTrigger("punch");
                GameObject[] list = GameObject.FindGameObjectsWithTag("Entity");
                bool check = true;
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != gameObject && SceneManager.GetActiveScene().name == "DowntownScene" && gameObject.GetComponent<DayManager>().day >= 4 && !list[i].GetComponent<BotController>().infected) check = false;
                    if (list[i] != gameObject && gameObject.GetComponent<EdgeCollider2D>().bounds.Contains(list[i].transform.position))
                    {
                        list[i].GetComponent<BotController>().infected = true;
                        if (!gameObject.GetComponent<DayManager>().infectedName.Contains(list[i].name)) gameObject.GetComponent<DayManager>().infectedName.Add(list[i].name);
                    }
                }
                if (SceneManager.GetActiveScene().name == "DowntownScene" && gameObject.GetComponent<DayManager>().day >= 4 && check)
                {
                    SceneManager.LoadScene("Ending04");
                    Destroy(gameObject);
                }
            }
        }
    }
    int infectedN = 0;
    void interactCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<DayManager>().gun)
        {
            if (SceneManager.GetActiveScene().name == "DowntownScene")
            {
                anim.SetTrigger("picking");
                GameObject[] list = GameObject.FindGameObjectsWithTag("Entity");
                for (int i = 0; i < list.Length; i++) if (list[i].GetComponent<Animator>().runtimeAnimatorController!= null &&list[i] != gameObject && Vector2.Distance(transform.position, list[i].transform.position) < 5)
                    {
                        list[i].GetComponent<Animator>().runtimeAnimatorController = null;
                        list[i].GetComponent<SpriteRenderer>().sprite = dummy1;
                        list[i].transform.localScale *= new Vector2(Random.Range(0,10)>5?-1:1, 1);
                        Destroy(list[i].GetComponent<BotController>());
                        gameObject.GetComponent<AudioSource>().Play();
                        break;
                    }
                if (gameObject.GetComponent<DayManager>().day >= 4)
                {
                    List<GameObject> l = new List<GameObject>();
                    for (int i = 0; i < list.Length; i++) if (list[i].GetComponent<Animator>().runtimeAnimatorController != null) l.Add(list[i]);
                    if (l.Count <= 1)
                    {
                        if (!gameObject.GetComponent<DayManager>().wifeAlive) SceneManager.LoadScene("Ending03");
                        else SceneManager.LoadScene("Ending05");
                        Destroy(gameObject);
                    }
                }
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<DayManager>().knife)
        {
            anim.SetTrigger("attack");
            if(GameObject.Find("Dickson(Clone)") != null && Vector2.Distance(GameObject.Find("Dickson(Clone)").transform.position,transform.position) < 3)
            {
                GameObject.Find("Dickson(Clone)").GetComponent<Animator>().runtimeAnimatorController = null;
                GameObject.Find("Dickson(Clone)").GetComponent<SpriteRenderer>().sprite = dickson;
                Destroy(GameObject.Find("Dickson(Clone)").GetComponent<BotController>());
                Destroy(GameObject.Find("Dickson(Clone)").GetComponent<ThiefBehaviour>());
                gameObject.GetComponent<DayManager>().dicksondie = true;
            }
        }
    }
    void movementCheck()
    {
        if (!canMove) return;
        float moveHoriontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
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
        gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x+moveHoriontal*speed*Time.deltaTime, transform.position.y+ moveVertical*speed * Time.deltaTime));
        //transform.position += new Vector3(moveHoriontal * speed * Time.deltaTime, moveVertical * speed * Time.deltaTime, 0);
        anim.SetFloat("Horizontal", moveHoriontal);
        anim.SetFloat("Vertical", moveVertical);
    }
}
