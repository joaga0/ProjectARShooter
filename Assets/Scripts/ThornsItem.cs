using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsItem : MonoBehaviour
{
    float destructionDelay = 5.0f;

    void Start()    //thorns 객체 생성
    {
        Destroy(gameObject, destructionDelay);
    }

    void Update()
    {
        
    }
}
