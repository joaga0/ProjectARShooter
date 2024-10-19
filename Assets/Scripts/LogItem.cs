using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogItem : MonoBehaviour
{
    float destructionDelay = 5.0f;

    void Start()    //Log 객체 생성
    {
        Rigidbody rb;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // 회전을 막아서 통나무가 평면을 따라 움직이도록 설정
        rb.AddForce(Vector3.forward * 500f); // 통나무에 앞으로 가속도를 적용하여 움직이도록 함

        Destroy(gameObject, destructionDelay);
    }

    void Update()
    {
        
    }
}
