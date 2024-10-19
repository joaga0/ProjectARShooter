using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineItem : MonoBehaviour
{
    float destructionDelay = 5.0f;
    public GameObject bomb_obj;   //생성할 폭발효과
    GameObject effect;
    GameObject obj;
    Vector3 position;

    bool set;
    int count;

    void Start()    //mine 객체 생성
    {
        set = true;
        count = 0;

        obj = GameObject.Find("ItemSlot");
        position = obj.GetComponent<ItemSpawn>().worldPosition;
        position.y += 2;
        InvokeRepeating("blink_02", 3.5f, 0.2f);
        Invoke("bomb", destructionDelay);   //methodName 함수를 time 뒤에 실행한다.
        Destroy(gameObject, destructionDelay);
    }
    
    void bomb()
    {
        effect = Instantiate(bomb_obj, position, Quaternion.identity);
        Destroy(effect, 2.0f);
    }

    void blink_02()
    {
        if(set)
        {
            gameObject.SetActive(false);
            set = false;
        }
        else
        {
            gameObject.SetActive(true);
            set = true;
        }
        count++;
    }

    void blink_01()
    {
        if(set)
        {
            gameObject.SetActive(false);
            set = false;
        }
        else
        {
            gameObject.SetActive(true);
            set = true;
        }
        count++;
    }

    void Update()
    {
        if (count > 5)
        {
            CancelInvoke("blink_02");
            InvokeRepeating("blink_01", 0.0f, 0.15f);
        }
    }
}
