using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoard : MonoBehaviour
{
    [Header("Obj")]
    [SerializeField] GameObject hideCude;
    [SerializeField] GameObject initSpawnPos;
    [SerializeField] GameObject boardForSpawn;
    [SerializeField] GameObject boardForGame;
    [SerializeField] Camera cam;
    [Header("Value")]
    [SerializeField] float boardHeigt;
    [SerializeField] float spawnTime;
    [SerializeField] float delayTime;

    bool isSpawning = false;

    private void Start()
    {
        isSpawning = false;
        if (cam == null) cam = Camera.main; //AR적용시 확인필요 
        hideCude.SetActive(false);
        boardForSpawn.SetActive(false);
        boardForGame.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //For Debug
        {
            StartSpawnBoard(new Vector3(0,0,0));
        }
    }

    public void StartSpawnBoard(Vector3 Pos)
    {
        Vector3 camForward = cam.transform.forward;
        camForward = new Vector3 (camForward.x, 0, camForward.z);
        hideCude.transform.position = Pos;
        hideCude.transform.rotation = Quaternion.LookRotation(camForward);
        boardForSpawn.transform.position = initSpawnPos.transform.position;
        boardForSpawn.transform.rotation = initSpawnPos.transform.rotation;

        hideCude.SetActive(true);
        boardForSpawn.SetActive(true);
        StartCoroutine("MoveBoard");
    }

    private void FinishMove()
    {
        boardForGame.transform.position = boardForSpawn.transform.position;
        boardForGame.transform.rotation = boardForSpawn.transform.rotation;

        hideCude.SetActive(false);
        boardForSpawn.SetActive(false);
        boardForGame.SetActive(true);
        //add Game Start Fuction Here
    }
    private IEnumerator MoveBoard()
    {
        Vector3 startPos = initSpawnPos.transform.position;
        Vector3 endPos = hideCude.transform.position + new Vector3(0, boardHeigt,0);
        float elapsedTime = 0;

        while (elapsedTime < spawnTime)
        {
            boardForSpawn.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / spawnTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boardForSpawn.transform.position = endPos;
        yield return new WaitForSeconds(delayTime);
        FinishMove();
    }
}
