using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    //public Transform Camera_Pos;
    public float moveSpeed;
    private Rigidbody charRigidbody;
    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
       // Camera_Pos.position  = this.transform.position;

        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        charRigidbody.velocity = inputDir * moveSpeed;
    }
}
