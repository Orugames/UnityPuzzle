using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class VerticalModifiersButtonLogic : MonoBehaviour
{
    public bool expanded;
    public bool transitioning;
    RectTransform thisRectTrans;

    void Start()
    {
        thisRectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveVerticalPanel()
    {
        if (transitioning)
        {
            return;
        }
        transitioning = true;

        if (expanded)
        {
            thisRectTrans.DOAnchorPos3DY(-25, 0.5f).OnComplete(() => {
                expanded = false;
                transitioning = false;
            });
        }
        else
        {
            thisRectTrans.DOAnchorPos3DY(-160, 0.5f).OnComplete(() => {
                expanded = true;
                transitioning = false;
            });
        }
    }
}
