using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    GameObject plane;   //보드
    GameObject spawn;   //생성할 아이템 오브젝트

    public GameObject[] drag;
    public GameObject[] selectedImage;  //선택한 아이템 진하게 표시
    public GameObject[] ItemObject; //생성할 오브젝트
    public bool[] selected;
    bool put;

    BoxCollider boxCollider;
    public Vector3 worldPosition;
    Ray ray;

    void Start()
    {
        put = false;
        selected = new bool[7];
        plane = GameObject.Find("Gound");

        boxCollider = plane.GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (ScreenToWorld())    //마우스 포인터 위치가 보드 위일때
        {
            if (Input.GetMouseButton(0))    //마우스 왼쪽 클릭하고있는 상태
            {
                for (int i = 0; i < 7; i++) //선택한 아이템의 오브젝트를 spawn에 할당
                {
                    if (selected[i] == true)
                    {
                        spawn = drag[i];
                        if (!put)
                        {
                            // spawn = Instantiate(ItemObject[i], worldPosition, Quaternion.identity);
                            put = true;
                        }
                        break;
                    }
                }
                if (spawn != null)
                {
                    spawn.transform.position = worldPosition;
                    spawn.SetActive(true);
                }
            }
            if (Input.GetMouseButtonUp(0))  //마우스 클릭 해제시
            {
                spawn.SetActive(false);
                for(int i = 0; i < 7; i++)
                {
                    if (selected[i] == true)
                    {
                        spawn = Instantiate(ItemObject[i], worldPosition, Quaternion.identity);
                    }
                }
                put = false;
                spawn.SetActive(true);
                spawn = null;
                Reset(-1);
            }
        }
    }

    void Reset(int s)
    {
        for (int i = 0; i < 7; i++)
        {
            selected[i] = false;
            selectedImage[i].SetActive(false);
        }
        if (s != -1)
        {
            selected[s] = true;
            selectedImage[s].SetActive(true);
        }
    }

    public void SlowItemClick() //슬로우
    {
        if (selected[0])
            Reset(-1);
        else
            Reset(0);
    }

    public void MineItemClick() //지뢰
    {
        if (selected[1])
            Reset(-1);
        else
            Reset(1);
    }

    public void ThornItemClick() //가시
    {
        if (selected[2])
            Reset(-1);
        else
            Reset(2);
    }

    public void LogItemClick() //통나무
    {
        if (selected[3])
            Reset(-1);
        else
            Reset(3);
    }

    public void StoneItemClick() //돌
    {
        if (selected[4])
            Reset(-1);
        else
            Reset(4);
    }

    public void PotionItemClick() //물약
    {
        //나중에 구현
    }

    public void BulletItemClick() //총알
    {
        //나중에 구현
    }

    bool ScreenToWorld()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(boxCollider.Raycast(ray, out hit, 1000))
        {
            worldPosition = hit.point;
            return (true);
        }
        else
            return (false);
    }
}
