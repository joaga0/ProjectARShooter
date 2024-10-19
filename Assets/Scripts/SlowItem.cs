using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowItem : MonoBehaviour
{
    float destructionDelay = 5.0f;

    void Start()    //slow 객체 생성
    {
        Destroy(gameObject, destructionDelay);
    }

    void Update()
    {
    }
}
