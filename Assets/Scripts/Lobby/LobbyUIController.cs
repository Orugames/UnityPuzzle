using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LobbyUIController : MonoBehaviour
{
    public RectTransform mainMenuPanel;
    public RectTransform LevelSelectionPanel;
    public RectTransform LevelSelectionTopPanel;
    public RectTransform LevelSelectionDownPanel;

    public RectTransform blackTransition;
    public RectTransform whiteTransition;
    public RectTransform blackTransition2;
    public RectTransform whiteTransition2;
    public RectTransform blackTransition3;
    public RectTransform whiteTransition3;
    public RectTransform blackTransition4;



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
                LevelSelectionPanel.DOAnchorPos(new Vector2(800, 0), 0.5f);
                //LevelSelectionTopPanel.DOAnchorPos(new Vector2(0, 144), 0.5f);
                LevelSelectionTopPanel.DOAnchorPos(new Vector2(0, 0), 0.25f).OnComplete(() =>
                {
                    blackTransition.DOScale(0, 0.4f).SetDelay(0.2f);
                    whiteTransition.DOScale(0, 0.4f).OnComplete(() =>
                    {
                        mainMenuPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
                        tweenFinished = true;

                    });


                });
                break;
            case 1:
                mainMenuPanel.DOAnchorPos(new Vector2(-800, 0), 0.5f);
                whiteTransition.DOScale(20, 1f).SetDelay(0.2f);
                blackTransition.DOScale(20, 1).OnComplete(() =>
                {
                    LevelSelectionTopPanel.DOAnchorPos(new Vector2(0, -200), 0.5f);
                    LevelSelectionPanel.DOAnchorPos(new Vector2(0, 0), 0.5f).SetDelay(0.5f).OnComplete(() => tweenFinished = true);

                });
                break;
            case 2:
                LevelSelectionTopPanel.DOAnchorPos(new Vector2(0, 144), 0.25f);
                LevelSelectionPanel.DOAnchorPos(new Vector2(-800, 0), 0.25f).OnComplete(() =>
                {
                    blackTransition2.DOScale(20, 1);
                    whiteTransition2.DOScale(20, 1f).SetDelay(0.2f);
                    blackTransition3.DOScale(20, 1.1f).SetDelay(0.4f);
                    whiteTransition3.DOScale(20, 1.2f).SetDelay(0.6f);
                    blackTransition4.DOScale(20, 1.3f).SetDelay(0.8f).OnComplete(() => { SceneManager.LoadSceneAsync(1); });
                    tweenFinished = true;
                    
                });


                break;
        }
    }
}
