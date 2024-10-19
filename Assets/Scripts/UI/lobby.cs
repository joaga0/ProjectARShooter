using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for touch input on mobile devices
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Load the next scene
            LoadNextScene();
        }

        // Also check for mouse click for testing in the editor
        if (Input.GetMouseButtonDown(0))
        {
            // Load the next scene
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Assuming the next scene is at index 1 in the Build Settings
        SceneManager.LoadScene("lobby");
    }
}
