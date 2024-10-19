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
            popupWindow = FindObjectOfType<PanelHandler>(); // ���� �ִ� PanelHandler�� ã�Ƽ� �Ҵ�
            if (popupWindow == null)
            {
                Debug.LogError("������ PanelHandler�� ã�� �� �����ϴ�.");
            }
        }
    }

    public void OnButtonClick()
    {
        if (popupWindow == null)
        {
            Debug.LogError("popupWindow�� �������� �ʾҽ��ϴ�.");
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
