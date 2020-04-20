using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTrigger : MonoBehaviour
{
    public TextBehaviour text;
    
    void Start()
    {
        StartCoroutine(WaitForLoading());
    }

    IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(1f);
        TriggerText();
    }
    public void TriggerText()
    {
        if (SceneManager.GetActiveScene().name == "DowntownScene") if (GameObject.Find("Player").GetComponent<DayManager>().day >= 4)
        {
            if (GameObject.Find("Player").GetComponent<DayManager>().gun)
            {
                GameObject.FindObjectOfType<TransitionStart>().dayStatus = DayStatus.downtownwgun;

            }
            else
            {
                GameObject.FindObjectOfType<TransitionStart>().dayStatus = DayStatus.downtownnogun;
            }
                GameObject.FindObjectOfType<TransitionStart>().reload();
        }
        FindObjectOfType<TextController>().StartText(text);
    }
}
