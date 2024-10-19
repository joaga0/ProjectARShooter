using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe : MonoBehaviour
{
    public Color[] colors;
    public GameObject scrollbar, imageContent;
    private float scroll_pos = 0;
    float[] pos;

    void Start()
    {
        // 초기 설정을 여기서 수행할 수 있습니다.
    }

    void Update()
    {
        int childCount = transform.childCount;
        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < childCount; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < childCount; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                Debug.LogWarning("Current Selected Level" + i);
                if (i < imageContent.transform.childCount && colors.Length > 1)
                {
                    imageContent.transform.GetChild(i).GetComponent<Image>().color = colors[1];
                }

                for (int j = 0; j < childCount; j++)
                {
                    if (j != i && j < imageContent.transform.childCount && colors.Length > 0)
                    {
                        imageContent.transform.GetChild(j).GetComponent<Image>().color = colors[0];
                    }
                }
            }
        }
    }
}
