using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject note;
    public Transform spawnerPos;

    void Start()
    {
        
    }

    void Update()
    {
        Instantiate(note, spawnerPos.transform.position, spawnerPos.transform.rotation);
    }
}
