using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 2f;
    public bool isJumping = false;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();     // 항상 앞으로 이동

        if (Input.GetKeyDown(KeyCode.Z))    // 왼쪽 회전
        {
            RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.C))    // 오른쪽 회전
        {
            RotateRight();
        }
        if (Input.GetMouseButtonDown(1))    // 점프 (마우스 우클릭)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))    // 칼 휘둘러서 노트 맞춤 (마우스 좌클릭)
        {
            Debug.Log("brandish");
            Brandish();
        }
    }

    private void Move()
    {
        rigid.MovePosition(rigid.position + transform.forward * moveSpeed);
    }
    private void RotateLeft()
    {
        rigid.rotation = rigid.rotation * Quaternion.Euler(0f, -90.0f, 0f);
    }

    private void RotateRight()
    {
        rigid.rotation = rigid.rotation * Quaternion.Euler(0f, 90.0f, 0f);
    }

    private void Jump()
    {
        // 점프 몇 번까지 가능하게 할 지?

        if (!isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

    }
    private void Brandish()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}