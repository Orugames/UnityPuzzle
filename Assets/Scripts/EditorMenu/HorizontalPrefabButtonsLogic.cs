using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class HorizontalPrefabButtonsLogic : MonoBehaviour
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
    public void MoveHorizontalPanel()
    {
        if (transitioning)
        {
            return;
        }
        transitioning = true;

        if (expanded)
        {
            thisRectTrans.DOAnchorPos3DX(-20, 1).OnComplete(() =>{
                expanded = false;
                transitioning = false;
            });
        }
        else
        {
            thisRectTrans.DOAnchorPos3DX(-190, 1).OnComplete(() => {
                expanded = true;
                transitioning = false;
            });
        }
    }
}
