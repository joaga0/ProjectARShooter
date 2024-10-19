using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    static public InGameManager Instance;
    private void Start()
    {
        if(InGameManager.Instance == null)
        {
            InGameManager.Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] public GameObject tower;
}
