using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_S : MonoBehaviour
{
    List<GameObject> Ground_List = new List<GameObject>(); //땅 List, 땅 지우기 용도
    public GameObject[] Ground_Normal;
    public GameObject[] Ground_Turn;
    Quaternion Map_Q;
    bool create;
    int remove_index;

    void Start()
    {
        remove_index = 0;
        Map_Q = Quaternion.Euler(new Vector3(0,0,0));
        Ground_List.Add(GameObject.Find("Start_Zone").gameObject);
        create = true;
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine("Ground_Create", 0f);
        }
    }

    void Update()
    {
        Create_Ground();
        //Remove_Ground();
        

    }

    void Create_Ground()
    {
        if (create)
        {
            StartCoroutine("Ground_Create", 1.5f);
            create = false;
        }
    }

    void Remove_Ground()
    {
        if (Ground_List.Count >= 10)
        {
            Ground_List.RemoveAt(0);
            Destroy(Ground_List[remove_index].gameObject);
            remove_index++;
        }
        
    }

    IEnumerator Ground_Create(float create_time) //땅 랜덤 생성 코루틴
    {
        Debug.Log(Ground_List.Count);
        yield return new WaitForSeconds(create_time);
        int Straight_or_Turn = Random.Range(0,5);
        if(Straight_or_Turn < 3) //직진 붙이기
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
                //int k = (Ground_List[Ground_List.Count - 1] == GameObject.Find("Map_Go_Right").gameObject ? 2 : 0);
                Ground_List.Add(
                Instantiate(Ground_Normal[Ground_index],
                new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x + Ground_Normal[Ground_index].transform.GetChild(0).GetComponent<BoxCollider>().size.z, 0,
                Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z),
                Map_Q));
            }

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

                    Map_Q = Quaternion.Euler(new Vector3(0, -90, 0));
                }
                else if (Turn_index == 1) //Go_Right
                {
                    Debug.Log("Go_RIght");

                    Ground_List.Add(Instantiate(Ground_Turn[2],
                        new Vector3(Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.x, 0,
                        Ground_List[Ground_List.Count - 1].transform.GetChild(0).position.z + Ground_Turn[2].transform.GetChild(0).GetComponent<BoxCollider>().size.z)
                        , Map_Q));

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

                Map_Q = Quaternion.Euler(new Vector3(0, 0, 0));
            }

        }

        create = true;
    }

}
