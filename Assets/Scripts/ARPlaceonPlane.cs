using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARPlaceonPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject field;
    public GameObject[] monsterPrefabs; 
    public Button restartButton; 
    private List<GameObject> spawnedMonsters = new List<GameObject>(); 
    private bool isfieldPlaced = false;     //필드의 생성 확인

    void Start()
    {
        field.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (!isfieldPlaced)
        {
            UpdateCenterObject();
        }
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            field.SetActive(true);
            field.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            isfieldPlaced = true; 

            SpawnMonster();   
        }
    }
    private void SpawnMonster()
    {
        
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogError("Monster prefabs are not assigned");
            return;
        }
        Vector3 fieldPosition = field.transform.position;

        for (int i = 0; i < 5; i++)
        {
            //랜덤한 위치
            Vector3 randomPos = new Vector3(fieldPosition.x + Random.Range(-0.25f, 0.25f), fieldPosition.y, fieldPosition.z + Random.Range(-0.25f, 0.25f));

            GameObject selectedMonsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject monster = Instantiate(selectedMonsterPrefab, randomPos, Quaternion.identity);
            spawnedMonsters.Add(monster);
        }  
    }

    //맵 초기화
    public void RestartGame()
    {
        if (field != null)
        {
            field.SetActive(false);
        }

        foreach (GameObject monster in spawnedMonsters)
        {
            Destroy(monster);
        }
        spawnedMonsters.Clear();

        UpdateCenterObject();
    }
}
