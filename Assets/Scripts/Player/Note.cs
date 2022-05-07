using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Note : MonoBehaviour
{
    public float gravity = 9.8f;
    public float firingAngle = 45.0f;
    private float noteSpeed = 0.5f;

    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 angle;

    void Start()
    {
        StartCoroutine(SimulateProjectile());
    }

    public void Init(Vector3 pos, Vector3 endPos, Vector3 angle)
    {
        transform.localPosition = pos;
        startPos = pos;
        this.endPos = endPos;
        this.angle = angle;
    }

    IEnumerator SimulateProjectile()
    {
        //yield return new WaitForSeconds(0.0f);

        float targetDistance = Vector3.Distance(startPos, endPos);
        float projectileVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = targetDistance / Vx;

        transform.rotation = Quaternion.LookRotation(endPos - startPos);
        transform.eulerAngles += angle;

        float elapseTime = 0;
        while (elapseTime < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapseTime)) * Time.deltaTime * noteSpeed, Vx * Time.deltaTime * noteSpeed);

            elapseTime += Time.deltaTime * noteSpeed;

            yield return null;
        }

        Destroy(this.gameObject);
    }

    public void RayCasting(Ray ray)
    {
        RaycastHit hitObj;

        if (Physics.Raycast(ray, out hitObj, Mathf.Infinity))
        {
            Debug.Log("bamm!");
            Destroy(hitObj.collider.gameObject);

        }
    }
}