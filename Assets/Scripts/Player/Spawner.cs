using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Note note;
    public Vector3 startPos;            // 처음 시작 시 spawner position
    public Vector3 currPos;             // update되는 spawner의 random position

    public Transform hitTrans;
    public Transform playerTrans;
    public Transform noteParent;        // 하이어라키창에 clone된 note들을 관리하는 parent

    private bool canShoot;

    void Start()
    {
        startPos = transform.localPosition;     // 0, 6, 20 고정
        canShoot = true;
    }

    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(ShootNote());
        }
    }

    IEnumerator ShootNote()
    {
        canShoot = false;
        currPos = startPos + new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);

        Note noteTemp = Instantiate(note, noteParent);
        noteTemp.Init(currPos, hitTrans.localPosition, playerTrans.eulerAngles);
        
        yield return new WaitForSeconds(1.0f);      // 1초마다 생성
        canShoot = true;
    }
}