using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void press()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
