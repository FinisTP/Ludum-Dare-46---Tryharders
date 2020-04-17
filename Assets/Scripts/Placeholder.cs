using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{

    public GameObject character;

    private Transform tf;
    // Start is called before the first frame update

    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A)) character.SetActive(!character.activeSelf);

        tf.Translate(Vector3.forward * 10 * Time.deltaTime);

    }
}
