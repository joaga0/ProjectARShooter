using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneItem : MonoBehaviour
{
    float destructionDelay = 3.0f;
    GameObject obj;
    GameObject effect;
    Vector3 position;
    public GameObject explosion_obj;   //생성할 폭발효과

    void Start()    //Stone 객체 생성
    {
        Rigidbody rb;
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 speed = new Vector3(0, 10, 0);

        obj = GameObject.Find("ItemSlot");
        position = obj.GetComponent<ItemSpawn>().worldPosition;
        speed.x += (position.x - cameraPosition.x) * 40f;
        speed.z += (position.z - cameraPosition.z) * 40f;

        gameObject.transform.position = cameraPosition;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.AddForce(speed);

        Destroy(gameObject, destructionDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        Invoke("explosion", 0.0f);
        Destroy(gameObject, 0.05f);
    }

    void explosion()
    {
        position = gameObject.transform.position;
        effect = Instantiate(explosion_obj, position, Quaternion.identity);
        Destroy(effect, 2.0f);
    }

    void Update()
    {
        
    }
}
