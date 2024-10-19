using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrow;

    // 화살을 보이게 함
    public void ShowArrow(bool b)
    {
        if (arrow != null)
        {
            arrow.SetActive(b);
        }
    }
}