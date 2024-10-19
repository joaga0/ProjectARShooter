using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour
{
    public PanelHandler popupWindow;

    void Start()
    {
        if (popupWindow == null)
        {
            popupWindow = FindObjectOfType<PanelHandler>(); // 씬에 있는 PanelHandler를 찾아서 할당
            if (popupWindow == null)
            {
                Debug.LogError("씬에서 PanelHandler를 찾을 수 없습니다.");
            }
        }
    }

    public void OnButtonClick()
    {
        if (popupWindow == null)
        {
            Debug.LogError("popupWindow가 설정되지 않았습니다.");
            return;
        }

        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.1f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Show();
        });
    }
}
