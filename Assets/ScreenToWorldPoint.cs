using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorldPoint : MonoBehaviour
{
    GameObject plane;
    GameObject cube;

    BoxCollider boxCollider;
    Vector3 worldPosition;
    Ray ray;

    void Start()
    {
        plane = GameObject.Find("Gound");
        cube = GameObject.Find("Cube");

        boxCollider = plane.GetComponent<BoxCollider>();
    }

    void Update()
    {
        ScreenToWorld();
        cube.transform.position = worldPosition;
    }

    void ScreenToWorld()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(boxCollider.Raycast(ray, out hit, 1000))
        {
            worldPosition = hit.point;
        }

        Debug.Log(worldPosition);
    }
}
