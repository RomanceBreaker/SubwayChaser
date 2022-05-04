using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    private void Update()
    {

        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
        //fire = Input.GetButton("Fire1");
        //reload = Input.GetButtonDown("Fire3");
    }
}
