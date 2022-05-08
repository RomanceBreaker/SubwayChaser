using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_S : MonoBehaviour
{
    bool start_window;
    bool start_window2;
    public float start_time;
    public float start_time2;
    void Start()
    {
        start_window = true;
        start_window2 = true;
        StartCoroutine("Start_Window", start_time);
        StartCoroutine("Start_Window2", start_time2);
    }


    void Update()
    {
        if (start_window)
        {
            GameObject.Find("Train").transform.position += Vector3.forward/2;
        }
        if (start_window2)
        {
            GameObject.Find("Player").transform.position -= Vector3.right / 5;
        }

        if (GameObject.Find("Train").transform.position.z > GameObject.Find("Player").transform.position.z)
        {
            GameObject.Find("Main Camera").transform.LookAt(GameObject.Find("Train").transform);
        }

    }

    IEnumerator Start_Window(float start_time)
    {
        yield return new WaitForSeconds(start_time);
        start_window = false;
        //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator Start_Window2(float start_time2)
    {
        yield return new WaitForSeconds(start_time2);
        start_window2 = false;
        //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }

   
}
