using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 300f;

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
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
            Brandish();
        }
    }

    private void Move()
    {
        Vector3 moveDistance = transform.forward * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        //playerAnimator.SetBool("isRunning", moveDistance != Vector3.zero);
    }

    private void RotateLeft()
    {
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, -90, 0f);
    }

    private void RotateRight()
    {
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, 90, 0f);
    }
    private void Jump()
    {
        // 이중 점프 안되게 하기
        Debug.Log("jump");
        playerRigidbody.AddForce(new Vector3(0, 1f, 0) * jumpPower);
    }
    private void Brandish()
    {

    }
}