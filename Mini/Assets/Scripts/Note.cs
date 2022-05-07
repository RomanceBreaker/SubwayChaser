using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Note : MonoBehaviour
{
    public float gravity = 9.8f;
    public float firingAngle = 45.0f;
    private float noteSpeed = 3.0f;

    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 angle;

    private RaycastHit hit;

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

        // Calculate distance to target
        float target_Distance = Vector3.Distance(startPos, endPos);
        //Debug.Log("target_Distance : " + target_Distance);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        //Debug.Log("projectile_Velocity : " + projectile_Velocity);


        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        //Debug.Log("Vx : " + Vx);
        //Debug.Log("Vy : " + Vy);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
        //Debug.Log("flightDuration : " + flightDuration);

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(endPos - startPos);
        transform.eulerAngles += angle;

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime * noteSpeed, Vx * Time.deltaTime * noteSpeed);
            //Debug.Log("pos : " + transform.position);

            elapse_time += Time.deltaTime * noteSpeed;

            yield return null;
        }

        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("ii");

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("hi");
    //        this.gameObject.SetActive(false);
    //    }
    //}
}
