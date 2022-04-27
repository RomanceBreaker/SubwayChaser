using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(0, -1f, 1f));   
    }
}
