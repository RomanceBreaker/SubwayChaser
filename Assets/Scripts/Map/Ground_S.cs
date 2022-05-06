using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_S : MonoBehaviour
{
    List<GameObject> Ground_List = new List<GameObject>(); //땅 List, 땅 지우기 용도
    List<GameObject> Trap_List = new List<GameObject>(); //땅 List, 땅 지우기 용도
    public GameObject[] Ground_Normal;
    public GameObject[] Ground_Turn;
    public GameObject[] Ground_Trap;
    public GameObject Plane;
    Quaternion Map_Q;
    bool create;
    bool start_window;
    public float create_time;
    public float start_time;

    void Start()
    {
        Map_Q = Quaternion.Euler(new Vector3(0,0,0));
        Ground_List.Add(GameObject.Find("Start_Zone").gameObject);
        create = true;
        start_window = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine("Start_Window", start_time);

    }

    void Update()
    {

        if (start_window)
        {
            Create_Ground();
            Remove_Ground();
            Plane_Move();
        }
        else
        {
            GameObject.Find("Train").transform.position += Vector3.forward;
        }

    }

    void Create_Ground()
    {
        if (create)
        {
            StartCoroutine("Ground_Create", create_time);
            create = false;
        }
    }

    void Remove_Ground()
    {
        if (Ground_List.Count >= 10)
        {
            Ground_List.RemoveAt(0);
            Destroy(Ground_List[0].gameObject);

            Trap_List.RemoveAt(0);
            Destroy(Trap_List[0].gameObject);
        }
        
    }


    IEnumerator Start_Window(float start_time)
    {
        yield return new WaitForSeconds(start_time);
        start_window = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }


    IEnumerator Ground_Create(float create_time) //땅 랜덤 생성 코루틴
    {
   
        yield return new WaitForSeconds(create_time);
        int Straight_or_Turn = Random.Range(0,5);
        float trap_x = Random.Range(-2.5f, 2.5f);
        float trap_z = Random.Range(-2.5f, 2.5f);

        if (Straight_or_Turn < 3) //직진 붙이기
        {

            Debug.Log("Go");
            int Ground_index = Random.Range(0, Ground_Normal.Length);

            if (Map_Q == Quaternion.identity)
            {
                Ground_List.Add(
                    Instantiate(Ground_Normal[Ground_index],
                    new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x, 0,
                    Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z + Ground_Normal[Ground_index].transform.GetChild(0).GetComponent<BoxCollider>().size.z),
                    Map_Q));
            }
            else if (Map_Q == Quaternion.Euler(new Vector3(0, -90, 0)))
            {   
                Ground_List.Add(
                    Instantiate(Ground_Normal[Ground_index],
                    new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x - Ground_Normal[Ground_index].transform.GetChild(0).GetComponent<BoxCollider>().size.z, 0,
                    Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z),
                    Map_Q));


            }
            else if (Map_Q == Quaternion.Euler(new Vector3(0, 90, 0)))
            {
                Ground_List.Add(
                    Instantiate(Ground_Normal[Ground_index],
                    new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x + Ground_Normal[Ground_index].transform.GetChild(0).GetComponent<BoxCollider>().size.z, 0,
                    Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z),
                    Map_Q));
            }
            Trap_List.Add(
            Instantiate(Ground_Trap[Random.Range(0, 4)],
                                    new Vector3(Ground_List[Ground_List.Count - 1].transform.position.x + trap_x, Ground_List[Ground_List.Count - 1].transform.position.y, Ground_List[Ground_List.Count - 1].transform.position.z + trap_z), Map_Q)
            );
        }
        else
        {

            if (Map_Q == Quaternion.identity) //0,0,0 즉 Go인 상태
            {
                int Turn_index = Random.Range(0, 2);
                if (Turn_index == 0) //Go_Left
                {
                    Debug.Log("Go_Left");

                    Ground_List.Add(Instantiate(Ground_Turn[0],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x, 0,
                        Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z + Ground_Turn[0].transform.GetChild(0).GetComponent<BoxCollider>().size.z)
                        , Map_Q));

                    Trap_List.Add(
                    Instantiate(Ground_Trap[Random.Range(0, 4)],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.position.x + trap_x, Ground_List[Ground_List.Count - 1].transform.position.y, Ground_List[Ground_List.Count - 1].transform.position.z + trap_z), Map_Q)
                    );

                    Map_Q = Quaternion.Euler(new Vector3(0, -90, 0));
                }
                else if (Turn_index == 1) //Go_Right
                {
                    Debug.Log("Go_RIght");

                    Ground_List.Add(Instantiate(Ground_Turn[2],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x, 0,
                        Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z + Ground_Turn[2].transform.GetChild(0).GetComponent<BoxCollider>().size.z)
                        , Map_Q));

                    Trap_List.Add(
                    Instantiate(Ground_Trap[Random.Range(0, 4)],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.position.x + trap_x, Ground_List[Ground_List.Count - 1].transform.position.y, Ground_List[Ground_List.Count - 1].transform.position.z + trap_z), Map_Q)
                    );

                    Map_Q = Quaternion.Euler(new Vector3(0, 90, 0));
                }

            }

            else if (Map_Q == Quaternion.Euler(new Vector3(0, -90, 0))) //0,-90,0 즉, Left인 상태
            {
                Debug.Log("Left_RIght");
                Ground_List.Add(Instantiate(Ground_Turn[3],
                    new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).transform.position.x - Ground_Turn[3].transform.GetChild(0).GetComponent<BoxCollider>().size.z,
                    0,
                    Ground_List[Ground_List.Count - 1].transform.GetChild(0).transform.position.z)
                    , Map_Q));

                Trap_List.Add(
                Instantiate(Ground_Trap[Random.Range(0, 4)],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.position.x + trap_x, Ground_List[Ground_List.Count - 1].transform.position.y, Ground_List[Ground_List.Count - 1].transform.position.z + trap_z), Map_Q)
                );
                Map_Q = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            else if (Map_Q == Quaternion.Euler(new Vector3(0, 90, 0))) //0,-0,0 즉, Right인 상태
            {
                Debug.Log("Right_Left");

                Ground_List.Add(Instantiate(Ground_Turn[4],
                    new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).transform.position.x + Ground_Turn[4].transform.GetChild(0).GetComponent<BoxCollider>().size.z,
                    0,
                    Ground_List[Ground_List.Count - 1].transform.GetChild(0).transform.position.z)
                    , Map_Q));
                Trap_List.Add(
                Instantiate(Ground_Trap[Random.Range(0, 4)],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.position.x + trap_x, Ground_List[Ground_List.Count - 1].transform.position.y, Ground_List[Ground_List.Count - 1].transform.position.z + trap_z), Map_Q)
                );
                Map_Q = Quaternion.Euler(new Vector3(0, 0, 0));
            }

        }

        create = true;

    }

    void Plane_Move()
    {
        Plane.transform.position = new Vector3(GameObject.Find("Player").transform.position.x,  -1, GameObject.Find("Player").transform.position.z);
    }
}
