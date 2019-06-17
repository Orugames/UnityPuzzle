using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LobbyUIController : MonoBehaviour
{
    public RectTransform mainMenuPanel;
    public RectTransform LevelSelectionPanel;

    public bool tweenFinished = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MovePanels(int selection)
    {
        if (!tweenFinished) return;
        tweenFinished = false;
        switch (selection)
        {
            case 0:
                mainMenuPanel.DOAnchorPos(new Vector2(0, 0), 0.5f).OnComplete(() => tweenFinished = true);
                LevelSelectionPanel.DOAnchorPos(new Vector2(800, 0), 0.5f);
                break;
            case 1:
                mainMenuPanel.DOAnchorPos(new Vector2(-800, 0), 0.5f);
                LevelSelectionPanel.DOAnchorPos(new Vector2(0, 0), 0.5f).OnComplete(() => tweenFinished = true); ;
                break;
        }
    }
}
