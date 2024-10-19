using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct spawnInfo
{
    public float time;
    public GameObject[] Point1Monsters;    //spawnPoint1에 소환될 몬스터 리스트
    public GameObject[] Point2Monsters;    //spawnPoint2에 소환될 몬스터 리스트
    public GameObject[] Point3Monsters;    //spawnPoint3에 소환될 몬스터 리스트
}

public class SpawnManager : MonoBehaviour
{
    public float time;
    public Vector3[] spawnPoint;
    public spawnInfo[] StageData;
    int count;

    void Start()
    {
        time = 0.0f;
        count = StageData.Length;
        StartCoroutine(timer());
    }

    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            if(time == StageData[i].time)
            {
                StartCoroutine(spawn(StageData[i]));
                time += 1.0f;
            }
        }
    }

    IEnumerator timer()
    {
        while(true)
        {
            time += 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator spawn(spawnInfo stage)
    {
        while(true)
        {
            StartCoroutine(spawn0(stage));
            StartCoroutine(spawn1(stage));
            StartCoroutine(spawn2(stage));
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator spawn0(spawnInfo stage)
    {
        int i = 0;
        while(true)
        {
            if (i == stage.Point1Monsters.Length)
                yield break;
            Instantiate(stage.Point1Monsters[i++], spawnPoint[0], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator spawn1(spawnInfo stage)
    {
        int i = 0;
        while(true)
        {
            if (i == stage.Point2Monsters.Length)
                yield break;
            Instantiate(stage.Point2Monsters[i++], spawnPoint[1], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator spawn2(spawnInfo stage)
    {
        int i = 0;
        while(true)
        {
            if (i == stage.Point3Monsters.Length)
                yield break;
            Instantiate(stage.Point3Monsters[i++], spawnPoint[2], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
